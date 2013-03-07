using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Xml;
using AutoAssess.Data.PersistentObjects;
using NHibernate;
using System.Text.RegularExpressions;
using AutoAssess.Data.BusinessObjects;
using System.Net.Mail;

namespace AutoAssess.Web
{
	public partial class CreateProfile : AutoAssessPage
	{	
		protected void btnStartVerification_Click(object sender, EventArgs e)
		{
			btnStartVerification.Enabled = false;
			divCreateProfileExplanation.Attributes["class"] = "createProfileExplanationInactive";
			
			divProfileDetails.Attributes["class"] = "profileDetailsActive";
			txtProfileName.Enabled = true;
			btnSetProfileDetails.Enabled = true;
			rblProfileSchedule.Enabled = true;
			txtProfileDescription.Enabled = true;
		}
		
		protected void btnCreateProfile_Click(object sender, EventArgs e)
		{
			string url = ConfigurationManager.AppSettings["API"] + "/CreateProfile.ashx" +
				"?WebUserID=" + this.CurrentUser.ID.ToString() +
				"&UserID=" + ConfigurationManager.AppSettings["UserID"] + 
				"&ClientID=" + ConfigurationManager.AppSettings["ClientID"] + 
				"&ProfileDomain=" + Session["CreateProfile?CurrentHost"] + 
				"&ProfileSchedule=" + Session["CreateProfile?ProfileSchedule"] +
				"&ProfileDescription=" + Session["CreateProfile?ProfileDescription"] +  
				"&ProfileName=" + Session["CreateProfile?ProfileName"];
			
			WebRequest request = WebRequest.Create(url);
			
			string xml = string.Empty;
			
			using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))					
					xml = reader.ReadToEnd();
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			
			PersistentProfile profile = new PersistentProfile(doc.FirstChild);
			
			this.CurrentProfile = profile;
			
