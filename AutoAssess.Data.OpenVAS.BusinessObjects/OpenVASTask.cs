using System;
using System.Collections.Generic;
using System.Xml;
using AutoAssess.Data.OpenVAS;
using openvassharp;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASTask :  IOpenVASObject
	{
	
		public OpenVASTask ()
		{
		}
		
		public virtual Guid RemoteTaskID { get ;set; }
		
		public virtual OpenVASScan TaskReport { get; set; }
		
		public virtual string FullReport { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string Comment { get; set; }
		
		public virtual OpenVASConfig Config { get; set; }
		
		public virtual OpenVASTarget Target { get; set; }
		
		public virtual OpenVASEscalator Escalator { get; set; }
		
		public virtual OpenVASSchedule Schedule { get; set; }
		
		public virtual OpenVASSlave Slave { get; set; }
	
		public virtual void Create(OpenVASManager manager)
		{
			manager.CreateTask(this.Name, this.Comment, this.Config.RemoteConfigID.ToString(),
				this.Target.RemoteTargetID.ToString(), this.Escalator.RemoteEscalatorID.ToString(),
				this.Schedule.RemoteScheduleID.ToString(), this.Slave.RemoteSlaveID.ToString());
		}
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
	}
}

