using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class SQLMapResultsMap : ClassMap<PersistentSQLMapResults>
	{
		public SQLMapResultsMap ()
		{
			Table("sqlmapresult");
			
			Id(r => r.ID).Column("sqlmapresultid").GeneratedBy.Assigned();
			
			Map(s => s.CreatedBy).Column("createdby");
			Map(s => s.CreatedOn).Column("createdon");
			Map(s => s.FullOutput).Column("fulloutput");
			Map(s => s.Log).Column("log");
			Map(s => s.IsActive).Column("isactive");
			Map(s => s.LastModifiedBy).Column("lastmodifiedby");
			Map(s => s.LastModifiedOn).Column("lastmodifiedon");
			
			Map(s => s.ParentHostPortID)
				.Access.None()
				.ReadOnly()
				.Column("hostportid");
			
			References(s => s.ParentScan)
				.Column("scanid");
			
			References(s => s.User)
				.Column("userid");
			
			References(r => r.ParentHostPort)
				.Column("hostportid");
			
			HasMany(s => s.PersistentVulnerabilities)
				.KeyColumn("sqlmapresultid")
				.Table("sqlmapresultchildren")
				.Cascade.SaveUpdate();
			
		}
	}
}

