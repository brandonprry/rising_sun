using System;
using AutoAssess.Data.BusinessObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.Nessus.BusinessObjects;
using System.Xml;
using System.Text;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentScan : Scan, IEntity
	{
		public PersistentScan ()
		{
		}
		
		public PersistentScan(XmlNode scan)
		{
			foreach (XmlNode child in scan.FirstChild.ChildNodes)
			{
				if (child.Name == "id")
					this.ID = new Guid(child.InnerText);
				else if (child.Name == "name")
					this.Name = child.InnerText;
				else if (child.Name == "hasRun")
					this.HasRun = Boolean.Parse(child.InnerText);
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
				else if (child.Name == "scanOptions")
					this.ScanOptions = new PersistentScanOptions(child);
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
		
		public virtual Guid ParentProfileID { get { return this.ParentProfile.ID; } } 
		
		public virtual PersistentProfile ParentProfile 
		{ 
			get { return base.ParentProfile as PersistentProfile; }
			set { base.ParentProfile = value as Profile; } 
		}
		
		public virtual PersistentNessusScan NessusScan { get; set; }
		
		public virtual string OpenVASReportID { get; set; }
		
		public virtual string NexposeSiteID { get; set; }
		
		public virtual string MetasploitReportTaskID { get; set; }
		
		public virtual PersistentScanOptions ScanOptions 
		{ 
			get { return base.ScanOptions as PersistentScanOptions; } 
			set { base.ScanOptions = value as ScanOptions; }
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
			string xml = "<scan>";
			
			xml = xml + "<id>" + this.ID + "</id>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			xml = xml + "<hasRun>" + this.HasRun + "</hasRun>";
			xml = xml + "<name>" + this.Name + "</name>";
			
			if (this.ScanOptions != null)
				xml = xml + this.ScanOptions.ToPersistentXml();
	
			
			xml = xml + "</scan>";
			
			return xml;
		}
	}
}

