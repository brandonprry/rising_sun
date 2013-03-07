using System;
using System.Xml;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitHostDetail
	{
		public MetasploitHostDetail()
		{
		}
		public MetasploitHostDetail (XmlNode deets)
		{
			foreach (XmlNode child in deets.ChildNodes)
			{
				if (child.Name == "id")
					this.RemoteID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "host-id")
					this.RemoteHostID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "nx-console-id")
					this.NexposeConsoleID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "nx-device-id")
					this.NexposeDeviceID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "src")
					this.Source = child.InnerText;
				else if (child.Name == "nx-site-name")
					this.NexposeSiteName = child.InnerText;
				else if (child.Name == "nx-site-importance")
					this.NexposeSiteImportance = child.InnerText;
				else if (child.Name == "nx-scan-template")
					this.NexposeScanTemplate = child.InnerText;
				else if (child.Name == "nx-risk-score")
					this.NexposeRiskScore = string.IsNullOrEmpty(child.InnerText) ? -1m : decimal.Parse(child.InnerText);
			}
		}
		
		public virtual int RemoteID { get; set; }
		
		public virtual int RemoteHostID { get; set; }
		
		public virtual int NexposeConsoleID { get; set; }
		
		public virtual int NexposeDeviceID { get; set; }
		
		public virtual string Source { get; set; }
		
		public virtual string NexposeSiteName { get; set; }
		
		public virtual string NexposeSiteImportance { get; set; }
		
		public virtual string NexposeScanTemplate { get; set; }
		
		public virtual decimal NexposeRiskScore { get; set; }
	}
}

