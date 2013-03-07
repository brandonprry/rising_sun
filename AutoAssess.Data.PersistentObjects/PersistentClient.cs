using System;
using System.Collections.Generic;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentClient : Client, IEntity
	{
		public PersistentClient ()
		{
		}		
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual IList<PersistentUser> Users 
		{
			get { return base.Users as IList<PersistentUser>;}
			set { base.Users = value as IList<User>; }
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
		
		public  virtual string ToPersistentXml()
		{
			return this.ToPersistentXml(false);
		}
		
		public virtual string ToPersistentXml(bool getFullDetails)
		{
			string xml = string.Empty;
			
			xml = "<client>";
			
			xml = xml + "<id>" + this.ID.ToString() + "</id>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdBy>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			xml = xml + "<userID>" + this.User.ID + "</userID>";
			
			xml = xml + "<hasApiAccess>" + this.HasAPIAccess + "</hasApiAccess>";
			xml = xml + "<logoPath>" + this.LogoPath + "</logoPath>";
			xml = xml + "<name>" + this.Name + "</name>";
			
			xml = xml + "</client>";
			
			return xml;
		}
	}
}

