using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class ScanOptions 
	{
		public ScanOptions ()
		{
		}
		
		public virtual bool IsDSXS { get; set; }
		
		public virtual bool IsSQLMap { get; set; }
		
		public virtual bool IsOpenVASAssessment { get; set; }
		
		public virtual bool IsNexposeAssessment { get; set; }
		
		public virtual bool IsNessusAssessment { get; set; }
		
		public virtual bool IsMetasploitAssessment { get; set; }
		
		public virtual bool MetasploitDiscovers { get; set; }
		
		public virtual bool MetasploitBruteforces { get; set; }
		
		public virtual bool IsBruteForce { get; set; }
		
		public virtual int RemoteNessusPolicyID { get; set; }
		
		public virtual string RemoteOpenVASConfigID { get; set; }
		
		public virtual int RemoteNexposeSiteID { get; set; }
		
		public virtual Scan ParentScan { get; set; }
		
		public virtual SQLMapOptions SQLMapOptions { get; set; }
		
		public virtual string ToBusinessXml()
		{
			string xml = "<scanOptions>";
			
			xml = xml + "<nessusPolicyID>" + this.RemoteNessusPolicyID + "</nessusPolicyID>";
			xml = xml + "<openvasConfigID>" + this.RemoteOpenVASConfigID + "</openvasConfigID>";
			xml = xml + "<nexposeSiteID>" + this.RemoteNexposeSiteID + "</nexposeSiteID>";
			xml = xml + "<isBruteforce>" + this.IsBruteForce + "</isBruteforce>";
			xml = xml + "<isSQLMap>" + this.IsSQLMap + "</isSQLMap>";
			xml = xml + "<isDSXS>" + this.IsDSXS + "</isDSXS>";
			xml = xml + "<isOpenVASAssessment>" + this.IsOpenVASAssessment + "</isOpenVASAssessment>";
			xml = xml + "<isNessusAssessment>" + this.IsNessusAssessment + "</isNessusAssessment>";
			xml = xml + "<isNexposeAssessment>" + this.IsNexposeAssessment + "</isNexposeAssessment>";
			xml = xml + "<isMetasploitAssessment>" + this.IsMetasploitAssessment + "</isMetasploitAssessment>";
			
			if (this.SQLMapOptions != null)
				xml = xml + this.SQLMapOptions.ToBusinessXml();
			
			xml = xml + "</scanOptions>";
			
			return xml;
		}
	}
}

