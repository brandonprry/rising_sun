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

namespace import_nvds
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IPersistenceConfigurer config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString ("Server=" + ConfigurationManager.AppSettings ["PostgreSQL"] + ";Port=5432;Database=rising_sun;User Id=" + 
			                                                                                       ConfigurationManager.AppSettings ["PostgreSQLUser"] + ";Password=" + 
			                                                                                       ConfigurationManager.AppSettings ["PostgreSQLPassword"] + ";SSL=true;");
			
			
			ISessionFactory factory = Fluently.Configure ()
				.Database (config)
				.Mappings (m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentNVD> ())
				.Mappings (m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentNessusScan> ())
				.Mappings (m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentNexposeScan> ())
				.Mappings (m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentOpenVASTask> ())
				.BuildSessionFactory ();
			
			using (ISession session = factory.OpenSession()) {
				List<string> nvdExports = new List<string>();
					
				foreach (FileInfo file in new System.IO.DirectoryInfo(ConfigurationManager.AppSettings["nvdExportDir"]).EnumerateFiles())
					nvdExports.Add(file.FullName);
				
				foreach (string export in nvdExports) {
					
					using (ITransaction trans = session.BeginTransaction()) {
						string xml = System.IO.File.ReadAllText (export);
						
						XmlDocument doc = new XmlDocument ();
						doc.LoadXml (xml);
						
						foreach (XmlNode child in doc.LastChild.ChildNodes) {
							PersistentNVD nvd = new PersistentNVD (new NVD (child));
					
//							bool exists = session.CreateCriteria<PersistentNVD> ()
//								.Add (NHibernate.Criterion.Restrictions.Eq ("CVEID", nvd.CVEID))
//								.List<PersistentNVD> ()
//								.Any ();
//							
//							if (exists) {
//								Console.WriteLine ("Skipping NVD: " + nvd.CVEID);
//								continue;
//							}
							
							if (!string.IsNullOrEmpty (nvd.CVEID)) {
								PersistentCVE cve = session.CreateCriteria<PersistentCVE> ()
									.Add (NHibernate.Criterion.Restrictions.Eq ("Name", nvd.CVEID))
									.List<PersistentCVE> ()
									.SingleOrDefault ();
								
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
}
