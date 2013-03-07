using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class DsxsItem
	{
		public DsxsItem ()
		{
		}
		
		public virtual string Parameter { get; set; }
		
		public virtual string PossibleTechnique { get; set; }
		
		
	}
}

