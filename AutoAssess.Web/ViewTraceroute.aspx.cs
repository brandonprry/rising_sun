using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Criterion;
using AutoAssess.Data;
using AutoAssess.Data.PersistentObjects;

namespace AutoAssess.Web
{
	public partial class ViewTraceroute : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			
			PersistentTracerouteResults results = this.CurrentScanSession.CreateCriteria<PersistentTracerouteResults>()
				.Add(Restrictions.Eq("NMapHostID", new Guid(Request["tid"])))
				.List<PersistentTracerouteResults>()
				.FirstOrDefault();
			
			if (results != null)
			{
				this.gvRoutes.DataSource = results.Routes;
				this.gvRoutes.DataBind();
			}
		}
		
		protected void gvRoutes_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			
		}
	}
}

