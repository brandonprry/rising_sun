using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class SQLMapKeyMap : ClassMap<PersistentSQLMapVulnerability>
	{
		public SQLMapKeyMap ()
		{
			Table("sqlmapresultchildren");
			
			Id(k => k.ID).Column("sqlmapresultchildid").GeneratedBy.Assigned();
			
			Map(k => k.CreatedBy).Column("createdby");
			Map(k => k.CreatedOn).Column("createdon");
			Map(k => k.IsActive).Column("isactive");
			Map(k => k.LastModifiedBy).Column("lastmodifiedby");
			Map(k => k.LastModifiedOn).Column("lastmodifiedon");
			Map(k => k.Parameter).Column("parameter");
			Map(k => k.PayloadType).Column("payloadtype");
			Map(k => k.Payload).Column("payload");
			Map(k => k.Target).Column("url");
			Map(k => k.Title).Column("title");
			Map(k => k.HTTPRequestType).Column("httprequesttype");
			
			//References(k => k.ParentScan).Column("scanid");
			//References(k => k.User).Column("userid");
			
			References(k => k.ParentResults).Column("sqlmapresultid");
			
		}
	}
}

