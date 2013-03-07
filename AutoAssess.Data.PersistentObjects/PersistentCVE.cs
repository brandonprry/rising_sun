using System;
using AutoAssess.Data.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentCVE : CVE
	{
		public PersistentCVE ()
		{
		}
		
		public PersistentCVE (CVE cve)
		{
			if (cve == null)
				return;
			
			this.PersistentComments = new List<PersistentCVEComment>();
			
			if (cve.Comments != null)
			{
				foreach (CVEComment comment in cve.Comments)
					this.PersistentComments.Add(new PersistentCVEComment(comment));
			}
			
			this.Description = cve.Description;
			this.Name = cve.Name;
			
			this.PersistentReferences = new List<PersistentCVEReference>();
			
			if (cve.References != null)
			{
				foreach (CVEReference reference in cve.References)
					this.PersistentReferences.Add(new PersistentCVEReference(reference));
			}
		}
		
		public virtual IList<PersistentCVEComment> PersistentComments { get; set; }
		
		public virtual IList<PersistentCVEReference> PersistentReferences { get; set; }
		
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
			this.CreatedBy = userID;
			this.CreatedOn = now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
	
	public class PersistentCVEComment : CVEComment
	{
		public PersistentCVEComment()
		{}
		
		public PersistentCVEComment(CVEComment comment)
		{
			this.Comment = comment.Comment;
			this.Voter = comment.Voter;
		}
		
		public virtual PersistentCVE CVE { get; set; }
		
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
			this.CreatedBy = userID;
			this.CreatedOn = now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
	
	public class PersistentCVEReference : CVEReference
	{
		public PersistentCVEReference()
		{
		}
		
		public PersistentCVEReference(CVEReference reference)
		{
			this.Description = reference.Description;
			this.Source = reference.Source;
			this.URL = reference.URL;
		}
		
		public virtual PersistentCVE CVE { get; set; }
		
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
			this.CreatedBy = userID;
			this.CreatedOn = now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
}

