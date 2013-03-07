using System;
using AutoAssess.Data;

namespace AutoAssess.Web.Data
{
	[Serializable]
	public class WebUser : Entity
	{
		public WebUser ()
		{
		}
		
		public virtual int UserLevel { get; set; }
		
		public virtual Guid UserID 
		{
			get { return this.ID; }
			set { this.ID = value; }
		}
		
		public virtual string Username { get; set; }
		
		public virtual string PasswordHash { get; set; }
		
		public virtual string EmailAddress { get; set; }
		
		
	}
}

