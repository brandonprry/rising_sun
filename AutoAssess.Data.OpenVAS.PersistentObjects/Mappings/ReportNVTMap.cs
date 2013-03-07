using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.OpenVAS.PersistentObjects;

namespace Mappings
{
	public class ReportNVTMap: ClassMap<PersistentReportNVT>
	{
		public ReportNVTMap ()
		{
			Table ("openvasreportnvt");
			
			Id (c => c.ID).Column("openvasreportnvtid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.CVSSBaseValue).Column("cvssbasescore");
			Map (c => c.Name).Column("name");
			Map (c => c.OID).Column("oid");
			Map (c => c.RiskFactor).Column("riskfactor");
		}
	}
}

