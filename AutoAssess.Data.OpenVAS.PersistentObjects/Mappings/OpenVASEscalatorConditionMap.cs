using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASEscalatorConditionMap : ClassMap<PersistentEscalatorCondition>
	{
		public OpenVASEscalatorConditionMap ()
		{
			Table("openvasescalatorcondition");
			
			Id(e => e.ID).Column("openvasescalatorconditionid")
				.GeneratedBy.Assigned();
			
		}
	}
}

