using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Web.Data;
using System.Net;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Text;
using AutoAssess.Data.PersistentObjects;
using System.Configuration;
using System.Web.Security;
using System.Net.Sockets;

namespace AutoAssess.Web
{
	public partial class MasterPage : System.Web.UI.MasterPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			if (!this.Context.Request.Path.Contains("Login.aspx") && !this.Context.Request.Path.Contains("Registration.aspx"))
			{	
				if (!this.Context.User.Identity.IsAuthenticated)
					Response.Redirect("/Login.aspx");
				
				if (Session["User"] == null)
				{
					FormsAuthentication.SignOut();
					Session.Abandon();
					Response.Redirect("/Login.aspx");
				}
				
				divContent.Attributes["onmouseover"] = "javascript:hideMenuStuff();";
				
				divRightMenu.InnerHtml = "Hello, <a href=\"/UserProfile.aspx\">" + (Session["User"] as WebUser).Username + "</a>!";
				
				HttpWebRequest request = WebRequest
					.Create(ConfigurationManager.AppSettings["API"] + "/GetProfiles.ashx" +
						"?WebUserID=" + (Session["User"] as WebUser).UserID.ToString() +
						"&UserID=" + ConfigurationManager.AppSettings["UserID"] +
						"&IsActive=" + true +
						"&ClientID=" + ConfigurationManager.AppSettings["ClientID"]) as HttpWebRequest;
				
				WebResponse response = request.GetResponse();
				
				XmlDocument doc = new XmlDocument();	
				string xml = string.Empty;
				using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
					xml = rdr.ReadToEnd();
				
				xml = xml.Replace("&", "&amp;");
				
				doc.LoadXml(xml);
				
				List<PersistentProfile> profiles = new List<PersistentProfile>();
				
				foreach (XmlNode child in doc.FirstChild.ChildNodes)
					if (child.Name == "profile")
						profiles.Add(new PersistentProfile(child));
				
				divMenuItems.InnerHtml = "<div class=\"menuItem\"><a href=\"/\">Home</a></div>";
				divMenuItems.InnerHtml += "<div class=\"menuItem\" onmouseover=\"showProfiles()\"><a href=\"/ViewProfiles.aspx\">Profiles</a></div>"; 
				divMenuItems.InnerHtml += "<div class=\"menuItem\"><a href=\"/Remediation.aspx\">Remediation</a></div>"; 
				
				string html = string.Empty; //I really dislike this.
				foreach (PersistentProfile profile in profiles)
					html += "<div class=\"profileMenuItem\"><a href=\"/ViewProfile.aspx?pid=" + profile.ID.ToString() + "\">" + profile.Name + "</a></div>"; //xss
				
				html += "<div class=\"profileMenuItem\"><a href=\"/CreateProfile.aspx\">Create new profile</a></div>";
				divProfiles.InnerHtml = html;
			}
		}
	}
}

