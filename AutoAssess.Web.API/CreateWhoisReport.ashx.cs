using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.BusinessObjects;
using System.Configuration;
using NHibernate;
using AutoAssess.Data.PersistentObjects;

namespace AutoAssess.Web.API
{
	public class CreateWhoisReport : ApiHttpHandler
	{
		
		public bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			ISession s = this.CurrentSession;
			
			using (ITransaction t = s.BeginTransaction())
			{
				Guid userID = new Guid(context.Request["UserID"]);
				Guid clientID = new Guid(context.Request["ClientID"]);
				
				PersistentUser user = s.Get<PersistentUser>(userID);
				PersistentClient client = s.Get<PersistentClient>(clientID);
				
				if (user == null || !user.HasAPIAccess)
					throw new Exception("no api access");
				
				if (client == null || !client.HasAPIAccess)
					throw new Exception("no api access");
			}
			
			IToolOptions options = new WhoisToolOptions();
			
			(options as WhoisToolOptions).Host = context.Request["Host"];
			(options as WhoisToolOptions).Path = ConfigurationManager.AppSettings["whoisPath"];
			
			Whois whois = new Whois(options as IToolOptions);
			
			WhoisToolResults results = whois.Run() as WhoisToolResults;
			
			context.Response.Write(results.FullOutput);
		}
	}
}

