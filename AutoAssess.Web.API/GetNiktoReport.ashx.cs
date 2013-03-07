using System;
using System.Web;
using System.Web.UI;

namespace AutoAssess.Web.API
{
	public class GetNiktoReport : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
		}
	}
}

