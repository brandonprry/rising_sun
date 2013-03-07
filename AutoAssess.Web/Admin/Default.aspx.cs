using System;
using System.Web;
using System.Web.UI;

namespace AutoAssess.Web
{
	public partial class Default2 : AutoAssessAdminPage
	{
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			
			if (this.CurrentUser.UserLevel <= 2) //manager
			{
				AddManagerLinks();
			}
			if (this.CurrentUser.UserLevel <= 1) //account admin
			{
				AddAccountAdminLinks();
			}
			if (this.CurrentUser.UserLevel == 0) //super admin, w00t! bow down minions
			{
				AddSuperAdminLinks();
			}
		}

		protected void AddManagerLinks ()
		{
			//do nothing for now
		}

		protected void AddAccountAdminLinks ()
		{
			//do nothing for now
		}

		protected void AddSuperAdminLinks ()
		{
			//give me all the things
			
		}
	}
}

