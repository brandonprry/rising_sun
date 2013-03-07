using System;
using System.Configuration;
using System.Collections.Generic;
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

namespace AutoAssess.Web
{
	public partial class ViewProfiles : AutoAssessPage
	{
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			
			HttpWebRequest request = WebRequest
				.Create(ConfigurationManager.AppSettings["API"] + "/GetProfiles.ashx" +
					"?WebUserID=" + this.CurrentUser.UserID.ToString() +
					"&UserID=" + ConfigurationManager.AppSettings["UserID"] +
					"&IsActive=" + true +
					"&ClientID=" + ConfigurationManager.AppSettings["ClientID"]) as HttpWebRequest;
			
			WebResponse response = request.GetResponse();
			

			
			XmlDocument doc = new XmlDocument();	
			string xml = string.Empty;
			using (Stream stream = response.GetResponseStream())
			{
				byte[] buff = new byte[2048];
				int bytes = 0;
				do
				{
					bytes = stream.Read(buff, 0, buff.Length);
					
					xml = xml + ASCIIEncoding.ASCII.GetString(buff);
					buff = new byte[2048];
				} while (bytes > 0);
			}
			
			xml = xml.Replace("&", "&amp;");
			
			doc.LoadXml(xml);
			
			List<PersistentProfile> profiles = new List<PersistentProfile>();
			
			foreach (XmlNode child in doc.FirstChild.ChildNodes)
				if (child.Name == "profile")
					profiles.Add(new PersistentProfile(child));
			
			gvProfiles.DataSource = profiles;
			gvProfiles.DataBind();
		}
		
		protected void gvProfiles_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.DataItem is PersistentProfile)
			{
				Button view = e.Row.FindControl("lnkViewProfile") as Button;
				Button newRun = e.Row.FindControl("btnScheduleNewRun") as Button;
				Button delProfile = e.Row.FindControl("btnDeleteProfile") as Button;
				Button createScan = e.Row.FindControl("btnCreateScanFromProfile") as Button;
				
				createScan.CommandArgument =  (e.Row.DataItem as PersistentProfile).ID.ToString();
				
				if ((e.Row.DataItem as PersistentProfile).HasRun == false)
				{
					newRun.Text = "Profile scheduled for new run";
					newRun.Enabled = false;
				}
				
				newRun.CommandArgument = (e.Row.DataItem as PersistentProfile).ID.ToString();
				
				delProfile.CommandArgument = (e.Row.DataItem as PersistentProfile).ID.ToString();
				
				if ((e.Row.DataItem as PersistentProfile).CurrentResults != null)
				{
					view.CommandArgument = (e.Row.DataItem as PersistentProfile).ID.ToString();
					view.Text = "View profile: " + (e.Row.DataItem as PersistentProfile).Name;
					
					
					
				}
				else
				{
					view.Text = "Profile hasn't been created yet.";
					view.Enabled = false;
					
				}
			}
		}
		
		protected void btnScheduleNewRun_Click(object sender, EventArgs e)
		{
		}
		
		protected void lnkViewProfile_Click(object sender, EventArgs e)
		{
			Response.Redirect("/ViewProfile.aspx?pid=" + (sender as Button).CommandArgument);
			
		}
		
		protected void btnCreateScanFromProfile_Click(object sender, EventArgs e)
		{
			Button l = sender as Button;
			
			HttpWebRequest request = WebRequest
				.Create(ConfigurationManager.AppSettings["API"] + "/GetProfile.ashx" +
					"?WebUserID=" + this.CurrentUser.UserID.ToString() +
					"&UserID=" + ConfigurationManager.AppSettings["UserID"] +
					"&IsActive=" + true +
					"&ProfileID=" + l.CommandArgument +
					"&ClientID=" + ConfigurationManager.AppSettings["ClientID"]) as HttpWebRequest;
		
			WebResponse response = request.GetResponse();
			
			XmlDocument doc = new XmlDocument();	
			string xml = string.Empty;
			byte[] buff = new byte[2048];
			int bytes = 0;
			using (Stream stream = response.GetResponseStream())
			{
				do
				{
					bytes = stream.Read(buff, 0, buff.Length);
					
					xml = xml + ASCIIEncoding.ASCII.GetString(buff);
					
					buff = new byte[2048]; //clear cruft
				} while (bytes > 0);
			}
			
			doc.LoadXml(xml);
			
			PersistentProfile profile = new PersistentProfile(doc.DocumentElement);
			
			this.CurrentProfile = profile;
			
			Response.Redirect("/CreateScan.aspx");
		}
		
		protected void btnDeleteProfile_Click(object sender, EventArgs e)
		{
		}
	}
}

