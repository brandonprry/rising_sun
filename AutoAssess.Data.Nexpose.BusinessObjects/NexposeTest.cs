using System;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeTest
	{
		public NexposeTest ()
		{
		}
		
		public NexposeTest (XmlNode test)
		{
			this.ID = test.Attributes["id"].Value;
			this.Status = test.Attributes["status"].Value;
			
			if (test.Attributes["device-id"] != null)
				this.RemoteDeviceID = int.Parse(test.Attributes["device-id"].Value);
			
			if (test.Attributes["scan-id"] != null)
				this.RemoteScanID = int.Parse(test.Attributes["scan-id"].Value);
			
			if (test.Attributes["pci-compliance-status"] != null)
				this.IsPCICompliant = test.Attributes["pci-compliance-status"].Value == "pass" ? true : false;
			
			this.NexposeParagraph = NexposeParagraphToHTML(test.FirstChild);
			
			if (test.Attributes["vulnerable-since"] != null)
				this.VulnerableSince = test.Attributes["vulnerable-since"].Value;
			
		}
		
		public virtual string ID { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual int RemoteDeviceID { get; set; }
		
		public virtual int RemoteScanID { get; set; }
		
		public virtual string VulnerableSince { get; set; }
		
		public virtual bool IsPCICompliant { get; set; }
		
		public virtual string NexposeParagraph { get; set; }
		
		private string NexposeParagraphToHTML(XmlNode paragraph)
		{
			string tmp = paragraph.OuterXml;
			
			tmp = tmp.Replace("<Paragraph>", "<p>").Replace("</Paragraph>", "</p>");
			tmp = tmp.Replace("<UnorderedList>", "<ul>").Replace("</UnorderedList>", "</ul>");
			tmp = tmp.Replace("<ListItem>", "<li>").Replace("</ListItem>", "</li>");
			tmp = tmp.Replace("<ContainerBlockElement>", "<div>").Replace("</ContainerBlockElement>", "</div>");
			
			return tmp;
		}
	}
}

