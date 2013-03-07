using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using NHibernate;
using NHibernate.Criterion;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using AutoAssess.Data.PersistentObjects;

namespace AutoAssess.Web.API
{
	public class GetProfiles : ApiHttpHandler
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
			Guid webUserID = new Guid(context.Request["WebUserID"]);
			
			IList<PersistentProfile> profiles = sess.CreateCriteria<PersistentProfile>()
				.Add(Restrictions.Eq("WebUserID", webUserID))
				.Add(Restrictions.Eq("IsActive", isActive))
				.List<PersistentProfile>();
			
			string xml = string.Empty;
			
			xml = xml + "<profiles>";
			foreach (PersistentProfile profile in profiles)
				xml = xml + profile.ToPersistentXml();
			xml = xml + "</profiles>";
			
			context.Response.Write(xml);
		}
	}
}

