using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using NHibernate;
using NHibernate.Criterion;
using AutoAssess.Data.PersistentObjects;

namespace AutoAssess.Web.API
{
	public class GetScan : ApiHttpHandler
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
			Guid profileID = new Guid(context.Request["ProfileID"]);
			
			PersistentScan scan = sess.CreateCriteria<PersistentScan>()
				.Add(Restrictions.Eq("ParentProfileID", profileID))
				.Add(Restrictions.Eq("IsActive", isActive))
				.SetFetchMode("ScanOptions", FetchMode.Eager)
				.List<PersistentScan>()
				.LastOrDefault(); //in case IsActive is false and more than one scan related to profile is inactive.
			
			if (scan == null)
				throw new Exception("A scan with the conditions asked for doesn't exist.");
			
			string xml = scan.ToPersistentXml();
			
			context.Response.Write(xml);
		}
	}
}

