using System;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class MetasploitWebsiteMap : ClassMap<PersistentMetasploitWebsite>
	{
		public MetasploitWebsiteMap ()
		{
			Table ("metasploitwebsite");
			
			Id (c => c.ID).Column("metasploitwebsiteid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.Host).Column("host");
			Map (c => c.Options).Column("options");
			Map (c => c.Port).Column("port");
			Map (c => c.RemoteCreatedAt).Column("remotecreatedat");
			Map (c => c.RemoteID).Column("remoteid");
			Map (c => c.RemoteServiceID).Column("remoteserviceid");
			Map (c => c.RemoteUpdatedAt).Column("remoteupdatedat");
			Map (c => c.VirtualHost).Column("virtualhost");
		}
	}
}

