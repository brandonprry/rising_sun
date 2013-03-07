using System;
using System.Xml;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitService
	{
		public MetasploitService()
		{
		}
		public MetasploitService (XmlNode service)
		{
			foreach (XmlNode child in service.ChildNodes)
			{
				if (child.Name == "id")
					this.RemoteID = int.Parse(child.InnerText);
				else if (child.Name == "host-id")
					this.RemoteHostID = int.Parse(child.InnerText);
				else if (child.Name == "created-at")
					this.RemoteCreatedAt = child.InnerText;
				else if (child.Name == "port")
					this.Port = int.Parse(child.InnerText);
				else if (child.Name == "proto")
					this.Protocol = child.InnerText;
				else if (child.Name == "state")
					this.State = child.InnerText;
				else if (child.Name == "name")
					this.Name = child.InnerText;
				else if (child.Name == "updated-at")
					this.RemoteUpdatedAt = child.InnerText;
				else if (child.Name == "info")
					this.Info = child.InnerText;
			}
		}
		
		public virtual int RemoteID { get; set; }
		
		public virtual int RemoteHostID { get; set; }
		
		public virtual string RemoteCreatedAt { get; set; }
		
		public virtual int Port { get; set; }
		
		public virtual string Protocol { get; set; }
		
		public virtual string State { get;set;}
		
		public virtual string Name { get; set; }
		
		public virtual string RemoteUpdatedAt { get; set; }
		
		public virtual string Info { get; set; }
	}
}

