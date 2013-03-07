using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.IO;
using System.Xml;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.BusinessObjects;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace AutoAssess.Web
{
	public partial class AddHostToProfile : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			lblDomain.Text = "." + this.CurrentProfile.Domain;
		}
		
		protected void btnStartVerification_Click (object sender, EventArgs e)
		{
			btnStartVerification.Enabled = false;
			divCreateProfileExplanation.Attributes ["class"] = "createProfileExplanationInactive";
						
			divAddHostContainer.Attributes ["class"] = "addHostContainerActive";
			txtHostURL.Enabled = true;
			btnAddHost.Enabled = true;
		}
		
		protected void btnCreateProfile_Click (object sender, EventArgs e)
		{
			string url = ConfigurationManager.AppSettings ["API"] + "/AddHostToProfile.ashx" +
				"?WebUserID=" + this.CurrentUser.ID.ToString () +
				"&UserID=" + ConfigurationManager.AppSettings ["UserID"] + 
				"&ClientID=" + ConfigurationManager.AppSettings ["ClientID"] + 
				"&HostSubDomain=" + Session ["CreateProfile?CurrentHost"] + 
				"&ProfileID=" + this.CurrentProfile.ID.ToString();
			
			WebRequest request = WebRequest.Create (url);
			
			request.GetResponse();
			
			Response.Redirect ("/ViewProfile.aspx?pid=" + this.CurrentProfile.ID.ToString());
		}
		
		protected void btnVerifyEmailKey_Click (object sender, EventArgs e)
		{
			if (txtEmailKey.Text == Session ["CreateProfile?CurrentKey"] as string) {
				divMultiEmailContainer.Attributes ["class"] = "multiEmailContainerInactive";
				txtEmailKey.Enabled = false;
				btnSendVerification.Enabled = false;
				btnVerifyEmailKey.Enabled = false;
				
				btnVerifyFile.Enabled = true;
				divVerificationFileContainer.Attributes ["class"] = "verificationFileContainerActive";
				
			} else {
				
			}
		}
		
		protected void btnSendVerification_Click (object sender, EventArgs e)
		{
			Session ["CreateProfile?CurrentKey"] = Guid.NewGuid ().ToString ();
			this.SendVerificationEmail (ddlWhoisEmail.SelectedValue);
			
			txtEmailKey.Enabled = true;
			btnVerifyEmailKey.Enabled = true;
		}
		
		protected void btnVerifyFile_Click (object sender, EventArgs e)
		{
			string url = "http://" + Session ["CreateProfile?CurrentHost"] + "." + this.CurrentProfile.Domain + ":80/" + (Session ["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationFileName;
			
			WebRequest request = WebRequest.Create (url);
			string response = string.Empty;
			
			using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))					
				response = reader.ReadToEnd ();
			
			Guid key = new Guid (Convert.FromBase64String (response.Replace ("\n", string.Empty)));
			Guid ck = new Guid (Convert.FromBase64String ((Session ["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationData));
			if (ck == key) {
				divVerificationFileContainer.Attributes ["class"] = "verificationFileContainerInactive";
				btnVerifyFile.Enabled = false;
				
				divFinishUpContainer.Attributes ["class"] = "finishUpContainerActive";
				btnCreateProfile.Enabled = true;
			} else {
			}
		}
		
		protected void btnAddHost_Click (object sender, EventArgs e)
		{
			Session ["CreateProfile?CurrentHost"] = txtHostURL.Text;
			string url = ConfigurationManager.AppSettings ["API"] + "/CreateWhoisReport.ashx?Host=" + this.CurrentProfile.Domain +
				"&WebUserID=" + this.CurrentUser.ID.ToString () +
				"&UserID=" + ConfigurationManager.AppSettings ["UserID"] + 
				"&ClientID=" + ConfigurationManager.AppSettings ["ClientID"];
			
			WebRequest request = WebRequest.Create (url);
			string response = string.Empty;
			
			using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))					
				response = reader.ReadToEnd ();
			
			Session ["CreateProfile?CurrentProfileHost"] = new PersistentProfileHost (this.CurrentUser.UserID);
			Session ["CreateProfile?CurrentVerification"] = new PersistentProfileHostVerification (new ProfileHostVerification (), this.CurrentUser.UserID);
			
			(Session ["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).ProfileHost = Session ["CreateProfile?CurrentProfileHost"] as PersistentProfileHost;
			
			List<string> emails = FindEmailAddresses (response).ToList();
			
			if (emails.Count == 0)
				throw new Exception ("Cannot verify whois with an email address.");
			
			ddlWhoisEmail.DataSource = emails;
			ddlWhoisEmail.DataBind ();
			
			lblFileContents.Text = (Session ["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationData;
			lblFilename.Text = (Session ["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationFileName;
			lblVerificationURL.Text = "http://" + Session ["CreateProfile?CurrentHost"] + ":80/" + (Session ["CreateProfile?CurrentVerification"] as PersistentProfileHostVerification).VerificationFileName;
			
			divAddHostContainer.Attributes ["class"] = "addHostContainerInactive";
			txtHostURL.Enabled = false;
			btnAddHost.Enabled = false;
			
			divMultiEmailContainer.Attributes ["class"] = "multiEmailContainerActive";
			ddlWhoisEmail.Enabled = true;
			btnSendVerification.Enabled = true;
		}
		
		private IEnumerable<string> FindEmailAddresses (string response)
		{
			Regex regex = new Regex (@"[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})");
			List<string> emails = new List<string> ();
			
			foreach (Match match in regex.Matches(response))
				emails.Add (match.Value);
			
			return emails.Distinct ();
		}
		
		private void SendVerificationEmail (string toEmail)
		{
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {
				return true;};
			
			var fromAddress = new MailAddress ("noreply@volatileminds.net", "Asset Verification");
			var toAddress = new MailAddress (toEmail);
			string fromPassword = ConfigurationManager.AppSettings["mailPassword"];
			string subject = "Verifying your ownership of " + Session ["CreateProfile?CurrentHost"];
			string body = Session ["CreateProfile?CurrentKey"] as string;
			
			var smtp = new SmtpClient
			           {
			               Host = "smtp.gmail.com",
			               Port = 587,
			               EnableSsl = true,
			               DeliveryMethod = SmtpDeliveryMethod.Network,
			               UseDefaultCredentials = false,
			               Credentials = new NetworkCredential (ConfigurationManager.AppSettings["mainUser"], fromPassword)
			           };
			
			using (var message = new MailMessage(fromAddress, toAddress)
			                     {
			                         Subject = subject,
			                         Body = body
			                     })
				smtp.Send (message);
			
		}
	}
}