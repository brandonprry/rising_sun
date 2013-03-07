using System;
using AutoAssess.Data.OpenVAS.BusinessObjects;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	[Serializable]
	public class PersistentOpenVASTask : OpenVASTask, IEntity
	{
		public PersistentOpenVASTask ()
		{
			
		}
		
		public PersistentOpenVASTask (OpenVASTask task)
		{
			base.Comment = task.Comment;
			base.Config = task.Config;
			base.Escalator = task.Escalator;
			base.Name = task.Name;
			base.RemoteTaskID = task.RemoteTaskID;
			base.Schedule = task.Schedule;
			base.Slave = task.Slave;
			base.Target = task.Target;
			base.TaskReport = task.TaskReport;
			base.FullReport = task.FullReport;
		}
		
		public virtual PersistentOpenVASConfig Config { get; set; }
		
		public virtual PersistentOpenVASEscalator Escalator { get; set; }
		
		public virtual PersistentOpenVASSchedule Schedule { get; set; }
		
		public virtual PersistentOpenVASSlave Slave { get; set; }
		
		public virtual PersistentOpenVASTarget Target { get; set; }
		
		public virtual Guid ScanID { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual OpenVASTask ToBusinessObject()
		{
			OpenVASTask task = new OpenVASTask();
			
			task.Comment = base.Comment;
			task.Config = base.Config;
			task.Escalator = base.Escalator;
			task.Name = base.Name;
			task.RemoteTaskID = base.RemoteTaskID;
			task.Schedule = base.Schedule;
			task.Slave = base.Slave;
			task.Target = base.Target;
			task.TaskReport = base.TaskReport;
	
			return task;
		}
		
		public virtual void SetCreationInfo (Guid userID)
		{
			this.ID = Guid.NewGuid();
			this.CreatedOn = DateTime.Now;
			this.CreatedBy = userID;
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
	}
}

