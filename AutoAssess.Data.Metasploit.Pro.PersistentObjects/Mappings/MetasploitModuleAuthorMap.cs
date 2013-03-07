using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace Mappings
{
	public class MetasploitModuleAuthorMap : ClassMap<PersistentMetasploitModuleAuthor>
	{
		public MetasploitModuleAuthorMap ()
		{
			Table ("metasploitmoduleauthor");
			
			Id (c => c.ID).Column("metasploitmoduleauthorid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.Name).Column("name");
			Map (c => c.Email).Column("email");
		}
	}
}

