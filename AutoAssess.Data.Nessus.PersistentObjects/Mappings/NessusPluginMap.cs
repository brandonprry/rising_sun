using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	public class NessusPluginMap : ClassMap<PersistentNessusPlugin>
	{
		public NessusPluginMap ()
		{
			Table("plugin");
			
			Id(p => p.ID).Column("pluginid")
				.GeneratedBy.Assigned();
			
			Map(p => p.CreatedBy).Column("createdby");
			Map(p => p.CreatedOn).Column("createdon");
			Map(p => p.FamilyName).Column("familyname");
			Map(p => p.FileName).Column("filename");
			Map(p => p.IsActive).Column("isactive");
			Map(p => p.LastModifiedBy).Column("lastmodifiedby");
			Map(p => p.LastModifiedOn).Column("lastmodifiedon");
			Map(p => p.Name).Column("name");
			Map(p => p.RemotePluginID).Column("remotepluginid");
			
			References(p => p.Family).Column("pluginfamilyid");
		}
	}
}

