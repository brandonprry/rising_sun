
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using AutoAssess.Web.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using System.Web.Security;

namespace AutoAssess.Web
{

	public partial class Login : AutoAssessPage
	{
		protected void btnLogin_Click (object sender, System.EventArgs e)
		{
			string hash = AutoAssess.Misc.Hashing.GetMd5Hash(txtPassword.Text, "sadf");

			WebUser user = this.CurrentWebSession.CreateCriteria<WebUser> ()
				.Add (Restrictions.Eq ("Username", txtUsername.Text))
				.Add (Restrictions.Eq ("PasswordHash", hash))
				.Add (Restrictions.Eq ("IsActive", true))
				.List<WebUser>()
				.FirstOrDefault();
			
			if (user == null)
			{
				lblLoginError.Text = "Invalid username/password combination.";
				txtUsername.Text = string.Empty;
				txtPassword.Text = string.Empty;
				return;
			}
			
			VerificationKey key = this.CurrentWebSession.CreateCriteria<VerificationKey>()
				.Add (Restrictions.Eq ("WebUserID", user.ID))
				.UniqueResult<VerificationKey>();
			
			if (!key.IsVerifed)
			{
				lblLoginError.Text = "Please check your email for an account verification link.";
				txtUsername.Text = string.Empty;
				txtPassword.Text = string.Empty;
				return;
			}
			
			WebUserInfo info = this.CurrentWebSession.CreateCriteria<WebUserInfo>()
				.Add(Restrictions.Eq("WebUserID", user.ID))
				.UniqueResult<WebUserInfo>();
			
			info.LastLogin = DateTime.Now;
			
			using (ITransaction x = this.CurrentWebSession.BeginTransaction())
			{
				this.CurrentWebSession.SaveOrUpdate(info);
				
				try{
					x.Commit();
				}
				catch(Exception ex)
				{
					x.Rollback();
					throw ex;
				}
			}
			
			Session["User"] = user;
			
			FormsAuthenticationTicket tkt = new FormsAuthenticationTicket (1, user.UserID.ToString(), DateTime.Now, DateTime.Now.AddMinutes (30), false, string.Empty /*Whatever data you want*/);
			string cookiestr = FormsAuthentication.Encrypt (tkt);
			HttpCookie ck = new HttpCookie (FormsAuthentication.FormsCookieName, cookiestr);
			
			ck.Path = FormsAuthentication.FormsCookiePath;
			Response.Cookies.Add (ck);
			
			Response.Redirect ("/Default.aspx", true);
			
			System.Security.Principal.GenericIdentity i = new System.Security.Principal.GenericIdentity(string.Empty, null);
			
			this.Context.User = new System.Security.Principal.GenericPrincipal(i, null);
			
		}
	}
}

