using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AutoAssess.Data.BusinessObjects;
using AutoAssess.Data.PersistentObjects;
using NHibernate.Criterion;
using System.Net;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Text;
using AutoAssess.Data.Nessus.BusinessObjects;
using AutoAssess.Data.Nexpose.BusinessObjects;
using nexposesharp;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.OpenVAS;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.OpenVAS.PersistentObjects;

namespace AutoAssess.Web
{
	public partial class ViewHost : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			bool isNessus = false;
			bool isNexpose = false;
			bool isOpenVAS = false;
			bool isMetasploit = false;
			Guid hid = new Guid (Request ["hid"]);
			PersistentProfile profile = this.CurrentProfile;
			PersistentNMapHost host;
			host = profile.CurrentResults.PersistentHosts.Where (h => h.ProfileHost.ID == hid && h.IsActive).SingleOrDefault ();
			
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
				isOpenVAS = true;
			
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
			
			lblHostname.Text = host.Hostname;
			lblDeviceType.Text = host.DeviceType;
			lblIPv4.Text = host.IPAddressv4;
			lblNetworkDistance.Text = host.NetworkDistance;
			lblOS.Text = host.OS;
			
			List<DataTableObject> objs = new List<DataTableObject>();
			
			foreach (PersistentPort port in host.PersistentPorts.Where(p => p.IsTCP))
			{
				DataTableObject obj = new DataTableObject();
				
				obj.PortID = port.ID;
				obj.Port = port.PortNumber;
				obj.ServiceName = port.Service;
				
				if (isMetasploit)
				{
					PersistentMetasploitHost msfHost = msfScan.PersistentHosts.Where(h => h.Address == host.IPAddressv4).Single();
					
					obj.MetasploitCredentials = msfHost.PersistentCredentials.Where(c => c.Port == port.PortNumber).Count();
					obj.MetasploitExploits = msfHost.PersistentSessions.Where(s => s.Port == port.PortNumber.ToString()).Count();
					obj.ScannedByMetasploit = true;
				}
				else 
					obj.ScannedByMetasploit = false;
				
				if (isNessus)
				{
					PersistentNessusReportHost nssHost = nssScan.PersistentHosts.Where(h => h.PersistentHostProperties.HostIP == host.IPAddressv4).Single();
					
					obj.NessusGrade = nssHost.PersistentReportItems.Where(i => i.Severity != "0" && i.Port == port.PortNumber.ToString()).Count();
					obj.ScannedByNessus = true;
				}
				else	
					obj.ScannedByNessus = false;
				
				if (isNexpose)
				{
					PersistentNexposeAsset nxHost = nxScan.PersistentAssets.Where(a => a.IPAddressV4 == host.IPAddressv4).Single();
					
					if (nxHost.PersistentServices.Where(s => s.Port == port.PortNumber && s.Protocol == (port.IsTCP ? "tcp" : "udp")).Count() != 0)
					{
						PersistentNexposeHostService service = nxHost.PersistentServices.Where(s => s.Port == port.PortNumber && s.Protocol == (port.IsTCP ? "tcp" : "udp") ).Single();
					
						obj.NexposeGrade = service.PersistentTests.Where(t => t.Status == "vulnerable-exploited" || t.Status == "vulnerable-version").Count();
						obj.ScannedByNexpose = true;
					}
					else
						obj.ScannedByNexpose = false;
				}
				else
					obj.ScannedByNexpose = false;
				
				if (isOpenVAS)
				{	
					obj.ScannedByOpenVAS = true;
					obj.OpenVASGrade = ovasScan.PersistentResults.Where(r => r.Host == host.IPAddressv4 && r.Port.Contains("(" + port.PortNumber + "/")).Count();
				}
				else
					obj.ScannedByOpenVAS = false;
				
				objs.Add(obj);
			}
	
				gvPorts.DataSource = objs.OrderBy(o => o.Port);
				gvPorts.DataBind();

		}
		
		protected void gvPorts_RowDataBound (object sender, GridViewRowEventArgs e)
		{
			if (e.Row.DataItem is DataTableObject) {
				Button btn = e.Row.FindControl ("btnViewPortDetails") as Button;
				
				btn.Text = "View port: " + (e.Row.DataItem as DataTableObject).Port;
				btn.CommandArgument = (e.Row.DataItem as DataTableObject).PortID.ToString ();
			}
			if (e.Row.RowIndex%2 == 0)
			{
				e.Row.BackColor = Color.LightSteelBlue;
			}
		}
		
		protected void btnViewPortDetails_Click (object sender, EventArgs e)
		{
			Button b = sender as Button;
			
			Response.Redirect ("/ViewPortDetails.aspx?hpid=" + b.CommandArgument + "&hid=" + Request ["hid"]);
		}
		
		protected class DataTableObject
		{
			public Guid PortID { get; set; }
			
			public int Port { get; set; }
			
			public string ServiceName { get; set; }
			
			public bool ScannedByOpenVAS { get; set; }
			
			public bool ScannedByNexpose { get; set; }
			
			public bool ScannedByNessus { get; set; }
			
			public bool ScannedByMetasploit { get; set; }
			
			public int OpenVASGrade { get; set; }
			
			public int NessusGrade { get; set; }
			
			public int NexposeGrade { get; set; }
			
			public int MetasploitExploits { get; set; }
			
			public int MetasploitCredentials { get; set; }
		}
	}
	
}

