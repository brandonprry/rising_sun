using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.Nessus;
using AutoAssess.Data.Nessus.BusinessObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.PersistentObjects;
using NHibernate;
using NHibernate.Criterion;
using System.Configuration;

namespace AutoAssess.Web.API
{
	public class GetNessusPolicies : ApiHttpHandler
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
				.Add(Restrictions.Eq("ID", userID))
				.Add(Restrictions.Eq("IsActive", true))
				.UniqueResult<PersistentUser>();
		
			if (user == null || !user.HasAPIAccess)
				throw new Exception("no api access");
			
			if (!user.Client.HasAPIAccess)
				throw new Exception("no api access");
			
			using (NessusManagerSession session = new NessusManagerSession("https", "" + ConfigurationManager.AppSettings["nessusHost"] + "", 8834))
			{
				bool loggedIn = false;
				session.Authenticate("nessus", "nessus", 1234, out loggedIn);
				
				if (!loggedIn)
					throw new Exception("Not authed to nessus");
				
				NessusObjectManager manager = new NessusObjectManager(session);
				
				var policies = manager.GetPolicies();
				
				string xml = "<nessusPolicies>";
				
				foreach (NessusPolicy policy in policies)
					xml = xml + policy.ToBusinessXml();
				
				xml = xml + "</nessusPolicies>";
				 
				context.Response.Write(xml);
				
			}

		}
	}
}

