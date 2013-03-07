using System;
using AutoAssess.Data.Nexpose.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NexposeHostFingerprintMap : ClassMap<PersistentNexposeHostFingerprint>
	{
		public NexposeHostFingerprintMap ()
		{
			Table ("nexposehostfingerprint");
			
			Id (fp => fp.ID).Column("nexposehostfingerprintid")
				.GeneratedBy.Assigned();
			
			Map (fp => fp.CreatedBy).Column("createdby");
			Map (fp => fp.CreatedOn).Column("createdon");
			Map (fp => fp.LastModifiedBy).Column("lastmodifiedby");
			Map (fp => fp.LastModifiedOn).Column("lastmodifiedon");
			Map (fp => fp.IsActive).Column("isactive");
			Map (fp => fp.Certainty).Column("certainty");
			Map (fp => fp.DeviceClass).Column("deviceclass");
			Map (fp => fp.Family).Column("family");
			Map (fp => fp.Product).Column("product");
			Map (fp => fp.Vendor).Column("vendor");
		}
	}
}

