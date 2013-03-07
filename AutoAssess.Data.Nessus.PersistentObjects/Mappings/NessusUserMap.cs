using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	public class NessusUserMap : ClassMap<PersistentNessusUser>
	{
		public NessusUserMap ()
		{
			Table("nessususer");
			
			Id(u => u.ID).Column("nessususerid").GeneratedBy.Assigned();
			
			Map(u => u.CreatedBy).Column("createdby");
			Map(u => u.CreatedOn).Column("createdon");
			Map(u => u.IsActive).Column("isactive");
			Map(u => u.IsAdmin).Column("isadmin");
			Map(u => u.LastLogin).Column("lastlogin");
			Map(u => u.LastModifiedBy).Column("lastmodifiedby");
			Map(u => u.LastModifiedOn).Column("lastmodifiedon");
			Map(u => u.Name).Column("name");
			
		}
	}
}

