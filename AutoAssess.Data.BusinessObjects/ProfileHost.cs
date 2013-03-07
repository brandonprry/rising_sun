using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class ProfileHost
	{
		public ProfileHost ()
		{		
		}
		
		public virtual string Name { get; set; }
		
		public virtual string IPv4Address { get; set; }
		
		public virtual DateTime VerifiedOn { get; set; }
		
		public virtual bool IsVerified { get; set; }
		
		public virtual bool VerifiedByFile { get; set; }
		
		public virtual bool VerifiedByWhois { get; set; }
		
		public virtual bool WasManuallyVerified { get; set; }
	}
}

