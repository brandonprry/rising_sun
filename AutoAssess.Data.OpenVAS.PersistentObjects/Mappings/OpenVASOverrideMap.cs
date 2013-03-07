using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASOverrideMap : ClassMap<PersistentOpenVASOverride>
	{
		public OpenVASOverrideMap ()
		{
			Table("openvasoverride");
			
			Id(o => o.ID).Column("openvasoverrideid")
				.GeneratedBy.Assigned();
			
			
			References(o => o.NVT).Column("openvasnvtid");
			References(o => o.Task).Column("openvastaskid");
			References(o => o.Report).Column("openvasreportid");
			
			Map(o => o.Comment).Column("comment");
			Map(o => o.Content).Column("content");
			Map(o => o.CreatedBy).Column("createdby");
			Map(o => o.CreatedOn).Column("createdon");
			Map(o => o.Hosts).Column("hosts");
			Map(o => o.IsActive).Column("isactive");
			Map(o => o.LastModifiedBy).Column("lastmodifiedby");
			Map(o => o.LastModifiedOn).Column("lastmodifiedon");
			Map(o => o.NewThreat).Column("newthreat");
			Map(o => o.Port).Column("port");
			Map(o => o.RemoteOverrideID).Column("remoteoverrideid");
			Map(o => o.Threat).Column("threat");
			
		}
	}
}

