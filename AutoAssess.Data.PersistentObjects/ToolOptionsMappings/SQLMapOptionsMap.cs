using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class SQLMapOptionsMap : ClassMap<PersistentSQLMapOptions>
	{
		public SQLMapOptionsMap ()
		{
			Table("sqlmapoptions");
			
			Id(s => s.ID)
				.Column("sqlmapoptionsid")
				.GeneratedBy.Assigned();
			
			Map(s => s.CrawlLevel).Column("crawllevel");
			Map(s => s.CreatedBy).Column("createdby");
			Map(s => s.CreatedOn).Column("createdon");
			Map(s => s.DBMS).Column("dbms");
			Map(s => s.IsActive).Column("isactive");
			Map(s => s.LastModifiedBy).Column("lastmodifiedby");
			Map(s => s.LastModifiedOn).Column("lastmodifiedon");
			Map(s => s.Level).Column("level");
			Map(s => s.Port).Column("port");
			Map(s => s.Risk).Column("risk");
			Map(s => s.TestForms).Column("testforms");
			Map(s => s.Threads).Column("threads");
			Map(s => s.URL).Column("url");
			
		}
	}
}

