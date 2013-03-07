using System;
using System.Collections.Generic;
using System.Xml;


namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASOverride :  IOpenVASObject
	{
		public OpenVASOverride ()
		{
		}
		
		public virtual Guid RemoteOverrideID { get; set; }
		
		public virtual string Content { get; set; }
		
		public virtual OpenVASNVT NVT { get; set; }
		
		public virtual string Comment { get; set; }
		
		public virtual string Hosts { get; set; }
		
		public virtual string NewThreat { get; set; }
		
		public virtual int Port { get; set; }
		
		public virtual OpenVASScan Report { get; set; }
		
		public virtual OpenVASTask Task { get; set; }
		
		public virtual int Threat { get; set; }
	
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
	}
}

