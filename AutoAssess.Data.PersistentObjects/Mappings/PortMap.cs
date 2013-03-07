using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class PortMap : ClassMap<PersistentPort>
	{
		public PortMap ()
		{
			Table("hostport");
			
			Id(h => h.ID).Column("hostportid").GeneratedBy.Assigned();
			
			Map(h => h.HydraServiceName).Column("hydraservicename");
			Map(h => h.IsActive).Column("isactive");
			Map(h => h.IsUDP).Column("isudp");
			Map(h => h.PortNumber).Column("portnumber");
			Map(h => h.DeepScan).Column("deepscan");
			Map(h => h.Service).Column("service");
			Map(h => h.State).Column("state");
			Map(h => h.CreatedBy).Column("createdby");
			Map(h => h.CreatedOn).Column("createdon");
			Map(h => h.LastModifiedBy).Column("lastmodifiedby");
			Map(h => h.LastModifiedOn).Column("lastmodifiedon");
			
			References(h => h.ParentHost)
				.Cascade.All()
				.Column("nmaphostid");
			
			References(h => h.ParentProfile)
				.Cascade.All()
				.Column("parentprofileid");
			
			References(h => h.User)
				.Cascade.All()
				.Column("userid");
			
		}
	}
}

