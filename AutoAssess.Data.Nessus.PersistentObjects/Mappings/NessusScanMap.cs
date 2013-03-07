using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	public class NessusScanMap : ClassMap<PersistentNessusScan>
	{
		public NessusScanMap ()
		{
			Table("nessusscan");
			
			Id(s => s.ID).Column("nessusscanid")
				.GeneratedBy.Assigned();
			
			Map(s => s.CreatedBy).Column("createdby");
			Map(s => s.CreatedOn).Column("createdon");
			Map(s => s.IsActive).Column("isactive");
			Map(s => s.LastModifiedBy).Column("lastmodifiedby");
			Map(s => s.LastModifiedOn).Column("lastmodifiedon");
			Map(s => s.Name).Column("name");
			Map(s => s.Owner).Column("owner");
			Map(s => s.Range).Column("range");
			Map(s => s.RemoteScanID).Column("remotescanid");
			Map(s => s.StartTime).Column("starttime");
			Map(s => s.ParentScanID).Column("parentscanid");
			Map(s => s.UniqueReportNumber).Column("uniquereportno");
			
//			References(s => s.Report)
//				.Column("nessusreportid")
//				.Cascade.SaveUpdate();
			
			HasMany(s => s.PersistentHosts)
				.Table("nessusreporthost")
				.KeyColumn("nessusscanid")
				.Cascade.SaveUpdate();
		}
	}
}

