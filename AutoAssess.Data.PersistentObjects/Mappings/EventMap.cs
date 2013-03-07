using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.PersistentObjects;

namespace Mappings
{
	public class EventMap : ClassMap<PersistentEvent>
	{
		public EventMap ()
		{
			Table ("event");
			
			Id (i => i.ID).Column("eventid")
				.GeneratedBy.Assigned();
			
			Map(i => i.CreatedBy).Column("createdby");
			Map (i => i.CreatedOn).Column("createdon");
			Map (i => i.Description).Column("eventdescription");
			Map (i => i.IsActive).Column("isactive");
			Map (i => i.LastModifiedBy).Column("LastModifiedBy");
			Map (i => i.LastModifiedOn).Column("LastModifiedOn");
			Map (i => i.Severity).Column("eventseverity");
			Map (i => i.Timestamp).Column("eventat");
			Map (i => i.ProfileID).Column("profileid");
			Map (i => i.WebUserID).Column("webuserid");
		}
	}
}

