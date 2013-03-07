using System;
using AutoAssess.Data.BusinessObjects;
using System.Xml;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentSQLMapOptions : SQLMapOptions, IEntity
	{
		public PersistentSQLMapOptions ()
		{
		}
		
		public PersistentSQLMapOptions(XmlNode options)
		{
			foreach (XmlNode child in options.ChildNodes)
			{
				
			}
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentScan ParentScan { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
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
			return string.Empty;
		}
	}
}

