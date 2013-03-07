using System;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusReportHost
	{
		public NessusReportHost ()
		{
		}
		
		public NessusReportHost (XmlNode host)
		{
			this.Name = host.Attributes["name"].Value;
			this.ReportItems = new List<NessusReportItem>();
			foreach (XmlNode child in host.ChildNodes)
			{
				if (child.Name == "HostProperties")
					this.HostProperties = new NessusHostProperties(child);
				else if (child.Name == "ReportItem")
					this.ReportItems.Add(new NessusReportItem(child));
			}
		}
		
		public virtual string Name { get; set; }
		
		public virtual NessusHostProperties HostProperties { get; set; }
		
		public virtual IList<NessusReportItem> ReportItems { get; set; }
	}
}

