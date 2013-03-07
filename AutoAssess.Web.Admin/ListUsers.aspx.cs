
using System;
using System.Web;
using System.Web.UI;
using NHibernate;
using NHibernate.Criterion;
using AutoAssess.Web.Data;

namespace AutoAssess.Web.Admin
{


	public partial class ListUsers : System.Web.UI.Page //: AutoAssessPage
	{
		
		protected override void OnInit (EventArgs e)
		{
			base.OnInit(e);
			
			ISession s= this.Session["User?Session"] as ISession;
			
			var users = s.CreateCriteria<WebUser>()
				.Add(Restrictions.Eq("IsActive", true))
				.List<WebUser>();
			
			gvUsers.DataSource = users;
			gvUsers.AutoGenerateColumns = true;
			gvUsers.DataBind();
		
		}
	}
}

