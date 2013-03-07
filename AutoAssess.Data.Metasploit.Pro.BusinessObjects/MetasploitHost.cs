using System;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitHost
	{
		public MetasploitHost()
		{
		}
		
		public MetasploitHost (XmlNode doc)
		{
			foreach (XmlNode child in doc.ChildNodes)
			{
				if (child.Name == "id")
					this.RemoteID = int.Parse(child.InnerText);
				else if (child.Name == "created-at")
					this.RemoteCreatedAt = child.InnerText;
				else if (child.Name == "address")
					this.Address = child.InnerText;
				else if (child.Name == "mac")
				{}
				else if (child.Name == "comm")
				{}
				else if (child.Name == "name")
					this.Name = child.InnerText;
				else if (child.Name == "state")
					this.State = child.InnerText;
				else if (child.Name == "os-name")
					this.OSName = child.InnerText;
				else if (child.Name == "os-flavor")
					this.OSFlavor = child.InnerText;
				else if (child.Name == "os-sp")
					this.OSName = child.InnerText;
				else if (child.Name == "os-lang")
					this.OSLanguage = child.InnerText;
				else if (child.Name == "arch")
					this.OSArchitecture = child.InnerText;
				else if (child.Name == "workspace-id")
					this.RemoteWorkspaceID = int.Parse(child.InnerText);
				else if (child.Name == "updated-at")
					this.RemoteUpdatedAt = child.InnerText;
				else if (child.Name == "purpose")
					this.Purpose = child.InnerText;
				else if (child.Name == "info")
				{}
				else if (child.Name == "comments")
				{}
				else if (child.Name == "scope")
				{}
				else if (child.Name == "virtual-host")
				{}
				else if (child.Name == "host_details")
				{
					this.Details = new List<MetasploitHostDetail>();
					foreach (XmlNode deets in child.ChildNodes)
						this.Details.Add(new MetasploitHostDetail(deets));
				}
				else if (child.Name == "exploit_attempts")
				{
					this.ExploitAttempts = new List<MetasploitExploitAttempt>();
					foreach (XmlNode attempt in child.ChildNodes)
						this.ExploitAttempts.Add(new MetasploitExploitAttempt(attempt));
				}
				else if (child.Name == "services")
				{
					this.Services = new List<MetasploitService>();
					foreach (XmlNode service in child.ChildNodes)
						this.Services.Add(new MetasploitService(service));
				}
				else if (child.Name == "notes")
				{
					this.Notes = new List<MetasploitNote>();
					foreach(XmlNode note in child.ChildNodes)
						this.Notes.Add(new MetasploitNote(note));
				}
				else if (child.Name == "tags")
				{
					this.Tags = new List<MetasploitTag>();
				}
				else if (child.Name == "vulns")
				{
					this.Vulnerabilities = new List<MetasploitVulnerability>();
					foreach (XmlNode vuln in child.ChildNodes)
						this.Vulnerabilities.Add(new MetasploitVulnerability(vuln));
				}
				else if (child.Name == "creds")
				{
					this.Credentials = new List<MetasploitCredential>();
					foreach (XmlNode cred in child.ChildNodes)
						this.Credentials.Add(new MetasploitCredential(cred));
				}
				else if (child.Name == "sessions")
				{
					this.Sessions = new List<MetasploitExploitSession>();
					foreach (XmlNode session in child.ChildNodes)
						this.Sessions.Add(new MetasploitExploitSession(session));
				}
			}
		}
		
		public virtual int RemoteID { get; set; }
		
		public virtual string RemoteCreatedAt { get; set; }
		
		public virtual string Address { get; set; }
		
		public virtual string MAC { get; set; }
		
		public virtual string Comm { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string State { get; set; }
		
		public virtual string OSName { get; set; }
		
		public virtual string OSFlavor { get; set; }
		
		public virtual string OSServicePack { get; set; }
		
		public virtual string OSLanguage { get; set; }
		
		public virtual string OSArchitecture { get; set; }
		
		public virtual int RemoteWorkspaceID { get; set; }
		
		public virtual string RemoteUpdatedAt { get; set; }
		
		public virtual string Purpose { get; set; }
		
		public virtual string Info { get; set; }
		
		public virtual int NoteCount { get; set; }
		
		public virtual int VulnCount { get; set; }
		
		public virtual int ServiceCount { get; set; }
		
		public virtual int HostDetailCount { get; set; }
		
		public virtual int ExploitAttemptCount { get; set; }
		
		public virtual IList<MetasploitHostDetail> Details { get; set; }
		
		public virtual IList<MetasploitExploitAttempt> ExploitAttempts { get; set; }
		
		public virtual IList<MetasploitService> Services { get; set; }
		
		public virtual IList<MetasploitNote> Notes { get; set; }
		
		public virtual IList<MetasploitTag> Tags { get; set; }
		
		public virtual IList<MetasploitVulnerability> Vulnerabilities { get; set; }
	
		public virtual IList<MetasploitCredential> Credentials { get; set; }
		
		public virtual IList<MetasploitExploitSession> Sessions { get; set; }
	}
}