			Response.Redirect("/ViewProfile.aspx?pid=" + profile.ID.ToString());
		}
		
		protected void btnSetProfileDetails_Click(object sender, EventArgs e)
		{
			Session["CreateProfile?ProfileName"] = txtProfileName.Text;
			Session["CreateProfile?ProfileDescription"] = txtProfileDescription.Text;
			Session["CreateProfile?ProfileSchedule"] = rblProfileSchedule.SelectedValue;
			
			divProfileDetails.Attributes["class"] = "profileDetailsInactive";
			txtProfileName.Enabled = false;
			btnSetProfileDetails.Enabled = false;
			txtProfileDescription.Enabled = false;
			rblProfileSchedule.Enabled = false;
			
			divAddHostContainer.Attributes["class"] = "addHostContainerActive";
			txtHostURL.Enabled = true;
			btnAddHost.Enabled = true;
		}
		
		protected void btnVerifyEmailKey_Click(object sender, EventArgs e)
		{
			if (txtEmailKey.Text == Session["CreateProfile?CurrentKey"] as string)
			{
				divMultiEmailContainer.Attributes["class"] = "multiEmailContainerInactive";
				txtEmailKey.Enabled = false;
				btnSendVerification.Enabled = false;
				btnVerifyEmailKey.Enabled = false;
				
				btnVerifyFile.Enabled = true;
				divVerificationFileContainer.Attributes["class"] = "verificationFileContainerActive";
				
			}
			else
			{
				
			}
		}
		
		protected void btnSendVerification_Click(object sender, EventArgs e)
		{
			Session["CreateProfile?CurrentKey"] = Guid.NewGuid().ToString();
			this.SendVerificationEmail(ddlWhoisEmail.SelectedValue);
			
			txtEmailKey.Enabled = true;
			btnVerifyEmailKey.Enabled = true;
		}
		
		protected void btnVerifyFile_Click(object sender, EventArgs e)
		{
			string url = "http://" + Session["CreateProfile?CurrentHost"] + ":80/" + (Session["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationFileName;
			
			WebRequest request = WebRequest.Create(url);
			string response = string.Empty;
			
			using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))					
					response = reader.ReadToEnd();
			
			Guid key = new Guid(Convert.FromBase64String(response.Replace("\n", string.Empty)));
			Guid ck = new Guid(Convert.FromBase64String((Session["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationData));
			if (ck == key)
			{
				divVerificationFileContainer.Attributes["class"] = "verificationFileContainerInactive";
				btnVerifyFile.Enabled = false;
				
				divFinishUpContainer.Attributes["class"] = "finishUpContainerActive";
				btnCreateProfile.Enabled = true;
			}
			else
			{
			}
		}
		
		protected void btnAddHost_Click(object sender, EventArgs e)
		{
			Session["CreateProfile?CurrentHost"] = txtHostURL.Text;
			string url = ConfigurationManager.AppSettings["API"] + "/CreateWhoisReport.ashx?Host=" + Session["CreateProfile?CurrentHost"] +
				"&WebUserID=" + this.CurrentUser.ID.ToString() +
				"&UserID=" + ConfigurationManager.AppSettings["UserID"] + 
				"&ClientID=" + ConfigurationManager.AppSettings["ClientID"];
			
			WebRequest request = WebRequest.Create(url);
			string response = string.Empty;
			
			using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))					
					response = reader.ReadToEnd();
			
			Session["CreateProfile?CurrentProfileHost"] = new PersistentProfileHost(this.CurrentUser.UserID);
			Session["CreateProfile?CurrentVerification"] = new PersistentProfileHostVerification(new ProfileHostVerification(), this.CurrentUser.UserID);
			
			(Session["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).ProfileHost = Session["CreateProfile?CurrentProfileHost"] as PersistentProfileHost;
			
			List<string> emails = FindEmailAddresses(response).ToList();
			
			if (emails.Count == 0)
				throw new Exception("Cannot verify whois with an email address.");
			
			ddlWhoisEmail.DataSource = emails;
			ddlWhoisEmail.DataBind();
			
			lblFileContents.Text = (Session["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationData;
			lblFilename.Text = (Session["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationFileName;
			lblVerificationURL.Text = "http://" + Session["CreateProfile?CurrentHost"] + ":80/" + (Session["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationFileName;
			
			divAddHostContainer.Attributes["class"] = "addHostContainerInactive";
			txtHostURL.Enabled = false;
			btnAddHost.Enabled = false;
			
			divMultiEmailContainer.Attributes["class"] = "multiEmailContainerActive";
			ddlWhoisEmail.Enabled = true;
			btnSendVerification.Enabled = true;
		}
		
		private IEnumerable<string> FindEmailAddresses(string response)
		{
			Regex regex = new Regex(@"[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})");
			List<string> emails = new List<string>();
			
			foreach (Match match in regex.Matches(response))
				emails.Add(match.Value);
			
			return emails.Distinct();
		}
		
		private void SendVerificationEmail(string toEmail)
		{
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {return true;};
			
			var fromAddress = new MailAddress("noreply@volatileminds.net", "Asset Verification");
			var toAddress = new MailAddress(toEmail);
			string fromPassword = ConfigurationManager.AppSettings["mailPassword"];
			string subject = "Verifying your ownership of " + Session["CreateProfile?CurrentHost"];
			string body = Session["CreateProfile?CurrentKey"] as string;
			
			var smtp = new SmtpClient
			           {
			               Host = "smtp.gmail.com",
			               Port = 587,
			               EnableSsl = true,
			               DeliveryMethod = SmtpDeliveryMethod.Network,
			               UseDefaultCredentials = false,
			               Credentials = new NetworkCredential(ConfigurationManager.AppSettings["mailUser"], fromPassword)
			           };
			
			using (var message = new MailMessage(fromAddress, toAddress)
			                     {
			                         Subject = subject,
			                         Body = body
			                     })
			    smtp.Send(message);
			
		}
	}
}