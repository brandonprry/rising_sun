using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using NHibernate;
using AutoAssess.Data.PersistentObjects;
using NHibernate.Criterion;
using System.Linq;

namespace AutoAssess.Web.API
{
	public class GetOpenVASReport : ApiHttpHandler
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
			
			bool isActive = bool.Parse(context.Request["IsActive"]);
			Guid scanID = new Guid(context.Request["ScanID"]);
			
			PersistentOpenVASTask task = sess.CreateCriteria<PersistentOpenVASTask>()
				.Add(Restrictions.Eq("ScanID", scanID))
				.List<PersistentOpenVASTask>()
				.FirstOrDefault(); //in case IsActive is false and more than one scan related to profile is inactive.
			
			if (task == null)
				throw new Exception("A scan with the conditions asked for doesn't exist.");
			
			context.Response.Write(task.FullReport);
		}
	}
}

