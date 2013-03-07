using System;
using System.Collections.Generic;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentNVD : NVD
	{
		public PersistentNVD ()
		{
		}
		
		public PersistentNVD (NVD nvd)
		{
			this.CVE = new PersistentCVE(nvd.CVE);
			this.CVEID = nvd.CVEID;
			this.CVSS = new PersistentCVSS(nvd.CVSS);
			this.CWE = nvd.CWE;
			this.DatePublished = nvd.DatePublished;
			this.NVDID = nvd.NVDID;
			
			this.References = new List<PersistentNVDReference>();
			
			if (nvd.References != null)
			{
				foreach (NVDReference reference in nvd.References)
					this.References.Add(new PersistentNVDReference(reference));
			}
			
			this.Summary = nvd.Summary;
			
			this.VulnerableSoftware = new List<PersistentVulnerableSoftware>();
			
			if (nvd.VulnerableSoftware != null)
			{
				foreach (VulnerableSoftware app in nvd.VulnerableSoftware)
					this.VulnerableSoftware.Add(new PersistentVulnerableSoftware(app));
			}
		}
		
		public virtual PersistentCVE CVE { get; set; }
		
		public virtual PersistentCVSS CVSS { get; set; }
		
		public virtual IList<PersistentNVDReference> References { get; set; }
		
		public virtual IList<PersistentVulnerableSoftware> VulnerableSoftware { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }

		public virtual void SetCreationInfo (Guid userID)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedOn = now;
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
	
	public class PersistentCVSS : CVSS
	{
		public PersistentCVSS()
		{
		}
		
		public PersistentCVSS(CVSS cvss)
		{
			if (cvss == null)
				return;
			
			this.Authentication = cvss.Authentication;
			this.AvailabilityImpact = cvss.AvailabilityImpact;
			this.Complexity = cvss.Complexity;
			this.ConfidentialityImpact = cvss.ConfidentialityImpact;
			this.IntegrityImpact = cvss.IntegrityImpact;
			this.Score = cvss.Score;
			this.Vector = cvss.Vector;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo (Guid userID)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedOn = now;
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
		
	}
	
	public class PersistentVulnerableSoftware : VulnerableSoftware
	{
		public PersistentVulnerableSoftware()
		{
		}
		
		public PersistentVulnerableSoftware(VulnerableSoftware app)
		{
			this.Software = app.Software;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo (Guid userID)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedOn = now;
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
	
	public class PersistentNVDReference : NVDReference
	{
		public PersistentNVDReference()
		{
		}
		
		public PersistentNVDReference (NVDReference reference)
		{
			this.Description = reference.Description;
			this.Source = reference.Source;
			this.Type = reference.Type;
			this.URL = reference.URL;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }

		public virtual void SetCreationInfo (Guid userID)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedOn = now;
			this.CreatedBy = userID;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
}

