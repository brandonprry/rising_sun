using System;
using AutoAssess.Data.Nexpose.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NexposeServiceTestMap : ClassMap<PersistentNexposeServiceTest>
	{
		public NexposeServiceTestMap ()
		{
			Table ("nexposeservicetest");
			
			Id (n => n.ID).Column("nexposeservicetestid")
				.GeneratedBy.Assigned();
			
			Map (n => n.CreatedBy).Column("createdby");
			Map (n => n.CreatedOn).Column("createdon");
			Map (n => n.IsActive).Column("isactive");
			Map (n => n.LastModifiedBy).Column("lastmodifiedby");
			Map (n => n.LastModifiedOn).Column("lastmodifiedon");
			Map (n => n.NexposeParagraph).Column("nexposeparagraph");
			Map (n => n.RemoteDeviceID).Column("remotedeviceid");
			Map (n => n.RemoteScanID).Column("remotescanid");
			Map (n => n.Status).Column("status");
			Map (n => n.VulnerableSince).Column("vulnerablesince");
		}
	}
}

