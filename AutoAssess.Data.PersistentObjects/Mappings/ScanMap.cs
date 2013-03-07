using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class ScanMap : ClassMap<PersistentScan>
	{
		public ScanMap ()
		{
			Table("scan");
			
			Id(s => s.ID).Column("scanid").GeneratedBy.Assigned();
			
			Map(s => s.CreatedBy).Column("createdby");
			Map(s => s.CreatedOn).Column("createdon");
			Map(s => s.HasRun).Column("hasrun");
			Map(s => s.IsActive).Column("isactive");
			Map(s => s.LastModifiedBy).Column("lastmodifiedby");
			Map(s => s.LastModifiedOn).Column("lastmodifiedon");
			Map(s => s.NexposeSiteID).Column("nexposesiteid");
			Map(s => s.OpenVASReportID).Column("openvasreportid");
			Map(s => s.Name).Column("name");
			Map(s => s.MetasploitReportTaskID).Column("metasploitreporttaskid");
			Map (s => s.Duration).Column("duration");
			
			Map(s => s.ParentProfileID)
				.ReadOnly()
				.Access.None()
				.Column("profileid");
			
			References(s => s.User)
				.Column("userid");
			
			References(s => s.ParentProfile)
				.Column("profileid")
				.Cascade.SaveUpdate();
			
			References(s => s.NessusScan)
				.Column("nessusscanid")
				.Cascade.SaveUpdate();
			
			References(s => s.ScanOptions)
				.Column("scanoptionsid")
				.Cascade.SaveUpdate();
		}
	}
}

