using System;
using AutoAssess.Data.OpenVAS.BusinessObjects;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	[Serializable]
	public class PersistentReportResult : ReportResult
	{
		public PersistentReportResult ()
		{
		}
		
		public PersistentReportResult(ReportResult result)
		{
			this.PersistentNVT = new PersistentReportNVT(result.NVT);
			this.Description = result.Description;
			this.Host = result.Host;
			this.Port = result.Port;
			this.Subnet = result.Subnet;
			this.Threat = result.Threat;
		}
		
		public virtual PersistentReportNVT PersistentNVT { get; set; }
		
		public virtual Guid ID  { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
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
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

