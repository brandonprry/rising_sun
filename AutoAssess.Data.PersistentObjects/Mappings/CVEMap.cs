using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.PersistentObjects;

namespace AutoAssess.Data.PersistentObjects 
{
	public class CVEMap : ClassMap<PersistentCVE>
	{
		public CVEMap ()
		{
			Table("cve");
			
			Id(cve => cve.ID).Column("cveid")
				.GeneratedBy
				.Assigned();
			
			Map(cve => cve.Description).Column("description");
			Map(cve => cve.Name).Column("name");
			Map(cve => cve.CreatedBy).Column("createdby");
			Map(cve => cve.CreatedOn).Column("createdon");
			Map(cve => cve.IsActive).Column("isactive");
			Map(cve => cve.LastModifiedBy).Column("lastmodifiedby");
			Map(cve => cve.LastModifiedOn).Column("lastmodifiedon");
			
			HasMany<PersistentCVEComment>(cve => cve.PersistentComments)
				.KeyColumn("cveid")
				.Table("cvecomment")
				.Cascade.SaveUpdate();
			
			HasMany<PersistentCVEReference>(cve => cve.PersistentReferences)
				.KeyColumn("cveid")
				.Table("cvereference")
				.Cascade.SaveUpdate();
		}
	}
}

