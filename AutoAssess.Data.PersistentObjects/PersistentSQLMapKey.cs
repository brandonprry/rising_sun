using System;
using System.Xml;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentSQLMapVulnerability : SQLMapVulnerability, IEntity
	{
		public PersistentSQLMapVulnerability ()
		{
		}
		
		public PersistentSQLMapVulnerability (XmlNode vuln)
		{
			foreach (XmlNode child in vuln.ChildNodes)
			{
				if (child.Name == "id")
					this.ID = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedBy")
					this.LastModifiedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedOn")
					this.LastModifiedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "createdOn")
					this.CreatedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "createdBy")
					this.CreatedBy = new Guid(child.InnerText);
				else if (child.Name == "isActive")
					this.IsActive = Boolean.Parse(child.InnerText);
				else if (child.Name == "httpRequestType")
					this.HTTPRequestType = child.InnerText;
				else if (child.Name == "parameter")
					this.Parameter = child.InnerText;
				else if (child.Name == "payload")
					this.Payload = child.InnerText;
				else if (child.Name == "payloadType")
					this.PayloadType = child.InnerText;
				else if (child.Name == "target")
					this.Target = child.InnerText;
				else if (child.Name == "title")
					this.Title = child.InnerText;
				
			}
		}
		
		public PersistentSQLMapVulnerability(SQLMapVulnerability vuln)
		{
			this.HTTPRequestType = vuln.HTTPRequestType;
			this.Parameter = vuln.Parameter;
			this.Payload = vuln.Payload;
			this.PayloadType = vuln.PayloadType;
			this.Target = vuln.Target;
			this.Title = vuln.Title;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentScan ParentScan { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual PersistentSQLMapResults ParentResults 
		{
			get { return base.ParentResults as PersistentSQLMapResults; }
			set { base.ParentResults = value as SQLMapResults; }
		}
		
		
		
		public  virtual void SetCreationInfo(Guid userID)
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
			string xml = "<sqlMapVulnerability>";
			
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			xml = xml + "<id>" + this.ID.ToString() + "</id>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			
			xml = xml + "<httpRequestType>" + this.HTTPRequestType + "</httpRequestType>";
			xml = xml + "<parameter>" + this.Parameter + "</parameter>";
			xml = xml + "<payload>" + this.Payload + "</payload>";
			xml = xml + "<payloadType>" + this.PayloadType + "</payloadType>";
			xml = xml + "<target>" + this.Target + "</target>";
			xml = xml + "<title>" + this.Title + "</title>";
			
			xml = xml + "</sqlMapVulnerability>";
			
			return xml;
		}
	}
}

