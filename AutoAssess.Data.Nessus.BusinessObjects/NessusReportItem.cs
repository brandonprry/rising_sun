using System;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusReportItem
	{
		public NessusReportItem ()
		{
		}
		
		public NessusReportItem (XmlNode item)
		{
			this.Port = item.Attributes["port"].Value;
			this.ServiceName = item.Attributes["svc_name"].Value;
			this.Protocol = item.Attributes["protocol"].Value;
			this.Severity = item.Attributes["severity"].Value;
			this.PluginID = item.Attributes["pluginID"].Value;
			this.PluginName = item.Attributes["pluginName"].Value;
			this.PluginFamily = item.Attributes["pluginFamily"].Value;
			
			foreach (XmlNode child in item.ChildNodes)
			{
				if (child.Name == "description")
					this.Description = child.InnerText;
				else if (child.Name == "fname")
					this.FileName = child.InnerText;
				else if (child.Name == "plugin_modification_date")
					this.PluginModificationDate = child.InnerText;
				else if (child.Name == "plugin_publication_date")
					this.PluginPublicationDate = child.InnerText;
				else if (child.Name == "plugin_type")
					this.PluginType = child.InnerText;
				else if (child.Name == "risk_factor")
					this.RiskFactor = child.InnerText;
				else if (child.Name == "solution")
					this.Solution = child.InnerText;
				else if (child.Name == "synopsis")
					this.Synopsis = child.InnerText;
				else if (child.Name == "plugin_output")
					this.PluginOutput = child.InnerText;
				else if (child.Name == "bid")
					this.BID = child.InnerText;
				else if (child.Name == "cert")
					this.CERT = child.InnerText;
				else if (child.Name == "cpe")
					this.CPE = child.InnerText;
				else if (child.Name == "cve")
					this.CVE = child.InnerText;
				else if (child.Name == "cvss_base_score")
					this.CVSSBaseScore = child.InnerText;
				else if (child.Name == "cvss_temporal_score")
					this.CVSSTemporalScore = child.InnerText;
				else if (child.Name == "cvss_Temporal_vector")
					this.CVSSTemporalVector = child.InnerText;
				else if (child.Name == "cvss_vector")
					this.CVSSVector = child.InnerText;
				else if (child.Name == "edb-id")
				{
					if (string.IsNullOrEmpty(this.EDBID))
						this.EDBID = child.InnerText;
					else
						this.EDBID += "," + child.InnerText; 
				}
				else if (child.Name == "exploit_available")
					this.ExploitInCore  = bool.Parse(child.InnerText);
				else if (child.Name == "exploit_framework_core")
					this.ExploitInCore = Boolean.Parse(child.InnerText);
				else if (child.Name == "exploit_framework_metasploit")
					this.ExploitInMetasploit = Boolean.Parse(child.InnerText);
				else if (child.Name == "exploitability_ease")
					this.ExploitEase = child.InnerText;
				else if (child.Name == "metasploit_name")
					this.MetasploitName = child.InnerText;
				else if (child.Name == "osvdb")
					this.OSVDB = child.InnerText;
				else if (child.Name == "patch_publication_date")
					this.PatchPublicationDate = child.InnerText;
				else if (child.Name == "see_also")
					this.SeeAlso = child.InnerText;
				else if (child.Name == "vuln_publication_date")
					this.VulnerabilityPublicationDate = child.InnerText;
				else if (child.Name == "xref")
				{
					if (string.IsNullOrEmpty(this.XREF))
						this.XREF = child.InnerText;
					else 
						this.XREF += "," + child.InnerText;
				}
				else if (child.Name == "iava")
					this.IAVA = child.InnerText;
			}
		}
		
		public virtual string BID { get; set; }
		public virtual string IAVA { get; set;}
		public virtual string CERT { get; set; }
		public virtual string CPE { get; set; }
		public virtual string CVE { get; set; }
		public virtual string OSVDB { get; set; }
		public virtual string CVSSBaseScore { get; set; }
		public virtual string CVSSTemporalScore { get; set; }
		public virtual string CVSSTemporalVector { get; set; }
		public virtual bool ExploitIsAvailable { get; set; }
		public virtual bool ExploitInCore { get; set; }
		public virtual bool ExploitInMetasploit { get; set; }
		public virtual string ExploitEase { get; set; }
		public virtual string MetasploitName { get; set; }
		public virtual string CVSSVector { get; set; }
		public virtual string EDBID { get; set; }
		public virtual string Port { get; set; }
		public virtual string ServiceName { get; set; }
		public virtual string Protocol { get; set; }
		public virtual string Severity { get; set; }
		public virtual string PluginID { get; set; }
		public virtual string PluginName { get; set; }
		public virtual string PluginFamily { get; set; }
		public virtual string Description { get; set; }
		public virtual string FileName { get; set; }
		public virtual string PluginModificationDate { get; set; }
		public virtual string PluginPublicationDate { get; set; }
		public virtual string PluginType { get; set; }
		public virtual string RiskFactor { get; set; }
		public virtual string Solution { get; set; }
		public virtual string Synopsis { get; set; }
		public virtual string PluginOutput { get; set; }
		public virtual string PatchPublicationDate { get; set; }
		public virtual string SeeAlso { get; set; }
		public virtual string VulnerabilityPublicationDate { get; set; }
		public virtual string XREF { get; set;}
	}
}

