using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.Nessus;
using AutoAssess.Data.Nessus.BusinessObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using NHibernate;

namespace AutoAssess.Web.API
{
	public class GetNessusReport : ApiHttpHandler
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
			
			string readableName = context.Request["ReportReadableName"];
			
			if (string.IsNullOrEmpty(readableName))
				throw new Exception("no readable name");
			
			NessusManagerSession nessusSession = new NessusManagerSession ("192.168.56.101");
						
			bool loggedIn = false;
			nessusSession.Authenticate ("nessus", "nessus", 1234, out loggedIn);
					
			if (!loggedIn)
				throw new Exception ("Invalid username/password");
					
			NessusObjectManager nessusManager = new NessusObjectManager (nessusSession);
			
			NessusReport report = nessusManager.GetReports()
				.Where(r => r.ReadableName == readableName)
				.SingleOrDefault();
			
			context.Response.Write(report.ToBusinessXml());
		}
	}
}

