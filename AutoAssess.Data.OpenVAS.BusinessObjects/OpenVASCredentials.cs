using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASCredentials :  IOpenVASObject
	{
		public OpenVASCredentials ()
		{
		}
		
		public virtual string Username { get; set; }
		
		public virtual string Password { get ;set; }
		
		public virtual Guid RemoteCredentialID { get; set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
	}
}

