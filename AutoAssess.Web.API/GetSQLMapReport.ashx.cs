using System;
using System.Web;
using System.Web.UI;
using NHibernate;
using NHibernate.Criterion;
using AutoAssess.Data.PersistentObjects;

namespace AutoAssess.Web.API
{
	public class GetSQLMapReport : ApiHttpHandler
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
			
			Guid hostPortID = new Guid(context.Request["HostPortID"]);
			bool isActive = Boolean.Parse(context.Request["IsActive"]);
			
			PersistentSQLMapResults sqlMapResults = sess.CreateCriteria<PersistentSQLMapResults>()
				.Add(Restrictions.Eq("ParentHostPortID", hostPortID))
				.Add(Restrictions.Eq("IsActive", isActive))
				.UniqueResult<PersistentSQLMapResults>();
			
			if (sqlMapResults == null)
				throw new Exception("No record found with those restrictions.");
			
			string xml = sqlMapResults.ToPersistentXml();
			
			context.Response.Write(xml);
		}
	}
}

