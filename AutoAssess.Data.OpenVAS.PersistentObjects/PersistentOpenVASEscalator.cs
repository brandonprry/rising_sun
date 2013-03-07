using System;
using AutoAssess.Data.OpenVAS.BusinessObjects;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	[Serializable]
	public class PersistentOpenVASEscalator : OpenVASEscalator, IEntity
	{
		public PersistentOpenVASEscalator ()
		{
			//this.
		}
		public virtual PersistentEscalatorCondition Condition { get; set; }
		
		public virtual PersistentEscalatorEvent Event { get; set; }
		
		public virtual PersistentEscalatorMethod Method { get; set; }
		
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

