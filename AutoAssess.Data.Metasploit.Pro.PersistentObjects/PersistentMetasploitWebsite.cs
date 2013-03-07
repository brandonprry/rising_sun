using System;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;

namespace AutoAssess.Data.Metasploit.Pro.PersistentObjects
{
	[Serializable]
	public class PersistentMetasploitWebsite : MetasploitWebsite
	{
		public PersistentMetasploitWebsite ()
		{
		}
		
		public PersistentMetasploitWebsite (MetasploitWebsite site)
		{
			this.Host = site.Host;
			this.Options = site.Options;
			this.Port = site.Port;
			this.RemoteCreatedAt = site.RemoteCreatedAt;
			this.RemoteID = site.RemoteID;
			this.RemoteServiceID = site.RemoteServiceID;
			this.RemoteUpdatedAt = site.RemoteUpdatedAt;
			this.VirtualHost = site.VirtualHost;
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

