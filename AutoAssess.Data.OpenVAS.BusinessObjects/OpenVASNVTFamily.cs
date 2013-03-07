using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASNVTFamily : IOpenVASObject
	{
		public OpenVASNVTFamily ()
		{
		}
		
		public OpenVASNVTFamily (XmlNode node)
		{
			//this.RemoteFamilyID = new Guid(node.Attributes["id"].Value);
			
			foreach (XmlNode c in node.ChildNodes)
			{
				if (c.Name == "name")
					this.Name = c.InnerText;
				else if (c.Name == "max_nvt_count")
					this.MaxNVTCount = int.Parse(c.InnerText);
			}
		}
		
		public virtual Guid RemoteFamilyID { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual int MaxNVTCount { get; set; }
		
		public virtual IList<OpenVASNVT> NVTs { get; set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
	}
}

