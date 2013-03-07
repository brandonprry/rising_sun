using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.OpenVAS.PersistentObjects;

namespace Mappings
{
	public class ReportPortMap: ClassMap<PersistentReportPort>
	{
		public ReportPortMap ()
		{
			Table ("openvasreportport");
			
			Id (c => c.ID).Column("openvasreportportid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
		}
	}
}

