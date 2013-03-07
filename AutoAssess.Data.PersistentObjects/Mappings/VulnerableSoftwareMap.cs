using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class VulnerableSoftwareMap : ClassMap<PersistentVulnerableSoftware>
	{
		public VulnerableSoftwareMap ()
		{
			Table("nvdvulnerablesoftware");
			
			Id(vs => vs.ID).Column("nvdvulnerablesoftwareid")
				.GeneratedBy
				.Assigned();
			
			Map(vs => vs.Software).Column("software");
			Map(vs => vs.CreatedBy).Column("createdby");
			Map(vs => vs.CreatedOn).Column("createdon");
			Map(vs => vs.IsActive).Column("isactive");
			Map(vs => vs.LastModifiedBy).Column("lastmodifiedby");
			Map(vs => vs.LastModifiedOn).Column("lastmodifiedon");
		}
	}
}