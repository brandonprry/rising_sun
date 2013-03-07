using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class CVSSMap : ClassMap<PersistentCVSS>
	{
		public CVSSMap ()
		{
			Table("nvdcvss");
			
			Id(cvss => cvss.ID).Column("nvdcvssid")
				.GeneratedBy
				.Assigned();
			
			Map(cvss => cvss.Authentication).Column("authentication");
			Map(cvss => cvss.AvailabilityImpact).Column("availabilityimpact");
			Map(cvss => cvss.Complexity).Column("complexity");
			Map(cvss => cvss.ConfidentialityImpact).Column("confidentialityimpact");
			Map(cvss => cvss.IntegrityImpact).Column("integrityimpact");
			Map(cvss => cvss.Score).Column("score");
			Map(cvss => cvss.Vector).Column("vector");
			Map(cvss => cvss.CreatedBy).Column("createdby");
			Map(cvss => cvss.CreatedOn).Column("createdon");
			Map(cvss => cvss.IsActive).Column("isactive");
			Map(cvss => cvss.LastModifiedBy).Column("lastmodifiedby");
			Map(cvss => cvss.LastModifiedOn).Column("lastmodifiedon");
		}
	}
}

