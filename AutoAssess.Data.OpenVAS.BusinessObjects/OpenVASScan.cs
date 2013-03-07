using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASScan :  IOpenVASObject
	{
		public OpenVASScan ()
		{
		}
		
		public OpenVASScan(XmlNode response)
		{
			OpenVASScan report = this;
			
			XmlNode nd = (response.FirstChild.FirstChild.ChildNodes.Count == 0) ? response.FirstChild : response.FirstChild.FirstChild;
			
			foreach (XmlNode node in nd.ChildNodes)
			{
				Console.WriteLine(node.Name);
				
				if (node.Name == "filters")
				{
					foreach (XmlNode child in node.ChildNodes)
					{
						//ignore filters for now
					}
				}
				else if (node.Name == "ports")
				{
					report.Ports = new List<ReportPort>();
					foreach (XmlNode child in node.ChildNodes)
					{
						ReportPort port = new ReportPort();
						foreach (XmlNode c in child.ChildNodes)
						{
							if (c.Name == "host")
								port.Host = c.InnerText;
							else if (c.Name == "threat")
								port.Threat = c.InnerText;
							else
								port.Description = c.InnerText;
								
						}
						
						report.Ports.Add(port);
					}
				}
				else if (node.Name == "results")
				{
					report.Results = new List<ReportResult>();
					foreach (XmlNode child in node.ChildNodes)
					{
						ReportResult result = new ReportResult();
						foreach (XmlNode c in child.ChildNodes)
						{
							if (c.Name == "subnet")
								result.Subnet = c.InnerText;
							else if (c.Name == "host")
								result.Host = c.InnerText;
							else if (c.Name == "port")
								result.Port = c.InnerText;
							else if (c.Name == "nvt")
							{
								result.NVT = new ReportNVT();
								
								result.NVT.OID = c.Attributes["oid"].Value;
								foreach (XmlNode n in c.ChildNodes)
								{
									if (n.Name == "name")
										result.NVT.Name = n.InnerText;
									else if (n.Name == "cvss_base")
										result.NVT.CVSSBaseValue = n.InnerText;
									else if (n.Name == "risk_factor")
										result.NVT.RiskFactor = n.InnerText;
								}
							}
							else if (c.Name == "threat")
								result.Threat = c.InnerText;
							else if (c.Name == "description")
								result.Description = c.InnerText;
						}
						
						report.Results.Add(result);
					}
				}
			}
		}
				
		public virtual ResultCount ResultCount { get; set; }
		
		public virtual IList<ReportFilter> Filters { get; set; }
		
		public virtual IList<ReportResult> Results { get; set; }
		
		public virtual IList<ReportPort> Ports { get; set; }
		
		public virtual Guid RemoteReportID { get; set; }
		
		public virtual string ScanStart { get; set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument doc)
		{
			return new System.Collections.Generic.List<AutoAssess.Data.OpenVAS.BusinessObjects.IOpenVASObject>();
		}
	}
	
	[Serializable]
	public class ResultCount
	{
		public ResultCount()
		{
		}
		
		public virtual int Full { get; set; }
		public virtual int Filtered { get; set; }
		
		public virtual int FullDebug { get; set; }
		public virtual int FilteredDebug { get; set; }
		
		public virtual int FullHoles { get; set; }
		public virtual int FilteredHoles { get; set; }
		
		public virtual int FullInfo { get; set; }
		public virtual int FilteredInfo { get; set; }
		
		public virtual int FullLog { get; set; }
		public virtual int FilteredLog { get; set; }
		
		public virtual int FullWarning { get; set; }
		public virtual int FilteredWarning { get; set;}
	}
	
	[Serializable]
	public class ReportPort
	{
		public ReportPort()
		{
		}
		
		public virtual string Description { get; set; }
		public virtual string Host { get; set; }
		public virtual string Threat { get; set; }
	}
	
	[Serializable]
	public class ReportResult
	{
		public ReportResult()
		{
		}
		
		public virtual ReportNVT NVT { get; set; }
		public virtual string Subnet { get; set; }
		public virtual string Host { get; set; }
		public virtual string Port { get; set; }
		public virtual string Threat { get; set; }
		public virtual string Description { get; set; }
	}
	
	[Serializable]
	public class ReportNVT 
	{
		public ReportNVT()
		{
		}
		
		public virtual string OID { get; set; }
		public virtual string Name { get; set; }
		public virtual string CVSSBaseValue { get; set; }
		public virtual string RiskFactor { get; set; }
	}
	
	[Serializable]
	public class ReportFilter
	{
		public ReportFilter()
		{
		}
		
		public virtual string Phrase { get; set; }
		public virtual bool Notes { get; set; }
		public virtual bool Overrides { get; set; }
		public virtual bool ApplyOverrides { get; set; }
		public virtual bool ResultHostsOnly { get; set; }
		public virtual int MainCVSSBaseScore { get; set; }
	}
}

