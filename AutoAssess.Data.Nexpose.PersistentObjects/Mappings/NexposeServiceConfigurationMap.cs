using System;
using AutoAssess.Data.Nexpose.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NexposeServiceConfigurationMap : ClassMap<PersistentNexposeServiceConfiguration>
	{
		public NexposeServiceConfigurationMap ()
		{
			Table ("nexposeserviceconfiguration");
			
			Id (s => s.ID).Column("nexposeserviceconfigurationid")
				.GeneratedBy.Assigned();
			
			Map (s => s.CreatedBy).Column("createdby");
			Map (s => s.CreatedOn).Column("createdon");
			Map (s => s.IsActive).Column("isactive");
			Map (s => s.LastModifiedBy).Column("lastmodifiedby");
			Map (s => s.LastModifiedOn).Column("lastmodifiedon");
			Map (s => s.Name).Column("name");
			Map (s => s.Value).Column("value");
		}
	}
}

