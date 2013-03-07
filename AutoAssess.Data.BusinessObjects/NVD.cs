using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class NVD
	{
		public NVD ()
		{
		}
		
		public NVD (XmlNode nvd)
		{
			this.NVDID = nvd.Attributes["id"].Value;
			
			foreach (XmlNode child in nvd.ChildNodes)
			{
				if (child.Name == "vuln:vulnerable-software-list")
				{
					if (this.VulnerableSoftware == null)
						this.VulnerableSoftware = new List<VulnerableSoftware>();
					
					foreach (XmlNode vulnApp in child.ChildNodes)
						this.VulnerableSoftware.Add(new VulnerableSoftware(vulnApp));
				}
				else if (child.Name == "vuln:cve-id")
					this.CVEID = child.InnerText;
				else if (child.Name == "vuln:published-datetime")
					this.DatePublished = DateTime.Parse(child.InnerText);
				else if (child.Name == "vuln:last-modified-datetime")
					this.LastModified = DateTime.Parse(child.InnerText);
				else if (child.Name == "vuln:cvss")
					this.CVSS = new CVSS(child);
				else if (child.Name == "vuln:cwe")
					this.CWE = child.InnerText;
				else if (child.Name == "vuln:references")
				{
					if (this.References == null)
						this.References = new List<NVDReference>();
					
					this.References.Add(new NVDReference(child));
				}
				else if (child.Name == "vuln:summary")
					this.Summary = child.InnerText;
			}
		}
		
		public virtual string NVDID { get; set; }
		
		public virtual string CVEID { get; set; }
		
		public virtual CVE CVE { get; set; }
		
		public virtual CVSS CVSS { get; set; }
		
		public virtual List<VulnerableSoftware> VulnerableSoftware { get; set; }
		
		public virtual DateTime DatePublished { get; set; }
				
		public virtual DateTime LastModified { get; set; }
		
		public virtual string CWE { get; set; }
		
		public virtual List<NVDReference> References { get; set; }
		
		public virtual string Summary { get; set; }
	}
	
	public class NVDReference 
	{
		public NVDReference ()
		{
		}
		
		public NVDReference (XmlNode reference)
		{
			this.Type = reference.Attributes["reference_type"].Value;
			
			foreach (XmlNode child in reference.ChildNodes)
			{
				if (child.Name == "vuln:source")
					this.Source = child.InnerText;
				else if (child.Name == "vuln:reference")
				{
					this.URL = child.Attributes["href"].Value;
					this.Description = child.InnerText;
				}
			}
		}
		
		public virtual string Source { get; set; }
		
		public virtual string URL { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual string Type { get; set; }
	}
	
	public class VulnerableSoftware
	{
		public VulnerableSoftware()
		{}
		
		public VulnerableSoftware(XmlNode app)
		{
			this.Software = app.InnerText;
		}
		
		public virtual string Software { get; set; }
	}
	
	public class CVSS
	{
		public CVSS ()
		{
		}
		
		public CVSS (XmlNode cvss)
		{
			foreach (XmlNode child in cvss.FirstChild.ChildNodes)
			{
				if (child.Name == "cvss:score")
					this.Score = double.Parse(child.InnerText);
				else if (child.Name == "cvss:access-vector")
					this.Vector = child.InnerText;
				else if (child.Name == "cvss:access-complexity")
					this.Complexity = child.InnerText;
				else if (child.Name == "cvss:authentication")
					this.Authentication = child.InnerText;
				else if (child.Name == "cvss:confidentiality-impact")
					this.ConfidentialityImpact = child.InnerText;
				else if (child.Name == "cvss:integrity-impact")
					this.IntegrityImpact = child.InnerText;
				else if (child.Name == "cvss:availability-impact")
					this.AvailabilityImpact = child.InnerText;
			}
		}
		
		public virtual double Score { get; set; }
		
		public virtual string Vector { get; set; }
		
		public virtual string Complexity { get; set; }
		
		public virtual string Authentication { get; set; }
		
		public virtual string ConfidentialityImpact { get; set; }
		
		public virtual string IntegrityImpact { get; set; }
		
		public virtual string AvailabilityImpact { get; set; }
	}
}

