using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Nexpose.PersistentObjects;

namespace Mappings
{
	public class NexposeServiceFingerprintMap : ClassMap<PersistentNexposeServiceFingerprint>
	{
		public NexposeServiceFingerprintMap ()
		{
			Table ("nexposeservicefingerprint");
			
			Id (s => s.ID).Column("nexposeservicefingerprintid")
				.GeneratedBy.Assigned();
			
			Map (s => s.Certainty).Column("certainty");
			Map (s => s.Family).Column("family");
			Map (s => s.IsActive).Column("isactive");
			Map (s => s.LastModifiedBy).Column("lastmodifiedby");
			Map (s => s.LastModifiedOn).Column("lastmodifiedon");
			Map (s => s.Product).Column("product");
			Map (s => s.Vendor).Column("vendor");
			Map (s => s.Version).Column("version");
			
		}
	}
}

