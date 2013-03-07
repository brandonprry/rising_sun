using System;
using System.Xml;
using System.Collections.Generic;

using AutoAssess.Data.Nessus;
using AutoAssess.Data.Nessus.BusinessObjects;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusObjectManager : NessusManager, IDisposable
	{
		
		public NessusObjectManager (NessusManagerSession session) : base (session)
		{
			if (!session.IsAuthenticated)
				throw new Exception("This session hasn't been authenticated.");
		}
		
		public NessusScan CreateAndStartScan(string range, int remotePolicyID, string name)
		{
			name = name + new Random().Next().ToString();
			
			XmlDocument scanDoc = base
				.CreateScan(range, remotePolicyID, name); 
			
			NessusScan scan = new NessusScan();
			
			scan.Range = range;
			scan.Name = name;
			
			foreach (XmlNode node in scanDoc.LastChild.ChildNodes)
			{
				if (node.Name == "contents")
				{
					foreach (XmlNode child in node.FirstChild.ChildNodes)
					{
						if (child.Name == "uuid")
							scan.RemoteScanID = child.InnerText;
						else if (child.Name == "owner")
							scan.Owner = child.InnerText;
						else if (child.Name == "start_time")
							scan.StartTime = new DateTime(int.Parse(child.InnerText));
					}
				}
			}
			
			return scan;
		}
		
		public List<NessusReport> GetReports()
		{
			XmlDocument reportsXml = base.ListReports();
			List<NessusReport> reports = new List<NessusReport>();
			
			foreach (XmlNode c in reportsXml.LastChild.ChildNodes)
			{
				if (c.Name == "contents")
				{
					foreach (XmlNode child in c.FirstChild.ChildNodes)
					{
						if (child.Name == "report")
							reports.Add(new NessusReport(child));
					}
				}
			}
			
			return reports;
		}
		
		public List<NessusHost> GetReportHostObjects(string reportID)
		{
			XmlDocument doc = base.GetReportHosts(reportID);
			
			List<NessusHost> hosts = new List<NessusHost>();
			
			foreach (XmlNode node in doc.LastChild.LastChild.FirstChild.ChildNodes)
				hosts.Add(new NessusHost(node));
			
			return hosts;
		}
		
		public List<NessusPluginFamily> GetPluginFamilies(bool getPlugins)
		{
			XmlDocument familiesXml = base.ListPluginFamilies();
			
			List<NessusPluginFamily> families = new List<NessusPluginFamily>();
			
			foreach (XmlNode child in familiesXml.LastChild.ChildNodes) //first child is XmlDeclaration
			{
				if (child.Name == "contents")
				{
					foreach (XmlNode familyChild in child.FirstChild.ChildNodes)
					{
						NessusPluginFamily family = new NessusPluginFamily(familyChild);
					
						if (getPlugins)
						{
							family.ChildPlugins = new List<NessusPlugin>();
							
							XmlDocument familyPlugins = base.ListPluginsByFamily(family.Name);
							
							foreach (XmlNode node in familyPlugins.LastChild.ChildNodes)
							{
								if (node.Name == "contents")
								{
									foreach (XmlNode plugin in node.FirstChild.ChildNodes)
										family.ChildPlugins.Add(new NessusPlugin(plugin) { Family = family });
								}
							}
							
						}
						
						families.Add(family);
					}
				}
			}
			
			return families;
		}
		
		public List<NessusPlugin> GetPluginsByFamily(string familyName)
		{
			XmlDocument pluginsXml = base.ListPluginsByFamily(familyName);
			
			List<NessusPlugin> plugins = new List<NessusPlugin>();
			
			foreach (XmlNode child in pluginsXml.LastChild.ChildNodes)//first child is XmlDeclaration
				plugins.Add(new NessusPlugin(child));
			
			return plugins;
		}
		
		public List<NessusPolicy> GetPolicies()
		{
			XmlDocument policiesXml = base.ListPolicies();
			
			List<NessusPolicy> policies = new List<NessusPolicy>();
			
			foreach (XmlNode node in policiesXml.LastChild) //first child is XmlDeclaration
			{
				if (node.Name == "contents")
				{
					foreach (XmlNode child in node.FirstChild.ChildNodes)
					{
						policies.Add(new NessusPolicy(child));
					}
				}
			}
			
			return policies;
		}
		
		public void Dispose()
		{}
	}
}

