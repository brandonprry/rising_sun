using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASEscalatorMethodMap : ClassMap<PersistentEscalatorMethod>
	{
		public OpenVASEscalatorMethodMap ()
		{
			Table("openvasescalatormethod");
			
			Id(e => e.ID).Column("openvasescalatormethodid")
				.GeneratedBy.Assigned();
			
		}
	}
}

