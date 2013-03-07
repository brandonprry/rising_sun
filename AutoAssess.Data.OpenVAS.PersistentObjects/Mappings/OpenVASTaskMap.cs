using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASTaskMap : ClassMap<PersistentOpenVASTask>
	{
		public OpenVASTaskMap ()
		{
			Table("openvastask");
			
			Id(t => t.ID).Column("openvastaskid")
				.GeneratedBy.Assigned();
			
			References(t => t.Config).Column("openvasconfigid");
			References(t => t.Escalator).Column("openvasescalatorid");
			References(t => t.Schedule).Column("openvasscheduleid");
			References(t => t.Slave).Column("openvasslaveid");
			References(t => t.Target).Column("openvastargetid");
			
			Map (t => t.ScanID).Column("scanid");
			Map (t => t.FullReport).Column("fullreport");
			Map(t => t.Comment).Column("comment");
			Map(t => t.CreatedBy).Column("createdby");
			Map(t => t.CreatedOn).Column("createdon");
			Map(t => t.IsActive).Column("isactive");
			Map(t => t.LastModifiedBy).Column("lastmodifiedby");
			Map(t => t.LastModifiedOn).Column("lastmodifiedon");
			Map(t => t.Name).Column("name");
			Map(t => t.RemoteTaskID).Column("remotetaskid");
		}
	}
}

