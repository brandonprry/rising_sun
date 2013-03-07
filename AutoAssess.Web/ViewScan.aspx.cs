using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using AutoAssess.Data;
using AutoAssess.Data.Nessus;
using NHibernate.Criterion;
using AutoAssess.Data.PersistentObjects;
using System.Net;
using System.IO;
using System.Configuration;
using System.Text;

namespace AutoAssess.Web
{
	public partial class ViewScan : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			HttpWebRequest request = WebRequest
				.Create(ConfigurationManager.AppSettings["API"] + "/GetScan.ashx" +
					"?WebUserID=" + this.CurrentUser.UserID.ToString() +
					"&UserID=" + ConfigurationManager.AppSettings["UserID"] +
					"&IsActive=" + true +
					"&ProfileID=" + Request["pid"] +
					"&ClientID=" + ConfigurationManager.AppSettings["ClientID"]) as HttpWebRequest;
		
			WebResponse response = request.GetResponse();
			
			XmlDocument doc = new XmlDocument();	
			string xml = string.Empty;
			byte[] buff = new byte[2048];
			int bytes = 0;
			using (Stream stream = response.GetResponseStream())
			{
				do
				{
					bytes = stream.Read(buff, 0, buff.Length);
					
					xml = xml + ASCIIEncoding.ASCII.GetString(buff);
					
					buff = new byte[2048]; //clear cruft
				} while (bytes > 0);
			}
			
			xml = xml.Replace("&", "&amp;");
			
			doc.LoadXml(xml);
			
			//PersistentScan scan = new PersistentScan(doc);
			
		}
		
		protected void btnViewNessusScan_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			
			Response.Redirect("/ViewNessusScan.aspx?nsid=" + btn.CommandArgument);
		}
		

	}
}

