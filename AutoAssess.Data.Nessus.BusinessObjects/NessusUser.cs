using System;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusUser
	{
		public NessusUser()
		{}
		
		public NessusUser (XmlNode userNode)
		{
			foreach (XmlNode c in userNode.ChildNodes)
			{
				if (c.Name == "name")
					Name = c.Value;
				else if (c.Name == "admin")
					IsAdmin = Boolean.Parse(c.Value);
				else if (c.Name == "lastlogin")
					LastLogin = c.Value;
			}
			
		}
		
		public virtual string Name { get; private set; }
		
		public virtual bool IsAdmin { get; private set; }
				
		public virtual string LastLogin { get; private set; }
	}
}

