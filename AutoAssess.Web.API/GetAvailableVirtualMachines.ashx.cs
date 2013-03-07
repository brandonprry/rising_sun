using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.Virtualbox;

namespace AutoAssess.Web.API
{
	public class GetAvailableVirtualMachines : System.Web.IHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public virtual void ProcessRequest (HttpContext context)
		{
			using (VirtualboxManager manager = new VirtualboxManager("vboxmanage"))
			{
				VirtualMachine[] available = manager.ListAllVirtualMachines();
				
				string xml = "<machines>";
				
				foreach (var box in available)	
				{
					xml += "<machine>";
					xml += "<name>" + box.Name + "</name>";
					xml += "<guid>" + box.Guid.ToString() + "</guid>";
					xml += "<username>" + box.Username + "</username>";
					xml += "<password>" + box.Password + "</password>";
					xml += "</machine>";
				}
				
				xml += "</machines>";
				
				context.Response.Write(xml);
			}
		}
	}
}

