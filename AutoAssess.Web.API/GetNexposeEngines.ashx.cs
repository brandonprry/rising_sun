using System;
using System.Web;
using System.Web.UI;
using nexposesharp;
using NHibernate;
using AutoAssess.Data.PersistentObjects;
using System.Xml;
using System.Configuration;

namespace AutoAssess.Web.API
{
	public class GetNexposeEngines : ApiHttpHandler
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
			
			using (NexposeSession nsess = new NexposeSession("" + ConfigurationManager.AppSettings["Nexpose"] + ""))
			{
				nsess.Authenticate("nexpose", "nexpose");
				
				using (NexposeManager11 manager = new NexposeManager11(nsess))
				{
					XmlDocument engines = manager.GetScanEngineListing();
					
					context.Response.Write(engines.OuterXml);
				}
			}
		}
	}
}

