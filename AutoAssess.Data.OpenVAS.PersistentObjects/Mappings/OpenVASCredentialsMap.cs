using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASCredentialsMap : ClassMap<PersistentOpenVASCredentials>
	{
		public OpenVASCredentialsMap ()
		{
			Table("openvascredential");
			
			Id(c => c.ID).Column("openvascredentialid")
				.GeneratedBy.Assigned();
			
			
			Map(c => c.CreatedBy).Column("createdby");
			Map(c => c.CreatedOn).Column("createdon");
			Map(c => c.IsActive).Column("isactive");
			Map(c => c.LastModifiedBy).Column("lastmodifiedby");
			Map(c => c.LastModifiedOn).Column("lastmodifiedon");
			Map(c => c.Password).Column("password"); //better fucking encrypt me god damnit
			Map(c => c.Username).Column("username");
			Map(c => c.RemoteCredentialID).Column("remotecredentialid");
		}
	}
}

