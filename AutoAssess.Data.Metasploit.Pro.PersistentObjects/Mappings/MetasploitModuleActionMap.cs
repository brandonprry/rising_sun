using System;
using NHibernate.Cfg;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace Mappings
{
	public class MetasploitModuleActionMap : ClassMap<PersistentMetasploitModuleAction>
	{
		public MetasploitModuleActionMap ()
		{
			
			Table ("metasploitmoduleaction");
			
			Id (c => c.ID).Column("metasploitmoduleactionid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
		}
	}
}

