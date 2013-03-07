using System;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitScan
	{
		public MetasploitScan ()
		{
		}
		
		public MetasploitScan(XmlNode report)
		{
			XmlNode parent = null;
			
			if (report.ChildNodes.Count == 2) //includes <?xml version="1.0" encoding="UTF-8"?>
				parent = report.LastChild;
			else
				parent = report.FirstChild;
			
			foreach (XmlNode child in report.ChildNodes)
			{
				if (child.Name == "hosts")
				{
					this.Hosts = new List<MetasploitHost>();
					foreach (XmlNode host in child.ChildNodes)
						this.Hosts.Add(new MetasploitHost(host));
				}
				else if (child.Name == "events")
				{
					this.Events = new List<MetasploitEvent>();
					foreach (XmlNode evnt in child.ChildNodes) //event is C# keyword
						this.Events.Add(new MetasploitEvent(evnt));
				}
				else if (child.Name == "credentials")
				{
					this.Credentials = new List<MetasploitCredential>();
					foreach (XmlNode cred in child.ChildNodes)
						this.Credentials.Add(new MetasploitCredential(cred));
				}
				else if (child.Name == "web_sites")
				{
					this.Websites = new List<MetasploitWebsite>();
					foreach (XmlNode site in child.ChildNodes)
						this.Websites.Add(new MetasploitWebsite(site));
				}
				else if (child.Name == "web_pages")
				{
					this.WebPages = new List<MetasploitWebpage>();
					foreach (XmlNode page in child.ChildNodes)
						this.WebPages.Add(new MetasploitWebpage(page));
				}
				else if (child.Name == "web_forms")
				{
					this.WebForms = new List<MetasploitWebForm>();
					foreach (XmlNode form in child.ChildNodes)
						this.WebForms.Add(new MetasploitWebForm(form));
				}
				else if (child.Name == "web_vulns")
				{
					this.WebVulnerabilities = new List<MetasploitWebVulnerability>();
					foreach (XmlNode vuln in child.ChildNodes)
						this.WebVulnerabilities.Add(new MetasploitWebVulnerability(vuln));
				}
				else if (child.Name == "module_details")
				{
					this.ModuleDetails = new List<MetasploitModuleDetails>();
					foreach (XmlNode deets in child.ChildNodes)
						this.ModuleDetails.Add(new MetasploitModuleDetails(deets));
				}
			}
		}
		
		
		
		public virtual IList<MetasploitHost> Hosts { get; set; }
		
		public virtual IList<MetasploitEvent> Events { get; set; }
		
		public virtual IList<MetasploitExploitSession> Sessions { get; set; }
		
		public virtual IList<MetasploitWebsite> Websites { get; set; }
		
		public virtual IList<MetasploitWebpage> WebPages { get; set; }
		
		public virtual IList<MetasploitWebForm> WebForms { get; set; }
		
		public virtual IList<MetasploitWebVulnerability> WebVulnerabilities { get; set; }
		
		public virtual IList<MetasploitModuleDetails> ModuleDetails { get; set; }
		
		public virtual IList<MetasploitCredential> Credentials { get; set; }
	}
}

