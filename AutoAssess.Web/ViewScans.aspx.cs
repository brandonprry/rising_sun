using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AutoAssess.Data.PersistentObjects;
using NHibernate;
using NHibernate.Criterion;

namespace AutoAssess.Web
{
	public partial class ViewScans : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			

		}
		
		protected void gvScans_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Button btn = e.Row.FindControl("btnViewScan") as Button;
				
				btn.CommandArgument = (e.Row.DataItem as PersistentScan).ID.ToString();
				
				//if (!(e.Row.DataItem as Scan).HasRun)
					//btn.Enabled = false;
			}
		}
		
		protected void btnViewScan_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			
			Response.Redirect("/ViewScan.aspx?sid=" + btn.CommandArgument);
		}
	}
}

