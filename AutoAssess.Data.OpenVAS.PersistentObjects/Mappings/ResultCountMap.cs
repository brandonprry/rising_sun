using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.OpenVAS.PersistentObjects;

namespace Mappings
{
	public class ResultCountMap: ClassMap<PersistentResultCount>
	{
		public ResultCountMap ()
		{
			Table ("openvasresultcount");
			
			Id (c => c.ID).Column("openvasresultcountid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
		}
	}
}

