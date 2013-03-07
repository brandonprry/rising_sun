using System;
using AutoAssess.Data.Nexpose.BusinessObjects;

namespace AutoAssess.Data.Nexpose.PersistentObjects
{
	[Serializable]
	public class PersistentNexposeServiceConfiguration : NexposeServiceConfiguration
	{
		public PersistentNexposeServiceConfiguration ()
		{
		}
		
		public PersistentNexposeServiceConfiguration(NexposeServiceConfiguration config)
		{
			this.Name = config.Name;
			this.Value = config.Value;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get;set;}
		
		public virtual void SetCreationInfo (Guid owner)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.IsActive = true;
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
		}
		
		public virtual void SetUpdateInfo (Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
		
	}
}

