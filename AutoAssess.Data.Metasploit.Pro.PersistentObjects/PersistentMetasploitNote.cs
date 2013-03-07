using System;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;

namespace AutoAssess.Data.Metasploit.Pro.PersistentObjects
{
	[Serializable]
	public class PersistentMetasploitNote : MetasploitNote
	{
		public PersistentMetasploitNote ()
		{
		}
		
		public PersistentMetasploitNote(MetasploitNote note)
		{
			this.Data = note.Data;
			this.NoteType = note.NoteType;
			this.RemoteCreatedAt = note.RemoteCreatedAt;
			this.RemoteHostID = note.RemoteHostID;
			this.RemoteID = note.RemoteID;
			this.RemoteServiceID = note.RemoteServiceID;
			this.RemoteUpdatedAt = note.RemoteUpdatedAt;
			this.RemoteWorkspaceID = note.RemoteWorkspaceID;
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

