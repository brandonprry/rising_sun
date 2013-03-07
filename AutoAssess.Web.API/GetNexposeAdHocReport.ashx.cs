using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using nexposesharp;
using System.Text;

namespace AutoAssess.Web.API
{
	public class GetNexposeAdHocReport : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			using (NexposeSession session = new NexposeSession("192.168.56.105"))
			{
				session.Authenticate("nexpose", "nexpose");
				
				using (NexposeManager11 manager = new NexposeManager11(session))
				{
					string template = "audit-report";
					string format = "text";
					string siteID = context.Request["SiteID"];
					string xml = string.Empty;
					
					xml = xml + "<AdhocReportConfig template-id=\"" + template + "\" format=\"" + format + "\">";
					xml = xml + "<Filters><filter type=\"site\" id=\"" + siteID + "\"></filter></Filters>";
					xml = xml + "</AdhocReportConfig>";
					
					XmlDocument request = new XmlDocument();
					request.LoadXml(xml);
					
					byte[] report = manager.GenerateAdHocReport(request);
					
					context.Response.Write(Encoding.ASCII.GetString(report));

				}
			}
		}
	}
}

