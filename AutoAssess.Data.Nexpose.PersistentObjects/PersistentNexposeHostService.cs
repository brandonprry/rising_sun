using System;
using AutoAssess.Data.Nexpose.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.Nexpose.PersistentObjects
{
	[Serializable]
	public class PersistentNexposeHostService : NexposeHostService
	{
		public PersistentNexposeHostService ()
		{
			
		}
		
		public PersistentNexposeHostService(NexposeHostService service)
		{
			this.Name = service.Name;
			this.Port = service.Port;
			this.Protocol = service.Protocol;
			this.Status = service.Status;
			
			this.PersistentConfigurations = new List<PersistentNexposeServiceConfiguration>();
			this.PersistentFingerprints = new List<PersistentNexposeServiceFingerprint>();
			this.PersistentTests = new List<PersistentNexposeServiceTest>();
			
			if (service.Configurations != null)
				foreach (var config in service.Configurations)
					this.PersistentConfigurations.Add(new PersistentNexposeServiceConfiguration(config));
			
			if (service.Fingerprints != null)
				foreach (var fp in service.Fingerprints)
					this.PersistentFingerprints.Add(new PersistentNexposeServiceFingerprint(fp));
			
			if (service.ServiceTests != null)
				foreach (var test in service.ServiceTests)
					this.PersistentTests.Add(new PersistentNexposeServiceTest(test));
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get;set ;}
		
		public virtual IList<PersistentNexposeServiceConfiguration> PersistentConfigurations { get; set; }
		
		public virtual IList<PersistentNexposeServiceFingerprint> PersistentFingerprints { get; set; }
		
		public virtual IList<PersistentNexposeServiceTest> PersistentTests { get; set; }
		
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
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

