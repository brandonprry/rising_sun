using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class NMapResultsMap : ClassMap<PersistentNMapResults>
	{
		public NMapResultsMap ()
		{
			Table("nmapresult");
			
			Id(n => n.ID).Column("nmapresultid").GeneratedBy.Assigned();
			
			Map(n => n.FullOutput).Column("fulloutput");
			Map(n => n.CreatedBy).Column("createdby");
			Map(n => n.CreatedOn).Column("createdon");
			Map(n => n.IsActive).Column("isactive");
			Map(n => n.LastModifiedBy).Column("lastmodifiedby");
			Map(n => n.LastModifiedOn).Column("lastmodifiedon");
			
			References(n => n.ParentProfile)
				.Column("profileid");
			
			References(n => n.User)
				.Column("userid");
			
			HasMany(h => h.PersistentHosts)
				.KeyColumn("nmapresultid")
				.Table("nmaphost")
				.Cascade.All()
				.AsBag();
		}
	}
}

