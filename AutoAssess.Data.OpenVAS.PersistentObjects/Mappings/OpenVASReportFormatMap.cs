using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASReportFormatMap : ClassMap<PersistentOpenVASReportFormat>
	{
		public OpenVASReportFormatMap ()
		{
			Table("openvasreportformat");
			
			Id(f => f.ID).Column("openvasreportformatid")
				.GeneratedBy.Assigned();
			
			
			Map(f => f.CreatedBy).Column("createdby");
			Map(f => f.CreatedOn).Column("createdon");
			Map(f => f.IsActive).Column("isactive");
			Map(f => f.LastModifiedBy).Column("lastmodifiedby");
			Map(f => f.LastModifiedOn).Column("lastmodifiedon");
			Map(f => f.RemoteReportFormatID).Column("remotereportformatid");
		}
	}
}

