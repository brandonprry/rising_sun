using System;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusPortDetails
	{
		public NessusPortDetails ()
		{
		}
		
		public NessusPortDetails(XmlNode details)
		{
			foreach (XmlNode child in details.ChildNodes)
			{
				if (child.Name == "item_id")
					this.RemoteItemID = int.Parse(child.InnerText);
				else if (child.Name == "tickets")
					this.NumberOfTickets = int.Parse(child.InnerText);
				else if (child.Name == "port")
					this.PortInfo = child.InnerText;
				else if (child.Name == "severity")
					this.Severity = int.Parse(child.InnerText);
				else if (child.Name == "pluginID")
					this.RemotePluginID = int.Parse(child.InnerText);
				else if (child.Name == "pluginName")
					this.PluginName = child.InnerText;
				else if (child.Name == "data")
					this.Data = new NessusPortDetailsData(child);
			}
		}
		
		public virtual int RemoteItemID { get; set; }
		
		public virtual int NumberOfTickets { get; set; }
		
		public virtual string PortInfo { get; set; }
		
		public virtual int Severity { get; set; }
		
		public virtual int RemotePluginID { get; set; }
		
		public virtual string PluginName { get; set; }
		
		public virtual NessusPortDetailsData Data { get; set; }
	}
	
	public class NessusPortDetailsData
	{
		public NessusPortDetailsData()
		{
			
		}
		
		public NessusPortDetailsData(XmlNode data)
		{
			foreach (XmlNode child in data.ChildNodes)
			{
				if (child.Name == "solution")
					this.Solution = child.InnerText;
				else if (child.Name == "risk_factor")
					this.RiskFactor = child.InnerText;
				else if (child.Name == "description")
					this.Description = child.InnerText;
				else if (child.Name == "plugin_publication_date")
					this.PluginPublicationDate = DateTime.Parse(child.InnerText);
				else if (child.Name == "synopsis")
					this.Synopsis = child.InnerText;
				else if (child.Name == "plugin_type")
					this.PluginType = child.InnerText;
				else if (child.Name == "plugin_modification_date")
					this.PluginModificationDate = DateTime.Parse(child.InnerText);
				else if (child.Name == "plugin_output")
					this.PluginOutput = child.InnerText;
				else if (child.Name == "plugin_version")
					this.PluginVersion = child.InnerText;
				else if (child.Name == "cve")
				{
					if (this.CVEs == null)
						this.CVEs = new List<string>();
					
					this.CVEs.Add(child.InnerText);
				}
			}
		}
		
		public virtual string Solution { get; set; }
		
		public virtual string RiskFactor { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual DateTime PluginPublicationDate { get; set; }
		
		public virtual string Synopsis { get; set; }
		
		public virtual string PluginType { get; set; }
		
		public virtual DateTime PluginModificationDate { get; set; }
		
		public virtual string PluginOutput { get; set; }
		
		public virtual string PluginVersion { get; set; }
		
		public virtual List<string> CVEs { get; set; }
	}
}

