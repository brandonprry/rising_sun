using System;
using AutoAssess.Data.Nessus.BusinessObjects;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	[Serializable]
	public class PersistentNessusReportItem : NessusReportItem
	{
		public PersistentNessusReportItem ()
		{
		}
		
		public PersistentNessusReportItem (NessusReportItem item)
		{
			this.BID = item.BID;
			this.CERT = item.CERT;
			this.CPE = item.CPE;
			this.CVE = item.CVE;
			this.CVSSBaseScore = item.CVSSBaseScore;
			this.CVSSTemporalScore = item.CVSSTemporalScore;
			this.CVSSTemporalVector = item.CVSSTemporalVector;
			this.CVSSVector = item.CVSSVector;
			this.Description = item.Description;
			this.EDBID = item.EDBID;
			this.ExploitEase = item.ExploitEase;
			this.ExploitInCore = item.ExploitInCore;
			this.ExploitInMetasploit = item.ExploitInMetasploit;
			this.ExploitIsAvailable = item.ExploitIsAvailable;
			this.FileName = item.FileName;
			this.IAVA = item.IAVA;
			this.MetasploitName = item.MetasploitName;
			this.OSVDB = item.OSVDB;
			this.PatchPublicationDate = item.PatchPublicationDate;
			this.PluginFamily = item.PluginFamily;
			this.PluginID = item.PluginID;
			this.PluginModificationDate = item.PluginModificationDate;
			this.PluginName = item.PluginName;
			this.PluginOutput = item.PluginOutput;
			this.PluginPublicationDate = item.PluginPublicationDate;
			this.PluginType = item.PluginType;
			this.Port = item.Port;
			this.Protocol = item.Protocol;
			this.RiskFactor = item.RiskFactor;
			this.SeeAlso = item.SeeAlso;
			this.ServiceName = item.ServiceName;
			this.Severity = item.Severity;
			this.Solution = item.Solution;
			this.Synopsis = item.Synopsis;
			this.VulnerabilityPublicationDate = item.VulnerabilityPublicationDate;
			this.XREF = item.XREF;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo(Guid owner)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

