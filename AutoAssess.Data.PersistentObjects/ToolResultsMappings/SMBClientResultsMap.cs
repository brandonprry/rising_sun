using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class SMBClientResultsMap : ClassMap<PersistentSMBClientResults>
	{
		public SMBClientResultsMap ()
		{
			Table("smbresult");
			
			Id(s => s.ID).Column("smbresultid").GeneratedBy.Assigned();
			
			Map(s => s.CreatedBy).Column("createdby");
			Map(s => s.CreatedOn).Column("createdon");
			Map(s => s.FullOutput).Column("fulloutput");
			Map(s => s.IsActive).Column("isactive");
			Map(s => s.LastModifiedBy).Column("lastmodifiedby");
			Map(s => s.LastModifiedOn).Column("lastmodifiedon");
			
			References(s => s.ParentScan)
				.Column("scanid");
			References(s => s.User)
				.Column("userid");
			References(s => s.ParentPort)
				.Column("hostportid");
			
			HasMany<PersistentShareDetails>(s => s.ShareDetails)
				.KeyColumn("smbresultid")
				.Cascade.SaveUpdate();
		}
	}
}

