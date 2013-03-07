using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	public class NessusPolicyMap : ClassMap<PersistentNessusPolicy>
	{
		public NessusPolicyMap ()
		{
			Table("policy");
			
			Id(p => p.ID).Column("policyid").GeneratedBy.Assigned();
			
			Map(p => p.CreatedBy).Column("createdby");
			Map(p => p.CreatedOn).Column("createdon");
			Map(p => p.IsActive).Column("isactive");
			Map(p => p.LastModifiedBy).Column("lastmodifiedby");
			Map(p => p.LastModifiedOn).Column("lastmodifiedon");
			Map(p => p.Name).Column("name");
			Map(p => p.Owner).Column("owner");
			Map(p => p.RemotePolicyID).Column("remotepolicyid");
			Map(p => p.Visibility).Column("visibility");
		}
	}
}

