using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASNVTFamilyMap : ClassMap<PersistentOpenVASNVTFamily>
	{
		public OpenVASNVTFamilyMap ()
		{
			Table("openvasnvtfamily");
			
			Id(f => f.ID).Column("openvasnvtfamilyid").GeneratedBy.Assigned();
			
			Map(f => f.CreatedBy).Column("createdby");
			Map(f => f.CreatedOn).Column("createdon");
			Map(f => f.IsActive).Column("isactive");
			Map(f => f.LastModifiedBy).Column("lastmodifiedby");
			Map(f => f.LastModifiedOn).Column("lastmodifiedon");
			Map(f => f.MaxNVTCount).Column("maxnvtcount");
			Map(f => f.Name).Column("name");
			Map(f => f.RemoteFamilyID).Column("remotefamilyid");
			
			HasMany<PersistentOpenVASNVT>(f => f.NVTs)
				.KeyColumn("openvasnvtfamilyid")
				.Table("openvasnvt")
				.Cascade.SaveUpdate();
		}
	}
}

