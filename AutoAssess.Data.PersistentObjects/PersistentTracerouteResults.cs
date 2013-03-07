using System;
using System.Collections.Generic;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentTracerouteResults : TracerouteToolResults, IEntity
	{
		public PersistentTracerouteResults ()
		{
		}
		
		public PersistentTracerouteResults (TracerouteToolResults results)
		{
			this.PersistentRoutes = new List<PersistentRoute>();
			
			foreach (var route in results.Routes)
			{
				PersistentRoute pr = new PersistentRoute(route);
				pr.SetCreationInfo(Guid.Empty);
				this.PersistentRoutes.Add(pr);
			}
			
			this.FullOutput = results.FullOutput;
		}
		
		public virtual Guid NMapHostID { get; set;}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
		public virtual PersistentNMapHost ParentNMapHost { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual IList<PersistentRoute> PersistentRoutes { get; set; }
		
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

