using System;
using AutoAssess.Data.OpenVAS.BusinessObjects;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	[Serializable]
	public class PersistentReportFilter : ReportFilter
	{
		public PersistentReportFilter ()
		{
		}
		
		public PersistentReportFilter(ReportFilter filter)
		{
			this.MainCVSSBaseScore = filter.MainCVSSBaseScore;
			this.Notes = filter.Notes;
			this.Overrides = filter.Overrides;
			this.Phrase = filter.Phrase;
			this.ResultHostsOnly = filter.ResultHostsOnly;
			this.ApplyOverrides = filter.ApplyOverrides;
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
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

