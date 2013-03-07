using System;
using AutoAssess.Data.Nessus.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NessusHostPropertiesMap : ClassMap<PersistentNessusHostProperties>
	{
		public NessusHostPropertiesMap ()
		{
			Table("nessushostproperties");
			
			Id(p => p.ID).Column("nessushostpropertiesid")
				.GeneratedBy.Assigned();
			
			Map (p => p.CreatedBy).Column("createdby");
			Map (p => p.CreatedOn).Column("createdon");
			Map (p => p.LastModifiedBy).Column("lastmodifiedby");
			Map (p => p.LastModifiedOn).Column("lastmodifiedon");
			Map (p => p.IsActive).Column("isactive");
			Map (p => p.HostBegin).Column("hostbegin");
			Map (p => p.HostEnd).Column("hostend");
			Map (p => p.HostFQDN).Column("hostfqdn");
			Map (p => p.HostIP).Column("hostip");
			Map (p => p.OperatingSystem).Column("operatingsystem");
			Map (p => p.SystemType).Column("systemtype");
		}
	}
}

