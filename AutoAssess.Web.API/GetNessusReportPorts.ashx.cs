using System;
using System.Web;
using System.Web.UI;
using NHibernate;
using AutoAssess.Data.PersistentObjects;
using nessusssharp;
using AutoAssess.Data.Nessus.BusinessObjects;
using System.Xml;
using System.Configuration;

namespace AutoAssess.Web.API
{
	public class GetNessusReportPorts : ApiHttpHandler
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
			
			string uuid = context.Request["ReportUUID"];
			string hostname = context.Request["Hostname"];
			
			if (string.IsNullOrEmpty(uuid))
				throw new Exception("no uuid");
			
			NessusManagerSession nessusSession = new NessusManagerSession (ConfigurationManager.AppSettings["nessusHost"]);
						
			bool loggedIn = false;
			nessusSession.Authenticate ("nessus", "nessus", 1234, out loggedIn);
					
			if (!loggedIn)
				throw new Exception ("Invalid username/password");
					
			NessusObjectManager nessusManager = new NessusObjectManager (nessusSession);
			
			XmlDocument ports = nessusManager.GetPortsForHostFromReport(uuid, hostname);
			
			context.Response.Write(ports.OuterXml);
		}
	}
}

