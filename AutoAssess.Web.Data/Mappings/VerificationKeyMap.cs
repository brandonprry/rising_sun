using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Web.Data
{
	public class VerificationKeyMap : ClassMap<VerificationKey>
	{
		public VerificationKeyMap ()
		{
			Table("userverification");
			
			Id(k => k.ID).Column("verificationid")
				.GeneratedBy.Assigned();
			
			Map (k => k.CreatedBy).Column("createdby");
			Map (k => k.CreatedOn).Column("createdon");
			Map (k => k.Key).Column("key");
			Map (k => k.IsSent).Column("issent");
			Map (k => k.IsVerifed).Column("isverified");
			Map (k => k.LastModifiedBy).Column("lastmodifiedby");
			Map (k => k.LastModifiedOn).Column("lastmodifiedon");
			Map (k => k.IsActive).Column("isactive");
			
			Map (k => k.WebUserID).Column("webuserid")
				.ReadOnly()
				.Access.ReadOnly();
			
			References<WebUser>(k => k.User)
				.Column("webuserid");
		}
	}
}

