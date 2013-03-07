using System;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.Metasploit.Pro.PersistentObjects
{
	[Serializable]
	public class PersistentMetasploitHost : MetasploitHost
	{
		public PersistentMetasploitHost ()
		{
		}
		
		public PersistentMetasploitHost(MetasploitHost host)
		{
			this.Address = host.Address;
			this.Comm = host.Comm;
			this.ExploitAttemptCount = host.ExploitAttemptCount;
			this.HostDetailCount = host.HostDetailCount;
			this.Info = host.Info;
			this.MAC = host.MAC;
			this.Name = host.Name;
			this.NoteCount = host.NoteCount;
			this.OSArchitecture = host.OSArchitecture;
			this.OSFlavor = host.OSFlavor;
			this.OSLanguage = host.OSLanguage;
			this.OSName = host.OSName;
			this.OSServicePack = host.OSServicePack;
			this.Purpose = host.Purpose;
			this.RemoteCreatedAt = host.RemoteCreatedAt;
			this.RemoteID = host.RemoteID;
			this.RemoteUpdatedAt = host.RemoteUpdatedAt;
			this.RemoteWorkspaceID = host.RemoteWorkspaceID;
			this.ServiceCount = host.ServiceCount;
			this.State = host.State;
			this.VulnCount = host.VulnCount;
			
			this.PersistentDetails = new List<PersistentMetasploitHostDetails>();
			this.PersistentExploitAttempts = new List<PersistentMetasploitExploitAttempt>();
			this.PersistentServices = new List<PersistentMetasploitService>();
			this.PersistentNotes = new List<PersistentMetasploitNote>();
			this.PersistentTags = new List<PersistentMetasploitTag>();
			this.PersistentVulnerabilities = new List<PersistentMetasploitVulnerability>();
			this.PersistentCredentials = new List<PersistentMetasploitCredential>();
			this.PersistentSessions = new List<PersistentMetasploitExploitSession>();
			
			foreach (var deets in host.Details)
				this.PersistentDetails.Add(new PersistentMetasploitHostDetails(deets));
			foreach (var attempt in host.ExploitAttempts)
				this.PersistentExploitAttempts.Add(new PersistentMetasploitExploitAttempt(attempt));
			foreach (var service in host.Services)
				this.PersistentServices.Add(new PersistentMetasploitService(service));
			foreach (var note in host.Notes)
				this.PersistentNotes.Add(new PersistentMetasploitNote(note));
			foreach (var tag in host.Tags)
				this.PersistentTags.Add(new PersistentMetasploitTag(tag));
			foreach (var vuln in host.Vulnerabilities)
				this.PersistentVulnerabilities.Add(new PersistentMetasploitVulnerability(vuln));
			foreach (var cred in host.Credentials)
				this.PersistentCredentials.Add(new PersistentMetasploitCredential(cred));
			foreach (var session in host.Sessions)
				this.PersistentSessions.Add(new PersistentMetasploitExploitSession(session));
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual IList<PersistentMetasploitHostDetails> PersistentDetails { get; set; }
		
		public virtual IList<PersistentMetasploitExploitAttempt> PersistentExploitAttempts { get; set; }
		
		public virtual IList<PersistentMetasploitService> PersistentServices { get; set; }
		
		public virtual IList<PersistentMetasploitNote> PersistentNotes { get; set; }
		
		public virtual IList<PersistentMetasploitTag> PersistentTags { get; set; }
		
		public virtual IList<PersistentMetasploitVulnerability> PersistentVulnerabilities { get; set; }
	
		public virtual IList<PersistentMetasploitCredential> PersistentCredentials { get; set; }
		
		public virtual IList<PersistentMetasploitExploitSession> PersistentSessions { get; set; }
		
		public virtual void SetCreationInfo (Guid owner)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedOn = now;
			this.LastModifiedBy = owner;
			this.IsActive = true;
		}
		
		public virtual void SetCreationInfo(Guid owner, bool recursive)
		{
			this.SetCreationInfo(owner);
			
			if (recursive)	
			{
				foreach (var deet in this.PersistentDetails)
					deet.SetCreationInfo(owner);
				foreach (var attempt in this.PersistentExploitAttempts)
					attempt.SetCreationInfo(owner);
				foreach (var service in this.PersistentServices)
					service.SetCreationInfo(owner);
				foreach (var note in this.PersistentNotes)
					note.SetCreationInfo(owner);
				foreach( var tag in this.PersistentTags)
					tag.SetCreationInfo(owner);
				foreach (var vuln in this.PersistentVulnerabilities)
					vuln.SetCreationInfo(owner, true);
				foreach (var cred in this.PersistentCredentials)
					cred.SetCreationInfo(owner);
				foreach(var sess in this.PersistentSessions)
					sess.SetCreationInfo(owner);
			}
		}
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

