using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASLSCCredential :  IOpenVASObject
	{
		public OpenVASLSCCredential ()
		{
		}
		
		public virtual Guid RemoteCredentialsID { get; set; }
		
		public virtual string LoginUsername { get; set; }
		
		public virtual string Password { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string Comment { get; set; }
	
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
	}
}

