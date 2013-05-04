using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.BusinessObjects;
using AutoAssess.Data.Nessus;
using NHibernate.Criterion;
using System.Net;
using System.Configuration;
using System.IO;
using System.Text;
using AutoAssess.Data.Nessus.BusinessObjects;
using nexposesharp;
using AutoAssess.Data.Nexpose.BusinessObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.OpenVAS;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using System.Text.RegularExpressions;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace AutoAssess.Web
{
	public class NotSQLWebVuln
	{
		public string Method { get; set; }
		
		public string URL { get; set; }
		
		public string Parameter { get; set; }
	}
	
	public class WebVuln : NotSQLWebVuln
	{
		public string IsExploitable { get; set; }
	}
	
	public partial class ViewPortDetails : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
		
			Guid hpid = new Guid (this.Request ["hpid"]);
			Guid hid = new Guid (this.Request ["hid"]);
			
			PersistentNMapHost host = this.CurrentProfile.CurrentResults.PersistentHosts
				.Where (h => h.ProfileHost.ID == hid)
				.Single ();
			
			PersistentPort port = host.PersistentPorts
				.Where (p => p.ID == hpid)
				.SingleOrDefault ();
			
			if (port == null)
				return;
			
			bool isNessus = false;
			bool isNexpose = false;
			bool isOpenVAS = false;
			bool isMetasploit = false;
			PersistentProfile profile = this.CurrentProfile;
			host = profile.CurrentResults.PersistentHosts.Where (h => h.ProfileHost.ID == hid && h.IsActive).SingleOrDefault ();
			
			PersistentScan latestScan = this.CurrentScanSession.CreateCriteria<PersistentScan> ()
				.Add (Restrictions.Eq ("ParentProfileID", profile.ID))
				.Add (Restrictions.Eq ("HasRun", true))
				.List<PersistentScan> ()
				.LastOrDefault ();
			
			PersistentNessusScan nssScan = this.CurrentScanSession.CreateCriteria<PersistentNessusScan> ()
				.Add (Restrictions.Eq ("ParentScanID", latestScan.ID))
				.List<PersistentNessusScan> ()
				.SingleOrDefault ();
			
			if (nssScan != null)
				isNessus = true;
			
			PersistentOpenVASScan ovasScan = this.CurrentScanSession.CreateCriteria<PersistentOpenVASScan> ()
				.Add (Restrictions.Eq ("ParentScanID", latestScan.ID))
				.List<PersistentOpenVASScan> ()
				.SingleOrDefault ();
			
			if (ovasScan != null)
				isOpenVAS = true;
			
			PersistentNexposeScan nxScan = this.CurrentScanSession.CreateCriteria<PersistentNexposeScan> ()
				.Add (Restrictions.Eq ("ParentScanID", latestScan.ID))
				.List<PersistentNexposeScan> ()
				.SingleOrDefault ();
			
			if (nxScan != null)
				isNexpose = true;
			
			PersistentMetasploitScan msfScan = this.CurrentScanSession.CreateCriteria<PersistentMetasploitScan> ()
				.Add (Restrictions.Eq ("ParentScanID", latestScan.ID))
				.List<PersistentMetasploitScan> ()
				.SingleOrDefault ();
			
			if (msfScan != null)
				isMetasploit = true;
			
			if (isOpenVAS) {
				lblOpenVASPortResults.Text = "<h2><u>OpenVAS Results</u></h2>";
				
				var results = ovasScan.PersistentResults.Where (r => r.Threat != "Log" && r.Host == host.IPAddressv4 && r.Port.Contains ("(" + port.PortNumber + "/")).ToList ();
				
				List<DataTableObject> objs = new List<DataTableObject> ();
				foreach (var result in results) {
					DataTableObject obj = new DataTableObject ();
					
					obj.Name = result.PersistentNVT.Name;
					obj.Threat = result.Threat;
					
					objs.Add (obj);
				}
				
				if (objs.Count () == 0) {
					lblOpenVASPortResults.Text = string.Empty;
					lblOpenVASPortResults.Visible = false;
					gvOpenVASPortResults.Visible = false;
				} else {
					gvOpenVASPortResults.DataSource = objs.Where (o => o.Threat != "Log").ToList ();
					gvOpenVASPortResults.DataBind ();
				}
			} else {
				gvOpenVASPortResults.Visible = false;
			}
			
			if (isNessus) {
				lblNessusPortResults.Text = "<h2><u>Nessus Results</u></h2>";
				
				PersistentNessusReportHost nssHost = nssScan.PersistentHosts.Where (h => h.PersistentHostProperties.HostIP == host.IPAddressv4).Single ();
				
				var items = nssHost.PersistentReportItems.Where (i => i.Severity != "0" && i.Port == port.PortNumber.ToString ());
				
				List<DataTableObject> objs = new List<DataTableObject> ();
				foreach (var item in items) {
					DataTableObject obj = new DataTableObject ();
					
					obj.Name = item.PluginName;
					obj.Threat = item.Severity;
					
					objs.Add (obj);
				}
				
				if (objs.Count () == 0) {
					lblNessusPortResults.Text = string.Empty;
					lblNessusPortResults.Visible = false;
					gvNessusPortResults.Visible = false;
				} else {
					gvNessusPortResults.DataSource = objs.OrderByDescending (o => o.Threat).ToList ();
					gvNessusPortResults.DataBind ();
				}
			} else {
				gvNessusPortResults.Visible = false;
			}
			
			if (isNexpose) {
				lblNexposePortResults.Text = "<h2><u>Nexpose Results</u></h2>";
				
				List<DataTableObject> objs = new List<DataTableObject> ();
				PersistentNexposeAsset nxHost = nxScan.PersistentAssets.Where (a => a.IPAddressV4 == host.IPAddressv4).Single ();
					
				if (nxHost.PersistentServices.Where (s => s.Port == port.PortNumber && s.Protocol == (port.IsTCP ? "tcp" : "udp")).Count () > 0) {
					PersistentNexposeHostService service = nxHost.PersistentServices.Where (s => s.Port == port.PortNumber && s.Protocol == (port.IsTCP ? "tcp" : "udp")).Single ();
				
					var tests = service.PersistentTests.Where (s => s.Status == "vulnerable-exploited" || s.Status == "vulnerable-version");
				
					foreach (var test in tests) {
						DataTableObject obj = new DataTableObject ();
					
						string n = (new Regex ("<.*?>", RegexOptions.Compiled)).Replace ((test as NexposeTest).NexposeParagraph, string.Empty).Replace ("&lt;", "<").Replace ("&gt;", ">");
					
						if (objs.Where (o => o.Name == n).Count () > 0)
							continue;
					
						obj.Name = n;
						obj.Threat = test.IsPCICompliant ? "Pass" : "Fail";
					
						objs.Add (obj);
					}
				}
				
				if (objs.Count () == 0) {
					lblNexposePortResults.Text = string.Empty;
					lblNexposePortResults.Visible = false;
					gvNexposePortResults.Visible = false;
				} else {
					gvNexposePortResults.DataSource = objs.OrderByDescending (o => o.Name).ToList ();
					gvNexposePortResults.DataBind ();
				}
			} else {
				gvNexposePortResults.Visible = false;
			}
			
			if (isMetasploit) {
				PersistentMetasploitHost msfHost = msfScan.PersistentHosts.Where (h => h.Address == host.IPAddressv4).Single ();
				
				var creds = msfHost.PersistentCredentials.Where (c => c.Port == port.PortNumber);
				var sessions = msfHost.PersistentSessions.Where (s => s.Port == port.PortNumber.ToString ());
			} else {
			}
			
			
			if (port.Service == "ssh") {					
				PersistentSSLScanResults sslResults = this.CurrentScanSession.CreateCriteria<PersistentSSLScanResults> ()
					.Add (Restrictions.Eq ("HostPortID", hpid))
					.List<PersistentSSLScanResults> ()
					.FirstOrDefault ();
				
				if (sslResults != null) {
					//lblSSLScanHeader.Text = "<br /><br /><h3><u>SSL Scan Results</u></h3>";
					//lblSSLScan.Text = sslResults.FullOutput.Replace ("\n", ",<br />");
				}
			}
			
			if (port.Service == "snmp") {
			
				PersistentOneSixtyOneResults snmpResults = this.CurrentScanSession.CreateCriteria<PersistentOneSixtyOneResults> ()
					.Add (Restrictions.Eq ("HostPortID", hpid))
					.List<PersistentOneSixtyOneResults> ()
					.FirstOrDefault ();
				
				if (snmpResults != null) {
					lblSNMPResultsHeader.Text = "<br /><br /><h3><u>SNMP Results</u></h3>";
					lblSNMPResults.Text = snmpResults.FullOutput.Replace ("\n", ",<br />");
				}	
			} else if (port.Service == "smb") {
				PersistentSMBClientResults smbResults = this.CurrentScanSession.CreateCriteria<PersistentSMBClientResults> ()
					.Add (Restrictions.Eq ("HostPortID", hpid))
					.List<PersistentSMBClientResults> ()
					.FirstOrDefault ();
				if (smbResults != null) {
					lblSMBScanHeader.Text = "<br /><br /><h3><u>SMB Results</u></h3>";
					lblSMBScan.Text = smbResults.FullOutput.Replace ("\n", ",<br />");
				}
			} else if (port.Service == "http" || port.Service == "https") {
				
				if (port.Service == "https") {
					PersistentSSLScanResults sslResults = this.CurrentScanSession.CreateCriteria<PersistentSSLScanResults> ()
						.Add (Restrictions.Eq ("HostPortID", hpid))
						.List<PersistentSSLScanResults> ()
						.FirstOrDefault ();
					
					if (sslResults != null) {
						//lblSSLScanHeader.Text = "<br /><br /><h3><u>SSL Scan Results</u></h3>";
						//lblSSLScan.Text = sslResults.FullOutput.Replace ("\n", ",<br />");
					}
				}
				PersistentWapitiResults wapitiResults = this.CurrentScanSession.CreateCriteria<PersistentWapitiResults> ()
					.Add (Restrictions.Eq ("HostPortID", hpid))
					.List<PersistentWapitiResults> ()
					.FirstOrDefault ();
				
				IList<PersistentSQLMapResults> results = this.CurrentScanSession.CreateCriteria<PersistentSQLMapResults> ()
					.Add (Restrictions.Eq ("ParentHostPortID", hpid))
					.List<PersistentSQLMapResults> ();
			
				List<PersistentSQLMapVulnerability> vulns = new List<PersistentSQLMapVulnerability> ();
				
				foreach (var result in results)
					vulns.AddRange (result.PersistentVulnerabilities.ToList ());


				if (wapitiResults != null && wapitiResults.Bugs != null) {
					var sqlInjectionPoints = wapitiResults.Bugs.Where (b => b.Info.Contains ("SQL Injection") && !b.Info.Contains ("Blind")).ToList ();
					var wxss = wapitiResults.Bugs.Where (b => b.Info.Contains ("XSS")).ToList ();
					var wincludes = wapitiResults.Bugs.Where (b => b.Info.Contains ("include"));
					var wexecution = wapitiResults.Bugs.Where (b => b.Info.Contains ("execution"));
					
					
					List<NotSQLWebVuln> xss = new List<NotSQLWebVuln> ();
					List<NotSQLWebVuln> includes = new List<NotSQLWebVuln> ();
					List<NotSQLWebVuln> execution = new List<NotSQLWebVuln> ();
					
					foreach (var x in wxss) {
						NotSQLWebVuln v = new NotSQLWebVuln ();
						
						v.Method = x.URL.Contains (x.Parameter) ? "GET" : "POST";
						v.Parameter = x.Parameter;
						v.URL = x.URL;
						
						xss.Add (v);
					}
					
					foreach (var x in wincludes) {
						NotSQLWebVuln i = new NotSQLWebVuln ();
						
						i.Method = x.URL.Contains (x.Parameter) ? "GET" : "POST";
						i.Parameter = x.Parameter;
						i.URL = x.URL;
						
						includes.Add (i);
					}
					
					foreach (var x in wexecution) {
						NotSQLWebVuln ex = new NotSQLWebVuln ();
						
						ex.Method = x.URL.Contains (x.Parameter) ? "GET" : "POST";
						ex.Parameter = x.Parameter;
						ex.URL = x.URL;
						
						execution.Add (ex);
					}
					
					lblXSS.Text = "XSS Vulnerabilities";
					gvXSS.DataSource = xss;
					gvXSS.DataBind ();

					lblIncludes.Text = "Remote and Local File Include Vulnerabilities";
					gvIncludes.DataSource = includes;
					gvIncludes.DataBind ();
					
					lblCommandExecution.Text = "Remote Command Execution Vulnerabilities";
					gvCommandExecution.DataSource = execution;
					gvCommandExecution.DataBind ();
				
					if (sqlInjectionPoints.Count () > 0) {
						List<WebVuln> exploitedVulns = new List<WebVuln> ();
						List<WebVuln> otherVulns = new List<WebVuln> ();
					
						foreach (var bug in sqlInjectionPoints) {
							WebVuln v = new WebVuln ();
						
							v.URL = bug.URL;
							v.Method = (bug.URL.Contains (bug.Parameter) ? "GET" : "POST");
						
							var vul = vulns.Where (vuln => vuln.Target == bug.URL).FirstOrDefault ();
						
							v.IsExploitable = (vul != null) ? "Exploited with " + vul.PayloadType + " SQL injection." : string.Empty;
						
							foreach (string parm in bug.Parameter.Split('&')) {
								if (parm.Contains ("%BF%27%22%28"))
									v.Parameter = "<b>" + parm.Split ('=') [0] + "</b>";
								else if (parm.Contains ("%27+or+sleep%287%29%23")) {
									v.Parameter = parm.Split ('=') [0];
								
									if (string.IsNullOrEmpty (v.IsExploitable))
										v.IsExploitable = "Exploited with a blind SQL injection.";
								}
							}
						
							if (string.IsNullOrEmpty (v.IsExploitable)) {
								otherVulns.Add (v);
								continue;
							}
						
							exploitedVulns.Add (v);
						}
					
						lblPossibleSQLInjections.Text = "Possible SQL Injection Vulnerabilities";
						gvPossibleInjectionPoints.DataSource = otherVulns;
						gvPossibleInjectionPoints.DataBind ();
					
						lblSQLInjections.Text = "Exploitable SQL Injection Vulnerabilities";
						gvSQLInjections.DataSource = exploitedVulns;
						gvSQLInjections.DataBind ();
					}
			
				}

				PersistentNiktoResults niktoResults = this.CurrentScanSession.CreateCriteria<PersistentNiktoResults> ()
					.Add (Restrictions.Eq ("HostPortID", hpid))
					.List<PersistentNiktoResults> ()
					.FirstOrDefault ();
				
				if (niktoResults != null) {
					lblNiktoResultsHeader.Text = "<h3><u>General Information or Insecure Configurations</u></h3>";
					lblNiktoResults.Text = "<ul>";
					
					foreach (var item in niktoResults.Items.Where(i=>!string.IsNullOrEmpty(i.Data)))
						lblNiktoResults.Text += "<li style=\"margin:5px;\">" + item.Data.Remove (0, 2) + "</li>";

					
					lblNiktoResults.Text += "</ul>";
				}
			}
			
			if (string.IsNullOrEmpty (lblNiktoResults.Text) && !string.IsNullOrEmpty (port.DeepScan)) {
				lblNiktoResultsHeader.Text = "<h2><u>Deep scan results</u></h2>";
				lblNiktoResults.Text = port.DeepScan.Replace ("\n", "<br />");
			}
		}
		
		protected void gvPossibleInjectionPoints_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			if (e.Row.DataItem is WebVuln) {
				Label method = e.Row.FindControl ("lblPossibleMethod") as Label;
				Label url = e.Row.FindControl ("lblPossibleURL") as Label;
				Label parm = e.Row.FindControl ("lblPossibleParameter") as Label;
				
				method.Text = (e.Row.DataItem as WebVuln).Method;
				url.Text = (e.Row.DataItem as WebVuln).URL;
				parm.Text = (e.Row.DataItem as WebVuln).Parameter;
			}
			if (e.Row.RowIndex % 2 == 0) {
				e.Row.BackColor = Color.LightSteelBlue;
			}
			
		}
		
		protected void gvSQLInjections_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			if (e.Row.DataItem is WebVuln) {
				Label method = e.Row.FindControl ("lblMethod") as Label;
				Label isExploitable = e.Row.FindControl ("lblKnownExploitable") as Label;
				Label url = e.Row.FindControl ("lblURL") as Label;
				Label parm = e.Row.FindControl ("lblParameter") as Label;
				
				method.Text = (e.Row.DataItem as WebVuln).Method;
				isExploitable.Text = (e.Row.DataItem as WebVuln).IsExploitable;
				url.Text = (e.Row.DataItem as WebVuln).URL;
				parm.Text = (e.Row.DataItem as WebVuln).Parameter;

			}
			if (e.Row.RowIndex % 2 == 0) {
				e.Row.BackColor = Color.LightSteelBlue;
			}
		}
		
		protected void gvIncludes_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			
			if (e.Row.RowIndex % 2 == 0) {
				e.Row.BackColor = Color.LightSteelBlue;
			}
		}
		
		protected void gvXSS_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			
			if (e.Row.RowIndex % 2 == 0) {
				e.Row.BackColor = Color.LightSteelBlue;
			}
		}
		
		protected void gvCommandExecution_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowIndex % 2 == 0) {
				e.Row.BackColor = Color.LightSteelBlue;
			}
		}
		
		protected void gvOpenVASPortResults_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			string cveid = string.Empty;
			
			e.Row.Attributes.Add ("ondblclick", "javascript:window.open('/RemediationDetails.aspx?cveid=" + cveid + "','','width=400,height=400')");
		}
		
		protected void gvSQLMapResults_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			
		}
		
		protected void gvNessusPortResults_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			string cveid = string.Empty;
			
			e.Row.Attributes.Add ("ondblclick", "javascript:window.open('/RemediationDetails.aspx?cveid=" + cveid + "','','width=400,height=400');return false;");
		}
		
		protected void gvWapitiResults_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			if (e.Row.DataItem is WapitiBug) {
				string requestType = (e.Row.DataItem as WapitiBug).URL.Contains ((e.Row.DataItem as WapitiBug).Parameter) ? "GET" : "POST";
				Label lblRequestType = e.Row.FindControl ("lblRequestType") as Label;
				
				lblRequestType.Text = requestType;
			}
			if (e.Row.RowIndex % 2 == 0) {
				e.Row.BackColor = Color.LightSteelBlue;
			}
		}
		
		protected void gvNexposePortResults_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			string cveid = string.Empty;

			e.Row.Attributes.Add ("ondblclick", "javascript:window.open('/RemediationDetails.aspx?cveid=" + cveid + "','','width=400,height=400')");
		}
		
		private Dictionary<string, string> ParseSSLScanOutput (string output)
		{
			Dictionary<string, string> result = new Dictionary<string, string> (); 
			string[] tmp = output.Split ('\n');
			
			foreach (string line in tmp) {
				string newline = line.Trim ();
				
				if (newline.StartsWith ("Accepted")) {
					
				} else if (newline.StartsWith ("Rejected")) {
					
				} else if (newline.StartsWith ("Failed")) {
					
				}
			}
			
			return result;
		}
		
		protected class DataTableObject
		{
			public string Threat { get; set; }
			
			public string Name { get; set; }
		}
	}
}

