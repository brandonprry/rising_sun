using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class NVDReferenceMap : ClassMap<PersistentNVDReference>
	{
		public NVDReferenceMap ()
		{
			Table("nvdreference");
			
			Id(r => r.ID).Column("nvdreferenceid")
				.GeneratedBy
				.Assigned();
			
			Map(r => r.Description).Column("description");
			Map(r => r.Source).Column("source");
			Map(r => r.Type).Column("type");
			Map(r => r.URL).Column("url");
			Map(r => r.CreatedBy).Column("createdby");
			Map(r => r.CreatedOn).Column("createdon");
			Map(r => r.IsActive).Column("isactive");
			Map(r => r.LastModifiedBy).Column("lastmodifiedby");
			Map(r => r.LastModifiedOn).Column("lastmodifiedon");
			
			
		}
	}
}

