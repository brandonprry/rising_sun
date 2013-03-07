using System;
using System.Xml;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using AutoAssess.Data.Nexpose.BusinessObjects;
using AutoAssess.Data.Nessus.BusinessObjects;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using FluentNHibernate.Cfg;

namespace load_ovas_nss_nx_to_msf_and_save
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string conn = "Server=192.168.1.5;";
			conn += "Port=5433;";
			conn += "Database=autoassess;";
			conn += "User Id=postgres;";
			conn += "Password=password;";
			conn += "SSL=true;";
			
			IPersistenceConfigurer config = PostgreSQLConfiguration.PostgreSQL82
				.ConnectionString (conn);
			
			ISessionFactory factory = Fluently.Configure ()
				.Database (config)
				.Mappings (m =>
					    m.FluentMappings.AddFromAssemblyOf<PersistentMetasploitScan> ())
				.Mappings (m => 
					    m.FluentMappings.AddFromAssemblyOf<PersistentOpenVASNVT> ())
				.Mappings (m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentNessusScan> ())
				.Mappings (m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentNexposeScan> ())
				.BuildSessionFactory ();
			
			string msf = "/home/bperry/tmp/metasploit.xml";
			string ovas = "/home/bperry/tmp/openvas.xml";
			string nx = "/home/bperry/tmp/nexpose.xml";
			string nss = "/home/bperry/tmp/nessus.xml";
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(System.IO.File.ReadAllText(ovas));
			
			OpenVASScan ovasScan = new OpenVASScan(doc.FirstChild);
			
			doc = new XmlDocument();
			doc.LoadXml(System.IO.File.ReadAllText(nx));
			
			NexposeScan nxScan = new NexposeScan(doc.FirstChild);
			
			doc = new XmlDocument();
			doc.LoadXml(System.IO.File.ReadAllText(nss));
			
			NessusScan nssScan = new NessusScan(doc.LastChild);
			
			doc = new XmlDocument();
			doc.LoadXml(System.IO.File.ReadAllText(msf));
			
			MetasploitScan msfScan = new MetasploitScan(doc.LastChild);
			
			PersistentMetasploitScan pmsfScan = new PersistentMetasploitScan(msfScan);
			pmsfScan.SetCreationInfo(Guid.Empty, true);
			
			PersistentNessusScan pnssScan = new PersistentNessusScan(nssScan);
			pnssScan.SetCreationInfo(Guid.Empty, true);
			
			PersistentOpenVASScan povasScan = new PersistentOpenVASScan(ovasScan);
			povasScan.SetCreationInfo(Guid.Empty, true);
			
			PersistentNexposeScan pnxScan = new PersistentNexposeScan(nxScan);
			pnxScan.SetCreationInfo(Guid.Empty, true);
			
			using(ISession session = factory.OpenSession())
			{
				using (ITransaction x = session.BeginTransaction())
				{
					try
					{
						pmsfScan.ParentScanID = Guid.NewGuid();
						Console.WriteLine("Saving metasploit");
						session.Save(pmsfScan);
						x.Commit();
					}
					catch (Exception ex)
					{
						Console.WriteLine("I broke, rolling back...");
						x.Rollback();
						throw ex;
					}
				}
				using (ITransaction x = session.BeginTransaction())
				{
					try
					{
						pnssScan.ParentScanID = Guid.NewGuid();
						Console.WriteLine("Saving nessus");
						session.Save(pnssScan);
						x.Commit();
					}
					catch (Exception ex)
					{
						Console.WriteLine("I broke, rolling back...");
						x.Rollback();
						throw ex;
					}
				}
				using (ITransaction x = session.BeginTransaction())
				{
					try
					{	
						povasScan.ParentScanID = Guid.NewGuid();
						Console.WriteLine("Saving openvas");
						session.Save(povasScan);
						x.Commit();
					}
					catch (Exception ex)
					{
						Console.WriteLine("I broke, rolling back...");
						x.Rollback();
						throw ex;
					}
				}
				using (ITransaction x = session.BeginTransaction())
				{
					try
					{	
						pnxScan.ParentScanID = Guid.NewGuid();
						Console.WriteLine("Saving nexpose");
						session.Save(pnxScan);
						x.Commit();
					}
					catch (Exception ex)
					{
						Console.WriteLine("I broke, rolling back...");
						x.Rollback();
						throw ex;
					}
				}
			}
			
			Console.WriteLine("yay?");
		}
	}
}
