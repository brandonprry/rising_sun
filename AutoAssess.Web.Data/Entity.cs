using System;

namespace AutoAssess.Web.Data
{
	[Serializable]
	public abstract class Entity
	{
		public Entity ()
		{
		}
		
		public virtual Guid ID { get;set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo(Guid userID)
		{
			this.ID = Guid.NewGuid();
			this.CreatedBy = userID;
			this.CreatedOn = DateTime.Now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.IsActive = isActive;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

