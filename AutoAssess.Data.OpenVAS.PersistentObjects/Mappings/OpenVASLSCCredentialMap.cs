using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASLSCCredentialMap : ClassMap<PersistentOpenVASLSCCredential>
	{
		public OpenVASLSCCredentialMap ()
		{
			Table("openvaslsccredential");
			
			Id(c => c.ID).Column("openvaslsccredentialid")
				.GeneratedBy.Assigned();
			
		}
	}
}

