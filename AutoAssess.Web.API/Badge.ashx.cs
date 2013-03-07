using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.PersistentObjects;
using NHibernate;
using NHibernate.Criterion;

namespace AutoAssess.Web.API
{
	public class Badge : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			ISession s = this.CurrentSession;
			
			PersistentProfile profile = s.CreateCriteria<PersistentProfile>()
				.Add(Restrictions.Eq("ID", new Guid(context.Request["ProfileID"])))
				.Add(Restrictions.Eq("IsActive", true))
				.UniqueResult<PersistentProfile>();
			
			if (profile == null)
				return;
		
			string badge = string.Empty;
			
			switch (profile.BadgeState)
			{
			case "Excellent":
				badge = GetExcellentBadge();
				break;
			case "Good":
				badge = GetGetGoodBadge();
				break;
			case "Average":
				badge = GetAverageBadge();
				break;
			case "Below Average":
				badge = GetBelowAverageBadge();
				break;
			case "Poor":
				badge = GetPoorBadge();
				break;
			default:
				break;
			}
			
			context.Response.Write(badge);
		}

		public string GetExcellentBadge ()
		{
			throw new NotImplementedException ();
		}

		public string GetGetGoodBadge ()
		{
			throw new NotImplementedException ();
		}

		public string GetAverageBadge ()
		{
			throw new NotImplementedException ();
		}

		public string GetBelowAverageBadge ()
		{
			throw new NotImplementedException ();
		}

		public string GetPoorBadge ()
		{
			throw new NotImplementedException ();
		}
	}
}

