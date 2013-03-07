using System;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;

namespace AutoAssess.Data.Metasploit.Pro.PersistentObjects
{
	[Serializable]
	public class PersistentMetasploitCredential : MetasploitCredential
	{
		public PersistentMetasploitCredential ()
		{
		}
		
		public PersistentMetasploitCredential(MetasploitCredential cred)
		{
			this.Password = cred.Password;
			this.PasswordType = cred.PasswordType;
			this.Port = cred.Port;
			this.Proof = cred.Proof;
			this.RemoteCreatedAt = cred.RemoteCreatedAt;
			this.RemoteIsActive = cred.RemoteIsActive;
			this.RemoteUpdatedAt = cred.RemoteUpdatedAt;
			this.ServiceName = cred.ServiceName;
			this.SourceID = cred.SourceID;
			this.SourceType = cred.SourceType;
			this.Username = cred.Username;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo(Guid owner)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

