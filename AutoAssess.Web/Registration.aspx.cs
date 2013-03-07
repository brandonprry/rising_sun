using System;
using System.Web;
using System.Web.UI;
using NHibernate;
using AutoAssess.Web.Data;
using AutoAssess.Misc;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace AutoAssess.Web
{
	public partial class Registration : AutoAssessPage
	{
		protected void btnCreateUser_Click(object sender, EventArgs e)
		{	
			ISession s = this.CurrentWebSession;
			using (ITransaction t = s.BeginTransaction())
			{
				DateTime now = DateTime.Now;
				
				WebUser user = new WebUser();
				user.Username = txtUsername.Text;
				user.EmailAddress = txtEmailAddress.Text;
				user.UserID = Guid.NewGuid();
				user.IsActive = true;
				//user.CreatedBy = this.CurrentUser.UserID;
				//user.CreatedOn = DateTime.UtcNow;
				
				WebUserInfo info = new WebUserInfo();
				info.WebUser = user;
				info.FirstName = txtFirstName.Text;
				info.ID = Guid.NewGuid();
				info.LastName = txtLastName.Text;
				info.LastLogin = DateTime.Now;
				info.PrimaryPhone = txtPrimaryPhone.Text;
				info.SecondaryPhone = txtSecondaryPhone.Text;
				info.Hosts = int.Parse(ddlNumberOfHosts.SelectedValue);
				info.MainSecurityConcern = ddlMainConcern.SelectedValue;
				info.Provider = ddlProvider.SelectedValue;
				info.PrimaryWebsite = txtPrimaryWebsite.Text;
				info.IsActive = true;
				
				string hash = Hashing.GetMd5Hash(txtPassword.Text,  "sadf");
				
				user.PasswordHash = hash;
				
				VerificationKey vkey = new VerificationKey();
				vkey.ID = Guid.NewGuid();
				vkey.Key = Guid.NewGuid();
				vkey.IsActive = true;
				vkey.CreatedBy = Guid.Empty;
				vkey.CreatedOn = now;
				vkey.LastModifiedBy = Guid.Empty;
				vkey.LastModifiedOn = now;
				vkey.IsVerifed = false;
				vkey.IsSent = true; //sending below
				vkey.User = user;
				
				s.SaveOrUpdate(vkey);
				s.SaveOrUpdate(info);
				s.SaveOrUpdate(user);
				
				try
				{
					t.Commit();
				}
				catch (Exception ex)
				{
					t.Rollback();
					throw ex;
				}
				
				SendVerificationEmail(info.FirstName + " " + info.LastName, user.EmailAddress, user.ID.ToString(), vkey.Key.ToString());
					
				Response.Redirect("Login.aspx");
			}
		}
		
		private void SendVerificationEmail(string toName, string toEmail, string userID, string verify)
		{
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {return true;};
			
			var fromAddress = new MailAddress("noreply@volatileminds.net", "Registration Verification");
			var toAddress = new MailAddress(toEmail, toName);
			string fromPassword = ConfigurationManager.AppSettings["mailPassword"];
			string subject = "Verify your account on AutoAssess";
			string body = "http://127.0.0.1:8081/VerifyRegistration.aspx?verify=" + verify + "&user=" + userID;
			
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

