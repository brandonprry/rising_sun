using System;
using AutoAssess.Data.BusinessObjects;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentScanOptions : ScanOptions, IEntity
	{
		public PersistentScanOptions ()
		{
		}
		
		public PersistentScanOptions(XmlNode scanOptions)
		{
			foreach (XmlNode child in scanOptions.ChildNodes)
			{
				if (child.Name == "id")
					this.ID = new Guid(child.InnerText);
				else if (child.Name == "createdOn")
					this.CreatedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "createdBy")
					this.CreatedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedBy")
					this.LastModifiedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedOn")
					this.LastModifiedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "isActive")
					this.IsActive = Boolean.Parse(child.InnerText);
				else if (child.Name == "sqlmapOptions")
					this.SQLMapOptions = new PersistentSQLMapOptions(child);
				else if (child.Name == "isBruteforce")
					this.IsBruteForce = Boolean.Parse(child.InnerText);
				else if (child.Name == "isDSXS")
					this.IsDSXS = Boolean.Parse(child.InnerText);
				else if (child.Name == "isSQLMap")
					this.IsSQLMap = Boolean.Parse(child.InnerText);
				else if (child.Name == "isMetasploitAssessment")
					this.IsMetasploitAssessment = Boolean.Parse(child.InnerText);
				else if (child.Name == "isNessusAssessment")
					this.IsNessusAssessment = Boolean.Parse(child.InnerText);
				else if (child.Name == "isNexposeAssessment")
					this.IsNexposeAssessment = Boolean.Parse(child.InnerText);
				else if (child.Name == "isOpenVASAssessment")
					this.IsOpenVASAssessment = Boolean.Parse(child.InnerText);
				else if (child.Name == "nessusPolicyID")
					this.RemoteNessusPolicyID = int.Parse(child.InnerText);
				else if (child.Name == "openvasConfigID")
					this.RemoteOpenVASConfigID = child.InnerText;
				else if (child.Name == "nexposeSiteID")
					this.RemoteNexposeSiteID = int.Parse(child.InnerText);
				else
					throw new Exception("I don't know what to do with element: " + child.Name);
			}
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual IList<PersistentVirtualMachine> VirtualMachines { get; set; }
		
		public virtual PersistentScan ParentScan 
		{
			get { return base.ParentScan as PersistentScan; }
			set { base.ParentScan = value as Scan; }
		}
		
		public virtual PersistentSQLMapOptions SQLMapOptions
		{
			get { return base.SQLMapOptions as PersistentSQLMapOptions; }
			set { base.SQLMapOptions = value as SQLMapOptions; }
		}
		
		public virtual void SetCreationInfo(Guid userID)
		{
			this.ID = Guid.NewGuid();
			this.CreatedBy = userID;
			this.CreatedOn = DateTime.Now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = isActive;
		}
		public virtual string ToPersistentXml()
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
			xml = xml + "<createdOn>" + this.CreatedOn.ToString() + "</createdOn>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToString() + "</lastModifiedOn>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			
			if (this.SQLMapOptions != null)
				xml = xml + this.SQLMapOptions.ToBusinessXml();
			
			xml = xml + "</scanOptions>";
			
			return xml;
		}
	}
}

