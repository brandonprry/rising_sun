using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using AutoAssess.Data.PersistentObjects;
using System.Xml;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using AutoAssess.Data.BusinessObjects;
using System.Configuration;
using System.IO;

namespace import_cves
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IPersistenceConfigurer config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString ("Server=" + ConfigurationManager.AppSettings ["PostgreSQL"] + ";Port=5432;Database=autoassess;User Id=postgres;Password=password;SSL=true;");
			
			
			ISessionFactory factory = Fluently.Configure ()
				.Database (config)
				.Mappings (m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentCVE> ())
				.Mappings (m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentNessusScan> ())
				.Mappings (m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentNexposeScan> ())
				.Mappings (m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentOpenVASTask> ())
				.Mappings (m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentOneSixtyOneResults> ())
				.BuildSessionFactory ();
			
			List<PersistentCVE> cves = new List<PersistentCVE> ();
			using (ISession session = factory.OpenSession()) {
				string xml = System.IO.File.ReadAllText ("/home/bperry/tmp/cve/allitems.xml");
					
				XmlDocument doc = new XmlDocument ();
				doc.LoadXml (xml);
					
				using (ITransaction trans = session.BeginTransaction()) {
	
					foreach (XmlNode child in doc.LastChild.ChildNodes) {
						PersistentCVE cve = new PersistentCVE (new CVE (child));

						cve.SetCreationInfo (Guid.Empty);
						
						foreach (PersistentCVEReference reference in cve.PersistentReferences) {
							reference.CVE = cve;
							reference.SetCreationInfo (Guid.Empty);
						}
						
						foreach (PersistentCVEComment comment in cve.PersistentComments) {
							comment.CVE = cve;
							comment.SetCreationInfo (Guid.Empty);
						}
						
						Console.WriteLine ("Saving " + cve.Name);
						
						session.Save (cve);
						cves.Add(cve);
					}
					
					List<string> nvdExports = new List<string>();
					
					foreach (FileInfo file in new System.IO.DirectoryInfo(ConfigurationManager.AppSettings["nvdExportDir"]).EnumerateFiles())
						nvdExports.Add (file.FullName);
				
					foreach (string export in nvdExports) {
					
						xml = System.IO.File.ReadAllText (export);
						
						doc = new XmlDocument ();
						doc.LoadXml (xml);
						
						foreach (XmlNode child in doc.LastChild.ChildNodes) {
							PersistentNVD nvd = new PersistentNVD (new NVD (child));

							if (cves.Where (c => c.Name == nvd.CVEID).Count() != 1)
								continue;
							
							if (!string.IsNullOrEmpty (nvd.CVEID)) {
								PersistentCVE cve = cves.Where (c => c.Name == nvd.CVEID).Single ();
								
								if (cve == null)
									throw new Exception ("CVE " + nvd.CVEID + " doesn't exist.");
								
								nvd.CVE = cve;
							}
							
							Console.WriteLine (nvd.NVDID);
							nvd.SetCreationInfo (Guid.Empty);
							
							if (nvd.CVSS != null)
								nvd.CVSS.SetCreationInfo (Guid.Empty);
					
							if (nvd.References != null)
								foreach (PersistentNVDReference reference in nvd.References)
									reference.SetCreationInfo (Guid.Empty);
							
							if (nvd.VulnerableSoftware != null)
								foreach (PersistentVulnerableSoftware vs in nvd.VulnerableSoftware)
									vs.SetCreationInfo (Guid.Empty);
							
							session.SaveOrUpdate (nvd);
						}
					}
										
					try {
						Console.WriteLine ("Committing...");
						trans.Commit ();
					} catch (Exception ex) {
						trans.Rollback ();
						throw ex;
					}
				}
			}
		}
	}
}
