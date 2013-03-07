using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusPluginFamily
	{
		public NessusPluginFamily ()
		{
		
		}
		
		public NessusPluginFamily(XmlNode familyNode)
		{
			foreach (XmlNode c in familyNode.ChildNodes)
			{
				if (c.Name == "familyName")
					Name = c.InnerText;
				else if (c.Name == "numFamilyMembers")
					NumberOfMembers = int.Parse(c.InnerText);
			}
			
		}
		
		public virtual string Name { get; set; }
		
		public virtual int NumberOfMembers { get; set; }
		
		public virtual IList<NessusPlugin> ChildPlugins { get; set; }
	}
}

