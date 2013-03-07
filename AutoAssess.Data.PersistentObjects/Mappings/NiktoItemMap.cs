using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.PersistentObjects;

namespace Mappings
{
	public class NiktoItemMap : ClassMap<PersistentNiktoItem>
	{
		public NiktoItemMap ()
		{
			Table ("niktoitem");
			
			Id (i => i.ID).Column("niktoitemid")
				.GeneratedBy.Assigned();
			
			
			Map (i => i.Data).Column("data");
			Map (i => i.CreatedBy).Column("createdby");
			Map (i => i.CreatedOn).Column("createdon");
			Map (i => i.IsActive).Column("isactive");
			Map (i => i.LastModifiedBy).Column("lastmodifiedby");
			Map (i => i.LastModifiedOn).Column("lastmodifiedon");
			
			References(i => i.ParentResults)
				.Column("niktoresultid");
		}
	}
}

