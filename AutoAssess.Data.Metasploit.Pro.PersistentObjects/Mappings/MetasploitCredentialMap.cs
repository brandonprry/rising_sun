using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace Mappings
{
	public class MetasploitCredentialMap : ClassMap<PersistentMetasploitCredential>
	{
		public MetasploitCredentialMap ()
		{
			Table ("metasploitcredential");
			
			Id (c => c.ID).Column("metasploitcredentialid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.Password).Column("password");
			Map (c => c.PasswordType).Column("passwordtype");
			Map (c => c.Port).Column("port");
			Map (c => c.Proof).Column("proof");
			Map (c => c.RemoteCreatedAt).Column("remotecreatedat");
			Map (c => c.RemoteIsActive).Column("active");
			Map (c => c.RemoteUpdatedAt).Column("remoteupdatedat");
			Map (c => c.ServiceName).Column("servicename");
			Map (c => c.SourceID).Column("sourceid");
			Map (c => c.SourceType).Column("sourcetype");
			Map (c => c.Username).Column("username");
		}
	}
}

