using System;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class MetasploitServiceMap : ClassMap<PersistentMetasploitService>
	{
		public MetasploitServiceMap ()
		{
			Table ("metasploitservice");
			
			Id (c => c.ID).Column("metasploitserviceid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.Info).Column("info");
			Map (c => c.Name).Column("name");
			Map (c => c.Protocol).Column("protocol");
			Map (c => c.RemoteCreatedAt).Column("remotecreatedat");
			Map (c => c.RemoteHostID).Column("remotehostid");
			Map (c => c.RemoteID).Column("remoteid");
			Map (c => c.RemoteUpdatedAt).Column("remoteupdatedat");
			Map (c => c.State).Column("state");
		}
	}
}

