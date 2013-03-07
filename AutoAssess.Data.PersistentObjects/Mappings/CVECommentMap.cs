using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class CVECommentMap : ClassMap<PersistentCVEComment>
	{
		public CVECommentMap ()
		{
			Table("cvecomment");
			
			Id(c => c.ID).Column("cvecommentid")
				.GeneratedBy
				.Assigned();
			
			Map(c => c.Comment).Column("comment");
			Map(c => c.Voter).Column("voter");
			Map(c => c.CreatedBy).Column("createdby");
			Map(c => c.CreatedOn).Column("createdon");
			Map(c => c.IsActive).Column("isactive");
			Map(c => c.LastModifiedBy).Column("lastmodifiedby");
			Map(c => c.LastModifiedOn).Column("lastmodifiedon");
			
			References<PersistentCVE>(c => c.CVE)
				.Column("cveid");
		}
	}
}

