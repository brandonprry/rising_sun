using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class ProfileMap : ClassMap<PersistentProfile>
	{
		public ProfileMap ()
		{
			Table("profile");
			
			Id(p => p.ID).Column("profileid")
				.GeneratedBy.Assigned();
			
			Map(p => p.Name).Column("name");
			Map(p => p.Description).Column("description");
			Map(p => p.Range).Column("range");
			Map(p => p.HasRun).Column("hasrun");
			Map(p => p.IsActive).Column("isactive");
			Map(p => p.CreatedBy).Column("createdby");
			Map(p => p.CreatedOn).Column("createdon");
			Map(p => p.LastModifiedBy).Column("lastmodifiedby");
			Map(p => p.LastModifiedOn).Column("lastmodifiedon");
			Map(p => p.WebUserID).Column("webuserid");
			Map(p => p.Duration).Column("duration");
			Map (p => p.Domain).Column("domain");
			Map (p => p.BadgeState).Column("badgestate");
			
			References(p => p.User)
				.Cascade.SaveUpdate()
				.Column("userid");
			
			References(p => p.CurrentResults)
				.Cascade.SaveUpdate()
				.Column("currentresultsid");
			
			HasMany<PersistentProfileHost> (p => p.ProfileHosts)
				.Table("profilehost")
				.KeyColumn("profileid")
				.Cascade.SaveUpdate();
			
			HasMany<PersistentNMapResults>(p => p.AllResults)
				.Table("nmapresult")
				.KeyColumn("profileid")
				.Cascade.SaveUpdate();
			
			HasMany(p => p.Events)
				.Table("event")
				.KeyColumn("profileid")
				.Cascade.SaveUpdate();
		}
	}
}

