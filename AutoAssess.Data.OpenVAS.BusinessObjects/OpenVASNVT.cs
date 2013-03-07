using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASNVT  : IOpenVASObject
	{
		public OpenVASNVT ()
		{
		}
		
		public OpenVASNVT(XmlNode node)
		{
			this.OID = node.Attributes["oid"].Value;
			
			foreach (XmlNode c in node.ChildNodes)
			{
				if (c.Name == "name")
					this.Name = c.InnerText;
				
				else if (c.Name == "category")
					this.Category = c.InnerText;
				
				else if (c.Name == "copyright")
					this.Copyright = c.InnerText;
				
				else if (c.Name == "description")
					this.Description = c.InnerText;
				
				else if (c.Name == "summary")
					this.Summary = c.InnerText;
				
				else if (c.Name == "family")
					this.FamilyName = c.InnerText;
				
				else if (c.Name == "version")
					this.Version = c.InnerText;
				
				else if (c.Name == "cvss_base")
					this.CVSSBase = c.InnerText;
				
				else if (c.Name == "risk_factor")
					this.RiskFactor = c.InnerText;
				
				else if (c.Name == "cve_id")
					this.CVEID = c.InnerText;
				
				else if (c.Name == "bugtraq_id")
					this.BugtraqID = c.InnerText;
				
				else if (c.Name == "xrefs")
					this.Xrefs = c.InnerText;
				
				else if (c.Name == "fingerprints")
					this.Fingerprints = c.InnerText;
				
				else if (c.Name == "tags")
					this.Tags = c.InnerText;
			}
		}
		
		public virtual string OID { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string Checksum { get; set; }
		
		public virtual string ChecksumAlgorithm { get; set; }
		
		public virtual string Category { get; set; }
		
		public virtual string Copyright { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual string Summary { get; set; }
		
		public virtual string FamilyName { get; set; }
		
		public virtual string Version { get; set; }
		
		public virtual string CVSSBase { get; set; }
		
		public virtual string RiskFactor { get; set; }
		
		public virtual string CVEID { get; set; }
		
		public virtual string BugtraqID { get; set; }
		
		public virtual string Xrefs { get; set; }
		
		public virtual string Fingerprints { get; set; }
		
		public virtual string Tags { get; set; }
		
		public virtual OpenVASNVTFamily ParentFamily { get; set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
		
	}
}

