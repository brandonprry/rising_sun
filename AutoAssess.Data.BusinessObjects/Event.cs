using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class Event
	{
		public Event ()
		{
			
		}
		
		public virtual int Severity { get; set; }
		public virtual string Description { get; set; }
		public virtual DateTime Timestamp { get; set; }
	}
}

