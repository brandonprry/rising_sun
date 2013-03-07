using System;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class MetasploitNoteMap : ClassMap<PersistentMetasploitNote>
	{
		public MetasploitNoteMap ()
		{
			Table ("metasploitnote");
			
			Id (c => c.ID).Column("metasploitnoteid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.Data).Column("data");
			Map (c => c.NoteType).Column("notetype");
			Map (c => c.RemoteCreatedAt).Column("remotecreatedat");
			Map (c => c.RemoteHostID).Column("remotehostid");
			Map (c => c.RemoteID).Column("remoteid");
			Map (c => c.RemoteServiceID).Column("remoteserviceid");
			Map (c => c.RemoteUpdatedAt).Column("remoteupdatedat");
			Map (c => c.RemoteWorkspaceID).Column("remoteworkspaceid");
		}
	}
}

