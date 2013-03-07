using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace Mappings
{
	public class MetasploitModuleReferenceMap : ClassMap<PersistentMetasploitModuleReference>
	{
		public MetasploitModuleReferenceMap ()
		{
			Table ("metasploitmodulereference");
			
			Id (c => c.ID).Column("metasploitmodulereferenceid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
		}
	}
}

