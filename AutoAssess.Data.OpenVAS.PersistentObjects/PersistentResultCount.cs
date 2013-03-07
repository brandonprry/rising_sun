using System;
using AutoAssess.Data.OpenVAS.BusinessObjects;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	[Serializable]
	public class PersistentResultCount : ResultCount
	{
		public PersistentResultCount ()
		{
		}
		
		public PersistentResultCount(ResultCount rc)
		{
			this.Filtered = rc.Filtered;
			this.FilteredDebug = rc.FilteredDebug;
			this.FilteredHoles = rc.FilteredHoles;
			this.FilteredInfo = rc.FilteredInfo;
			this.FilteredLog = rc.FilteredLog;
			this.FilteredWarning = rc.FilteredWarning;
			this.Full = rc.Full;
			this.FullDebug = rc.FullDebug;
			this.FullHoles = rc.FullHoles;
			this.FullInfo = rc.FullInfo;
			this.FullLog = rc.FullLog;
			this.FullWarning = rc.FullWarning;
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
		
		public virtual void SetUpdateInfo (Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

