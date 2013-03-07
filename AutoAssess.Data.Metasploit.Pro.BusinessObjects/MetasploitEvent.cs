using System;
using System.Xml;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitEvent
	{
		public MetasploitEvent()
		{}
		
		public MetasploitEvent (XmlNode evnt)
		{
			foreach (XmlNode child in evnt.ChildNodes)
			{
				if (child.Name == "id")
					this.RemoteID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "workspace-id")
					this.RemoteWorkspaceID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "host-id")
				{}
				else if (child.Name == "created-at")
					this.RemoteCreatedAt = child.InnerText;
				else if (child.Name == "name")
					this.Name = child.InnerText;
				else if (child.Name == "updated-at")
					this.RemoteUpdatedAt = child.InnerText;
				else if (child.Name == "critical")
				{}
				else if (child.Name == "seen")
				{}
				else if (child.Name == "username")
					this.Username = child.InnerText;
				else if (child.Name == "info")
					this.Info = child.InnerText;
				else if (child.Name == "module-rhost")
					this.ModuleRHOST = child.InnerText;
				else if (child.Name == "module-name")
					this.ModuleName = child.InnerText;
			}
		}
		
		public virtual int RemoteID { get; set; }
		
		public virtual int RemoteWorkspaceID { get; set; }
		
		public virtual int RemoteHostID { get; set; }
		
		public virtual string RemoteCreatedAt { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string RemoteUpdatedAt { get; set; }
		
		public virtual string Username { get; set; }
		
		public virtual string Info { get; set; }
		
		public virtual string ModuleRHOST { get; set; }
		
		public virtual string ModuleName { get; set; }
	}
}

