using System;
using System.Web;
using System.Web.UI;

namespace AutoAssess.Web.API
{
	public class GetWhoisReport : System.Web.IHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public virtual void ProcessRequest (HttpContext context)
		{
		}
	}
}

