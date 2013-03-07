using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASNVTMap : ClassMap<PersistentOpenVASNVT>
	{
		public OpenVASNVTMap ()
		{
			Table("openvasnvt");
			
			Id(n => n.ID).Column("openvasnvtid").GeneratedBy.Assigned();
			
			Map(n => n.BugtraqID).Column("bugtraqid");
			Map(n => n.Category).Column("category");
			Map(n => n.Checksum).Column("checksum");
			Map(n => n.ChecksumAlgorithm).Column("checksumalgorithm");
			Map(n => n.Copyright).Column("copyright");
			Map(n => n.CreatedBy).Column("createdby");
			Map(n => n.CreatedOn).Column("createdon");
			Map(n => n.CVEID).Column("cveid");
			Map(n => n.CVSSBase).Column("cvssbase");
			Map(n => n.Description).Column("description");
			Map(n => n.FamilyName).Column("family");
			Map(n => n.Fingerprints).Column("fingerprints");
			Map(n => n.IsActive).Column("isactive");
			Map(n => n.LastModifiedBy).Column("lastmodifiedby");
			Map(n => n.LastModifiedOn).Column("lastmodifiedon");
			Map(n => n.Name).Column("name");
			Map(n => n.OID).Column("oid");
			Map(n => n.RiskFactor).Column("riskfactor");
			Map(n => n.Summary).Column("summary");
			Map(n => n.Tags).Column("tags");
			Map(n => n.Version).Column("version");
			Map(n => n.Xrefs).Column("xrefs");
			
			References(n => n.ParentFamily).Column("openvasnvtfamilyid");
		}
	}
}

