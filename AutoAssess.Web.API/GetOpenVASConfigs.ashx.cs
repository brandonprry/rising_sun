using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.OpenVAS;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using System.Xml;
using System.Configuration;
using openvassharp;

namespace AutoAssess.Web.API
{
	public class GetOpenVASConfigs : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			using (OpenVASManagerSession session = new OpenVASManagerSession(ConfigurationManager.AppSettings["openvasUser"], ConfigurationManager.AppSettings["openvasPass"], ConfigurationManager.AppSettings["openvasHost"]))
			{
				using (OpenVASManager manager = new OpenVASManager(session))
				{
					XmlDocument doc = manager.GetConfigs(string.Empty, false, false, string.Empty, string.Empty);
					
					context.Response.Write(doc.OuterXml);
				}
			}
		}
	}
}

