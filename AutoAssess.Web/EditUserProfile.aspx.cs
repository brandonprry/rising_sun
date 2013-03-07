using System;
using System.Web;
using System.Web.UI;
using NHibernate;
using AutoAssess.Web.Data;
using NHibernate.Criterion;

namespace AutoAssess.Web
{
	public partial class EditUserProfile : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			WebUserInfo info = this.CurrentWebSession.CreateCriteria<WebUserInfo>()
				.Add(Restrictions.Eq("WebUserID", (Session["User"] as WebUser).ID))
				.UniqueResult<WebUserInfo>();
			
			txtEmailAddress.Text = (Session["User"] as WebUser).EmailAddress;
			txtFirstName.Text = info.FirstName;
			txtPrimaryPhone.Text = info.PrimaryPhone;
			txtSecondaryPhone.Text = info.SecondaryPhone;
			txtLastName.Text = info.LastName;
		}
		
		protected void btnEditUser_Click(object sender, EventArgs e)
		{
			ISession s = this.CurrentWebSession;
			using (ITransaction x = s.BeginTransaction())
			{
				WebUserInfo info = this.CurrentWebSession.CreateCriteria<WebUserInfo>()
					.Add(Restrictions.Eq("WebUserID", (Session["User"] as WebUser).ID))
					.UniqueResult<WebUserInfo>();
				
				if (info == null)
					throw new Exception("User info null");
				
				info.FirstName = txtFirstName.Text;
				info.LastName = txtLastName.Text;
				info.PrimaryPhone = txtPrimaryPhone.Text;
				info.SecondaryPhone = txtSecondaryPhone.Text;
				
				s.SaveOrUpdate(info);
				
				try { 
					x.Commit();
				}
				catch (Exception ex)
				{
					x.Rollback();
					throw ex;
				}
			}
			
			Response.Redirect("/UserProfile.aspx");
		}
	}
}

