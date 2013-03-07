using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Web.Data;
using NHibernate.Criterion;

namespace AutoAssess.Web
{
	public partial class UserProfile : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			WebUserInfo info = this.CurrentWebSession.CreateCriteria<WebUserInfo>()
				.Add(Restrictions.Eq("WebUserID", (Session["User"] as WebUser).ID))
				.UniqueResult<WebUserInfo>();
			
			if (info == null)
				throw new Exception("Info null");
			
			lblEmailAddress.Text = (Session["User"] as WebUser).EmailAddress;
			lblFirstName.Text = info.FirstName;
			lblLastName.Text = info.LastName;
			lblPrimaryPhone.Text = info.PrimaryPhone;
			lblSecondaryPhone.Text = info.SecondaryPhone;
		}
	}
}

