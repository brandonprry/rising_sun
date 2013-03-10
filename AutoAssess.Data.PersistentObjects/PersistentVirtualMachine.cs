using System;
using AutoAssess.Data.Virtualbox;

namespace AutoAssess.Data.PersistentObjects
{
	public class PersistentVirtualMachine : VirtualMachine
	{
		public PersistentVirtualMachine ()
		{
		}
		
		public PersistentVirtualMachine(VirtualMachine machine)
		{
			this.Name = machine.Name;
			this.Guid = machine.Guid;
			this.Username = machine.Username;
			this.Password = machine.Password;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
		public virtual void SetCreationInfo(Guid userID)
		{
			DateTime now  = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedBy = userID;
			this.CreatedOn = now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
}

