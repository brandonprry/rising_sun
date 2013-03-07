using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	public class NessusPluginFamilyMap : ClassMap<PersistentNessusPluginFamily>
	{
		public NessusPluginFamilyMap ()
		{
			Table("pluginfamily");
			
			Id(f => f.ID).Column("pluginfamilyid")
				.GeneratedBy.Assigned();
			
			Map(f => f.CreatedBy).Column("createdby");
			Map(f => f.CreatedOn).Column("createdon");
			Map(f => f.IsActive).Column("isactive");
			Map(f => f.LastModifiedBy).Column("lastmodifiedby");
			Map(f => f.LastModifiedOn).Column("lastmodifiedon");
			Map(f => f.Name).Column("name");
			Map(f => f.NumberOfMembers).Column("noofmembers");
			
			HasMany(f => f.ChildPlugins)
				.KeyColumn("pluginfamilyid")
				.Table("plugin")
				.Cascade.SaveUpdate();
		}
	}
}