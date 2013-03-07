using System;
using System.Xml;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitCredential
	{
		public MetasploitCredential()
		{}
		
		public MetasploitCredential (XmlNode cred)
		{
			foreach (XmlNode child in cred.ChildNodes)
			{
				if (child.Name == "port")
					this.Port = int.Parse(child.InnerText);
				else if (child.Name == "sname")
					this.ServiceName = child.InnerText;
				else if (child.Name == "created-at")
					this.RemoteCreatedAt = child.InnerText;
				else if (child.Name == "updated-at")
					this.RemoteUpdatedAt = child.InnerText;
				else if (child.Name == "user")
					this.Username = child.InnerText;
				else if (child.Name == "pass")
					this.Password = child.InnerText;
				else if (child.Name == "active")
					this.RemoteIsActive = Boolean.Parse(child.InnerText);
				else if (child.Name == "proof")
					this.Proof = child.InnerText;
				else if (child.Name == "ptype")
					this.PasswordType = child.InnerText;
				else if (child.Name == "source-id")
					this.SourceID = int.Parse(child.InnerText);
				else if (child.Name == "source-type")
					this.SourceType = child.InnerText;
				else if (child.Name == "id")
					this.RemoteID = int.Parse(child.InnerText);
				else if (child.Name == "service-id")
					this.RemoteServiceID = int.Parse(child.InnerText);
				else 
					throw new Exception("wtf?");
			}
				
		}
		
		public virtual int RemoteServiceID { get;set; }
		
		public virtual int RemoteID { get; set; }
		
		public virtual int Port { get; set; }
		
		public virtual string ServiceName { get; set; }
		
		public virtual string RemoteCreatedAt { get;set; }
		
		public virtual string RemoteUpdatedAt { get; set; }
		
		public virtual string Username { get; set; }
		
		public virtual string Password { get; set; }
		
		public virtual bool RemoteIsActive { get; set; }
		
		public virtual string Proof { get; set; }
		
		public virtual string PasswordType { get; set; }
		
		public virtual int SourceID { get; set; }
		
		public virtual string SourceType { get; set; }
		
	}
}

