using System;
using System.Collections.Generic;
using System.Xml;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentSQLMapResults : SQLMapResults, IEntity
	{
		public PersistentSQLMapResults ()
		{
		}
		
		public PersistentSQLMapResults(XmlNode results)
		{
			foreach (XmlNode child in results.ChildNodes)
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
				else if (child.Name == "vulnerabilities")
				{
					this.PersistentVulnerabilities = new List<PersistentSQLMapVulnerability>();
					
					foreach (XmlNode vuln in child.ChildNodes)
						this.PersistentVulnerabilities.Add(new PersistentSQLMapVulnerability(vuln));
					
				}
				
			}
		}
		
		public PersistentSQLMapResults (SQLMapResults results)
		{
			this.FullOutput = results.FullOutput;
			
			this.ParentHostPort = new PersistentPort(results.ParentHostPort);
			
			this.Log = results.Log;
			
			this.PersistentVulnerabilities = new List<PersistentSQLMapVulnerability>();
			
			if (results.Vulnerabilities != null)
			{
				foreach (SQLMapVulnerability vuln in results.Vulnerabilities)
				{
					PersistentSQLMapVulnerability pvuln = new PersistentSQLMapVulnerability(vuln);
					pvuln.SetCreationInfo(Guid.Empty);
					
					pvuln.ParentResults = this;
					
					
					this.PersistentVulnerabilities.Add(pvuln);
				}
			}
		}
		
		public virtual Guid ParentHostPortID { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentScan ParentScan { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual IList<PersistentSQLMapVulnerability> PersistentVulnerabilities
		{
			get; set; 
		}
		
		public virtual PersistentPort ParentHostPort 
		{
			get { return base.ParentHostPort as PersistentPort; }
			set { base.ParentHostPort = value as Port; }
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
			string xml = "<sqlMapResult>";
			
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			xml = xml + "<id>" + this.ID.ToString() + "</id>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			
			//xml = xml + "<fullOutput>" + this.FullOutput + "</fullOutput>"; //needs to be cleaned most assuredly
			
			xml = xml + "<vulnerabilities>";
				
			foreach (PersistentSQLMapVulnerability vuln in this.PersistentVulnerabilities)
				xml = xml + vuln.ToPersistentXml();
			
			xml = xml + "</vulnerabilities>";
			
			xml = xml + "</sqlMapResult>";
			
			return xml;
		}
	}
}

