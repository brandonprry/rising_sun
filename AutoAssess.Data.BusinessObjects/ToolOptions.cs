using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public abstract class ToolOptions 
	{
		public ToolOptions () 
		{
		}
		
		public virtual Profile ParentProfile { get; set; }
		
		public virtual Scan ParentScan { get; set; }
	}
}

