using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASAgentMap : ClassMap<PersistentOpenVASAgent>
	{
		public OpenVASAgentMap ()
		{
			Table("openvasagent");
			
			Id(o => o.ID).Column("openvasagentid")
				.GeneratedBy.Assigned();
			
			Map(o => o.Comment).Column("comment");
			Map(o => o.CreatedBy).Column("createdby");
			Map(o => o.CreatedOn).Column("createdon");
			Map(o => o.Installer).Column("installer");
			Map(o => o.InUse).Column("inuse");
			Map(o => o.IsActive).Column("isactive");
			Map(o => o.LastModifiedBy).Column("lastmodifiedby");
			Map(o => o.LastModifiedOn).Column("lastmodifiedon");
			Map(o => o.Name).Column("name");
			Map(o => o.RemoteAgentID).Column("remoteagentid");
			Map(o => o.Signature).Column("signature");
		}
	}
}

