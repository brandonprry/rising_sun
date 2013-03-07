using System;

namespace AutoAssess.Web.Data
{
	[Serializable]
	public class WebUserInfo
	{
		public WebUserInfo ()
		{
		}
		
		public virtual string FirstName { get; set; }
		
		public virtual string LastName { get; set; }
		
		public virtual int Hosts { get; set; }
		
		public virtual string Provider { get; set; }
		
		public virtual string MainSecurityConcern { get; set; }
		
		public virtual string PrimaryPhone { get; set; }
		
		public virtual string SecondaryPhone { get; set; }
		
		public virtual string PrimaryWebsite { get; set; }
		
		public virtual Guid WebUserID { get; set; }
		
		public virtual DateTime LastLogin { get; set; }
		
		public virtual WebUser WebUser { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual Guid ID { get; set; }
	}
}

