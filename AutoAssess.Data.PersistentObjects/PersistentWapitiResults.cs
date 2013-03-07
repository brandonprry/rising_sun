using System;
using AutoAssess.Data.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentWapitiResults : WapitiToolResults, IEntity 
	{
		public PersistentWapitiResults ()
		{
		}
		
		public PersistentWapitiResults (WapitiToolResults results)
		{
			this.Bugs = new List<PersistentWapitiBug>();
			
			foreach (WapitiBug bug in results.Bugs)
			{
				if (string.IsNullOrEmpty(bug.URL))
					continue;
				
				this.Bugs.Add(new PersistentWapitiBug(bug, Guid.Empty));
			}
			
			this.FullOutput = results.FullOutput;
			this.HostIPAddressV4 = results.HostIPAddressV4;
			this.HostPort = results.HostPort;
			this.HostPortID = results.HostPortID;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual string FullOutput { get; set; }
		
		public virtual IList<PersistentWapitiBug> Bugs { get; set; }
		
		public virtual Guid ParentProfileID { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
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

