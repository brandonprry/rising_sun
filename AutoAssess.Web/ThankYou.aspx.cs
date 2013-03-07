using System;
using System.Web;
using System.Web.UI;

namespace AutoAssess.Web
{
	public partial class ThankYou : System.Web.UI.Page
	{
		protected void btnGoHome_Click(object sender, EventArgs e)
		{
			Response.Redirect("/Default.aspx");
		}
	}
}

