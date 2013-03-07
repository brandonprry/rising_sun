using System;
using System.Collections.Generic;
using AutoAssess.Data.Nessus.BusinessObjects;

 namespace AutoAssess.Data.Nessus.PersistentObjects
{
	[Serializable]
	public class PersistentNessusPluginFamily : NessusPluginFamily, IEntity
	{
		public PersistentNessusPluginFamily ()
		{
		}
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual IList<PersistentNessusPlugin> ChildPlugins
		{
			get { return base.ChildPlugins as IList<PersistentNessusPlugin>; }
			set { base.ChildPlugins = value as IList<NessusPlugin>; }
		}
		
		public virtual void SetCreationInfo(Guid userID)
		{
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.CreatedOn = DateTime.Now;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
			this.ID = Guid.NewGuid();
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.IsActive = IsActive;
			
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

