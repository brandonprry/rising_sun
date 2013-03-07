using System;
using AutoAssess.Data.Nexpose.BusinessObjects;

namespace AutoAssess.Data.Nexpose.PersistentObjects
{
	[Serializable]
	public class PersistentNexposeHostTest : NexposeHostTest
	{
		public PersistentNexposeHostTest ()
		{
		}
		
		public PersistentNexposeHostTest(NexposeHostTest test)
		{
			this.IsPCICompliant = test.IsPCICompliant;
			this.NexposeParagraph = test.NexposeParagraph;
			this.RemoteDeviceID = test.RemoteDeviceID;
			this.RemoteScanID = test.RemoteScanID;
			this.Status = test.Status;
			this.VulnerableSince = test.VulnerableSince;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
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

