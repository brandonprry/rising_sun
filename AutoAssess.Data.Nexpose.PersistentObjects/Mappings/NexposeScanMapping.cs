using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.Nexpose.PersistentObjects
{
	public class NexposeScanMapping : ClassMap<PersistentNexposeScan>
	{
		public NexposeScanMapping ()
		{
			Table("nexposescan");
			
			Id(s => s.ID)
				.Column("nexposescanid")
				.GeneratedBy.Assigned();
			
			Map (s => s.RemoteScanID).Column("remotescanid");
			Map (s => s.RemoteSiteID).Column("remotesiteid");
			Map (s => s.ParentScanID).Column("scanid");
			Map (s => s.CreatedBy).Column("createdby");
			Map (s => s.CreatedOn).Column("createdon");
			Map (s => s.IsActive).Column("isactive");
			Map (s => s.LastModifiedBy).Column("lastmodifiedby");
			Map (s => s.LastModifiedOn).Column("lastmodifiedon");
			Map (s => s.FullReport).Column("fullreport");
			Map (s => s.ReportType).Column("reporttype");
			
			HasMany (s => s.PersistentAssets)
				.KeyColumn("nexposescanid")
				.Cascade.All();
		}
	}
}

