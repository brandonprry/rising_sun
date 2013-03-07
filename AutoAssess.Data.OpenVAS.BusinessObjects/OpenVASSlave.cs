using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASSlave :  IOpenVASObject
	{
		public OpenVASSlave ()
		{
		}
		
		public virtual Guid RemoteSlaveID { get; set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
	}
}

