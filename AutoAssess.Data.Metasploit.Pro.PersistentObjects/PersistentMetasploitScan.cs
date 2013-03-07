using System;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.Metasploit.Pro.PersistentObjects
{
	[Serializable]
	public class PersistentMetasploitScan : MetasploitScan
	{
		public PersistentMetasploitScan ()
		{
		}
		
		public PersistentMetasploitScan(MetasploitScan scan)
		{
			this.PersistentHosts = new List<PersistentMetasploitHost>();
			this.PersistentCredentials = new List<PersistentMetasploitCredential>();
			this.PersistentEvents = new List<PersistentMetasploitEvent>();
			this.PersistentModuleDetails = new List<PersistentMetasploitModuleDetails>();
			this.PersistentSessions = new List<PersistentMetasploitExploitSession>();
			this.PersistentWebForms = new List<PersistentMetasploitWebForm>();
			this.PersistentWebPages = new List<PersistentMetasploitWebPage>();
			this.PersistentWebsites = new List<PersistentMetasploitWebsite>();
			this.PersistentWebVulnerabilities = new List<PersistentMetasploitWebVulnerability>();
			
			if (scan.Hosts != null)
				foreach (var host in scan.Hosts)
					this.PersistentHosts.Add (new PersistentMetasploitHost(host));
			if (scan.Events != null)
				foreach (var evnt in scan.Events)
					this.PersistentEvents.Add (new PersistentMetasploitEvent(evnt));
			if (scan.ModuleDetails != null)
				foreach (var deets in scan.ModuleDetails)
					this.PersistentModuleDetails.Add (new PersistentMetasploitModuleDetails(deets));
			if (scan.Sessions != null)
				foreach (var session in scan.Sessions)
					this.PersistentSessions.Add(new PersistentMetasploitExploitSession(session));
			if (scan.WebForms != null)
				foreach (var form in scan.WebForms)
					this.PersistentWebForms.Add(new PersistentMetasploitWebForm(form));
			if (scan.WebPages != null)
				foreach (var page in scan.WebPages)
					this.PersistentWebPages.Add(new PersistentMetasploitWebPage(page));
			if (scan.Websites != null)
				foreach (var site in scan.Websites)
					this.PersistentWebsites.Add(new PersistentMetasploitWebsite(site));
			if (scan.WebVulnerabilities != null)
				foreach (var vuln in scan.WebVulnerabilities)
					this.PersistentWebVulnerabilities.Add(new PersistentMetasploitWebVulnerability(vuln));
			if (scan.Credentials != null)
				foreach (var cred in scan.Credentials)
					this.PersistentCredentials.Add(new PersistentMetasploitCredential(cred));
		}
		
		public virtual Guid ID { get; set; }
		public virtual Guid CreatedBy { get; set; }
		public virtual DateTime CreatedOn { get; set; }
		public virtual Guid LastModifiedBy { get; set; }
		public virtual DateTime LastModifiedOn { get; set; }
		public virtual bool IsActive { get; set; }
		
		public virtual IList<PersistentMetasploitHost> PersistentHosts { get; set; }
		
		public virtual IList<PersistentMetasploitEvent> PersistentEvents { get; set; }
		
		public virtual IList<PersistentMetasploitExploitSession> PersistentSessions { get; set; }
		
		public virtual IList<PersistentMetasploitWebsite> PersistentWebsites { get; set; }
		
		public virtual IList<PersistentMetasploitWebPage> PersistentWebPages { get; set; }
		
		public virtual IList<PersistentMetasploitWebForm> PersistentWebForms { get; set; }
		
		public virtual IList<PersistentMetasploitWebVulnerability> PersistentWebVulnerabilities { get; set; }
		
		public virtual IList<PersistentMetasploitModuleDetails> PersistentModuleDetails { get; set; }
		
		public virtual IList<PersistentMetasploitCredential> PersistentCredentials { get; set; }
		
		public virtual Guid ParentScanID { get; set; }
		
		public virtual void SetCreationInfo (Guid owner)
		{
			DateTime now = DateTime.Now;
		
			this.ID = Guid.NewGuid();
			
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
		
		public virtual void SetCreationInfo (Guid owner, bool recursive)	
		{
			this.SetCreationInfo(owner);
			
			if (recursive)
			{
				foreach (var host in this.PersistentHosts)
					host.SetCreationInfo(owner, true);
				foreach (var evnt in this.PersistentEvents)
					evnt.SetCreationInfo(owner);
				foreach (var sess in this.PersistentSessions)
					sess.SetCreationInfo(owner);
				foreach (var site in this.PersistentWebsites)
					site.SetCreationInfo(owner);
				foreach (var page in this.PersistentWebPages)
					page.SetCreationInfo(owner);
				foreach (var form in this.PersistentWebForms)
					form.SetCreationInfo(owner);
				foreach (var vuln in this.PersistentWebVulnerabilities)
					vuln.SetCreationInfo(owner);
				foreach (var deet in this.PersistentModuleDetails)
					deet.SetCreationInfo(owner, true);
				foreach (var cred in this.PersistentCredentials)
					cred.SetCreationInfo(owner);
			}
		}
		
		public virtual void SetUPdateInfo (Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

