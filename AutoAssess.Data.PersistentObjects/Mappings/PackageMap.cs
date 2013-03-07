using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class PackageMap : ClassMap<PersistentPackage>
	{
		public PackageMap ()
		{
			Table ("package");
			
			Id (p => p.ID).Column("packageid")
				.GeneratedBy.Assigned();
			
			Map (p => p.AllowsRecurring).Column("allowsrecurring");
			Map (p => p.Cost).Column("cost");
			Map (p => p.CreatedBy).Column("createdby");
			Map (p => p.CreatedOn).Column("createdon");
			Map (p => p.Description).Column("description");
			Map (p => p.LastModifiedBy).Column("lastmodifiedby");
			Map (p => p.LastModifiedOn).Column("lastmodifiedon");
			Map (p => p.Name).Column("name");
			
			
		}
	}
}

