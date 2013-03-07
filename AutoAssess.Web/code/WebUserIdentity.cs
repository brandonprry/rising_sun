using System;
using System.Security.Principal;
namespace AutoAssess.Web
{
	public class WebUserIdentity : IIdentity
	{
		public WebUserIdentity (string name, bool isAuthed)
		{
			this.Name = name;
			this.IsAuthenticated = isAuthed;
		}

		public string AuthenticationType {
			get { return "Custom Authentication"; }
		}

		/// <summary> 
		/// Returns a value that indicates whether the user has been authenticated 
		/// </summary> 
		public bool IsAuthenticated { get; set; }

		/// <summary> 
		/// Returns the user's name 
		/// </summary> 
		public string Name { get; set; }
		
	}
}

