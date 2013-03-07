using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace Mappings
{
	public class MetasploitHostMap : ClassMap<PersistentMetasploitHost>
	{
		public MetasploitHostMap ()
		{
			Table ("metasploithost");
			
			Id (c => c.ID).Column("metasploithostid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.Address).Column("address");
			Map (c => c.Comm).Column("comm");
			Map (c => c.ExploitAttemptCount).Column("exploitattemptcount");
			Map (c => c.HostDetailCount).Column("hostdetailscount");
			Map (c => c.Info).Column("info");
			Map (c => c.MAC).Column("mac");
			Map (c => c.Name).Column("name");
			Map (c => c.NoteCount).Column("notecount");
			Map (c => c.OSArchitecture).Column("osarchitecture");
			Map (c => c.OSFlavor).Column("osflavor");
			Map (c => c.OSLanguage).Column("oslanguage");
			Map (c => c.OSName).Column("osname");
			Map (c => c.OSServicePack).Column("osservicepack");
			Map (c => c.Purpose).Column("purpose");
			Map (c => c.RemoteCreatedAt).Column("remotecreatedat");
			Map (c => c.RemoteID).Column("remoteid");
			Map (c => c.RemoteUpdatedAt).Column("remoteupdatedat");
			Map (c => c.RemoteWorkspaceID).Column("remoteworkspaceid");
			Map (c => c.ServiceCount).Column("servicecount");
			Map (c => c.State).Column("state");
			Map (c => c.VulnCount).Column("vulncount");
			
			HasMany(c => c.PersistentDetails)
				.KeyColumn("metasploithostid")
				.Cascade.All();
			HasMany(c => c.PersistentExploitAttempts)
				.KeyColumn("metasploithostid")
				.Cascade.All();
			HasMany(c => c.PersistentServices)
				.KeyColumn("metasploithostid")
				.Cascade.All();
			HasMany(c => c.PersistentNotes)
				.KeyColumn("metasploithostid")
				.Cascade.All();
			HasMany (c => c.PersistentTags)
				.KeyColumn("metasploithostid")
				.Cascade.All();
			HasMany (c => c.PersistentVulnerabilities)
				.KeyColumn("metasploithostid")
				.Cascade.All();
			HasMany(c => c.PersistentCredentials)
				.KeyColumn("metasploithostid")
				.Cascade.All();
			HasMany (c => c.PersistentSessions)
				.KeyColumn("metasploithostid")
				.Cascade.All();
		}
	}
}

