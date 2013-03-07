using System;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentSMBClientResults : SMBClientToolResults, IEntity
	{
		public PersistentSMBClientResults ()
		{
		}
		
		public PersistentSMBClientResults (SMBClientToolResults results)
		{
			this.FullOutput = results.FullOutput;
			this.ParentPort = new PersistentPort(results.ParentPort);
			this.ShareDetails = results.ShareDetails;
			this.HostIPAddressV4 = results.HostIPAddressV4;
			this.HostPort = results.HostPort;
			this.IsTCP = results.IsTCP;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentScan ParentScan { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual PersistentPort ParentPort 
		{
			get { return base.ParentPort as PersistentPort; }
			set { base.ParentPort = value as Port; }
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
			return string.Empty;
		}
	}
}

