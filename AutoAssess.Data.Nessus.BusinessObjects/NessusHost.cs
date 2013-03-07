using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusHost
	{
		public NessusHost ()
		{
			this.SeverityItems = new List<NessusSeverityItem>();
		}
		
		public NessusHost(XmlNode node)
		{
			this.SeverityItems = new List<NessusSeverityItem>();
			
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "hostname")
					this.Hostname = child.InnerText;
				else if (child.Name == "severity")
					this.Severity = int.Parse(child.InnerText);
				else if (child.Name == "scanProgressCurrent")
					this.ScanProgressCurrent = int.Parse(child.InnerText);
				else if (child.Name == "scanProgressTotal")
					this.ScanProgressTotal = int.Parse(child.InnerText);
				else if (child.Name == "severityCount")
				{
					foreach (XmlNode s in child.ChildNodes)
						this.SeverityItems.Add(new NessusSeverityItem(s));
				}
			}
			
		}
		
		public virtual string Hostname { get; set; }
		
		public virtual int SeverityCount { get; set; }
		
		public virtual int Severity { get; set; }
		
		public virtual int ScanProgressTotal { get; set; }
		
		public virtual int ScanProgressCurrent { get; set; }
		
		public virtual IList<NessusSeverityItem> SeverityItems { get; set; }
		
		public virtual string ToBusinessXml()
		{
			string xml = "<nessusHost>";
			
			xml = xml + "<hostname>" + this.Hostname + "</hostname>";
			xml = xml + "<scanProgressCurrent>" + this.ScanProgressCurrent + "</scanProgressCurrent>";
			xml = xml + "<scanProgressTotal>" + this.ScanProgressTotal + "</scanProgressTotal>";
			xml = xml + "<severity>" + this.Severity + "</severity>";
			
			xml = xml + "<severityCount>";
			foreach (NessusSeverityItem item in this.SeverityItems)
				xml = xml + item.ToBusinessXml();
				
			xml = xml + "</severityCount>";
			
			xml = xml + "</nessusHost>";
			
			return xml;
		}
		
	}
	
	public class NessusSeverityItem
	{
		public NessusSeverityItem()
		{}
		
		public NessusSeverityItem(XmlNode node)
		{
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "severityLevel")
					this.SeverityLevel = int.Parse(child.InnerText);
				else if (child.Name == "count")
					this.Count = int.Parse(child.InnerText);
			}
		}
		
		public virtual int SeverityLevel { get; set; }
		
		public virtual int Count { get; set; }
		
		public virtual string ToBusinessXml()
		{
			string xml = "<item>";
			
			xml = xml + "<severityLevel>" + this.SeverityLevel + "</severityLevel>";
			xml = xml + "<count>" + this.Count + "</count>";
			
			xml = xml + "</item>";
			
			return xml;
		}
	}
}

