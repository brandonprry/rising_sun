using System;
using AutoAssess.Data.Nexpose.BusinessObjects;

namespace AutoAssess.Data.Nexpose.PersistentObjects
{
	[Serializable]
	public class PersistentNexposeServiceFingerprint : NexposeServiceFingerprint
	{
		public PersistentNexposeServiceFingerprint ()
		{
		}
		
		public PersistentNexposeServiceFingerprint(NexposeServiceFingerprint fp)
		{
			this.Certainty = fp.Certainty;
			this.Family = fp.Family;
			this.Product = fp.Product;
			this.Version = fp.Version;
			this.Vendor = fp.Vendor;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get;set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get;set ;}
		
		public virtual void SetCreationInfo (Guid owner)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo (Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

