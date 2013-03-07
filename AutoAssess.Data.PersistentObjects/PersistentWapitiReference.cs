using System;
using AutoAssess.Data.BusinessObjects;
using System.Xml;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentWapitiReference
	{
		public PersistentWapitiReference()
		{}
		
		public PersistentWapitiReference(XmlNode node, Guid creatorID)
		{
			SetCreationInfo(creatorID);
		}
		
		public PersistentWapitiReference (WapitiReference reference, Guid creatorID)
		{
			this.Title = reference.Title;
			this.URL = reference.URL;
			
			SetCreationInfo(creatorID);
		}
		
		
		public virtual string Title { get; set; }
		
		public virtual string URL { get; set; }
		
		public virtual WapitiBug Bug { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }

		private void SetCreationInfo (Guid creatorID)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedBy = creatorID;
			this.CreatedOn = now;
			this.LastModifiedBy = creatorID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
}

