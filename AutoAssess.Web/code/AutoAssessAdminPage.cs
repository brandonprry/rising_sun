using System;

namespace AutoAssess.Web
{
	public class AutoAssessAdminPage : AutoAssessPage
	{
		public AutoAssessAdminPage ()
		{
		}
		
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			
			if (this.CurrentUser.UserLevel == 3)
				Response.Redirect("/Default.aspx");
		}
	}
}

