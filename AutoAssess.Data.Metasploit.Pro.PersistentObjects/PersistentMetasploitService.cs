using System;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;

namespace AutoAssess.Data.Metasploit.Pro.PersistentObjects
{
	[Serializable]
	public class PersistentMetasploitService : MetasploitService
	{
		public PersistentMetasploitService ()
		{
		}
		
		public PersistentMetasploitService (MetasploitService service)
		{
			this.Info = service.Info;
			this.Name = service.Name;
			this.Port = service.Port;
			this.Protocol = service.Protocol;
			this.RemoteCreatedAt = service.RemoteCreatedAt;
			this.RemoteHostID = service.RemoteHostID;
			this.RemoteID = service.RemoteID;
			this.RemoteUpdatedAt = service.RemoteUpdatedAt;
			this.State = service.State;
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

