using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class NVDMap : ClassMap<PersistentNVD>
	{
		public NVDMap ()
		{
			Table("nvd");
			
			Id(nvd => nvd.ID).Column("nvdid")
				.GeneratedBy
				.Assigned();
			
			Map(nvd => nvd.CVEID).Column("remotecveid");
			Map(nvd => nvd.CWE).Column("cwe");
			Map(nvd => nvd.DatePublished).Column("datepublished");
			Map(nvd => nvd.NVDID).Column("remotenvdid");
			Map(nvd => nvd.Summary).Column("summary");
			Map(nvd => nvd.LastModified).Column("lastmodified");
			
			References<PersistentCVE>(nvd => nvd.CVE)
				.Column("cveid");
			
			References<PersistentCVSS>(nvd => nvd.CVSS)
				.Column("nvdcvssid")
				.Cascade.All();
			
			HasMany<PersistentVulnerableSoftware>(nvd => nvd.VulnerableSoftware)
				.KeyColumn("nvdid")
				.Table("nvdvulnerablesoftware")
				.Cascade.All()
				.AsBag();
			
			HasMany<PersistentNVDReference>(nvd => nvd.References)
				.KeyColumn("nvdid")
				.Table("nvdreference")
				.Cascade.All()
				.AsBag();
		}
	}
}

