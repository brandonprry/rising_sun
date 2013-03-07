using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Web.Data;
using NHibernate;
using NHibernate.Criterion;

namespace AutoAssess.Web
{
	public partial class VerifyRegistration : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			if (!string.IsNullOrEmpty(Request["verify"]) && !string.IsNullOrEmpty(Request["user"]))
			{
				VerificationKey key = this.CurrentWebSession.CreateCriteria<VerificationKey>()
					.Add(Restrictions.Eq("Key", new Guid(Request["verify"])))
					.Add(Restrictions.Eq("WebUserID", new Guid(Request["user"])))
					.UniqueResult<VerificationKey>();
				
				if (key == null)
					Response.Redirect("/Login.aspx");
				
				key.IsVerifed = true;
				
				using (ITransaction x = this.CurrentWebSession.BeginTransaction())
				{
					this.CurrentWebSession.SaveOrUpdate(key);
					
					try
					{
						x.Commit();
					}
					catch (Exception ex)
					{
						x.Rollback();
						throw ex;
					}
				}
				
				lblVerifyText.ForeColor = System.Drawing.Color.DarkGreen;
				lblVerifyText.Text = "Account verified! You may now <a href=\"/Login.aspx\">login</a>.";
				
			}
			else 
				Response.Redirect("/Login.aspx");
		}
	}
}

