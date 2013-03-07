using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.PersistentObjects;
using System.Net;
using System.Xml;
using System.Configuration;
using System.IO;
using System.Text;

namespace AutoAssess.Web
{
	public partial class VerifyProfile : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			HttpWebRequest request = WebRequest
				.Create(ConfigurationManager.AppSettings["API"] + "/GetProfile.ashx" +
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
			
			PersistentProfile profile = new PersistentProfile(doc.DocumentElement);
			
			this.CurrentProfile = profile;
			
			string filename = Guid.NewGuid().ToString() + ".txt";
			string filedata = System.Convert.ToBase64String(Guid.NewGuid().ToByteArray());
			
			Session["VerifyProfile?FileName"] = filename;
			Session["VerifyProfile?FileData"] = filedata;
			
			
		}
	}
}

