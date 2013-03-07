using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace Mappings
{
	public class MetasploitModulePlatformMap : ClassMap<PersistentMetasploitModulePlatform>
	{
		public MetasploitModulePlatformMap ()
		{
			Table ("metasploitmoduleplatform");
			
			Id (c => c.ID).Column("metasploitmoduleplatformid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
		}
	}
}

