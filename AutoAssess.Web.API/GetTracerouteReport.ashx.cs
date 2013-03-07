using System;
using System.Web;
using System.Web.UI;
using NHibernate;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Web.API
{
	public class GetTracerouteReport : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{			
			ISession sess = this.CurrentSession; 
			
			PersistentUser user = sess.Get<PersistentUser>(new Guid(context.Request["UserID"]));
			PersistentClient client = sess.Get<PersistentClient>(new Guid(context.Request["ClientID"]));
			
			if (user == null || !user.HasAPIAccess)
				throw new Exception("no api access");
			
			if (client == null || !client.HasAPIAccess)
				throw new Exception("no api access");
			
			Guid tid = new Guid(context.Request["TracerouteID"]);
			
//			TracerouteToolResults traceroute = this.CurrentScanSession.CreateCriteria<TracerouteToolResults>()
//				.Add(Restrictions.Eq("NMapHostID", tid))
//				.Add(Restrictions.Eq("ProfileID", this.CurrentProfile.ProfileID))
//				.UniqueResult<TracerouteToolResults>();
			
		}
	}
}

