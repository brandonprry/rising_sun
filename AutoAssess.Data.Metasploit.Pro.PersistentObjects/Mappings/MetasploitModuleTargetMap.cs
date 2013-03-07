using System;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class MetasploitModuleTargetMap : ClassMap<PersistentMetasploitModuleTarget>
	{
		public MetasploitModuleTargetMap ()
		{
			Table ("metasploitmoduletarget");
			
			Id (c => c.ID).Column("metasploitmoduletargetid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
		}
	}
}

