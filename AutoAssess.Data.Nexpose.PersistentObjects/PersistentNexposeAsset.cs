using System;
using AutoAssess.Data.Nexpose.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.Nexpose.PersistentObjects
{
	[Serializable]
	public class PersistentNexposeAsset : NexposeAsset
	{
		public PersistentNexposeAsset ()
		{
		}
		
		public PersistentNexposeAsset(NexposeAsset asset)
		{
			this.IPAddressV4 = asset.IPAddressV4;
			this.RemoteDeviceID = asset.RemoteDeviceID;
			this.RiskScore = asset.RiskScore;
			this.ScanTemplate = asset.ScanTemplate;
			this.SiteImportance = asset.SiteImportance;
			this.SiteName = asset.SiteName;
			this.Status = asset.Status;
		
			this.PersistentFingerprints = new List<PersistentNexposeHostFingerprint>();
			this.PersistentNames = new List<PersistentNexposeHostName>();
			this.PersistentServices = new List<PersistentNexposeHostService>();
			this.PersistentHostTests = new List<PersistentNexposeHostTest>();
			
			foreach (var fp in asset.Fingerprints)
				this.PersistentFingerprints.Add(new PersistentNexposeHostFingerprint(fp));
			
			foreach (var name in asset.Names)
				this.PersistentNames.Add(new PersistentNexposeHostName(name));
			
			foreach (var service in asset.Services)
				this.PersistentServices.Add(new PersistentNexposeHostService(service));
			
			foreach (var test in asset.HostTests)
				this.PersistentHostTests.Add(new PersistentNexposeHostTest(test));
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual IList<PersistentNexposeHostFingerprint> PersistentFingerprints { get; set; }
		
		public virtual IList<PersistentNexposeHostName> PersistentNames { get; set; }
		
		public virtual IList<PersistentNexposeHostService> PersistentServices { get; set; }
		
		public virtual IList<PersistentNexposeHostTest> PersistentHostTests { get; set; }
		
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

