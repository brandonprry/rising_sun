using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASTargetMap : ClassMap<PersistentOpenVASTarget>
	{
		public OpenVASTargetMap ()
		{
			Table("openvastarget");
			
			Id(t => t.ID).Column("openvastargetid")
				.GeneratedBy.Assigned();
			
			
			References(t => t.SMBCredentials).Column("smblsccredentialsid");
			References(t => t.SSHCredentials).Column("sshlsccredentialsid");
			
			Map(t => t.Comment).Column("comment");
			Map(t => t.CreatedBy).Column("createdby");
			Map(t => t.CreatedOn).Column("createdon");
			Map(t => t.Hosts).Column("hosts");
			Map(t => t.IsActive).Column("isactive");
			Map(t => t.LastModifiedBy).Column("lastmodifiedby");
			Map(t => t.LastModifiedOn).Column("lastmodifiedon");
			Map(t => t.Name).Column("name");
			Map(t => t.PortRange).Column("portrange");
			Map(t => t.RemoteTargetID).Column("remotetargetid");
			
		}
	}
}

