using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using AutoAssess.Data;
using System.Net;
using System.Xml;
using System.IO;
using System.Text;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class Profile
	{
		List<ProfileHost> _profileHosts;
		
		public Profile ()
		{
		}
		
		public virtual string Name { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual string Range { get; set; }
		
		public virtual string Domain { get; set; }
		
		public virtual Dictionary<string, string> Configuration { get; set; }
		
		public virtual bool HasRun { get; set; }
		
		public virtual string Duration { get; set; }
		
		public virtual DateTime RunAfter { get; set; }
		
		public virtual TimeSpan RunEvery { get; set; }
		
		public virtual int DaysBetweenScan { get; set; }
		
		public virtual NMapToolResults CurrentResults { get; set; }
		
		public virtual IList<NMapToolResults> AllResults { get; set; }
		
		public virtual void SetProfileHosts (List<ProfileHost> hosts)
		{
			_profileHosts = hosts;
		}
		
		public virtual void Run (out List<List<IToolResults>> listOfToolResults)
		{
			string eventDescription = string.Empty;
			DateTime start = DateTime.Now;
			IToolOptions _options;
			
			_options = new NMapToolOptions ();
			(_options as NMapToolOptions).Range = this.Range;
			(_options as NMapToolOptions).Path = this.Configuration ["nmapPath"];
			
			NMap nmap = new NMap (_options as NMapToolOptions);
			
			eventDescription = "Starting profile creation with nmap at " + DateTime.Now.ToLongTimeString ().ToString() + " for profile: " + this.Name;
			
			CreateEvent(DateTime.Now, eventDescription, 1);
			Console.WriteLine (eventDescription);
			
			this.CurrentResults = (nmap.Run (this) as NMapToolResults);
					
			eventDescription = "Found " + this.CurrentResults.Hosts.Count + " hosts for profile " + this.Name + ": " + DateTime.Now.ToString();
			CreateEvent(DateTime.Now, eventDescription, 1);
			Console.WriteLine (eventDescription);
					
			List<Thread > portThreads = new List<Thread> ();
			List<string > deepResults;
				
			ProfileHost profileHost;
			
			eventDescription = "Entering deep scan phase: " + DateTime.Now.ToString();
			CreateEvent(DateTime.Now, eventDescription, 1);
			Console.WriteLine(eventDescription);
			foreach (NMapHost host in this.CurrentResults.Hosts) {
				deepResults = new List<string> ();
						
				foreach (Port port in host.Ports) {		
					string ip = host.IPAddressv4;
					string portNo = port.PortNumber.ToString ();
							
					Thread thread = new Thread (() => deepResults.Add (new NMap (_options as NMapToolOptions).DeepScan (ip, portNo)));
							
					thread.Start ();
							
					portThreads.Add (thread);

				}	
						
				while (portThreads.Where(t => t.IsAlive).Count() > 0)
					Thread.Sleep (500);
				
				foreach (Thread thread in portThreads)
					thread.Join ();
				
				foreach (Port port in host.Ports) {
					string fp = String.Format ("\n{0}/{1}", port.PortNumber, (port.IsTCP ? "tcp" : "udp"));
					port.DeepScan = deepResults
								.Where (s => s.Contains (fp))
								.FirstOrDefault ();
							
				}
						
			}
			
			
			
			eventDescription = "Finished deep scan phase: " + DateTime.Now.ToString();
			CreateEvent(DateTime.Now, eventDescription, 1);
			Console.WriteLine(eventDescription);
			
			listOfToolResults = new List<List<IToolResults>> (); //yuck
			foreach (NMapHost host in this.CurrentResults.Hosts) {
				List<IToolResults> toolResults;
				ScanHost (host, out toolResults);
							
				listOfToolResults.Add (toolResults);
			}
					
			DateTime end = DateTime.Now;
					
			TimeSpan time = end - start;
					
			this.Duration = time.TotalSeconds.ToString ();
		}
		
		private void CreateEvent (DateTime timestamp, string description, int severity)
		{
			HttpWebRequest req = WebRequest
					.Create (Configuration["API"] + "/CreateEvent.ashx" +
						"?WebUserID=" + Guid.Empty.ToString() +
						"&UserID=" + Configuration ["UserID"] +
						"&IsActive=" + true +
						"&ProfileID=" + this.Configuration["profileID"] +
						"&Description=" + description + 
				        "&TimeStamp=" + DateTime.Now.ToString () + 
				        "&Severity=" + severity.ToString ()) as HttpWebRequest;
			
			WebResponse resp = req.GetResponse ();
			
			XmlDocument d = new XmlDocument ();	
			string x = string.Empty;
			byte[] buf = new byte[2048];
			int b = 0;
			using (Stream stream = resp.GetResponseStream()) {
				do {
					b = stream.Read (buf, 0, buf.Length);
						
					x = x + ASCIIEncoding.ASCII.GetString (buf);
						
					buf = new byte[2048]; //clear cruft
				} while (b > 0);
			}
				
			x = x.Replace ("&", "&amp;");
			d.LoadXml (x);
			//do nothing?
			
		}
		
		private void ScanHost (NMapHost host, out List<IToolResults> toolResults)
		{
			
			IToolOptions _options; 
			string eventDescription;
			toolResults = new List<IToolResults> ();
					
			bool routed = false;
			int tries = 0;
					
			foreach (Port port in host.Ports) {
					
				if (port.Service == "http") {
					_options = new NiktoToolOptions ();
							
					(_options as NiktoToolOptions).Host = host.IPAddressv4;
					(_options as NiktoToolOptions).Port = port.PortNumber;
					(_options as NiktoToolOptions).Path = this.Configuration ["niktoPath"];
							
					Nikto nikto = new Nikto (_options);
							
					eventDescription = "Running nikto (http) on host: " + (string.IsNullOrEmpty (host.Hostname) ? host.IPAddressv4 : host.Hostname);
					CreateEvent(DateTime.Now, eventDescription, 1);
					Console.WriteLine(eventDescription);
							
					NiktoToolResults niktoResults = (nikto.Run () as NiktoToolResults);
					niktoResults.HostIPAddressV4 = host.IPAddressv4;
					niktoResults.HostPort = port.PortNumber;
					niktoResults.IsTCP = true;
					toolResults.Add (niktoResults);
							
				} else if (port.Service == "https") {
					_options = new SSLScanToolOptions ();
							
					(_options as SSLScanToolOptions).Host = host.IPAddressv4;
					(_options as SSLScanToolOptions).Port = port.PortNumber;
					(_options as SSLScanToolOptions).Path = this.Configuration ["sslscanPath"];
							
					SSLScan sslscan = new SSLScan (_options);
							
							
					eventDescription = "Running sslscan (https) on host: " + (string.IsNullOrEmpty (host.Hostname) ? host.IPAddressv4 : host.Hostname);
					CreateEvent(DateTime.Now, eventDescription, 1);
					Console.WriteLine(eventDescription);
							
					SSLScanToolResults sslResults = (sslscan.Run () as SSLScanToolResults);
					
					sslResults.HostIPAddressV4 = host.IPAddressv4;
					sslResults.HostPort = port.PortNumber;
					sslResults.IsTCP = true;
					toolResults.Add (sslResults);
							
					_options = new NiktoToolOptions ();
							
					(_options as NiktoToolOptions).Host = host.IPAddressv4;
					(_options as NiktoToolOptions).Port = port.PortNumber;
					(_options as NiktoToolOptions).IsSSL = true;
					(_options as NiktoToolOptions).Path = this.Configuration ["niktoPath"];
							
					Nikto nikto = new Nikto (_options);
							
							
					eventDescription = "Running nikto (https) on host: " + (string.IsNullOrEmpty (host.Hostname) ? host.IPAddressv4 : host.Hostname);
					CreateEvent(DateTime.Now, eventDescription, 1);
					Console.WriteLine(eventDescription);
							
					NiktoToolResults niktoResults = (nikto.Run () as NiktoToolResults);
					

					niktoResults.HostIPAddressV4 = host.IPAddressv4;
					niktoResults.HostPort = port.PortNumber;
					niktoResults.IsTCP = true;
					toolResults.Add (niktoResults);
						
				} else if (port.Service == "ssh") {
					_options = new SSLScanToolOptions ();
							
					(_options as SSLScanToolOptions).Host = host.IPAddressv4;
					(_options as SSLScanToolOptions).Port = port.PortNumber;
					(_options as SSLScanToolOptions).Path = this.Configuration ["sslscanPath"];
							
					SSLScan sslscan = new SSLScan (_options);	
							
					eventDescription = "Running sslscan (ssh) on host: " + (string.IsNullOrEmpty (host.Hostname) ? host.IPAddressv4 : host.Hostname);
					CreateEvent(DateTime.Now, eventDescription, 1);
					Console.WriteLine(eventDescription);
							
					SSLScanToolResults sslResults = (sslscan.Run () as SSLScanToolResults);

					sslResults.HostIPAddressV4 = host.IPAddressv4;
					sslResults.HostPort = port.PortNumber;
					sslResults.IsTCP = true;
					toolResults.Add (sslResults);
							
				} else if (port.PortNumber == 445) { //smb
					_options = new SMBClientToolOptions ();
							
					(_options as SMBClientToolOptions).Host = host.IPAddressv4;
					(_options as SMBClientToolOptions).RecurseShares = true;
					(_options as SMBClientToolOptions).Path = this.Configuration ["smbclientPath"];		
					
					SMBClient smb = new SMBClient (_options);
						
					eventDescription = "Running smbclient (cifs) on host: " + (string.IsNullOrEmpty (host.Hostname) ? host.IPAddressv4 : host.Hostname);
					CreateEvent(DateTime.Now, eventDescription, 1);
					Console.WriteLine(eventDescription);
						
					SMBClientToolResults smbResults = smb.Run () as SMBClientToolResults;
					
					smbResults.ParentPort = port;

					smbResults.HostIPAddressV4 = host.IPAddressv4;
					smbResults.HostPort = port.PortNumber;
					smbResults.IsTCP = true;
					toolResults.Add (smbResults);
						
					eventDescription = string.Format("Found {0} shares on host {1}", smbResults.ShareDetails.Count, host.Hostname);
					CreateEvent(DateTime.Now, eventDescription, 1);
					Console.WriteLine(eventDescription);
								
		
				} else if (port.Service == "snmp") {
					_options = new OneSixtyOneToolOptions ();
							
					(_options as OneSixtyOneToolOptions).Host = host.IPAddressv4;
					(_options as OneSixtyOneToolOptions).Path = this.Configuration ["onesixtyonePath"];
					
					OneSixtyOne onesixone = new OneSixtyOne (_options);
						
					eventDescription = "Running onesixtyone (snmp) on host: " + (string.IsNullOrEmpty (host.Hostname) ? host.IPAddressv4 : host.Hostname);
					CreateEvent(DateTime.Now, eventDescription, 1);
					Console.WriteLine(eventDescription);
							
					OneSixtyOneToolResults osoResults = onesixone.Run () as OneSixtyOneToolResults;

					osoResults.HostIPAddressV4 = host.IPAddressv4;
					osoResults.HostPort = port.PortNumber;
					osoResults.IsTCP = true;
					toolResults.Add (osoResults);
				}
						
			}
				
			eventDescription = "Finished host " + (string.IsNullOrEmpty (host.Hostname) ? host.IPAddressv4 : host.Hostname);
			CreateEvent(DateTime.Now, eventDescription, 1);
			Console.WriteLine(eventDescription);
		}
		
		public virtual string ToBusinessXml (bool getAllResults)
		{
			string xml = "<profile>";
			
			xml = xml + "<daysBetweenScan>" + this.DaysBetweenScan + "</daysBetweenScan>";
			xml = xml + "<hasRun>" + this.HasRun + "</hasRun>";
			xml = xml + "<name>" + this.Name + "</name>";
			xml = xml + "<range>" + this.Range + "</range>";
			xml = xml + "<runAfter>" + this.RunAfter.ToLongDateString () + "</runAfter>";
			xml = xml + "<runEvery>" + this.RunEvery.Days + " days</runEvery>";
			xml = xml + "<resultCount>" + this.AllResults.Count + "</resultCount>";
			
			if (!getAllResults) {
				xml = xml + this.CurrentResults.ToBusinessXml ();
				
			} else {
				
			}
			xml = xml + "</profile>";
			
			return xml;
		}
	}
}
