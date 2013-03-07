using System;
using AutoAssess.Data.Nexpose.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NexposeHostNameMap : ClassMap<PersistentNexposeHostName>
	{
		public NexposeHostNameMap ()
		{
			Table("nexposehostname");
			
			Id (n => n.ID).Column("nexposehostnameid")
				.GeneratedBy.Assigned();
			
			Map(n => n.CreatedBy).Column("createdby");
			Map (n => n.CreatedOn).Column("createdon");
			Map (n => n.IsActive).Column("isactive");
			Map (n => n.LastModifiedBy).Column("lastmodifiedby");
			Map (n => n.LastModifiedOn).Column("lastmodifiedon");
			Map (n => n.Name).Column("name");
		}
	}
}

