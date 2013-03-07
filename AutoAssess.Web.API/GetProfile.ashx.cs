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
	public class GetProfile : ApiHttpHandler
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
			
			PersistentProfile profile = sess.Get<PersistentProfile>(profileID);
			string xml = profile.ToPersistentXml();
			
			context.Response.Write(xml);
		}
	}
}

