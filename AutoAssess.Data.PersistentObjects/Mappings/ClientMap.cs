using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class ClientMap : ClassMap<PersistentClient>
	{
		public ClientMap ()
		{
			Table("client");
			
			Id(c => c.ID)
				.Column("clientid")
				.GeneratedBy.Assigned();
			
			Map(c => c.CreatedBy).Column("createdby");
			Map(c => c.CreatedOn).Column("createdon");
			Map(c => c.HasAPIAccess).Column("hasapiaccess");
			Map(c => c.IsActive).Column("isactive");
			Map(c => c.LastModifiedBy).Column("lastmodifiedby");
			Map(c => c.LastModifiedOn).Column("lastmodifiedon");
			Map(c => c.LogoPath).Column("logopath");
			Map(c => c.Name).Column("name");
			
			HasMany<PersistentUser>(c => c.Users)
				.Inverse()
				.Cascade.All()
				.KeyColumn("clientid");
		}
	}
}

