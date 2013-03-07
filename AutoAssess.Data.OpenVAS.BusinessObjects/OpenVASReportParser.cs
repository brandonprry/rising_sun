using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public static class OpenVASReportParser
	{
		public static Dictionary<string, OpenVASReportHost> ParseHostsFromReport(XmlDocument doc) 
		{
			Dictionary<string, OpenVASReportHost> hosts = new Dictionary<string, OpenVASReportHost>();
			
			foreach (XmlNode node in doc.FirstChild.FirstChild.FirstChild.ChildNodes)
			{
				if (node.Name == "ports")
				{
					foreach (XmlNode port in node.ChildNodes)
					{
						string description = string.Empty;
						string host = string.Empty;
						string threat = string.Empty;
						
						foreach (XmlNode portInfo in port.ChildNodes)
						{
							if (portInfo.Name == "host")
								host = portInfo.InnerText;
							else if (portInfo.Name == "threat")
								threat = portInfo.InnerText;
							else
								description = portInfo.InnerText;
						}
						OpenVASReportHostPort rport = new OpenVASReportHostPort();
						rport.Description = description;
						rport.Host = host;
						rport.Threat = threat;
						
						try
						{
							hosts[host].Ports.Add(rport);
						}
						catch
						{
							OpenVASReportHost rhost = new OpenVASReportHost();
							rhost.IPAddress = host;
							rhost.Ports = new List<OpenVASReportHostPort>();
							rhost.Results = new List<OpenVASReportHostResult>();
							
							hosts.Add(host, rhost);
							hosts[host].Ports.Add(rport);
						}
					}
				}
				else if (node.Name == "results")
				{
					foreach (XmlNode result in node.ChildNodes)
					{
						string id = result.Attributes["id"].Value;
						
						string subnet = string.Empty;
						string host = string.Empty;
						string port = string.Empty;
						OpenVASReportHostResultNVT nvt = null;
						string threat = string.Empty;
						string description = string.Empty;
						
						foreach (XmlNode resultInfo in result.ChildNodes)
						{
							if (resultInfo.Name == "subnet")
								subnet = resultInfo.InnerText;
							else if (resultInfo.Name == "host")
								host = resultInfo.InnerText;
							else if (resultInfo.Name == "port")
								port = resultInfo.InnerText;
							else if (resultInfo.Name == "nvt")
							{
								nvt = new OpenVASReportHostResultNVT();
								nvt.OID = resultInfo.Attributes["oid"].Value;
								
								foreach (XmlNode nvtinfo in resultInfo.ChildNodes)
								{
									if (nvtinfo.Name == "name")
										nvt.Name = nvtinfo.InnerText;
									else if (nvtinfo.Name == "cvss_base")
										nvt.CVSSBaseScore = nvtinfo.InnerText;
									else if (nvtinfo.Name == "risk_factor")
										nvt.RiskFactor = nvtinfo.InnerText;
									else if (nvtinfo.Name == "cve")
										nvt.CVE = nvtinfo.InnerText;
									else if (nvtinfo.Name == "bid")
										nvt.BID = nvtinfo.InnerText;
								}
							}
							else if (resultInfo.Name == "threat")
								threat = resultInfo.InnerText;
							else if (resultInfo.Name == "description")
								description = resultInfo.InnerText;
						}
						
						OpenVASReportHostResult rresult = new OpenVASReportHostResult();
						rresult.Description = description;
						rresult.Host = host;
						rresult.ID = id;
						rresult.NVT = nvt;
						rresult.Port = port;
						rresult.Subnet = subnet;
						rresult.Threat = threat;
						
						hosts[host].Results.Add(rresult);
					}
				}
			}
			
			//calculate some pseudo-quantitative number regarding overall risk
			foreach (var pair in hosts)
			{
				int risk = 0;
				
				foreach(var port in pair.Value.Ports)
				{
					if (port.Threat == "High")
						risk += 4;
					else if (port.Threat == "Medium")
						risk += 3;
					else if (port.Threat == "Low")
						risk += 2;
					else if (port.Threat == "Log")
						risk += 1;
					else if (port.Threat == "Debug")
						risk += 0;
				}
				
				pair.Value.Risk = risk;
			}
			
			return hosts;
		}
	}
	
	[Serializable]
	public class OpenVASReportHost
	{
		public string IPAddress { get; set; }
		
		public int Risk { get; set; }
		
		public List<OpenVASReportHostResult> Results { get; set; }
		public List<OpenVASReportHostPort> Ports { get; set; }
	}
	
	[Serializable]
	public class OpenVASReportHostPort 
	{
		public string Description { get; set; }
		public string Host { get; set; }
		public string Threat { get; set; }
	}
	
	[Serializable]
	public class OpenVASReportHostResult
	{
		public string ID { get; set; }
		public string Subnet { get; set; }
		public string Host { get; set; }
		public string Port { get; set; }
		public OpenVASReportHostResultNVT NVT { get; set; }
		public string Threat { get; set; }
		public string Description { get; set; }
		
		public string Name 
		{
			get { return NVT.Name; }
		}
	}
	
	[Serializable]
	public class OpenVASReportHostResultNVT
	{
		public string OID { get; set; }
		public string Name { get; set; }
		public string CVSSBaseScore { get; set; }
		public string RiskFactor { get; set; }
		public string CVE { get; set; }
		public string BID { get; set; }
	}
}

