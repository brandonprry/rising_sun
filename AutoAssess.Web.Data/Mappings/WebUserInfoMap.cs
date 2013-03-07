using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Web.Data
{
	public class WebUserInfoMap : ClassMap<WebUserInfo>
	{
		public WebUserInfoMap ()
		{
			Table ("webuserinfo");
			
			Id (w => w.ID).Column("webuserinfoid")
				.GeneratedBy.Assigned();
			
			Map (w => w.FirstName).Column("firstname");
			Map (w => w.LastName).Column("lastname");
			Map (w => w.IsActive).Column("isactive");
			Map (w => w.LastLogin).Column("lastlogin");
			Map (w => w.PrimaryPhone).Column("primaryphone");
			Map (w => w.SecondaryPhone).Column("secondaryphone");
			Map (w => w.Hosts).Column("hosts");
			Map (w => w.MainSecurityConcern).Column("mainsecurityconcern");
			Map (w => w.PrimaryWebsite).Column("primarywebsite");
			Map (w => w.Provider).Column("provider");
			
			Map (w => w.WebUserID).Column("webuserid")
				.ReadOnly()
				.Access.ReadOnly();
			
			References<WebUser>(w => w.WebUser)
				.Column("webuserid");
		}
	}
}

