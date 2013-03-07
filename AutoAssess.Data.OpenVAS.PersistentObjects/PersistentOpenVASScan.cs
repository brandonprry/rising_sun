using System;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	[Serializable]
	public class PersistentOpenVASScan : OpenVASScan, IEntity
	{
		public PersistentOpenVASScan ()
		{
		}
		
		public PersistentOpenVASScan (OpenVASScan scan)
		{
			this.RemoteReportID = scan.RemoteReportID;
			
			if (scan.ResultCount != null)
				this.PersistentResultCount = new PersistentResultCount(scan.ResultCount);
			
			this.PersistentFilters = new List<PersistentReportFilter>();
			this.PersistentResults = new List<PersistentReportResult>();
			this.PersistentPorts = new List<PersistentReportPort>();
			
			if (scan.Filters != null)
				foreach (var filter in scan.Filters)
					this.PersistentFilters.Add (new PersistentReportFilter(filter));
			
			if (scan.Results != null)
				foreach (var result in scan.Results)
					this.PersistentResults.Add(new PersistentReportResult(result));
			
			if (scan.Ports != null)
				foreach (var port in scan.Ports)
					this.PersistentPorts.Add(new PersistentReportPort(port));
		}
		
		public virtual Guid ParentScanID { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual string FullReport { get; set; }
		
		public virtual string ReportType { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentResultCount PersistentResultCount { get; set; }
		
		public virtual IList<PersistentReportFilter> PersistentFilters { get; set; }
		
		public virtual IList<PersistentReportResult> PersistentResults { get; set; }
		
		public virtual IList<PersistentReportPort> PersistentPorts { get; set; }
		
		public virtual void SetCreationInfo (Guid userID)
		{
			this.ID = Guid.NewGuid();
			this.CreatedOn = DateTime.Now;
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
		}
		
		public virtual void SetCreationInfo (Guid owner, bool recursive)
		{
			this.SetCreationInfo(owner);
			
			
			if (this.PersistentResultCount != null)
				this.PersistentResultCount.SetCreationInfo(owner);
			
			foreach (var filter in this.PersistentFilters)
				filter.SetCreationInfo(owner);
			foreach (var result in this.PersistentResults)
			{
				result.PersistentNVT.SetCreationInfo(owner);
				result.SetCreationInfo(owner);
			}
			foreach (var port in this.PersistentPorts)
				port.SetCreationInfo(owner);
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = isActive;
		}
	}
}

