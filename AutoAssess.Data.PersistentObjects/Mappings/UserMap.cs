using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class UserMap : ClassMap<PersistentUser>
	{
		public UserMap ()
		{
			Table("\"user\"");
			
			Id(u => u.ID).Column("userid").GeneratedBy.Assigned();
			
			Map(u => u.FullName).Column("fullname");
			Map(u => u.Username).Column("username");
			Map(u => u.UserLevel).Column("userlevel");
			Map(u => u.CreatedBy).Column("createdby");
			Map(u => u.CreatedOn).Column("createdon");
			Map(u => u.HasAPIAccess).Column("hasapiaccess");
			Map(u => u.IsActive).Column("isactive");
			Map(u => u.LastModifiedBy).Column("lastmodifiedby");
			Map(u => u.LastModifiedOn).Column("lastmodifiedon");
			
			References(u => u.Client)
				.Column("clientid")
				.Cascade.All();
		}
	}
}

