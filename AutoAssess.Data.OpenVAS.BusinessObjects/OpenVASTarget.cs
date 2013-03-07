using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASTarget :  IOpenVASObject
	{
		public OpenVASTarget ()
		{
		}
		
		public virtual string Name { get; set; }
		
		public virtual string Comment { get; set; }
		
		public virtual string Hosts { get ;set; }
		
		public virtual OpenVASLSCCredential SSHCredentials { get; set; }
		
		public virtual OpenVASLSCCredential SMBCredentials { get; set; }
		
		public virtual string PortRange { get; set; }
		
		public virtual Guid RemoteTargetID { get; set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
		
	}
}

