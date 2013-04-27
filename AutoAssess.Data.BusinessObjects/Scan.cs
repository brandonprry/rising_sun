using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using AutoAssess.Data;
using nessusssharp;
using AutoAssess.Data.Nessus.BusinessObjects;
using nexposesharp;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using AutoAssess.Data.OpenVAS;
using AutoAssess.Data.Nexpose.BusinessObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using metasploitsharp;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;
using openvassharp;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class Scan
	{
		//csv list of our hosts we are scanning
		string csv;
		List<string> services;
		
		public Scan ()
		{
		}
		
		public virtual string Duration { get; set; }
		
		public virtual bool HasRun { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual Dictionary<string, string> Configuration { get; set; }
		
		public virtual Profile ParentProfile { get; set; }
		
		public virtual Dictionary<string, List<WapitiBug>> WapitiBugs { get; set; }
		
		public virtual ScanOptions ScanOptions { get; set; }
		
		public virtual void Run (out NessusScan nessusScan, out NexposeScan nexposeScan, out OpenVASScan openvasScan, out MetasploitScan metasploitScan, out Dictionary<NMapHost, IList<IToolResults>> toolResults)
		{
			if (this.Configuration == null)
				throw new Exception ("Configuration not set");
			
			DateTime start = DateTime.Now;
			metasploitScan = null;
			nessusScan = null;
			nexposeScan = null;
			openvasScan = null;
			
			string openvasTaskID = string.Empty;
			string nessusScanID = string.Empty;
			string nexposeScanID = string.Empty;
			
			int uniqueNo = new Random ().Next ();
				
			IList<NMapHost > hosts = this.ParentProfile.CurrentResults.Hosts;
			
			if (hosts.Count() == 0)
			{
				Console.WriteLine("ERROR: no hosts in the profile. Aborting.");
				toolResults = null;
				return;
			}
			
			
			csv = string.Empty;
			
			foreach (NMapHost host in hosts)
				csv = csv + host.IPAddressv4 + ", "; //trailing , is OK in this case
			

			
			string openvasReportID = string.Empty;
			if (this.ScanOptions.IsOpenVASAssessment) {
				Console.WriteLine ("Creating OpenVAS Scan...");			
				
				OpenVASTarget target = new OpenVASTarget ();
				target.Hosts = csv;
				target.Name = this.ParentProfile.Name + uniqueNo.ToString ();
				target.SMBCredentials = new OpenVASLSCCredential ();
				target.SSHCredentials = new OpenVASLSCCredential ();
				
				using (OpenVASManagerSession ovasSession = new OpenVASManagerSession(this.Configuration ["openvasUser"], this.Configuration ["openvasPass"], this.Configuration ["openvasHost"])) {
							
					using (OpenVASObjectManager openvasManager = new OpenVASObjectManager(ovasSession)) {	
								
						target = openvasManager.CreateTarget (target);
						
						OpenVASConfig config = openvasManager.GetAllConfigs ()
							.Where (c => c.RemoteConfigID == new Guid (this.Configuration["openvasConfig"]))
							.SingleOrDefault ();
						
						OpenVASTask task = new OpenVASTask ();
						
						task.Comment = string.Format ("Task for scan {0}", this.Name);
						task.Target = target;
						task.Config = config;
						
						task = openvasManager.CreateTask (task);
						
						XmlDocument taskResponse = openvasManager.StartTask (task.RemoteTaskID.ToString ());
						
						if (!taskResponse.FirstChild.Attributes ["status"].Value.StartsWith ("20"))
							throw new Exception ("Creating OpenVAS scan failed: " + 
								taskResponse.FirstChild.Attributes ["status_text"].Value);
						
						openvasReportID = taskResponse.FirstChild.FirstChild.InnerText;
						openvasTaskID = task.RemoteTaskID.ToString ();
					}
					
					Console.WriteLine ("Done creating and starting OpenVAS scan.");
				}
			}
			
			if (this.ScanOptions.IsNessusAssessment) {
				Console.WriteLine ("Creating Nessus scan...");
				using (NessusManagerSession nessusSession = new NessusManagerSession (this.Configuration["nessusHost"])) {
								
					bool loggedIn = false;
					nessusSession.Authenticate (this.Configuration ["nessusUser"], this.Configuration ["nessusPass"], 1234, out loggedIn);
								
					if (!loggedIn)
						throw new Exception ("Invalid username/password");
								
					using (NessusObjectManager nessusManager = new NessusObjectManager (nessusSession)) {
							
					var tmp = nessusManager.CreateAndStartScan (csv, -2, this.Name + uniqueNo.ToString ());
							
						string scanName = tmp.Name;
						nessusScanID = scanName;
							
						string reportID = string.Empty;
						foreach (XmlNode node in nessusManager.ListReports().LastChild.ChildNodes) {
							if (node.Name == "contents") {
								string tmpReportID = string.Empty;
								foreach (XmlNode child in node.FirstChild.ChildNodes) {
									foreach (XmlNode c in child.ChildNodes) {
										if (c.Name == "name")
											tmpReportID = c.InnerText;
										else if (c.Name == "readableName" && c.InnerText == scanName)
											reportID = tmpReportID;
											
									}
								}		
								tmpReportID = string.Empty;
							}
						}
							
					}
					Console.WriteLine ("Done creating and starting Nessus scan.");
				}
			}
			if (this.ScanOptions.IsNexposeAssessment) {				
				Console.WriteLine ("Creating NeXpose scan...");
				int siteID = 0;
				if (this.ScanOptions.RemoteNexposeSiteID <= 0) {					
					XmlDocument d = null;
					string id = "-1";
					string template = "full-audit";
					string name = this.Name + uniqueNo.ToString ();
					string description = "A site for the the profile " + this.ParentProfile.Name;
								
					string siteXml = "<Site id=\"" + id + "\" name=\"" + name + "\" description=\"" + description + "\">";
									
					siteXml = siteXml + "<Hosts>";
								
					foreach (string host in csv.Split(','))
						siteXml = siteXml + "<host>" + host + "</host>";
								
					siteXml = siteXml + "</Hosts>" +
										"<Credentials></Credentials>" +
										"<Alerting></Alerting>" +
										"<ScanConfig configID=\"" + id + "\" name=\"" + name + "\" templateID=\"" + template + "\"></ScanConfig>" +
										"</Site>";
								
					XmlDocument doc = new XmlDocument ();
					doc.LoadXml (siteXml);
								
					using (NexposeSession session = new NexposeSession(this.Configuration["nexposeHost"])) {
						session.Authenticate (this.Configuration ["nexposeUser"], this.Configuration ["nexposePass"]);
									
						using (NexposeManager11 manager = new NexposeManager11(session)) {
							XmlDocument response = manager.SaveOrUpdateSite (doc.FirstChild);
										
							d = response;
						}
					}
												
					siteID = int.Parse (d.FirstChild.Attributes ["site-id"].Value);
							
					this.ScanOptions.RemoteNexposeSiteID = siteID;
				} else {
					siteID = this.ScanOptions.RemoteNexposeSiteID;
				}
						
				using (NexposeSession session = new NexposeSession(this.Configuration["nexposeHost"])) {
					session.Authenticate (this.Configuration ["nexposeUser"], this.Configuration ["nexposePass"]);
							
					using (NexposeManager11 manager = new NexposeManager11(session)) {
						XmlDocument response = manager.ScanSite (siteID.ToString ());
								
						nexposeScanID = response.FirstChild.FirstChild.Attributes ["scan-id"].Value;	
					}
				}
							
				Console.WriteLine ("Done creating and starting NeXpose scan.");
			}
			
			Dictionary<NMapHost, IList<IToolResults >> results = new Dictionary<NMapHost, IList<IToolResults>> ();
			services = new List<string> ();
			
			foreach (var host in hosts) { 
				
				foreach (Port port in (host as NMapHost).Ports)
					services.Add (port.Service);
				
				NMapHost threadHost = host as NMapHost;	
			
				Console.WriteLine ("Starting scan for host: " + threadHost.Hostname + "(" + threadHost.IPAddressv4 + ")");
				
				results.Add (threadHost, ScanHost (threadHost, this.ScanOptions.SQLMapOptions, this.Configuration));

			}
			
			toolResults = results;
			bool done = false;
				
			if (this.ScanOptions.IsNessusAssessment || this.ScanOptions.IsNexposeAssessment || this.ScanOptions.IsOpenVASAssessment) {
				while (!done) {
					if (!string.IsNullOrEmpty (openvasTaskID) && this.OpenVASScanIsRunning (openvasTaskID)) {
						Console.WriteLine ("Waiting on OpenVAS scan " + openvasTaskID);
						Thread.Sleep (new TimeSpan (0, 0, 60));
						continue;
					}
					
					if (!string.IsNullOrEmpty (nessusScanID) && this.NessusScanIsRunning (nessusScanID)) {
						Console.WriteLine ("Waiting on Nessus scan " + nessusScanID);
						Thread.Sleep (new TimeSpan (0, 0, 60));
						continue;
					}
					
					if (!string.IsNullOrEmpty (nexposeScanID) && this.NexposeScanIsRunning (nexposeScanID)) {
						Console.WriteLine ("Waiting on NeXpose scan " + nexposeScanID);
						Thread.Sleep (new TimeSpan (0, 0, 60));
						continue;
					}
					
					done = true;
				}
				
				Dictionary<VulnerabilityScanType, string> scans = new Dictionary<VulnerabilityScanType, string> ();
				
				if (!string.IsNullOrEmpty (openvasReportID))
					scans.Add (VulnerabilityScanType.OpenVAS, openvasReportID);
				
				if (!string.IsNullOrEmpty (nexposeScanID))
					scans.Add (VulnerabilityScanType.Nexpose, this.ScanOptions.RemoteNexposeSiteID.ToString ());
				
				if (!string.IsNullOrEmpty (nessusScanID))
					scans.Add (VulnerabilityScanType.Nessus, nessusScanID);
				
				Dictionary<VulnerabilityScanType, XmlNode> reports = this.GetReports (scans);
				foreach (var report in reports) {
					if (report.Key == VulnerabilityScanType.Nessus)
						nessusScan = new NessusScan(report.Value);
					else if (report.Key == VulnerabilityScanType.Nexpose)
						nexposeScan = new NexposeScan(report.Value);
					else if (report.Key == VulnerabilityScanType.OpenVAS)
						openvasScan = new OpenVASScan(report.Value);
					else
						throw new Exception ("Don't know this scan type");
				}
				
				if (this.ScanOptions.IsMetasploitAssessment) {
					string workspace = Guid.NewGuid ().ToString ();
					this.CreateNewMetasploitWorkspace (workspace);
					this.ImportScansIntoMetasploitPro (reports, workspace);
					
					string proTaskID = this.BeginMetasploitProAssessment (workspace, csv, false);
					
					while (this.MetasploitProAssessmentIsRunning(proTaskID)) {
						Console.WriteLine ("Waiting on exploit assessment from metasploit: task " + proTaskID);
						System.Threading.Thread.Sleep (new TimeSpan (0, 0, 30));
					}
					
					metasploitScan = new MetasploitScan(this.GetMetasploitProReport (workspace));
				}
			}
			
			TimeSpan duration = DateTime.Now - start;
			
			this.Duration = duration.TotalSeconds.ToString ();
			
			this.HasRun = true;
		}

		private Dictionary<VulnerabilityScanType, XmlNode> GetReports (Dictionary<VulnerabilityScanType, string> scans)
		{
			Dictionary<VulnerabilityScanType, XmlNode> reports = new Dictionary<VulnerabilityScanType, XmlNode> ();
			
			foreach (var scan in scans) {
				switch (scan.Key) {
				case VulnerabilityScanType.Nessus:
					reports.Add (VulnerabilityScanType.Nessus, this.GetNessusReport (scan.Value));
					break;
				case VulnerabilityScanType.Nexpose:
					reports.Add (VulnerabilityScanType.Nexpose, this.GetNexposeReport (scan.Value));
					break;
				case VulnerabilityScanType.OpenVAS:
					reports.Add (VulnerabilityScanType.OpenVAS, this.GetOpenVASReport (scan.Value));
					break;
				}
			}
			
			return reports;
		}

		private XmlNode GetOpenVASReport (string id)
		{
			string report = string.Empty;
			using (OpenVASManagerSession session = new OpenVASManagerSession(this.Configuration ["openvasUser"], this.Configuration ["openvasPass"], this.Configuration ["openvasHost"])) {
				using (OpenVASObjectManager manager = new OpenVASObjectManager(session)) {	
					report = manager.GetReportByID (id).OuterXml; 
				}
			}
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(report);
			
			return doc.LastChild;
		}

		private XmlNode GetNexposeReport (string id)
		{
			byte[] report;
			using (NexposeSession session = new NexposeSession(this.Configuration["nexposeHost"])) {
				session.Authenticate (this.Configuration ["nexposeUser"], this.Configuration ["nexposePass"]);
				
				using (NexposeManager11 manager = new NexposeManager11(session)) {
					Dictionary<NexposeReportFilterType, string> filters = new Dictionary<NexposeReportFilterType, string> ();
					filters.Add (NexposeReportFilterType.Site, id);
					
					report = manager.GenerateAdHocReport (NexposeUtil.GenerateAdHocReportConfig ("audit-report", NexposeReportFormat.RawXML, filters));
					
					//stupid hack
					while (report.Length < 91) {
						Thread.Sleep (500);
						report = manager.GenerateAdHocReport (NexposeUtil.GenerateAdHocReportConfig ("audit-report", NexposeReportFormat.RawXML, filters));
					}
				}
			}
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(Encoding.UTF8.GetString(report));
			
			return doc.LastChild;
		}

		private XmlNode GetNessusReport (string id)
		{
			string report = string.Empty;
			using (NessusManagerSession session = new NessusManagerSession(this.Configuration["nessusHost"])) {
				bool loggedIn = false;
				session.Authenticate (this.Configuration ["nessusUser"], this.Configuration ["nessusPass"], 1234, out loggedIn);
				
				if (!loggedIn)
					throw new Exception ("Bad username/password combo");
				
				using (NessusManager manager = new NessusManager(session)) {
					
					XmlDocument reportList = manager.ListReports ();
					string reportID = string.Empty;
					foreach (XmlNode node in reportList.LastChild.ChildNodes) {
						if (node.Name == "contents") {
							string tmpReportID = string.Empty;
							foreach (XmlNode child in node.FirstChild.ChildNodes) {
								foreach (XmlNode c in child.ChildNodes) {
									if (c.Name == "name")
										tmpReportID = c.InnerText;
									else if (c.Name == "readableName" && c.InnerText == id)
										reportID = tmpReportID;
									
								}
							}		
							tmpReportID = string.Empty;
						}
					}
					report = manager.GetNessusV2Report (reportID).OuterXml;
				}
			}
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(report);
			return doc.LastChild;
		}
		
		private bool OpenVASScanIsRunning (string openvasTaskID)
		{
			using (OpenVASManagerSession ovasSession = new OpenVASManagerSession(this.Configuration ["openvasUser"], this.Configuration ["openvasPass"], this.Configuration["openvasHost"])) {

				using (OpenVASObjectManager openvasManager = new OpenVASObjectManager(ovasSession)) {	
					XmlDocument taskInfo = openvasManager
						.GetTasks (openvasTaskID, true, false, false, string.Empty, string.Empty);
			
					foreach (XmlNode child in taskInfo.FirstChild.ChildNodes) {
						if (child.Name != "task")
							continue;
						
						foreach (XmlNode c in child.ChildNodes) {
							if (c.Name != "status")
								continue;
							
							if (c.InnerText == "Done")
								return false;
						}
					}
			
					return true;		
				}
			}
		}
		
		private void CreateNewMetasploitWorkspace (string workspace)
		{
			using (MetasploitSession session = new MetasploitSession(this.Configuration["metasploitUser"], 
			                                                          this.Configuration["metasploitPass"], 
			                                                          "https://" + this.Configuration["metasploitHost"] + ":3790/api/1.1")) {
				using (MetasploitProManager manager = new MetasploitProManager(session)) {
//					Dictionary<object, object> options = new Dictionary<object, object>();
//					options.Add("name", workspace);
//					
//					manager.AddProWorkspace(options);
					
					Dictionary<string, object> response = manager.CreateConsole ();
					string consoleID = response ["id"] as string;
					
					manager.WriteToConsole (consoleID, "workspace -a \"" + workspace + "\"\n");
					Thread.Sleep (new TimeSpan (0, 0, 30));
					manager.WriteToConsole (consoleID, "workspace \n");
					manager.DestroyConsole (consoleID);
				}
			}
		}
		
		private bool NessusScanIsRunning (string nessusScanID)
		{
			using (NessusManagerSession nessusSession = new NessusManagerSession (this.Configuration["nessusHost"])) {
				bool loggedIn = false;
				nessusSession.Authenticate (this.Configuration ["nessusUser"], this.Configuration ["nessusPass"], 1234, out loggedIn);
								
				if (!loggedIn)
					throw new Exception ("Invalid username/password");
								
				using (NessusObjectManager nessusManager = new NessusObjectManager (nessusSession)) {
					var report = nessusManager.GetReports ()
								.Where (r => r.ReadableName == nessusScanID)
								.SingleOrDefault ();
					
					if (report.Status == "completed")
						return false;
					
					return true;
				}
			}
		}
		
		private bool NexposeScanIsRunning (string nexposeScanID)
		{
			using (NexposeSession session = new NexposeSession(this.Configuration["nexposeHost"])) {
				session.Authenticate (this.Configuration ["nexposeUser"], this.Configuration ["nexposePass"]);
			
				using (NexposeManager11 manager = new NexposeManager11(session)) {
					XmlDocument response = manager.GetScanStatus (nexposeScanID);
				
					string status = response.FirstChild.Attributes ["status"].Value;

					if (status == "finished" || status == "stopped")
						return false;
					
					return true;
				}
			}
		}
		
		private bool MetasploitProAssessmentIsRunning (string msfProScanID)
		{
			using (MetasploitSession session = new MetasploitSession(this.Configuration["metasploitUser"],
			                                                          this.Configuration["metasploitPass"], 
			                                                          "https://" + this.Configuration["metasploitHost"]+ ":3790/api/1.1")) {
				using (MetasploitProManager manager = new MetasploitProManager(session)) {
					Dictionary<string, object> response = manager.GetProTaskStatus (msfProScanID);
					
					if (response.ContainsKey ("error"))
						throw new Exception (response ["error_message"] as string);
					
					string status = (response.First ().Value as Dictionary<string, object>) ["status"] as string;
					
					if (status == "running")
						return true;
					else
						return false;
				}
			}
		}
		
		private List<IToolResults> ScanHost (NMapHost host, SQLMapOptions sqlmapOptions, Dictionary<string, string> config)
		{
			List<IToolResults > _results = new List<IToolResults> ();
			
			Console.WriteLine ("Scanning host: " + host.Hostname);
			foreach (var port in host.Ports) {
				
				port.ParentIPAddress = host.IPAddressv4;
				
				if ((port.Service == "http" || port.Service == "https") && bool.Parse(config["isSQLMap"])) {
					IToolOptions _options = new WapitiToolOptions();
					
					(_options as WapitiToolOptions).Host = host.IPAddressv4;
					(_options as WapitiToolOptions).Port = port.PortNumber;
					(_options as WapitiToolOptions).Path = config["wapitiPath"];
					
					Wapiti wapiti = new Wapiti(_options);
					
					Console.WriteLine("Running wapiti (http/" + port.PortNumber + ") on host: " + (string.IsNullOrEmpty(host.Hostname) ? host.IPAddressv4 : host.Hostname));
					WapitiToolResults wapitiResults = null;
					try
					{	
						wapitiResults = wapiti.Run(new TimeSpan(0,10,0)) as WapitiToolResults;
						wapitiResults.HostIPAddressV4 = host.IPAddressv4;
						wapitiResults.HostPort = port.PortNumber;
						wapitiResults.IsTCP = true;
						
						_results.Add(wapitiResults);
					}
					catch(Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
						
					if (sqlmapOptions != null && wapitiResults != null) {
						
						if (wapitiResults.Bugs == null) { // we get bugs from the findings of wapiti, if wapiti didn't run, no bugs.
							
							sqlmapOptions.URL = port.Service + "://" + host.IPAddressv4;
							sqlmapOptions.Port = port.PortNumber;
							sqlmapOptions.Path = config ["sqlmapPath"];
							
							SQLMap mapper = new SQLMap (sqlmapOptions);
								
							SQLMapResults sqlmapResults = mapper.Run () as SQLMapResults;
							sqlmapResults.ParentHostPort = port;		
							
							_results.Add (sqlmapResults);
						} else {
							foreach (WapitiBug bug in wapitiResults.Bugs) {
								if (bug.Type.StartsWith ("SQL Injection")) {
									
									Console.WriteLine("Starting SQLMap on host/port: " + (string.IsNullOrEmpty(host.Hostname) ? host.IPAddressv4 : host.Hostname) + "/" + port.PortNumber);
									
									sqlmapOptions.Path = config ["sqlmapPath"];
									SQLMap mapper = new SQLMap (sqlmapOptions);
									
									SQLMapResults results = mapper.Run (bug) as SQLMapResults;
									
									if (results == null )
										continue;
									
									if (results.Vulnerabilities != null)
										foreach (var vuln in results.Vulnerabilities)
											vuln.Target = bug.URL;
									
									results.ParentHostPort = port;
									
									_results.Add (results);
								} else if (bug.Type.Contains ("Cross Site Scripting)")) {
									//dsxs
								}
							}
						}
					}
				}
			}
			
			Console.WriteLine ("Done with host: " + host.Hostname);
			
			return _results;
		}

		private bool ImportScanToMetasploitPro (string report, string workspace)
		{
					
			string filename = this.WriteRemoteReport (report);
			
			bool worked = this.ImportFileIntoMetasploitPro (filename, workspace);
			
			if (!worked)
				throw new Exception ("File import failed: " + filename);
					
			return worked;
		}
		
		//return workspace name
		private string ImportScansIntoMetasploitPro (Dictionary<VulnerabilityScanType, XmlNode> scans, string workspace)
		{
			foreach (var scan in scans) {
				string filename = this.WriteRemoteReport (scan.Value.OuterXml);
				this.ImportFileIntoMetasploitPro (filename, workspace);
			}
			
			return workspace;
		}

		string WriteRemoteReport (string report)
		{
			string sshfsDir = this.Configuration ["tmpSshfsDir"];
			
			string filename = Guid.NewGuid ().ToString ();
			
			System.IO.File.WriteAllText (sshfsDir + filename, report);
			
			return "/tmp/" + filename;
			
		}
		
		private bool ImportFileIntoMetasploitPro (string filename, string workspace)
		{
			using (MetasploitSession session = new MetasploitSession(this.Configuration["metasploitUser"],
			                                                          this.Configuration["metasploitPass"],
			                                                          "https://"+this.Configuration["metasploitHost"]+":3790/api/1.1")) {
				using (MetasploitProManager manager = new MetasploitProManager(session)) {
					Dictionary<string, object> options = new Dictionary<string, object> ();
					options.Add ("workspace", workspace);
					options.Add ("DS_PATH", filename);
					options.Add ("preserve_hosts", true);
					
					Dictionary<string, object> response = manager.StartImport (options);
					
					foreach (var pair in response)
						Console.WriteLine (pair.Key + ": " + pair.Value);
					
					Dictionary<string, object> taskResponse = manager.GetProTaskStatus (response ["task_id"] as string);
					
					taskResponse = taskResponse.First ().Value as Dictionary<string, object>;
					
					while (taskResponse["status"] as string == "running") {
						Console.WriteLine ("Waiting on file import: " + filename);
						Thread.Sleep (new TimeSpan (0, 0, 60));
						taskResponse = manager.GetProTaskStatus (response ["task_id"] as string);
						taskResponse = taskResponse.First ().Value as Dictionary<string, object>;
					
					}
					
					return true;
				}
			}
		}
		
		private XmlNode GetMetasploitProReport (string workspace)
		{
			Console.WriteLine ("Generating report for workspace: " + workspace);
			
			string taskID = string.Empty;
			using (MetasploitSession session = new MetasploitSession(this.Configuration["metasploitUser"], 
			                                                          this.Configuration["metasploitPass"],
			                                                          "https://"+this.Configuration["metasploitHost"]+":3790/api/1.1")) {
				using (MetasploitProManager manager = new MetasploitProManager(session)) {
					Dictionary<string, object> options = new Dictionary<string, object> ();
					options.Add ("DS_WHITELIST_HOSTS", string.Empty);
					options.Add ("DS_BLACKLIST_HOSTS", string.Empty);
					options.Add ("workspace", workspace);
					options.Add ("DS_MaskPasswords", false);
					options.Add ("DS_IncludeTaskLog", false);
					options.Add ("DS_JasperDisplaySession", true);
					options.Add ("DS_JasperDisplayCharts", true);
					options.Add ("DS_LootExcludeScreenshots", false);
					options.Add ("DS_LootExcludePasswords", false);
					options.Add ("DS_JasperTemplate", "msfxv3.jrxml");
					options.Add ("DS_REPORT_TYPE", "XML");
					options.Add ("DS_UseJasper", true);
					options.Add ("DS_UseCustomReporting", true);
					options.Add ("DS_JasperProductName", "AutoAssess");
					options.Add ("DS_JasperDbEnv", "production");
					options.Add ("DS_JasperLogo", string.Empty);
					options.Add ("DS_JasperDisplaySections", "1,2,3,4,5,6,7,8");
					options.Add ("DS_EnablePCIReport", true);
					options.Add ("DS_EnableFISMAReport", true);
					options.Add ("DS_JasperDisplayWeb", true);
					options.Add( "DS_CAMPAIGN_ID", "-1");
					
					Dictionary<string, object> response = manager.StartReport (options);
					
					Dictionary<string, object> taskResponse = manager.GetProTaskStatus (response ["task_id"] as string);
					
					taskResponse = taskResponse.First ().Value as Dictionary<string, object>;
					
					while (taskResponse["status"] as string == "running") {
						Console.WriteLine ("Waiting on metasploit report");
						Thread.Sleep (new TimeSpan (0, 0, 60));
						taskResponse = manager.GetProTaskStatus (response ["task_id"] as string);
						taskResponse = taskResponse.First ().Value as Dictionary<string, object>;
					}
					
					response = manager.DownloadReportByTask (response ["task_id"] as string);
					
					taskID = response["data"] as string;
				}
			}
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(taskID);
			
			return doc.LastChild;
		}
		
		private string BeginMetasploitProAssessment (string workspace, string whitelist, bool bruteforce)
		{
			using (MetasploitSession session = new MetasploitSession(this.Configuration["metasploitUser"], 
			                                                          this.Configuration["metasploitPass"],
			                                                          "https://"+this.Configuration["metasploitHost"]+":3790/api/1.1")) {
				
				using (MetasploitProManager manager = new MetasploitProManager(session)) {
					Dictionary<string, object> options = new Dictionary<string, object> ();
					Dictionary<string, object> response;
					Dictionary<string, object> taskResponse;
					object hosts = csv.Split (',');
					
					options.Add ("ips", hosts);
					options.Add ("workspace", workspace);
					
					if (this.ScanOptions.MetasploitDiscovers)
					{
						Console.WriteLine ("Discovering...");
						response = manager.StartDiscover (options);
						
						taskResponse = manager.GetProTaskStatus (response ["task_id"] as string);
						taskResponse = taskResponse.First ().Value as Dictionary<string, object>;
						
						while (taskResponse["status"] as string == "running") {
							Console.WriteLine ("Waiting on metasploit discovery");
							Thread.Sleep (new TimeSpan (0, 0, 60));
							taskResponse = manager.GetProTaskStatus (response ["task_id"] as string);
							taskResponse = taskResponse.First ().Value as Dictionary<string, object>;
						}
					}	
					
					if (this.ScanOptions.MetasploitBruteforces) {
							
						options = new Dictionary<string, object> ();
						string svcs = string.Empty;
						foreach (string service in services) {
							if (service == "postgresql")
								svcs = svcs + "Postgresql ";
							else if (service == "mysql")
								svcs = svcs + "MySQL ";
							else if (service == "mssql")
								svcs = svcs + "MSSQL ";
							else if (service == "oracle")
								svcs = svcs + "Oracle ";
							else if (service == "http")
								svcs = svcs + "HTTP ";
							else if (service == "https")
								svcs = svcs + "HTTPS ";
							else if (service == "ssh")
								svcs = svcs + "SSH ";
							else if (service == "telnet")
								svcs = svcs + "Telnet ";
							else if (service == "ftp")
								svcs = svcs + "FTP ";
							else if (service == "exec")
								svcs = svcs + "EXEC ";
							else if (service == "shell")
								svcs = svcs + "SHELL ";
							else if (service == "vnc")
								svcs = svcs + "VNC ";
						}

						Console.WriteLine ("Bruteforcing...");
						
						options.Add ("workspace", workspace);
						options.Add ("DS_WHITELIST_HOSTS", whitelist);
						options.Add ("DS_BRUTEFORCE_SCOPE", "quick");
						options.Add ("DS_BRUTEFORCE_SERVICES", svcs);
						options.Add ("DS_BRUTEFORCE_SPEED", "TURBO");
						options.Add ("DS_INCLUDE_KNOWN", true);
						options.Add ("DS_BRUTEFORCE_GETSESSION", true);
						
						response = manager.StartBruteforce (options);
						
						taskResponse = manager.GetProTaskStatus (response ["task_id"] as string);
					
						taskResponse = taskResponse.First ().Value as Dictionary<string, object>;
						
						while (taskResponse["status"] as string == "running") {
							Console.WriteLine ("Waiting on metasploit bruteforce");
							Thread.Sleep (new TimeSpan (0, 0, 30));
							taskResponse = manager.GetProTaskStatus (response ["task_id"] as string);
							taskResponse = taskResponse.First ().Value as Dictionary<string, object>;
						
						}
						
					}
					
					options = new Dictionary<string, object> ();		
					options.Add ("workspace", workspace);
					options.Add ("DS_WHITELIST", whitelist);
					options.Add ("DS_MinimumRank", "great");
					options.Add ("DS_EXPLOIT_SPEED", 5);
					options.Add ("DS_EXPLOIT_TIMEOUT", 2);
					options.Add ("DS_LimitSessions", false);
					options.Add ("DS_MATCH_VULNS", true);
					options.Add ("DS_MATCH_PORTS", true);
					
					response = manager.StartExploit (options);
					
					foreach (var pair in response)
						Console.WriteLine (pair.Key + ": " + pair.Value);
					
					return response ["task_id"] as string;
				}
			}
		}
		
		private enum VulnerabilityScanType
		{
			OpenVAS = 1,
			Nessus = 2,
			Nexpose = 3
		}
		
		public virtual string ToBusinessXml ()
		{
			string xml = "<scan>";
			
			xml = xml + "<hasRun>" + this.HasRun + "</hasRun>";
			xml = xml + "<name>" + this.Name + "</name>";
			
			if (this.ScanOptions != null)
				xml = xml + this.ScanOptions.ToBusinessXml ();
			
			xml = xml + "</scan>";
			
			return xml;
		}
	}

}