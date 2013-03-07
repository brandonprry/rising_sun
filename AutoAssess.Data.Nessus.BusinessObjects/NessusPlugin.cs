using System;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusPlugin
	{
		public NessusPlugin ()
		{
		}
		
		public NessusPlugin(XmlNode pluginNode)
		{
			foreach (XmlNode c in pluginNode.ChildNodes)
			{
				if (c.Name == "pluginFileName")
					FileName =  c.InnerText;
				else if (c.Name == "pluginID")
					RemotePluginID = int.Parse(c.InnerText);
				else if (c.Name == "pluginName")
					Name = c.InnerText;
				else if (c.Name == "pluginFamily")
					FamilyName = c.InnerText;
			}
		}
		
		public virtual string FileName { get; set; }
		
		public virtual int RemotePluginID { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string FamilyName { get; set; }
		
		public virtual NessusPluginFamily Family { get; set; }
	}
}

