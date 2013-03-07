using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects
{
	public class WapitiReferenceMap : ClassMap<PersistentWapitiReference>
	{
		public WapitiReferenceMap ()
		{
			Table ("wapitireference");
			
			Id(wr => wr.ID)
				.Column("wapitireferenceid")
				.GeneratedBy.Assigned();
			
			Map (wr => wr.Title).Column("title");
			Map (wr => wr.URL).Column("url");
			Map (wr => wr.CreatedBy).Column("createdby");
			Map (wr => wr.CreatedOn).Column("createdon");
			Map (wr => wr.LastModifiedBy).Column("lastmodifiedby");
			Map (wr => wr.LastModifiedOn).Column("lastmodifiedon");
			
			References<PersistentWapitiBug>(wr => wr.Bug)
				.Column("wapitibugid");
		}
	}
}

