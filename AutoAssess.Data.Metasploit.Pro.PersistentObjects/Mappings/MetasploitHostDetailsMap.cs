using System;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class MetasploitHostDetailsMap : ClassMap<PersistentMetasploitHostDetails>
	{
		public MetasploitHostDetailsMap ()
		{
			
			Table ("metasploithostdetail");
			
			Id (c => c.ID).Column("metasploithostdetailid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.NexposeConsoleID).Column("nexposeconsoleid");
			Map (c => c.NexposeDeviceID).Column("nexposedeviceid");
			Map (c => c.NexposeRiskScore).Column("nexposeriskscore");
			Map (c => c.NexposeScanTemplate).Column("nexposescantemplate");
			Map (c => c.NexposeSiteImportance).Column("nexposesiteimportance");
			Map (c => c.NexposeSiteName).Column("nexposesitename");
			Map (c => c.RemoteHostID).Column("remotehostid");
			Map (c => c.RemoteID).Column("remoteid");
			Map (c => c.Source).Column("source");
			
		}
	}
}

