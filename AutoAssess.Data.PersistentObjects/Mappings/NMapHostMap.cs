using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class NMapHostMap : ClassMap<PersistentNMapHost>
	{
		public NMapHostMap ()
		{
			Table("nmaphost");
			
			Id(h => h.ID).Column("nmaphostid").GeneratedBy.Assigned();
			
			Map(h => h.CreatedBy).Column("createdby");
			Map(h => h.CreatedOn).Column("createdon");
			Map(h => h.DeviceType).Column("devicetype");
			Map(h => h.Hostname).Column("hostname");
			Map(h => h.IPAddressv4).Column("ipaddressv4");
			Map(h => h.IPAddressV6).Column("ipaddressv6");
			Map(h => h.IsActive).Column("isactive");
			Map(h => h.LastModifiedBy).Column("lastmodifiedby");
			Map(h => h.LastModifiedOn).Column("lastmodifiedon");
			Map(h => h.MAC).Column("mac");
			Map(h => h.NetworkDistance).Column("networkdistance");
			Map(h => h.OS).Column("os");
			Map(h => h.OS_Details).Column("os_details");
			
			References(h => h.ParentProfile)
				.Column("profileid");
			
			References(h => h.ProfileHost)
				.Column("profilehostid");
			
			References(h => h.User)
				.Column("userid");
			
			References<PersistentNMapResults>(h => h.ParentResults)
				.Column("nmapresultid");
			
			HasMany(h => h.PersistentPorts)
				.KeyColumn("nmaphostid")
				.Table("hostport")
				.Cascade.All()
				.AsBag();
		}
	}
}

