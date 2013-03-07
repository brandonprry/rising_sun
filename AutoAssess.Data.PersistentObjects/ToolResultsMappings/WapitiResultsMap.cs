using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class WapitiResultsMap : ClassMap<PersistentWapitiResults>
	{
		public WapitiResultsMap ()
		{
			Table("wapiti");
			
			Id (w => w.ID).Column("wapitiresultid")
				.GeneratedBy.Assigned();
			
			Map (w => w.HostPortID).Column("portid");
			Map (w => w.CreatedBy).Column("createdby");
			Map (w => w.CreatedOn).Column("createdon");
			Map (w => w.FullOutput).Column("fulloutput");
			Map	(w => w.IsActive).Column("isactive");
			Map (w => w.LastModifiedBy).Column("lastmodifiedby");
			Map(w => w.LastModifiedOn).Column("lastmodifiedon");
			Map (w => w.HostIPAddressV4).Column("hostipaddressv4");
			Map (w => w.HostPort).Column("hostport");
			Map (w => w.IsTCP).Column("istcp");
			
			Map (w => w.ParentProfileID)
				.Column("parentprofileid")
				.ReadOnly();
			
			HasMany<PersistentWapitiBug>(w => w.Bugs)
				.KeyColumn("wapitiresultid")
				.Cascade.All();
			
			References<PersistentProfile>(w => w.ParentProfile)
				.Column("parentprofileid");
		}
	}
}

