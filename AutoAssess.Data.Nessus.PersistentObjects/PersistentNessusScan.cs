using System;
using AutoAssess.Data.Nessus.BusinessObjects;
using System.Xml;
using System.Collections.Generic;

 namespace AutoAssess.Data.Nessus.PersistentObjects
{
	[Serializable]
	public class PersistentNessusScan : NessusScan, IEntity
	{
		public PersistentNessusScan ()
		{
		}
		
		public PersistentNessusScan(NessusScan scan)
		{
			this.Name = scan.Name;
			this.Owner = scan.Owner;
			this.Range = scan.Range;
			this.RemoteScanID = scan.RemoteScanID;
			this.StartTime = scan.StartTime;
			this.UniqueReportNumber = scan.UniqueReportNumber;
			
			this.PersistentHosts = new List<PersistentNessusReportHost>();
			foreach (var host in scan.Hosts)
				this.PersistentHosts.Add(new PersistentNessusReportHost(host));

		}
		
		public virtual IList<PersistentNessusReportHost> PersistentHosts { get; set; }
		public virtual Guid CreatedBy { get; set; }
		public virtual Guid ID { get; set; }
		public virtual Guid LastModifiedBy { get; set; }
		public virtual DateTime CreatedOn { get; set; }
		public virtual DateTime LastModifiedOn { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual Guid ParentScanID { get; set; }
		
		
		public virtual void SetCreationInfo(Guid userID)
		{
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.CreatedOn = DateTime.Now;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
			this.ID = Guid.NewGuid();
		}
		
		public virtual void SetCreationInfo(Guid owner, bool recursive)
		{
			this.SetCreationInfo(owner);
			
			if (recursive)
			{
				foreach (var host in this.PersistentHosts)
					host.SetCreationInfo(owner, true);
			}
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.IsActive = IsActive;
			
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
		}
		
		public virtual string ToPersistentXml()
		{
			string xml = "<nessusScan>";
			
//			xml = xml + "<id>" + this.ID + "</id>";
//			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
//			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
//			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
//			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
//			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
//			
//			if (this.Report != null)
//				xml = xml + this.Report.ToPersistentXml();
//			
//			xml = xml + "<range>" + this.Range + "</range>";
//			xml = xml + "<readableName>" + this.Name + "</readableName>";
//			xml = xml + "<owner>" + this.Owner + "</owner>";
//			
//			xml = xml + "<startTime>" + this.StartTime.ToLongDateString() + "</startTime>";
//			
//			xml = xml + "</nessusScan>";
//			
			return xml;
		}
	}
}

