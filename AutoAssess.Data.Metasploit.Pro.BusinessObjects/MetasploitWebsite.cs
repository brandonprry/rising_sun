using System;
using System.Xml;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitWebsite
	{
		public MetasploitWebsite()
		{
		}
		public MetasploitWebsite (XmlNode site)
		{
			foreach (XmlNode child in site.ChildNodes)
			{
				if (child.Name == "id")
					this.RemoteID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "service-id")
					this.RemoteServiceID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "created-at")
					this.RemoteCreatedAt = child.InnerText;
				else if (child.Name == "updated-at")
					this.RemoteUpdatedAt = child.InnerText;
				else if (child.Name == "vhost")
					this.VirtualHost = child.InnerText;
				else if (child.Name == "comments")
				{}
				else if (child.Name == "options")
					this.Options = child.InnerText;
				else if (child.Name == "host")
					this.Host = child.InnerText;
				else if (child.Name == "port")
					this.Port = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "ssl")
				{}
			}
		}
		
		public virtual int RemoteID { get; set; }
		
		public virtual int RemoteServiceID { get; set; }
		
		public virtual string RemoteCreatedAt { get; set; }
		
		public virtual string RemoteUpdatedAt { get; set; }
		
		public virtual string VirtualHost { get; set; }
		
		public virtual string Options { get; set; }
		
		public virtual string Host { get;set;}
		
		public virtual int Port { get; set; }
	}
}

