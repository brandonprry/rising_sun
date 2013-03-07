using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	public class NessusReportMap : ClassMap<PersistentNessusReport>
	{
		public NessusReportMap ()
		{
			Table("nessusreport");
			
			Id(r => r.ID).Column("nessusreportid").GeneratedBy.Assigned();
			
			Map(r => r.CreatedBy).Column("createdby");
			Map(r => r.CreatedOn).Column("createdon");
			Map(r => r.IsActive).Column("isactive");
			Map(r => r.LastModifiedBy).Column("lastmodifiedby");
			Map(r => r.LastModifiedOn).Column("lastmodifiedon");
			Map(r => r.ReadableName).Column("readablename");
			Map(r => r.RemoteReportID).Column("remotereportid");
			Map(r => r.Status).Column("status");
			Map(r => r.TimeStamp).Column("ts");
			Map(r => r.FullReport).Column("fullreport");
			Map(r => r.ReportType).Column("reporttype");
			
			References(r => r.ParentScan)
				.Column("parentnessusscanid");
		}
	}
}

