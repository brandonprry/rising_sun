using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

using AutoAssess.Data;
using NHibernate.Criterion;

namespace AutoAssess.Web
{
	public partial class CompareProfiles : AutoAssessPage
	{
		
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
//			
//			Guid firstProfile = new Guid(Request["p1"]);
//			Guid secondProfile = new Guid(Request["p2"]);
//			
//			NMapToolResults firstResults = this.CurrentScanSession.CreateCriteria<NMapToolResults>()
//				.Add(Restrictions.Eq("NMapResultsID", firstProfile))
//				.UniqueResult<NMapToolResults>();
//			
//			NMapToolResults secondResults = this.CurrentScanSession.CreateCriteria<NMapToolResults>()
//				.Add(Restrictions.Eq("NMapResultsID", secondProfile))
//				.UniqueResult<NMapToolResults>();
//				
//			List<NMapHost> firstHosts = new List<NMapHost>();
//			List<NMapHost> secondHosts = new List<NMapHost>();
//			
//			//firstResults.Hosts.CopyTo(firstHosts, 0);
//			//secondResults.Hosts.CopyTo(secondHosts,0);
//			
//			foreach (var host1 in firstResults.Hosts)
//			{
//				foreach (var host2 in secondResults.Hosts)
//				{
//					
//				}
//			}
		}
	}
}

