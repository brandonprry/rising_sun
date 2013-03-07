using System;
using FluentNHibernate.Mapping;


namespace AutoAssess.Data.PersistentObjects
{
	public class WapitiBugMap : ClassMap<PersistentWapitiBug>
	{
		public WapitiBugMap ()
		{
			Table ("wapitibug");
			
			Id (wb => wb.ID)
				.Column("wapitibugid")
				.GeneratedBy.Assigned();
			
			Map (wb => wb.CreatedBy).Column("createdby");
			Map (wb => wb.CreatedOn).Column("createdon");
			Map (wb => wb.Host).Column("host");
			Map (wb => wb.Info).Column("info");
			Map (wb => wb.IsActive).Column("isactive");
			Map (wb => wb.LastModifiedBy).Column("lastmodifiedby");
			Map (wb => wb.LastModifiedOn).Column("lastmodifiedon");
			Map (wb => wb.Level).Column("level");
			Map (wb => wb.Parameter).Column("parameter");
			Map (wb => wb.Port).Column("port");
			Map (wb => wb.Timestamp).Column("wapititimestamp");
			Map (wb => wb.Type).Column("type");
			Map (wb => wb.URL).Column("url");
			
			References<PersistentWapitiResults>(wb => wb.ParentResults)
				.Column("wapitiresultid");
			
			HasMany<PersistentWapitiReference>(wb => wb.References)
				.KeyColumn("wapitibugid")
				.Cascade.All();
		}
	}
}

