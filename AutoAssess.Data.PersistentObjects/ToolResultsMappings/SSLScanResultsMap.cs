using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class SSLScanResultsMap : ClassMap<PersistentSSLScanResults>
	{
		public SSLScanResultsMap ()
		{
			Table("sslscanresult");
			
			Id(s => s.ID).Column("sslscanresultid").GeneratedBy.Assigned();
			
			Map(s => s.CreatedBy).Column("createdby");
			Map(s => s.CreatedOn).Column("createdon");
			Map(s => s.FullOutput).Column("fulloutput");
			Map(s => s.HostPortID).Column("hostportid");
			Map(s => s.IsActive).Column("isactive");
			Map(s => s.LastModifiedBy).Column("lastmodifiedby");
			Map(s => s.LastModifiedOn).Column("lastmodifiedon");
			
			References(s => s.ParentProfile)
				.Column("profileid");
			References(s => s.User)
				.Column("userid");
		}
	}
}

