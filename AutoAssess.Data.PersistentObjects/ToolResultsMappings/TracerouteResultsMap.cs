using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class TracerouteResultsMap : ClassMap<PersistentTracerouteResults>
	{
		public TracerouteResultsMap ()
		{
			Table("tracerouteresult");
			
			Id(t => t.ID).Column("tracerouteresultid").GeneratedBy.Assigned();
			
			Map(t => t.CreatedBy).Column("createdby");
			Map(t => t.CreatedOn).Column("createdon");
			Map(t => t.FullOutput).Column("fulloutput");
			Map(t => t.IsActive).Column("isactive");
			Map(t => t.LastModifiedBy).Column("lastmodifiedby");
			Map(t => t.LastModifiedOn).Column("lastmodifiedon");
			
			Map(t => t.NMapHostID)
				.Column("nmaphostid")
				.ReadOnly();
			
			References(t => t.ParentProfile)
				.Column("profileid");
			
			References(t => t.ParentNMapHost)
				.Column("nmaphostid");
			
			References(t => t.User)
				.Column("userid");
			
			HasMany<PersistentRoute>(r => r.PersistentRoutes)
				.Table("tracerouteroute")
				.KeyColumn("tracerouteresultid")
				.Cascade.SaveUpdate();
				
		}
	}
}

