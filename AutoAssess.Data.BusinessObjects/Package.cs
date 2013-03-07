using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class Package
	{
		public Package ()
		{
		}
		
		public virtual string Name { get; set; }
		
		public virtual int Cost { get; set; }
		
		public virtual bool AllowsRecurring { get; set; }
		
		public virtual string Description { get; set; }
	}
}

