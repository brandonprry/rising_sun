using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class ShareDetails 
	{
		public ShareDetails ()
		{
		}
		
		public virtual SMBClientToolResults ParentResults { get; set; }
		
		public virtual string ShareName { get; set; }
		
		public virtual string FilesOnShare { get; set; }
	}
}

