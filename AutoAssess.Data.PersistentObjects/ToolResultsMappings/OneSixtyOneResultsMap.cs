using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class OneSixtyOneResultsMap : ClassMap<PersistentOneSixtyOneResults>
	{
		public OneSixtyOneResultsMap ()
		{			
			Table("onesixtyoneresult");
			
			Id(n => n.ID).Column("onesixtyoneresultid");
			
			Map(n => n.CreatedBy).Column("createdby");
			Map(n => n.CreatedOn).Column("createdon");
			Map(n => n.FullOutput).Column("fulloutput");
			Map(n => n.HostPortID).Column("hostportid");
			Map(n => n.IsActive).Column("isactive");
			Map(n => n.LastModifiedBy).Column("lastmodifiedby");
			Map(n => n.LastModifiedOn).Column("lastmodifiedon");
			
			References(n => n.ParentProfile)
				.Column("profileid");
			References(n => n.User)
				.Column("userid");

		}
	}
}

