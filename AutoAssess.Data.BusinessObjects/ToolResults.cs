using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public abstract class ToolResults 
	{
		public ToolResults ()
		{
		}
		
		public virtual string FullOutput { get; set; }
	}
}

