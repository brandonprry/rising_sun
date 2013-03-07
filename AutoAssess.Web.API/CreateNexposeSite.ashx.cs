using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using nexposesharp;
using System.Configuration;

namespace AutoAssess.Web.API
{
	public class CreateNexposeSite : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			string id = "-1";
			string template = "full-audit";
			string name = context.Request["SiteName"];
			string description = context.Request["SiteDescription"];
			string hosts = context.Request["Hosts"];
			
			string siteXml = "<Site id=\"" + id + "\" name=\"" + name+ "\" description=\"" + description + "\">";
				
			siteXml = siteXml + "<Hosts>";
			
			foreach (string host in hosts.Split(','))
				siteXml = siteXml + "<host>" + host + "</host>";
			
			siteXml = siteXml + "</Hosts>" +
								 "<Credentials></Credentials>" +
								 "<Alerting></Alerting>" +
								 "<ScanConfig configID=\"" + id + "\" name=\"" + name + "\" templateID=\"" + template + "\"></ScanConfig>" +
								 "</Site>";
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(siteXml);
			
			using (NexposeSession session = new NexposeSession("" + ConfigurationManager.AppSettings["Nexpose"] + ""))
			{
				session.Authenticate("nexpose", "nexpose");
				
				using (NexposeManager11 manager = new NexposeManager11(session))
				{
					XmlDocument response = manager.SaveOrUpdateSite(doc.FirstChild);
					
					context.Response.Write(response.OuterXml);
				}
			}
		}
	}
}

