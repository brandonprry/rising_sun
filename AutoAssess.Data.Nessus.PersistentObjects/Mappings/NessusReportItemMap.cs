using System;
using FluentNHibernate.Mapping;
using AutoAssess.Data.Nessus.PersistentObjects;

namespace Mappings
{
	public class NessusReportItemMap : ClassMap<PersistentNessusReportItem>
	{
		public NessusReportItemMap ()
		{
			Table ("nessusreportitem");
			
			Id (n => n.ID).Column("nessusreportitemid")
				.GeneratedBy.Assigned();
			
			Map (n => n.BID).Column("bid");
			Map (n => n.CERT).Column("cert");
			Map (n => n.CPE).Column("cpe");
			Map (n => n.CreatedBy).Column("createdby");
			Map (n => n.CreatedOn).Column("createdon");
			Map (n => n.CVE).Column ("cve");
			Map (n => n.CVSSBaseScore).Column("cvssbasescore");
			Map (n => n.CVSSTemporalScore).Column("cvsstemporalscore");
			Map (n => n.CVSSTemporalVector).Column("cvsstemporalvector");
			Map (n => n.CVSSVector).Column("cvssvector");
			Map (n => n.Description).Column("description");
			Map (n => n.EDBID).Column("edbid");
			Map (n => n.ExploitEase).Column("exploitease");
			Map (n => n.ExploitInCore).Column("exploitincore");
			Map (n => n.ExploitInMetasploit).Column("exploitinmetasploit");
			Map (n => n.ExploitIsAvailable).Column("exploitisavailable");
			Map (n => n.FileName).Column("filename");
			Map (n => n.IAVA).Column("iava");
			Map (n => n.IsActive).Column("isactive");
			Map (n => n.LastModifiedBy).Column("lastmodifiedby");
			Map (n => n.LastModifiedOn).Column("lastmodifiedon");
			Map (n => n.MetasploitName).Column("metasploitname");
			Map (n => n.OSVDB).Column("osvdb");
			Map (n => n.PatchPublicationDate).Column("patchpublicationdate");
			Map (n => n.PluginFamily).Column("pluginfamily");
			Map (n => n.PluginID).Column("pluginid");
			Map (n => n.PluginModificationDate).Column("pluginmodificationdate");
			Map (n => n.PluginName).Column("pluginname");
			Map (n => n.PluginOutput).Column("pluginoutput");
			Map (n => n.PluginPublicationDate).Column("pluginpublicationdate");
			Map (n => n.PluginType).Column("plugintype");
			Map (n => n.Port).Column("port");
			Map (n => n.Protocol).Column("protocol");
			Map (n => n.RiskFactor).Column("riskfactor");
			Map (n => n.SeeAlso).Column("seealso");
			Map (n => n.ServiceName).Column("servicename");
			Map (n => n.Severity).Column("severity");
			Map (n => n.Solution).Column("solution");
			Map (n => n.Synopsis).Column("synopsis");
			Map (n => n.VulnerabilityPublicationDate).Column("vulnerabilitypublicationdate");
			Map (n => n.XREF).Column("xref");
		}
	}
}

