using System;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using NHibernate;
using NHibernate.Criterion;
using AutoAssess.Data.PersistentObjects;
using System.IO;
using AutoAssess.Data.Nessus.BusinessObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.Nexpose.BusinessObjects;
using AutoAssess.Data.OpenVAS;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using nexposesharp;
using metasploitsharp;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace AutoAssess.Web
{
	public partial class ViewProfile : AutoAssessPage
	{
	
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			bool isNessus = false;
			bool isOpenvas = false;
			bool isNexpose = false;
			bool isMetasploit = false;
			
			PersistentProfile profile = this.CurrentScanSession.Get<PersistentProfile>(new Guid(this.Request["pid"]));
			
			if (profile.CurrentResults == null)
			{
				return;
			}
			
			//whee
			foreach (PersistentNMapHost host in profile.CurrentResults.PersistentHosts)
			{
				foreach (PersistentPort port in host.PersistentPorts)
				{
					
				}
			}
			
			this.CurrentProfile = profile;
			
			PersistentScan latestScan = this.CurrentScanSession.CreateCriteria<PersistentScan>()
				.Add(Restrictions.Eq ("ParentProfileID", profile.ID))
				.Add(Restrictions.Eq("HasRun", true))
				.List<PersistentScan>()
				.LastOrDefault();
			
			PersistentNessusScan nssScan = this.CurrentScanSession.CreateCriteria<PersistentNessusScan>()
				.Add(Restrictions.Eq("ParentScanID", latestScan.ID))
				.List<PersistentNessusScan>()
				.SingleOrDefault();
			
			if (nssScan != null)
				isNessus = true;
			
			PersistentOpenVASScan ovasScan = this.CurrentScanSession.CreateCriteria<PersistentOpenVASScan>()
				.Add(Restrictions.Eq("ParentScanID", latestScan.ID))
				.List<PersistentOpenVASScan>()
				.SingleOrDefault();
			
			if (ovasScan != null)
				isOpenvas = true;
			
			PersistentNexposeScan nxScan = this.CurrentScanSession.CreateCriteria<PersistentNexposeScan>()
				.Add(Restrictions.Eq ("ParentScanID", latestScan.ID))
				.List<PersistentNexposeScan>()
				.SingleOrDefault();
			
			if (nxScan != null)
				isNexpose = true;
			
			PersistentMetasploitScan msfScan = this.CurrentScanSession.CreateCriteria<PersistentMetasploitScan>()
				.Add(Restrictions.Eq ("ParentScanID", latestScan.ID))
				.List<PersistentMetasploitScan>()
				.SingleOrDefault();
			
			if (msfScan != null)
				isMetasploit = true;
			
			List<DataTableObject> objs = new List<DataTableObject>();
			
			foreach (PersistentNMapHost host in profile.CurrentResults.PersistentHosts)
			{
				DataTableObject obj = new DataTableObject();
				
				obj.IP = host.IPAddressv4;
				obj.HostName = host.Hostname;
				
				PersistentNessusReportHost nssHost = null;
				if (isNessus)
				{
					nssHost = nssScan.PersistentHosts.Where(h => h.PersistentHostProperties.HostIP == host.IPAddressv4).SingleOrDefault();
					
					if (nssHost != null)
					{
						obj.ScannedByNessus = true;
						obj.NessusGrade = nssHost.PersistentReportItems.Where(r => int.Parse(r.Severity) > 0).Count();
					}
					else
						obj.ScannedByNessus = false;
				}
				
				PersistentMetasploitHost msfHost = null;
				if (isMetasploit)
				{
					msfHost = msfScan.PersistentHosts.Where(h => h.Address == host.IPAddressv4).SingleOrDefault();
					
					if (msfHost != null)
					{
						obj.ScannedByMetasploit = true;
						obj.Exploits = msfHost.PersistentSessions.Count();
						obj.MetasploitGrade = msfHost.PersistentVulnerabilities.Count();
					}
					else 
						obj.ScannedByMetasploit = false;
				}
				else
					obj.ScannedByMetasploit = false;
				
				PersistentNexposeAsset nxHost = null;
				if (isNexpose)
				{
					nxHost = nxScan.PersistentAssets.Where(a => a.IPAddressV4 == host.IPAddressv4).SingleOrDefault();
					
					if (nxHost != null)
					{
						obj.ScannedByNexpose = true;
						obj.NexposeGrade = nxHost.PersistentHostTests.Where(t => t.Status == "vulnerable-version" || t.Status == "vulnerable-exploited").Count();
						
						foreach (PersistentNexposeHostService service in nxHost.PersistentServices)
							obj.NexposeGrade += service.PersistentTests.Where(t => t.Status == "vulnerable-version" || t.Status == "vulnerable-exploited").Count();
					}
					else 
						obj.ScannedByNexpose = false;
				}
				else
					obj.ScannedByNexpose = false;
				
				List<PersistentReportResult> ovasHost = null;
				if (isOpenvas)
				{
					ovasHost = new List<PersistentReportResult>();
					foreach (PersistentReportResult result in ovasScan.PersistentResults)
					{
						if (result.Host == host.IPAddressv4)
							ovasHost.Add(result);
					}
					
					if (ovasHost.Count() > 0)
					{
						obj.ScannedByOpenVAS = true;
						obj.OpenVASGrade = ovasHost.Count();
					}
					else
						obj.ScannedByOpenVAS = false;
				}
				else
					obj.ScannedByOpenVAS = false;
				
				obj.HostID = host.ProfileHost.ID;
				objs.Add(obj);
			}
			
			gvHosts.DataSource = objs;
			gvHosts.DataBind();
		}
		
		protected void btnGotoHost_Click(object sender, EventArgs e)
		{
			Response.Redirect("/ViewHost.aspx?hid=" + (sender as Button).CommandArgument);
		}
		
		protected void gvHosts_OnRowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.DataItem != null)
			{
				Button btnHost = (e.Row.FindControl("btnGoToHost") as Button);
				Label lblHostname = (e.Row.FindControl("lblHostname") as Label);
				Label lblOverallRisk = (e.Row.FindControl("lblOverallRisk") as Label);
				Label lblNessusGrade = (e.Row.FindControl("lblNessusGrade") as Label);
				Label lblNexposeGrade = (e.Row.FindControl("lblNexposeGrade") as Label);
				Label lblOpenVASGrade = (e.Row.FindControl("lblOpenVASGrade") as Label);
				Label lblMetasploitGrade = (e.Row.FindControl("lblMetasploitGrade") as Label);
				Label lblExploitable = (e.Row.FindControl("lblExploitable") as Label);
				
				DataTableObject obj = e.Row.DataItem as DataTableObject;
				
				lblHostname.Text = obj.HostName;
				btnHost.Text = obj.IP;
				btnHost.CommandArgument = obj.HostID.ToString();
				lblOverallRisk.Text = (((double)(obj.NexposeGrade + obj.NessusGrade + obj.OpenVASGrade + obj.MetasploitGrade))/4d).ToString();
				lblMetasploitGrade.Text = obj.MetasploitGrade.ToString();
				lblNessusGrade.Text = obj.NessusGrade.ToString();
				lblNexposeGrade.Text = obj.NexposeGrade.ToString();
				lblOpenVASGrade.Text = obj.OpenVASGrade.ToString();
				lblExploitable.Text = obj.Exploits.ToString();
			}
		}
	}
	
	[Serializable]
	public class DataTableObject
	{
		public Guid HostID { get; set; }
		
		public string IP { get; set; }
		
		public string HostName { get; set; }
		
		public bool ScannedByNessus { get; set; }
		
		public bool ScannedByNexpose { get; set; }
		
		public bool ScannedByOpenVAS { get; set; }
		
		public bool ScannedByMetasploit { get; set; }
		
		//public int RiskiestPort { get; set; }
		
		//public int RiskiestPortVulns { get; set; }
		
		public int Exploits { get; set; }
		
		public int NessusGrade { get; set; }
		
		public int NexposeGrade { get; set; }
		
		public int OpenVASGrade { get; set; }
		
		public int MetasploitGrade { get; set; }
	}
}

