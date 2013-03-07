using System;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentEvent : Event
	{
		public PersistentEvent ()
		{
		}
		
		public virtual Guid ID { get; set; }
		public virtual Guid ProfileID { get; set; }
		public virtual Guid CreatedBy { get; set; }
		public virtual DateTime CreatedOn { get; set; }
		public virtual Guid LastModifiedBy { get; set;} 
		public virtual DateTime LastModifiedOn { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual Guid WebUserID { get; set; }
		
		public virtual string ToPersistentXml ()
		{
			string xml = "<event>";
			
			xml += "<eventID>" + this.ID.ToString() + "</eventID>";
			xml += "<profileID>" + this.ProfileID.ToString() + "</profileID>";
			xml += "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml += "<createdOn>" + this.CreatedOn.ToString() + "</createdOn>";
			xml += "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml += "<lastModifiedOn>" + this.LastModifiedOn.ToString() + "</lastModifiedOn>";
			xml += "<isActive>" + this.IsActive.ToString() + "</isActive>";
			xml += "<severity>" + this.Severity.ToString() + "</severity>";
			xml += "<description>" + this.Description.ToString() + "</description>";
			xml += "<timestamp>" + this.Timestamp.ToString() + "</timestamp>";
			
			xml += "</event>";
			return xml;
		}
	}
}

