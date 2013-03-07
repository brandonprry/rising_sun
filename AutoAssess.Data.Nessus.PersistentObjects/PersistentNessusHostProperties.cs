using System;
using AutoAssess.Data.Nessus.BusinessObjects;

namespace AutoAssess.Data.Nessus.PersistentObjects
{
	[Serializable]
	public class PersistentNessusHostProperties : NessusHostProperties
	{
		public PersistentNessusHostProperties ()
		{
		
		}
		
		public PersistentNessusHostProperties(NessusHostProperties props)
		{
			this.HostBegin = props.HostBegin;
			this.HostEnd = props.HostEnd;
			this.HostFQDN = props.HostFQDN;
			this.HostIP = props.HostIP;
			this.OperatingSystem = props.OperatingSystem;
			this.SystemType = props.SystemType;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo(Guid owner)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

