using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASNoteMap : ClassMap<PersistentOpenVASNote>
	{
		public OpenVASNoteMap ()
		{
			Table("openvasnote");
			
			Id(n => n.ID).Column("openvasnoteid")
				.GeneratedBy.Assigned();
			
			
			References(n => n.NVT).Column("openvasnvtid");
			References(n => n.Report).Column("openvasreportid");
			References(n => n.Task).Column("openvastaskid");
			
			Map(n => n.Comment).Column("comment");
			Map(n => n.Content).Column("content");
			Map(n => n.CreatedBy).Column("createdby");
			Map(n => n.CreatedOn).Column("createdon");
			Map(n => n.Hosts).Column("hosts");
			Map(n => n.IsActive).Column("isactive");
			Map(n => n.LastModifiedBy).Column("lastmodifiedby");
			Map(n => n.LastModifiedOn).Column("lastmodifiedon");
			Map(n => n.Port).Column("port");
			Map(n => n.RemoteNoteID).Column("remotenoteid");
			Map(n => n.ThreatLevel).Column("threatlevel");
		}
	}
}

