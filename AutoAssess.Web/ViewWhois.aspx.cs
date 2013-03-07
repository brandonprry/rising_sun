using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using AutoAssess.Data;
using NHibernate.Criterion;
using AutoAssess.Data.PersistentObjects;

namespace AutoAssess.Web
{
	public partial class ViewWhois : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			Guid hid = new Guid(Request["hid"]);
			
			PersistentWhoisResults results = this.CurrentScanSession.CreateCriteria<PersistentWhoisResults>()
				.Add(Restrictions.Eq("NMapHostID", hid))
				.List<PersistentWhoisResults>()
				.FirstOrDefault();
			
			if (results != null)
				Response.Write(results.FullOutput.Replace("\n", "<br />"));
		}
	}
}

