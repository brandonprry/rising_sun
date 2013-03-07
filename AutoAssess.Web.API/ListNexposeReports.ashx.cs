using System;
using System.Web;
using System.Web.UI;
using nexposesharp;
using System.Xml;

namespace AutoAssess.Web.API
{
	public class ListNexposeReports : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			using (NexposeSession session = new NexposeSession("192.168.56.103"))
			{
				session.Authenticate("nexpose", "nexpose");
				
				using (NexposeManager11 manager = new NexposeManager11(session))
				{
					XmlDocument doc = manager.GetReportListing();
					
					context.Response.Write(doc.OuterXml);
				}
			}
		}
	}
}

