using System;
using AutoAssess.Data.Nessus.BusinessObjects;

 namespace AutoAssess.Data.Nessus.PersistentObjects
{
	[Serializable]
	public class PersistentNessusPreference : NessusPreference, IEntity
	{
		public PersistentNessusPreference ()
		{
		}
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual Guid ID { get; set; }
		
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

