using System;
using AutoAssess.Web.Data;
using FluentNHibernate.Mapping;

namespace AutoAssess.Web
{
	public class WebUserMap : ClassMap<WebUser>
	{
		public WebUserMap ()
		{
			Table("webuser");
			
			Id(u => u.UserID).Column("webuserid")
				.GeneratedBy.Assigned();
			Map(u => u.EmailAddress).Column("emailaddress");
			Map(u => u.Username).Column("username");
			Map(u => u.PasswordHash).Column("passwordhash");
			Map (u => u.UserLevel).Column("userlevel");
			Map(u => u.IsActive).Column("isactive");
		}
	}
}

