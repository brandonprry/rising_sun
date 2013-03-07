using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASEscalatorMap : ClassMap<PersistentOpenVASEscalator>
	{
		public OpenVASEscalatorMap ()
		{
			Table("openvasescalator");
			
			Id(e => e.ID).Column("openvasescalatorid")
				.GeneratedBy.Assigned();
			
			
			References(e => e.Condition).Column("openvasconditionid");
			References(e => e.Event).Column("openvaseventid");
			References(e => e.Method).Column("openvasmethodid");
			
			Map(e => e.CreatedBy).Column("createdby");
			Map(e => e.CreatedOn).Column("createdon");
			Map(e => e.IsActive).Column("isactive");
			Map(e => e.LastModifiedBy).Column("lastmodifiedby");
			Map(e => e.LastModifiedOn).Column("lastmodifiedon");
			Map(e => e.Name).Column("name");
			Map(e => e.RemoteEscalatorID).Column("remoteescalatorid");
		}
	}
}

