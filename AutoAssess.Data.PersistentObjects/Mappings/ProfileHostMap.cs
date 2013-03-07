using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using AutoAssess.Data.PersistentObjects;

namespace Mappings
{
	public class ProfileHostMap : ClassMap<PersistentProfileHost>
	{
		public ProfileHostMap ()
		{
			Table ("profilehost");
			
			Id (h => h.ID).Column("profilehostid")
				.GeneratedBy.Assigned();
			
			Map (h => h.CreatedBy).Column("createdby");
			Map (h => h.CreatedOn).Column("createdon");
			Map (h => h.IPv4Address).Column("ipv4address");
			Map (h => h.Name).Column("name");
			Map (h => h.IsActive).Column("isactive");
			Map (h => h.IsVerified).Column("isverified");
			Map (h => h.LastModifiedBy).Column("lastmodifiedby");
			Map (h => h.LastModifiedOn).Column("lastmodifiedon");
			Map (h => h.VerifiedByFile).Column("verifiedbyfile");
			Map (h => h.VerifiedByWhois).Column("verifiedbywhois");
			Map (h => h.VerifiedOn).Column("verifiedon");
			Map (h => h.WasManuallyVerified).Column("wasmanuallyverified");
			
			Map (h => h.ParentProfileID).Column("profileid")
				.ReadOnly()
				.Access.ReadOnly();
			
			HasMany (h => h.NmapHosts)
				.KeyColumn("profilehostid")
				.Cascade.All()
				.AsBag();
			
			References(h => h.ParentProfile)
				.Column("profileid");
		}
	}
}

