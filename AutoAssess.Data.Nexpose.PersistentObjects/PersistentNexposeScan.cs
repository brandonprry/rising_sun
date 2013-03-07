using System;
using AutoAssess.Data.Nexpose.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.Nexpose.PersistentObjects
{
	[Serializable]
	public class PersistentNexposeScan : NexposeScan
	{
		public PersistentNexposeScan ()
		{
		}
		
		public PersistentNexposeScan(NexposeScan scan)
		{
			this.EndTime = scan.EndTime;
			this.Name = scan.Name;
			this.RemoteScanID = scan.RemoteScanID;
			this.RemoteSiteID = scan.RemoteSiteID;
			this.StartTime = scan.StartTime;
			this.Status = scan.Status;
			
			this.PersistentAssets = new  List<PersistentNexposeAsset>();
			foreach (NexposeAsset asset in scan.Assets)
				this.PersistentAssets.Add (new PersistentNexposeAsset(asset));
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid ParentScanID { get; set; }
		
		public virtual int RemoteScanID { get; set; }
		
		public virtual int RemoteSiteID { get; set; }
		
		public virtual string FullReport { get; set; }
		
		public virtual string ReportType { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual IList<PersistentNexposeAsset> PersistentAssets { get; set; }
		
		public virtual void SetCreationInfo (Guid owner)
		{
			DateTime now = DateTime.Now;
		
			this.ID = Guid.NewGuid();
			
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
		
		public virtual void SetCreationInfo(Guid owner, bool recursive)
		{
			this.SetCreationInfo(owner);
			
			if (recursive)
			{
				foreach (var asset in PersistentAssets)
				{
					asset.SetCreationInfo(owner);
					
					foreach (var name in asset.PersistentNames)
						name.SetCreationInfo(owner);
					foreach (var fp in asset.PersistentFingerprints)
						fp.SetCreationInfo(owner);
					foreach (var test in asset.PersistentHostTests)
						test.SetCreationInfo(owner);
					foreach (var service in asset.PersistentServices)
					{
						service.SetCreationInfo(owner);
						
						foreach (var test in service.PersistentTests)
							test.SetCreationInfo(owner);
						foreach (var config in service.PersistentConfigurations)
							config.SetCreationInfo(owner);
						foreach (var fp in service.PersistentFingerprints)
							fp.SetCreationInfo(owner);
					}
				}
			}
		}
	}
}

