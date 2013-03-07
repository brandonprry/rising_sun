using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASScheduleMap : ClassMap<PersistentOpenVASSchedule>
	{
		public OpenVASScheduleMap ()
		{
			Table("openvasschedule");
			
			Id(s => s.ID).Column("openvasscheduleid")
				.GeneratedBy.Assigned();
			
			
			Map(s => s.CreatedBy).Column("createdby");
			Map(s => s.CreatedOn).Column("createdon");
			Map(s => s.IsActive).Column("isactive");
			Map(s => s.LastModifiedBy).Column("lastmodifiedby");
			Map(s => s.LastModifiedOn).Column("lastmodifiedon");
			Map(s => s.RemoteScheduleID).Column("remotescheduleid");
		}
	}
}

