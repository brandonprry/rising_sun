using System;
using AutoAssess.Data.Nexpose.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NexposeAssetMap : ClassMap<PersistentNexposeAsset>
	{
		public NexposeAssetMap ()
		{
			Table ("nexposeasset");
			
			Id (a => a.ID).Column("nexposeassetid")
				.GeneratedBy.Assigned();
			
			Map (a => a.CreatedBy).Column("createdby");
			Map (a => a.CreatedOn).Column("createdon");
			Map (a => a.IsActive).Column("isactive");
			Map (a => a.LastModifiedBy).Column("lastmodifiedby");
			Map (a => a.LastModifiedOn).Column("lastmodifiedon");
			Map (a => a.RemoteDeviceID).Column("remotedeviceid");
			Map (a => a.RiskScore).Column("riskscore");
			Map (a => a.ScanTemplate).Column("scantemplate");
			Map (a => a.SiteImportance).Column("siteimportance");
			Map (a => a.SiteName).Column("sitename");
			Map (a => a.Status).Column("status");
			Map (a => a.IPAddressV4).Column("ipaddressv4");
			
			HasMany (a => a.PersistentFingerprints)
				.KeyColumn("nexposeassetid")
				.Cascade.All();
			HasMany (a => a.PersistentNames)
				.KeyColumn("nexposeassetid")
				.Cascade.All();
			HasMany (a => a.PersistentServices)
				.KeyColumn("nexposeassetid")
				.Cascade.All();
			HasMany(a => a.PersistentHostTests)
				.KeyColumn("nexposeassetid")
				.Cascade.All();
		}
	}
}

