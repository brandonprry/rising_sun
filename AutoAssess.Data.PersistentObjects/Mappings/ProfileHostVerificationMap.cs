using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class ProfileHostVerificationMap : ClassMap<PersistentProfileHostVerification>
	{
		public ProfileHostVerificationMap ()
		{
			Table ("profilehostverification");
			
			Id (p => p.ID)
				.Column("profilehostverificationid")
				.GeneratedBy.Assigned();
			
			Map (p => p.CreatedBy).Column("createdby");
			Map (p => p.CreatedOn).Column("createdon");
			Map (p => p.IsActive).Column("isactive");
			Map (p => p.LastModifiedBy).Column("lastmodifiedby");
			Map (p => p.LastModifiedOn).Column("lastmodifiedon");
			Map (p => p.VerificationData).Column("verificationdata");
			Map (p => p.VerificationFileName).Column("verificationfilename");
			Map (p => p.WhoisEmail).Column("whoisregex");
			Map (p => p.VerificationError).Column("verificationerror");
			
			References(p => p.ProfileHost)
				.Column("profilehostid");
		}
	}
}

