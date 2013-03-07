using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.PersistentObjects;
using NHibernate;

namespace AutoAssess.Web.API
{
	public class GetProfileHosts : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			Guid userID = new Guid(context.Request["UserID"]);
			Guid clientID = new Guid(context.Request["ClientID"]);
			
			ISession s = this.CurrentSession;
			
			PersistentUser user = s.CreateCriteria<PersistentUser>()
				.Add(NHibernate.Criterion.Restrictions.Eq("ID", userID))
				.Add(NHibernate.Criterion.Restrictions.Eq("IsActive", true))
				.UniqueResult<PersistentUser>();
		
			if (user == null || !user.HasAPIAccess)
				throw new Exception("no api access");
			
			if (!user.Client.HasAPIAccess)
				throw new Exception("no api access");
			
			string xml = "<profileHosts>";
			
			IList<PersistentProfileHost> hosts = s.CreateCriteria<PersistentProfileHost>()
				.Add(NHibernate.Criterion.Restrictions.Eq("ParentProfileID", new Guid(context.Request["ProfileID"])))
				.List<PersistentProfileHost>();
			
			foreach (var host in hosts)
				xml += host.ToPersistentXML(true);
			
			xml += "</profileHosts>";
			context.Response.Write(xml);
		}
	}
}

