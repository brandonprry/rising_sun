using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.Nessus;
using AutoAssess.Data.Nessus.BusinessObjects;
using NHibernate;
using System.Configuration;

namespace AutoAssess.Web.API
{
	public class GetNessusReportHostPort : ApiHttpHandler
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
			int port = int.Parse(context.Request["PortNumber"]);
			string protocol = context.Request["Protocol"];
			
			if (string.IsNullOrEmpty(uuid))
				throw new Exception("no uuid");
			
			NessusManagerSession nessusSession = new NessusManagerSession (ConfigurationManager.AppSettings["nessusHost"]);
						
			bool loggedIn = false;
			nessusSession.Authenticate (ConfigurationManager.AppSettings["nessusUser"], ConfigurationManager.AppSettings["nessusPass"], 1234, out loggedIn);
					
			if (!loggedIn)
				throw new Exception ("Invalid username/password");
					
			NessusObjectManager nessusManager = new NessusObjectManager (nessusSession);
			
			XmlDocument doc = nessusManager.GetReportDetailsByPortAndHost(uuid, hostname, port, protocol);
			
			context.Response.Write(doc.OuterXml);
		}
	}
}

