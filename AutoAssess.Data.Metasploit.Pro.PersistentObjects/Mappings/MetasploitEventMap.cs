using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace Mappings
{
	public class MetasploitEventMap :ClassMap<PersistentMetasploitEvent>
	{
		public MetasploitEventMap ()
		{
			Table ("metasploitevent");
			
			Id (c => c.ID).Column("metasploiteventid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.Info).Column("info");
			Map (c => c.ModuleName).Column("modulename");
			Map (c => c.ModuleRHOST).Column("modulerhost");
			Map (c => c.Name).Column("name");
			Map (c => c.RemoteCreatedAt).Column("remotecreatedat");
			Map (c => c.RemoteHostID).Column("remotehostid");
			Map (c => c.RemoteID).Column("remoteid");
			Map (c => c.RemoteUpdatedAt).Column("remoteupdatedat");
			Map (c => c.RemoteWorkspaceID).Column("remoteworkspaceid");
			Map (c => c.Username).Column("username");
			
		}
	}
}

