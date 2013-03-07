using System;
using AutoAssess.Data.Nessus.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	[Serializable]
	public class PersistentNessusReportHost : NessusReportHost
	{
		public PersistentNessusReportHost ()
		{
		}
		
		public PersistentNessusReportHost (NessusReportHost host)
		{
			this.PersistentHostProperties= new PersistentNessusHostProperties(host.HostProperties);
			
			this.PersistentReportItems = new List<PersistentNessusReportItem>();
			foreach (NessusReportItem item in host.ReportItems)
				this.PersistentReportItems.Add(new PersistentNessusReportItem(item));
		}
		
		public virtual IList<PersistentNessusReportItem> PersistentReportItems { get; set; }
		
		public virtual PersistentNessusHostProperties PersistentHostProperties { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo(Guid owner)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.IsActive = true;
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
		}
		
		public virtual void SetCreationInfo (Guid owner, bool recursive)
		{
			this.SetCreationInfo(owner);
			
			if (recursive)
			{
				if (this.PersistentHostProperties != null)
					this.PersistentHostProperties.SetCreationInfo(owner);
				
				foreach (var item in this.PersistentReportItems)
					item.SetCreationInfo(owner);
			}
		}
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

