using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class ScanOptionsMap : ClassMap<PersistentScanOptions>
	{
		public ScanOptionsMap ()	
		{
			Table("scanoptions");
			
			Id(so => so.ID).Column("scanoptionsid")
				.GeneratedBy.Assigned();
			
			Map(so => so.CreatedBy).Column("createdby");
			Map(so => so.CreatedOn).Column("createdon");
			Map(so => so.IsActive).Column("isactive");
			Map(so => so.IsBruteForce).Column("isbruteforce");
			Map(so => so.IsSQLMap).Column("issqlmap");
		    Map(so => so.IsDSXS).Column("isdsxs");
			Map (so => so.IsMetasploitAssessment).Column("ismetasploitassessment");
			Map (so => so.IsNessusAssessment).Column("isnessusassessment");
			Map (so => so.IsNexposeAssessment).Column("isnexposeassessment");
			Map (so => so.IsOpenVASAssessment).Column("isopenvasassessment");
			Map(so => so.LastModifiedBy).Column("lastmodifiedby");
			Map(so => so.LastModifiedOn).Column("lastmodifiedon");
			Map(so => so.RemoteNessusPolicyID).Column("remotenessuspolicyid");
			Map(so => so.RemoteOpenVASConfigID).Column("remoteopenvasconfigid");
			Map(so => so.RemoteNexposeSiteID).Column("remotenexposesiteid");
			Map(so => so.MetasploitDiscovers).Column("metasploitdiscovers");
			Map (so => so.MetasploitBruteforces).Column("metasploitbruteforces");
			
			References(so => so.User).Column("userid");
			References(so => so.ParentScan).Column("parentscanid");
			
			References(so => so.SQLMapOptions)
				.Column("sqlmapoptionsid")
				.Cascade.SaveUpdate();
			
		}
	}
}