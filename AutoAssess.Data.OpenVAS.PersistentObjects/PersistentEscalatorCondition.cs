using System;
using AutoAssess.Data.OpenVAS.BusinessObjects;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	[Serializable]
	public class PersistentEscalatorCondition : EscalatorCondition, IEntity 
	{
		public PersistentEscalatorCondition ()
		{
		}
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo (Guid userID)
		{
			this.CreatedOn = DateTime.Now;
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = isActive;
		}
	}
}

