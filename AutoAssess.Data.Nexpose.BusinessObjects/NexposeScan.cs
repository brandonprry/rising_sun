using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeScan 
	{
		public NexposeScan ()
		{
		}
		
		public NexposeScan (XmlNode report)
		{
			foreach (XmlNode child in report.ChildNodes)
			{
				if (child.Name == "scans")
				{
					foreach (XmlNode scan in child.ChildNodes) //for right now there is only one....
					{
						this.RemoteScanID = int.Parse(scan.Attributes["id"].Value);
						this.Name = scan.Attributes["name"].Value;
						this.StartTime = scan.Attributes["startTime"].Value;
						this.EndTime = scan.Attributes["endTime"].Value;
						this.Status = scan.Attributes["status"].Value;
					}
				}
				else if (child.Name == "nodes")
				{
					this.Assets = new List<NexposeAsset>();
					foreach (XmlNode node in child.ChildNodes)
						this.Assets.Add(new NexposeAsset(node));
				}
			}
		}
	
		public virtual int RemoteScanID { get; set; }
		
		public virtual int RemoteSiteID { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string StartTime { get; set; }
		
		public virtual string EndTime { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual IList<NexposeAsset> Assets { get; set; }
	}
}

