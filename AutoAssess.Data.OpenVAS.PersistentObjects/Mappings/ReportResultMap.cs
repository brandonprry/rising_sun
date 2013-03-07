using System;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class ReportResultMap: ClassMap<PersistentReportResult>
	{
		public ReportResultMap ()
		{
			Table ("openvasreportresult");
			
			Id (c => c.ID).Column("openvasreportresultid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.Description).Column("description");
			Map (c => c.Host).Column("host");
			Map (c => c.Port).Column("port");
			Map (c => c.Subnet).Column("subnet");
			Map (c => c.Threat).Column("threat");
			
			References (c => c.PersistentNVT)
				.Column("openvasnvtid")
				.Cascade.All();
		}
	}
}

