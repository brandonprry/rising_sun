using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Criterion;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.BusinessObjects;
using AutoAssess.Data.Nessus;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.Nessus.BusinessObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.Nexpose.BusinessObjects;
using System.Xml;
using nexposesharp;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using AutoAssess.Data.OpenVAS;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using System.Configuration;
using System.Net;
using System.IO;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using AutoAssess.Data.Virtualbox;

namespace autoassess_service
{
	public class Runner
	{
		public static void Main ()
		{
			string conn = "Server=" + ConfigurationManager.AppSettings ["PostgreSQL"] + ";";
			conn += "Port=" + ConfigurationManager.AppSettings ["PostgreSQLPort"] + ";";
			conn += "Database=autoassess;";
			conn += "User Id=" + ConfigurationManager.AppSettings ["PostgreSQLUser"] + ";";
			conn += "Password=" + ConfigurationManager.AppSettings ["PostgreSQLPass"] + ";";
			conn += "SSL=true;";
			
			IPersistenceConfigurer config = PostgreSQLConfiguration.PostgreSQL82
				.ConnectionString (conn);
			
			ISessionFactory factory = Fluently.Configure ()
				.Database (config)
				.Mappings (m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentProfile> ())
				.Mappings (m => 
					    m.FluentMappings.AddFromAssemblyOf<PersistentOpenVASNVT> ())
				.Mappings (m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentNessusScan> ())
				.Mappings (m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentNexposeScan> ())
				.Mappings (m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentMetasploitScan> ())
				.BuildSessionFactory ();
				
			Dictionary<string, string> conf = null;
			IList<PersistentProfile > profilesToRun;
			
			using (ISession session = factory.OpenSession())
			using (ITransaction trans = session.BeginTransaction()) {
				profilesToRun = session.CreateCriteria<PersistentProfile> ()
						.Add (Restrictions.Eq ("HasRun", false))
						.Add (Restrictions.Eq ("IsActive", true))
						.List<PersistentProfile> ();
					
				foreach (PersistentProfile profile in profilesToRun) {
					
					if (profile.VirtualMachines.Count > 0)
					{
						using (VirtualboxManager manager = new VirtualboxManager("vboxmanage"))
						{
							foreach (var vm in profile.VirtualMachines)
								manager.StartVirtualMachine(vm);
						}
						
						
						Console.WriteLine("Letting vm's settle...waiting a bit");
						for (int i = 0;i< 10;i++)
						{
							Console.Write(i);
							System.Threading.Thread.Sleep(new TimeSpan(0,0,60));
						}
					
					}
					
					List<List<IToolResults>> toolResults = null;
					
					conf = new Dictionary<string, string> ();
					conf.Add ("sqlmapPath", ConfigurationManager.AppSettings ["sqlmapPath"]);
					conf.Add ("wapitiPath", ConfigurationManager.AppSettings ["wapitiPath"]);
					conf.Add ("nmapPath", ConfigurationManager.AppSettings ["nmapPath"]);
					conf.Add ("niktoPath", ConfigurationManager.AppSettings ["niktoPath"]);
					conf.Add ("onesixtyonePath", ConfigurationManager.AppSettings ["onesixtyonePath"]);
					conf.Add ("sslscanPath", ConfigurationManager.AppSettings ["sslscanPath"]);
					conf.Add ("smbclientPath", ConfigurationManager.AppSettings ["smbclientPath"]);
					conf.Add ("traceroutePath", ConfigurationManager.AppSettings ["traceroutePath"]);
					conf.Add ("whoisPath", ConfigurationManager.AppSettings ["whoisPath"]);
					conf.Add ("profileID", profile.ID.ToString());
					conf.Add ("API", ConfigurationManager.AppSettings["API"]);
					conf.Add ("UserID", ConfigurationManager.AppSettings["userID"]);
					
					profile.Configuration = conf;
					
					string newRange = string.Empty;
					
					foreach (var host in profile.ProfileHosts)
						newRange += host.IPv4Address + " ";
					
					profile.Range = newRange;
					
					profile.Run (out toolResults);
					
					profile.CurrentResults.SetCreationInfo (profile.WebUserID);
					
					foreach (PersistentNMapHost host in profile.CurrentResults.PersistentHosts) {
						host.CleanHost ();
						
						host.ProfileHost = profile.ProfileHosts.Where (ph => ph.IPv4Address == host.IPAddressv4).Single ();
						
						foreach (PersistentPort port in host.PersistentPorts) {
							port.CleanPort ();
							port.ParentProfile = profile;
							port.SetCreationInfo (profile.WebUserID);
						}
						
						foreach (List<IToolResults> list in toolResults) {
							foreach (IToolResults results in list.Where(k => k.HostIPAddressV4 == host.IPAddressv4)) {
								
								//this isn't very refactor friendly
								//You can't switch on the Type supplied by GetType() since types can be ambiguous
								switch (results.GetType ().Name) {
								case "SSLScanToolResults":
									PersistentSSLScanResults ssl = new PersistentSSLScanResults (results as SSLScanToolResults);
									ssl.HostPortID = host.PersistentPorts.Where (pp => pp.PortNumber == ssl.HostPort).FirstOrDefault ().ID;
									ssl.SetCreationInfo (Guid.Empty);
									session.Save (ssl);
									break;
								case "NiktoToolResults":
									PersistentNiktoResults nikto = new PersistentNiktoResults (results as NiktoToolResults);
									
									nikto.Items = new List<PersistentNiktoItem>();
									
									string[] items = nikto.ParseOutput().ToArray();
									
									foreach (string item in items)
									{
										PersistentNiktoItem i = new PersistentNiktoItem();
										DateTime now = DateTime.Now;
										
										i.ID = Guid.NewGuid();
										i.CreatedBy = Guid.Empty;
										i.CreatedOn = now;
										i.Data = item;
										i.LastModifiedBy = Guid.Empty;
										i.LastModifiedOn = now;
										i.IsActive = true;
										session.Save(i);	
										nikto.Items.Add(i);
									}
									
									nikto.HostPortID = host.PersistentPorts.Where (pp => pp.PortNumber == nikto.HostPort).FirstOrDefault ().ID;
									nikto.SetCreationInfo (Guid.Empty);
									session.Save (nikto);
									break;
								case "OneSixtyOneToolResults":
									PersistentOneSixtyOneResults onesixtyone = new PersistentOneSixtyOneResults (results as OneSixtyOneToolResults);
									onesixtyone.ParentPort = host.PersistentPorts.Where (pp => pp.PortNumber == onesixtyone.HostPort).FirstOrDefault ();
									onesixtyone.HostPortID = onesixtyone.ParentPort.ID;
									onesixtyone.SetCreationInfo (Guid.Empty);
									session.Save (onesixtyone);
									break;
								case "SMBClientToolResults":
									PersistentSMBClientResults smb = new PersistentSMBClientResults (results as SMBClientToolResults);
									smb.ParentPort = host.PersistentPorts.Where (pp => pp.PortNumber == smb.HostPort).FirstOrDefault ();
									smb.SetCreationInfo (Guid.Empty);
										//session.Save (smb);
									break;
								case "WhoisToolResults":
									PersistentWhoisResults whois = new PersistentWhoisResults (results as WhoisToolResults);
									whois.ParentNMapHost = host;
									whois.SetCreationInfo (Guid.Empty);
									session.Save (whois);
									break;
								case "TracerouteToolResults":
									PersistentTracerouteResults tracert = new PersistentTracerouteResults (results as TracerouteToolResults);
									tracert.ParentNMapHost = host;
									tracert.SetCreationInfo (Guid.Empty);
									session.Save (tracert);
									break;
								default:
									throw new Exception ("Don't know type: " + results.GetType ().Name);
								}
							}
						}
						
						host.SetCreationInfo (profile.WebUserID);
					}
					
					profile.RunAfter = DateTime.Now.AddDays (profile.RunEvery.Days);
					profile.HasRun = true;
					session.SaveOrUpdate (profile);
				}
				
				try {
					trans.Commit ();
				} catch (Exception ex) {
					trans.Rollback ();
					
					throw ex;
				}
			}
					
			IList<PersistentScan > scansToRun;
			
			using (ISession session = factory.OpenSession()) {
				scansToRun = session.CreateCriteria<PersistentScan> ()
						.Add (Restrictions.Eq ("HasRun", false))
						.Add (Restrictions.Eq ("IsActive", true))
						.List<PersistentScan> ();
			
			
				foreach (var scan in scansToRun) {
					
					scan.ParentProfile.CurrentResults.PopulateNonPersistentHosts ();
					if (scan.ParentProfile.VirtualMachines.Count > 0)
					{
						using (VirtualboxManager manager = new VirtualboxManager("vboxmanage"))
						{
							foreach (var vm in scan.ParentProfile.VirtualMachines)
								manager.StartVirtualMachine(vm);
						}
					}
					NessusScan nessusScan = null;
					NexposeScan nexposeScan = null;
					OpenVASScan openvasScan = null;
					MetasploitScan metasploitScan = null;
					
					Dictionary<NMapHost, IList<IToolResults >> toolResults = new Dictionary<NMapHost, IList<IToolResults>> ();
					
					conf = new Dictionary<string, string> ();
					
					if (scan.ScanOptions.IsSQLMap)
					{
						conf.Add ("wapitiPath", ConfigurationManager.AppSettings ["wapitiPath"]);
						conf.Add ("sqlmapPath", ConfigurationManager.AppSettings ["sqlmapPath"]);
						conf.Add("isSQLMap", "true");
					}
					else
						conf.Add("isSQLMap", "false");
					
					if (scan.ScanOptions.IsDSXS)
					{
						if (!conf.ContainsKey("wapitiPath"))
							conf.Add ("wapitiPath", ConfigurationManager.AppSettings ["wapitiPath"]);
						
						conf.Add ("dsxsPath", ConfigurationManager.AppSettings ["dsxsPath"]);
					}
					
					if (scan.ScanOptions.IsNessusAssessment) {
						conf.Add ("nessusHost", ConfigurationManager.AppSettings ["nessusHost"]);
						conf.Add ("nessusUser", ConfigurationManager.AppSettings ["nessusUser"]);
						conf.Add ("nessusPass", ConfigurationManager.AppSettings ["nessusPass"]);
					}
					
					if (scan.ScanOptions.IsNexposeAssessment) {
						conf.Add ("nexposeHost", ConfigurationManager.AppSettings ["nexposeHost"]);
						conf.Add ("nexposeUser", ConfigurationManager.AppSettings ["nexposeUser"]);
						conf.Add ("nexposePass", ConfigurationManager.AppSettings ["nexposePass"]);
					}
					
					if (scan.ScanOptions.IsOpenVASAssessment) {
						conf.Add ("openvasHost", ConfigurationManager.AppSettings ["openvasHost"]);
						conf.Add ("openvasUser", ConfigurationManager.AppSettings ["openvasUser"]);
						conf.Add ("openvasPass", ConfigurationManager.AppSettings ["openvasPass"]);
						
						string url = ConfigurationManager.AppSettings ["API"] + "/GetOpenVASConfigs.ashx";

						HttpWebRequest request = WebRequest.Create (url) as HttpWebRequest;
		
						string xml = string.Empty;
		
						using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))					
							xml = reader.ReadToEnd ();
		
						XmlDocument doc = new XmlDocument ();
						doc.LoadXml (xml);
		
						List<OpenVASConfig> configs = new List<OpenVASConfig> ();
		
						foreach (XmlNode c in doc.FirstChild.ChildNodes)
							configs.Add (new OpenVASConfig (c));
						
						conf.Add ("openvasConfig", configs.Where (c => c.Name == "Full and fast").Single ().RemoteConfigID.ToString ());
					}
					
					if (scan.ScanOptions.IsMetasploitAssessment) {
						conf.Add ("tmpSshfsDir", ConfigurationManager.AppSettings ["tmpSshfsDir"]);
						conf.Add ("metasploitHost", ConfigurationManager.AppSettings ["metasploitHost"]);
						conf.Add ("metasploitUser", ConfigurationManager.AppSettings ["metasploitUser"]);
						conf.Add ("metasploitPass", ConfigurationManager.AppSettings ["metasploitPass"]);
					}
					
					scan.Configuration = conf;
					
					scan.Run (out nessusScan, out nexposeScan, out openvasScan, out metasploitScan, out toolResults);
					
					if (toolResults == null)
					{
						if (scan.ParentProfile.VirtualMachines.Count > 0)
						{
							using (VirtualboxManager manager = new VirtualboxManager("vboxmanage"))
							{
								foreach (var vm in scan.ParentProfile.VirtualMachines)
									manager.StopVirtualMachine(vm);
							}
						}
						
						continue;
					}
					
					///this is really super dirty
					///coded myself into a corner, gotta unwind
					foreach (KeyValuePair<NMapHost, IList<IToolResults>> pair in toolResults) {
						foreach (IToolResults results in pair.Value) {
							if (results is WapitiToolResults) {
								PersistentWapitiResults wapiti = new PersistentWapitiResults (results as WapitiToolResults);
								foreach (var host in scan.ParentProfile.CurrentResults.PersistentHosts) {
									foreach (PersistentPort pport in host.PersistentPorts) {
										if (pport.PortNumber == (results as WapitiToolResults).HostPort &&
										    pport.ParentHost.IPAddressv4 == (results as WapitiToolResults).HostIPAddressV4) {
											
											wapiti.HostPortID = pport.ID;
											
											wapiti.SetCreationInfo (Guid.Empty);
											
											session.Save (wapiti);
										}
									}
								}
							} else if (results is SQLMapResults) {
								PersistentSQLMapResults pr = new PersistentSQLMapResults (results as SQLMapResults);
								foreach (var host in scan.ParentProfile.CurrentResults.PersistentHosts) {
									foreach (PersistentPort pport in host.PersistentPorts) {
										if (pport.PortNumber == (results as SQLMapResults).ParentHostPort.PortNumber &&
											pport.ParentHost.IPAddressv4 == (results as SQLMapResults).ParentHostPort.ParentIPAddress) {
											
											pr.ParentHostPort = pport;
											
											pr.SetCreationInfo (Guid.Empty);
											
											session.Save (pr);
										}
									}
								}
							}
						}
					}
							
					
					if (scan.ScanOptions.IsNessusAssessment) {
						PersistentNessusScan s = new PersistentNessusScan(nessusScan);
						s.SetCreationInfo(Guid.Empty, true);
						s.ParentScanID = scan.ID;
						session.Save (s);
					}
						
					if (scan.ScanOptions.IsOpenVASAssessment) {
						PersistentOpenVASScan s = new PersistentOpenVASScan(openvasScan);
						s.SetCreationInfo(Guid.Empty, true);
						s.ParentScanID = scan.ID;
						session.Save (s);
					}
						
					if (scan.ScanOptions.IsNexposeAssessment) {				
						PersistentNexposeScan s = new PersistentNexposeScan(nexposeScan);
						s.SetCreationInfo(Guid.Empty, true);
						s.ParentScanID = scan.ID;
						session.Save (s);
					}
					
					if (scan.ScanOptions.IsMetasploitAssessment) {
						PersistentMetasploitScan s = new PersistentMetasploitScan(metasploitScan);
						s.SetCreationInfo(Guid.Empty, true);
						s.ParentScanID = scan.ID;
						session.Save (s);
					}
					
					scan.HasRun = true;
					
					
					
					Console.WriteLine("Saving!");
					using (ITransaction trans = session.BeginTransaction()) {
						session.SaveOrUpdate (scan);
						
						try {
							trans.Commit ();
						} catch (Exception ex) {
							trans.Rollback ();
							Console.WriteLine("Save failed!");
							
							throw ex;
						}
					}
					
					if (scan.ParentProfile.VirtualMachines.Count > 0)
					{
						using (VirtualboxManager manager = new VirtualboxManager("vboxmanage"))
						{
							foreach (var vm in scan.ParentProfile.VirtualMachines)
								manager.StopVirtualMachine(vm);
						}
					}
				}
			}
			
//			while (true) {
//				DateTime start = new DateTime ();
//				IPAddress ipAd = IPAddress.Parse ("127.0.0.1");
//				
//				TcpListener list = new TcpListener (ipAd, 8082);
//				
//				list.Start ();
//				
//				TcpClient c = list.AcceptTcpClient ();
//				
//				ASCIIEncoding enc = new ASCIIEncoding ();
//				byte[] b = new byte[1024];
//				string xml = string.Empty;
//				
//				using (NetworkStream stream = c.GetStream ()) {
//					do {
//						stream.Read (b, 0, b.Length);
//						xml += xml + enc.GetString (b);
//						
//					} while (stream.DataAvailable);
//					
//					start = DateTime.Now;
//					
//					XmlDocument doc = new XmlDocument ();
//					doc.LoadXml (xml);
//					
//					foreach (XmlNode node in doc.ChildNodes) {
//						
//						if (node.Name == "create") {
//							if (node.Name == "scan") {
//								
//								Scan scan = new Scan ();
//								
//								if (node.HasChildNodes) {
//									foreach (XmlNode child in node.ChildNodes) {
//										
//										if (child.Name == "host")
//											scan.Host = child.InnerText; else if (child.Name == "range")
//										
//											scan.Range = child.InnerText; else if (child.Name == "type") {
//											if (child.InnerText == "profile")
//											
//												scan.ScanType = ScanType.Profile; else if (child.InnerText == "full")
//												scan.ScanType = ScanType.Full;
//										}
//									}
//								}
//								
//								
//								
//								scan.Run ();
//							} else if (node.Name == "get") {
//								
//							} else if (node.Name == "destroy") {
//							
//							}
//							
//						}
//						
//					}
//					
//					TimeSpan tt = DateTime.Now.Subtract (start);
//					
//					string complete = String.Format ("The scan has been run... Took {0} minutes, {1} seconds.\n", tt.Minutes, tt.Seconds);
//					
//					stream.Write (enc.GetBytes (complete), 0, complete.Length);
//				}
//				
//				list.Stop ();
//			}
		}
	}
}
