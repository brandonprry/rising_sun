using System;
using System.Security.Principal;
namespace AutoAssess.Web
{
	public class WebUserPrincipal : IPrincipal
	{
		private WebUserIdentity _identity;
		
		public WebUserPrincipal (WebUserIdentity i)
		{
			_identity = i;
		}
		
		public System.Security.Principal.IIdentity Identity 
	    { 
	        get 
	        { 
	            return _identity; 
	        } 
	    } 
		
		public bool IsInRole(string role) 
	    { 
			return true;
	        //return (role == _identity.Role.ToString()); 
	    } 
		
		public Guid ID { get; set; }
		
		public string Username { get; set; }
		
		public string EmailAddress { get; set; }
	}
}

