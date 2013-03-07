using System;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class MetasploitModuleDetailsMap : ClassMap<PersistentMetasploitModuleDetails>
	{
		public MetasploitModuleDetailsMap ()
		{
			Table ("metasploitmoduledetails");
			
			Id (c => c.ID).Column("metasploitmoduledetailsid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.DefaultTarget).Column("defaulttarget");
			Map (c => c.Description).Column("description");
			Map (c => c.DisclosureDate).Column("disclosuredate");
			Map (c => c.File).Column("file");
			Map (c => c.FullName).Column("fullname");
			Map (c => c.License).Column("license");
			Map (c => c.ModuleType).Column("moduletype");
			Map (c => c.Name).Column("name");
			Map (c => c.Priviledged).Column("priviledged");
			Map (c => c.Rank).Column("rank");
			Map (c => c.Ready).Column("ready");
			Map (c => c.ReferenceName).Column ("referencename");
			Map (c => c.RemoteID).Column("remoteid");
			Map (c => c.Stance).Column("stance");
			
			HasMany(c => c.PersistentActions)
				.KeyColumn("metasploitmoduledetailsid")
				.Cascade.All();
			HasMany(c => c.PersistentArchitectures)
				.KeyColumn("metasploitmoduledetailsid")
				.Cascade.All();
			HasMany(c => c.PersistentAuthors)
				.KeyColumn("metasploitmoduledetailsid")
				.Cascade.All();
			HasMany (c => c.PersistentMixins)
				.KeyColumn("metasploitmoduledetailsid")
				.Cascade.All();
			HasMany (c => c.PersistentPlatforms)
				.KeyColumn("metasploitmoduledetailsid")
				.Cascade.All();
			HasMany (c => c.PersistentReferences)
				.KeyColumn("metasploitmoduledetailsid")
				.Cascade.All();
			HasMany (c => c.PersistentTargets)
				.KeyColumn("metasploitmoduledetailsid")
				.Cascade.All();
		}
	}
}

