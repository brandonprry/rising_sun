using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;

namespace AutoAssess.Web
{
	public partial class Logout : System.Web.UI.Page
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			FormsAuthentication.SignOut();
			Session.Abandon();
			Response.Redirect("/Login.aspx");
		}
	}
}

