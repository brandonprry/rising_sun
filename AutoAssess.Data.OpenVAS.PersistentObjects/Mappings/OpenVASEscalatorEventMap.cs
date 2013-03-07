using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASEscalatorEventMap : ClassMap<PersistentEscalatorEvent>
	{
		public OpenVASEscalatorEventMap ()
		{
			Table("openvasescalatorevent");
			
			Id(e => e.ID).Column("openvasescalatoreventid")
				.GeneratedBy.Assigned();
			
		}
	}
}

