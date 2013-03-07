using System;
using AutoAssess.Data.Nessus.BusinessObjects;
using System.Xml;

 namespace AutoAssess.Data.Nessus.PersistentObjects
{
	[Serializable]
	public class PersistentNessusReport : NessusReport, IEntity
	{
		public PersistentNessusReport ()
		{
		}
		
		public PersistentNessusReport(NessusReport report)
		{
			this.ReadableName = report.ReadableName;
			
			//this.ParentScan = new PersistentNessusScan(report.ParentScan);
			
			this.RemoteReportID = report.RemoteReportID;
			this.FullReport = report.FullReport;
			this.Status = report.Status;
			this.TimeStamp = report.TimeStamp;
		}
		
		public PersistentNessusReport(XmlNode node)
		{
			foreach (XmlNode child in node.ChildNodes)
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
				else if (child.Name == "readableName")
					this.ReadableName = child.InnerText;
				else if (child.Name == "nessusReportID")
					this.RemoteReportID = child.InnerText;
				else if (child.Name == "status")
					this.Status = child.InnerText;
				else if (child.Name == "timestamp")
					this.TimeStamp = DateTime.Parse(child.InnerText);
				else if (child.Name == "fullReport")
					this.FullReport = child.InnerXml;
			}
		}
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual string ReportType { get; set; }
		
		public virtual PersistentNessusScan ParentScan 
		{
			get { return base.ParentScan as PersistentNessusScan; }
			set { base.ParentScan = value as NessusScan; }
		}
		
		public virtual void SetCreationInfo(Guid userID)
		{
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.CreatedOn = DateTime.Now;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
			this.ID = Guid.NewGuid();
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.IsActive = IsActive;
			
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
		}
		
		public virtual string ToPersistentXml()
		{
			var doc = new XmlDocument();
			doc.LoadXml(this.FullReport);
			
			string xml = "<nessusReport>";
			
			xml = xml + "<id>" + this.ID + "</id>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			xml = xml + "<fullReport>" + doc.LastChild.OuterXml + "</fullReport>";
			xml = xml + "<readableName>" + this.ReadableName + "</readableName>";
			xml = xml + "<nessusReportID>" + this.RemoteReportID + "</nessusReportID>";
			xml = xml + "<status>" + this.Status + "</status>";
			xml = xml + "<timestamp>" + this.TimeStamp.ToLongDateString() + "</timestamp>";
			
			xml = xml + "</nessusReport>";
			
			return xml;
		}
	}
}

