using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASScanMap : ClassMap<PersistentOpenVASScan>
	{
		public OpenVASScanMap ()
		{
			Table("openvasscan");
			
			Id(r => r.ID).Column("openvasscanid")
				.GeneratedBy.Assigned();
			
			
			Map(r => r.CreatedBy).Column("createdby");
			Map(r => r.CreatedOn).Column("createdon");
			Map(r => r.IsActive).Column("isactive");
			Map(r => r.LastModifiedBy).Column("lastmodifiedby");
			Map(r => r.LastModifiedOn).Column("lastmodifiedon");
			Map(r => r.RemoteReportID).Column("remotereportid");
			//Map(r => r.FullReport).Column("fullreport");
			//Map(r => r.ReportType).Column("reporttype");
			Map (r => r.ScanStart).Column("scanstart");
			Map (r => r.ParentScanID).Column("scanid");
			
			HasMany(c => c.PersistentFilters)
				.KeyColumn("openvasscanid")
				.Cascade.All();
			
			HasMany(c => c.PersistentResults)
				.KeyColumn("openvasscanid")
				.Cascade.All();
			
			HasMany(c => c.PersistentPorts)
				.KeyColumn("openvasscanid")
				.Cascade.All();
		}
	}
}

