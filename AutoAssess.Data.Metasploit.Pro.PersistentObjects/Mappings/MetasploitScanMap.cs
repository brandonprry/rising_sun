using System;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class MetasploitScanMap : ClassMap<PersistentMetasploitScan>
	{
		public MetasploitScanMap ()
		{
			
			Table ("metasploitscan");
			
			Id (c => c.ID).Column("metasploitscanid")
				.GeneratedBy.Assigned();
			
			Map (c => c.CreatedBy).Column("createdby");
			Map (c => c.CreatedOn).Column("createdon");
			Map (c => c.LastModifiedBy).Column("lastmodifiedby");
			Map (c => c.LastModifiedOn).Column("lastmodifiedon");
			Map (c => c.IsActive).Column("isactive");
			Map (c => c.ParentScanID).Column("scanid");
			
//			
//			this.PersistentHosts = new List<PersistentMetasploitHost>();
//			this.PersistentCredentials = new List<PersistentMetasploitCredential>();
//			this.PersistentEvents = new List<PersistentMetasploitEvent>();
//			this.PersistentModuleDetails = new List<PersistentMetasploitModuleDetails>();
//			this.PersistentSessions = new List<PersistentMetasploitExploitSession>();
//			this.PersistentWebForms = new List<PersistentMetasploitWebForm>();
//			this.PersistentWebPages = new List<PersistentMetasploitWebPage>();
//			this.PersistentWebsites = new List<PersistentMetasploitWebsite>();
//			this.PersistentWebVulnerabilities = new List<PersistentMetasploitWebVulnerability>();
			
			HasMany(s => s.PersistentHosts)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
			
			HasMany(s => s.PersistentCredentials)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
			
			HasMany(s => s.PersistentEvents)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
			
			HasMany (s => s.PersistentModuleDetails)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
			
			HasMany(s => s.PersistentSessions)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
			
			HasMany(s => s.PersistentWebForms)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
			
			HasMany(s => s.PersistentWebPages)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
			
			HasMany(s => s.PersistentWebsites)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
			HasMany(s => s.PersistentWebVulnerabilities)
				.KeyColumn("metasploitscanid")
				.Cascade.All();
		}
	}
}

