using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class NiktoResultsMap : ClassMap<PersistentNiktoResults>
	{
		public NiktoResultsMap ()
		{
			Table("niktoresult");
			
			Id(n => n.ID).Column("niktoresultid")
				.GeneratedBy.Assigned();
			
			Map(n => n.CreatedBy).Column("createdby");
			Map(n => n.CreatedOn).Column("createdon");
			Map(n => n.FullOutput).Column("fulloutput");
			Map(n => n.HostPortID).Column("hostportid");
			Map(n => n.IsActive).Column("isactive");
			Map(n => n.LastModifiedBy).Column("lastmodifiedby");
			Map(n => n.LastModifiedOn).Column("lastmodifiedon");
			
			HasMany(r => r.Items)
				.KeyColumn("niktoresultid");
			
			References(n => n.ParentProfile)
				.Column("profileid");
			References(n => n.User)
				.Column("userid");
		}
	}
}

