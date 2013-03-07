using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeAsset
	{
		public NexposeAsset ()
		{
		}
		
		public NexposeAsset (XmlNode node)
		{
			this.IPAddressV4 = node.Attributes["address"].Value;
			this.Status = node.Attributes["status"].Value;
			this.RemoteDeviceID = int.Parse(node.Attributes["device-id"].Value);
			
			if (node.Attributes["site-name"] != null)
				this.SiteName = node.Attributes["site-name"].Value;
			
			if (node.Attributes["site-importance"] != null)
				this.SiteImportance = node.Attributes["site-importance"].Value;
			
			if (node.Attributes["scan-template"] != null)
				this.ScanTemplate = node.Attributes["scan-template"].Value;
			
			if (node.Attributes["risk-score"] != null)
				this.RiskScore = decimal.Parse(node.Attributes["risk-score"].Value);
			
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "names")
				{
					this.Names = new List<NexposeHostName>();
					foreach (XmlNode name in child.ChildNodes)
						this.Names.Add(new NexposeHostName(name));
				}
				else if (child.Name == "fingerprints")
				{
					this.Fingerprints = new List<NexposeHostFingerprint>();
					foreach (XmlNode print in child.ChildNodes)
						this.Fingerprints.Add(new NexposeHostFingerprint(print));
				}
				else if (child.Name == "tests")
				{
					this.HostTests = new List<NexposeHostTest>();
					foreach (XmlNode test in child.ChildNodes)
						this.HostTests.Add(new NexposeHostTest(test));
				}
				else if (child.Name == "endpoints")
				{
					this.Services = new List<NexposeHostService>();
					foreach (XmlNode endpoint in child.ChildNodes)
					{
						string protocol = endpoint.Attributes["protocol"].Value;
						int port = int.Parse(endpoint.Attributes["port"].Value);
						string status = endpoint.Attributes["status"].Value;
						foreach (XmlNode service in endpoint.FirstChild.ChildNodes)
							this.Services.Add(new NexposeHostService(service) { Port = port, Protocol = protocol, Status = status });
					}
				}
			}
		}
		
		public virtual string IPAddressV4 { get; set; }
		public virtual string Status { get; set; }
		public virtual int RemoteDeviceID { get; set; }
		public virtual string SiteName { get; set; }
		public virtual string SiteImportance { get; set; }
		public virtual string ScanTemplate { get; set; }
		public virtual decimal RiskScore { get; set; }
		
		public virtual IList<NexposeHostName> Names { get; set; }
		public virtual IList<NexposeHostFingerprint> Fingerprints { get; set; }
		public virtual IList<NexposeHostTest> HostTests { get; set; }
		public virtual IList<NexposeHostService> Services { get; set; }
	}
}

