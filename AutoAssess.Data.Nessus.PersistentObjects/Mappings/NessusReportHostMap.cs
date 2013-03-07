using System;
using AutoAssess.Data.Nessus.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class NessusReportHostMap : ClassMap<PersistentNessusReportHost>
	{
		public NessusReportHostMap ()
		{
			Table ("nessusreporthost");
			
			Id (n => n.ID).Column("nessusreporthostid")
				.GeneratedBy.Assigned();
			
			Map (n => n.CreatedBy).Column("createdby");
			Map (n => n.CreatedOn).Column("createdon");
			Map (n => n.IsActive).Column("isactive");
			Map (n => n.LastModifiedBy).Column("lastmodifiedby");
			Map (n => n.LastModifiedOn).Column("lastmodifiedon");
			Map (n => n.Name).Column("name");
			
			HasMany(n => n.PersistentReportItems)
				.Table("nessusreportitem")
				.KeyColumn("nessusreporthostid")
				.Cascade
				.SaveUpdate();
			
			References(n => n.PersistentHostProperties)
				.Column("nessushostpropertiesid")
				.Cascade.SaveUpdate();
		}
	}
}

