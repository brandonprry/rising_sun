using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class WhoisResultsMap : ClassMap<PersistentWhoisResults>
	{
		public WhoisResultsMap ()
		{
			Table("whoisresult");
			
			Id(w => w.ID).Column("whoisresultid").GeneratedBy.Assigned();
			
			Map(w => w.CreatedBy).Column("createdby");
			Map(w => w.CreatedOn).Column("createdon");
			Map(w => w.FullOutput).Column("fulloutput");
			Map(w => w.IsActive).Column("isactive");
			Map(w => w.LastModifiedBy).Column("lastmodifiedby");
			Map(w => w.LastModifiedOn).Column("lastmodifiedon");
			
			References(w => w.ParentProfile)
				.Column("profileid");
			References(w => w.ParentNMapHost)
				.Column("nmaphostid");
			References(w => w.User)
				.Column("userid");
		}
	}
}

