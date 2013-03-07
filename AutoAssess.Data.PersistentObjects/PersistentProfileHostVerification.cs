using System;
using AutoAssess.Data.BusinessObjects;
using System.Xml;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentProfileHostVerification : ProfileHostVerification
	{
		public PersistentProfileHostVerification () : base()
		{
			
		}
		
		public PersistentProfileHostVerification (ProfileHostVerification verification, Guid owner)
		{
			this.VerificationData = verification.VerificationData;
			this.VerificationError = verification.VerificationFileName;
			this.VerificationFileName = verification.VerificationFileName;
			this.WhoisEmail = verification.WhoisEmail;
			
			
			this.SetCreationInfo(owner);
		}
		
		public PersistentProfileHostVerification (XmlDocument doc)
		{
			if (doc.FirstChild.Name != "profileVerification")
				throw new Exception("Node type isn't profileVerification");
			
			foreach (XmlNode node in doc.FirstChild.ChildNodes)
			{
				if (node.Name == "id")
					this.ID = new Guid(node.InnerText);
				else if (node.Name == "createdOn")
					this.CreatedOn = DateTime.Parse(node.InnerText);
				else if (node.Name == "createdBy")
					this.CreatedBy = new Guid(node.InnerText);
				else if (node.Name == "lastModifiedBy")
					this.LastModifiedBy = new Guid(node.InnerText);
				else if (node.Name == "lastModifiedOn")
					this.LastModifiedOn = DateTime.Parse(node.InnerText);
				else if (node.Name == "isActive")
					this.IsActive = Boolean.Parse(node.InnerText);
				else if (node.Name == "data")
					this.VerificationData = node.InnerText;
				else if (node.Name == "filename")
					this.VerificationFileName = node.InnerText;
				else if (node.Name == "error")
					this.VerificationError = node.InnerText;
				else if (node.Name == "whoisRegex")
					this.WhoisEmail = node.InnerText;
				else
					throw new Exception("Don't know element type: " + node.Name);
			}
		}
		
		public virtual PersistentProfileHost ProfileHost { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentProfileHostVerification SetCreationInfo(Guid userID)
		{
			DateTime now = DateTime.Now;
			
			this.CreatedBy = userID;
			this.CreatedOn = now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = now;
			this.ID = Guid.NewGuid();
			this.IsActive = true;
			
			return this;
		}
		
		public virtual string ToPersistentXml ()
		{
			string xml = string.Empty;
			
			xml = xml + "<profileVerification>";
			
			
			xml = xml + "<id>" + this.ID.ToString() + "</id>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			xml = xml + "<data>" + this.VerificationData + "</data>";
			xml = xml + "<filename>" + this.VerificationFileName + "</filename>";
			xml = xml + "<error>" + this.VerificationError + "</error>";
			xml = xml + "<whoisRegex>" + this.WhoisEmail + "</whoisRegex>";
			
			xml = xml + "</profileVerification>";
			
			return xml;
		}
	}
}

