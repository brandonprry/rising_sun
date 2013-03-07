using System;

namespace AutoAssess.Web.Data
{
	[Serializable]
	public class VerificationKey
	{
		public VerificationKey ()
		{
			
		}
		
		public virtual Guid ID { get;set; }
		
		public virtual WebUser User { get ;set; }
		
		public virtual Guid WebUserID { get; set; }
		
		public virtual Guid Key { get; set; }
		
		public virtual bool IsVerifed { get; set; }
		
		public virtual bool IsSent { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
	}
}

