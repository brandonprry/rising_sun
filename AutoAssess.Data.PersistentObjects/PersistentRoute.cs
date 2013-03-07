using System;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentRoute : Route, IEntity
	{
		public PersistentRoute ()
		{
		}
		
		public PersistentRoute (Route route)
		{
			this.FirstHostname = route.FirstHostname;
			this.FirstIPAddress = route.FirstIPAddress;
			this.FirstResult = route.FirstResult;
			this.Hop = route.Hop;
			this.SecondHostname = route.SecondHostname;
			this.SecondIPAddress = route.SecondIPAddress;
			this.SecondResult = route.SecondResult;
			this.ThirdHostname = route.ThirdHostname;
			this.ThirdIPAddress = route.ThirdIPAddress;
			this.ThirdResult = route.ThirdResult;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual PersistentTracerouteResults ParentResults
		{
			get { return base.ParentResults as PersistentTracerouteResults; }
			set { base.ParentResults = value as TracerouteToolResults; }
		}
		
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
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = isActive;
		}
		public virtual string ToPersistentXml()
		{
			return string.Empty;
		}
	}
}

