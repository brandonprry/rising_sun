using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class RouteMap : ClassMap<PersistentRoute>
	{
		public RouteMap ()
		{
			Table("tracerouteroute");
			
			Id(r => r.ID).Column("tracerouterouteid").GeneratedBy.Assigned();
			
			Map(r => r.CreatedBy).Column("createdby");
			Map(r => r.CreatedOn).Column("createdon");
			Map(r => r.FirstHostname).Column("firsthostname");
			Map(r => r.FirstIPAddress).Column("firstipaddress");
			Map(r => r.FirstResult).Column("firstresult");
			Map(r => r.Hop).Column("hop");
			Map(r => r.IsActive).Column("isactive");
			Map(r => r.LastModifiedBy).Column("lastmodifiedby");
			Map(r => r.LastModifiedOn).Column("lastmodifiedon");
			Map(r => r.SecondHostname).Column("secondhostname");
			Map(r => r.SecondIPAddress).Column("secondipaddress");
			Map(r => r.SecondResult).Column("secondresult");
			Map(r => r.ThirdHostname).Column("thirdhostname");
			Map(r => r.ThirdIPAddress).Column("thirdipaddress");
			Map(r => r.ThirdResult).Column("thirdresult");
			
			References(r => r.ParentResults)
				.Column("tracerouteresultid");
			
			References(r => r.ParentProfile)
				.Column("profileid");
			
			References(r => r.User)
				.Column("userid");
			
		}
	}
}

