using System;
using System.Web;
using System.Web.UI;

namespace AutoAssess.Web
{
	public partial class Impersonate : AutoAssessAdminPage
	{
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			
			if (this.CurrentUser.UserLevel != 0)
				Response.Redirect("/Default.aspx");
		}
	}
}

