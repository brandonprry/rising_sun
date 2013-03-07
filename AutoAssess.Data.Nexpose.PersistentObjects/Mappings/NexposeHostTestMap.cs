using System;
using AutoAssess.Data.Nexpose.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NexposeHostTestMap : ClassMap<PersistentNexposeHostTest>
	{
		public NexposeHostTestMap ()
		{
			Table ("nexposehosttest");
			
			Id (t => t.ID).Column("nexposehosttestid")
				.GeneratedBy.Assigned();
			
			Map (t => t.CreatedBy).Column("createdby");
			Map (t => t.CreatedOn).Column("createdon");
			Map (t => t.LastModifiedBy).Column("lastmodifiedby");
			Map (t => t.LastModifiedOn).Column("lastmodifiedon");
			Map (t => t.IsActive).Column("isactive");
			Map (t => t.IsPCICompliant).Column("ispcicompliant");
			Map (t => t.NexposeParagraph).Column("nexposeparagraph");
			Map (t => t.RemoteDeviceID).Column("remotedeviceid");
			Map (t => t.RemoteScanID).Column("remotescanid");
			Map (t => t.Status).Column("status");
			Map (t => t.VulnerableSince).Column("vulnerablesince");
		}
	}
}

