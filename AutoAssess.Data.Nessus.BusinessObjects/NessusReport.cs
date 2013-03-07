using System;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusReport 
	{
		public NessusReport ()
		{
		}
		
		public NessusReport (XmlNode node)
		{
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "name")
					this.RemoteReportID = child.InnerText;
				else if (child.Name == "status")
					this.Status = child.InnerText;
				else if (child.Name == "readableName")
					this.ReadableName = child.InnerText;
				else if (child.Name == "timestamp")
					this.TimeStamp = new DateTime(int.Parse(child.InnerText));
			}	
		}
	
		public virtual string RemoteReportID { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual string ReadableName { get; set; }
		
		public virtual string FullReport { get; set; }
		
		public virtual NessusScan ParentScan { get; set; }
		
		public virtual DateTime TimeStamp { get; set; }
		
		public virtual string ToBusinessXml()
		{
			string xml = "<nessusReport>";
			
			xml = xml + "<name>" + this.RemoteReportID + "</name>";
			xml = xml + "<status>" + this.Status + "</status>";
			xml = xml + "<readableName>" + this.ReadableName + "</readableName>";
			xml = xml + "<timestamp>" + this.TimeStamp.ToLongTimeString() + "</timestamp>";
			
			xml = xml + "</nessusReport>";
			
			return xml;
		}
		
	}
}

