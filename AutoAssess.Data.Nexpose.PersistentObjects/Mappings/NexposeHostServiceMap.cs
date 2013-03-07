using System;
using AutoAssess.Data.Nexpose.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NexposeHostServiceMap : ClassMap<PersistentNexposeHostService>
	{
		public NexposeHostServiceMap ()
		{
			Table ("nexposehostservice");
			
			Id (s => s.ID).Column("nexposehostserviceid")
				.GeneratedBy.Assigned();
			
			Map (s => s.Name).Column("name");
			Map (s => s.CreatedBy).Column("createdby");
			Map (s => s.CreatedOn).Column("createdon");
			Map (s => s.LastModifiedBy).Column("lastmodifiedby");
			Map (s => s.LastModifiedOn).Column("lastmodifiedon");
			Map (s => s.Port).Column("port");
			Map (s => s.Protocol).Column("protocol");
			Map (s => s.Status).Column("status");
			
			HasMany(s => s.PersistentConfigurations)
				.KeyColumn("nexposehostserviceid")
				.Cascade.All();
			HasMany(s => s.PersistentFingerprints)
				.KeyColumn("nexposehostserviceid")
				.Cascade.All();
			HasMany (s => s.PersistentTests)
				.KeyColumn("nexposehostserviceid")
				.Cascade.All();
		}
	}
}

