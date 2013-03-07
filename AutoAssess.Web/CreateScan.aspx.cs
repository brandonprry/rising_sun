using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Xml;
using NHibernate;
using NHibernate.Criterion;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using AutoAssess.Data;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using AutoAssess.Data.Nessus.BusinessObjects;

namespace AutoAssess.Web
{
	public partial class CreateScan : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
		
			if (!Page.IsPostBack)
			{
				if (this.CurrentProfile != null)
				{
					txtProfileName.Text = this.CurrentProfile.Name;
					txtProfileRange.Text = this.CurrentProfile.Range;
				}
				else
					throw new Exception("Current profile null.");
			}
		}
		
		protected void btnBruteforce_Click(object sender, EventArgs e)
		{
			Dictionary<string, string> parms = new Dictionary<string, string>();
			
			parms.Add("Name", txtScanName.Text);
			parms.Add("ParentProfileID", this.CurrentProfile.ID.ToString());
			parms.Add("ScanIsBasicBruteforce", "true");
			
			MakeScan(parms);
			
			Response.Redirect("/ThankYou.aspx");
		}
		
		protected void btnBasicWebAssessment_Click(object sender, EventArgs e)
		{	
			Dictionary<string, string> parms = new Dictionary<string, string>();
			
			parms.Add("Name", txtScanName.Text);
			parms.Add("ParentProfileID", this.CurrentProfile.ID.ToString());
			parms.Add("ScanIsSQLMap", "true");
			parms.Add("ScanIsDSXS", "true");
			
			MakeScan(parms);
			
			Response.Redirect("/ThankYou.aspx");
		}
		
		protected void btnBasicVulnAssessment_Click(object sender, EventArgs e)
		{	
			Dictionary<string, string> parms = new Dictionary<string, string>();
			
			parms.Add("Name", txtScanName.Text);
			parms.Add("ParentProfileID", this.CurrentProfile.ID.ToString());
			parms.Add("ScanIsBasicBruteforce", "true");			
			parms.Add("ScanIsSQLMap", "true");
			parms.Add("ScanIsDSXS", "true");
			parms.Add("ScanIsOpenVAS", "true");
			parms.Add("ScanIsNessus", "true");
			parms.Add("ScanIsNexpose", "true");
			parms.Add("ScanIsMetasploit", "true");
			
			MakeScan(parms);
			
			Response.Redirect("/ThankYou.aspx");
		}
		
		protected void btnFullVulnAssessment_Click(object sender, EventArgs e)
		{
			Dictionary<string, string> parms = new Dictionary<string, string>();
			
			parms.Add("Name", txtScanName.Text);
			parms.Add("ParentProfileID", this.CurrentProfile.ID.ToString());
			parms.Add("ScanIsOpenVAS", "true");
			parms.Add("ScanIsSQLMap", "true");
			parms.Add("ScanIsDSXS", "true");
			parms.Add("ScanIsNessus", "true");
			parms.Add("ScanIsNexpose", "true");
			parms.Add("ScanIsMetasploit", "true");
			
			MakeScan(parms);
			
			Response.Redirect("/ThankYou.aspx");
		}
		
		private void MakeScan(Dictionary<string, string> opts)
		{
			string url = ConfigurationManager.AppSettings["API"] + "/CreateScan.ashx";
			url = url + "?WebUserID=" + this.CurrentUser.ID;
			url = url + "&UserID=" + ConfigurationManager.AppSettings["UserID"];
			url = url + "&ClientID=" + ConfigurationManager.AppSettings["ClientID"];

			foreach (KeyValuePair<string, string> kv in opts)
				url = url + "&" + kv.Key + "=" + kv.Value;
			
			WebRequest request = WebRequest.Create(url);

			string xml = string.Empty;
			using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))					
					xml = reader.ReadToEnd();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
		}
	}
}

