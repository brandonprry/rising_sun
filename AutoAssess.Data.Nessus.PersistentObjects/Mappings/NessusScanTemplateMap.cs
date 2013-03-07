using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	public class NessusScanTemplateMap : ClassMap<PersistentNessusScanTemplate>
	{
		public NessusScanTemplateMap ()
		{
			Table("nessusscantemplate");
			
			Id(t => t.ID).Column("nessusscantemplateid").GeneratedBy.Assigned();
			
			Map(t => t.CreatedBy).Column("createdby");
			Map(t => t.CreatedOn).Column("createdon");
			Map(t => t.IsActive).Column("isactive");
			Map(t => t.LastModifiedBy).Column("lastmodifiedby");
			Map(t => t.LastModifiedOn).Column("lastmodifiedon");
			Map(t => t.Name).Column("name");
			Map(t => t.Owner).Column("owner");
			Map(t => t.ReadableName).Column("readablename");
			Map(t => t.RemotePolicyID).Column("remotepolicyid");
			Map(t => t.Target).Column("target");
		}
	}
}

