using System;
using System.Web;
using System.Web.UI;

namespace AutoAssess.Web.Admin.Admin
{
	public partial class Default : System.Web.UI.Page
	{
		
		public virtual void button1Clicked (object sender, EventArgs args)
		{
			button1.Text = "You clicked me";
		}
	}
}

