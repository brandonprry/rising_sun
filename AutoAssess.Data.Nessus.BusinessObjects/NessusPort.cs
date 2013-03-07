using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusPort
	{
		public NessusPort ()
		{
		}
		
		public NessusPort (XmlNode port)
		{
			foreach (XmlNode child in port.ChildNodes)
			{
				if (child.Name == "portNum")
					this.PortNumber = int.Parse(child.InnerText);
				else if (child.Name == "protocol")
					this.Protocol = child.InnerText;
				else if (child.Name == "severity")
					this.Severity = int.Parse(child.InnerText);
				else if (child.Name == "svcName")
					this.ServiceName = child.InnerText;
				else if (child.Name == "severityCount")
				{
					this.SeverityItems = new List<NessusPortSeverityItem>();
					
					foreach (XmlNode item in child.ChildNodes)
						this.SeverityItems.Add(new NessusPortSeverityItem(item));
				}		
			}
		}
		
		public virtual int PortNumber { get; set; }
		
		public virtual string Protocol { get; set; }
		
		public virtual int Severity { get; set; }
		
		public virtual string ServiceName { get; set; }
		
		public virtual List<NessusPortSeverityItem> SeverityItems { get; set; }
	}
	
	public class NessusPortSeverityItem
	{
		public NessusPortSeverityItem()
		{
			
		}
		
		public NessusPortSeverityItem(XmlNode item)
		{
			foreach (XmlNode child in item.ChildNodes)
			{
				if (child.Name == "severityLevel")
					this.SeverityLevel = int.Parse(child.InnerText);
				if (child.Name == "count")
					this.Count = int.Parse(child.InnerText);
			}
		}
		
		public int SeverityLevel { get; set; }
		
		public int Count { get; set; }
	}
}

