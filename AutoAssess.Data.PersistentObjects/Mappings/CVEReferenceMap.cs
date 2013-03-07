using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class CVEReferenceMap : ClassMap<PersistentCVEReference>
	{
		public CVEReferenceMap ()
		{
			Table("cvereference");
			
			Id(r => r.ID).Column("cvereferenceid")
				.GeneratedBy
				.Assigned();
			
			Map(r => r.Description).Column("description");
			Map(r => r.Source).Column("source");
			Map(r => r.URL).Column("url");
			Map(r => r.CreatedBy).Column("createdby");
			Map(r => r.CreatedOn).Column("createdon");
			Map(r => r.IsActive).Column("isactive");
			Map(r => r.LastModifiedBy).Column("lastmodifiedby");
			Map(r => r.LastModifiedOn).Column("lastmodifiedon");
			
			References<PersistentCVE>(r => r.CVE)
				.Column("cveid");
		}
	}
}