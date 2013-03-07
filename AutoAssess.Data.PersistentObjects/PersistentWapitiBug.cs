using System;
using AutoAssess.Data.BusinessObjects;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentWapitiBug : WapitiBug
	{
		public PersistentWapitiBug()
		{}
		
		public PersistentWapitiBug (Guid creatorID)
		{
			SetCreationInfo(creatorID);
		}
		
		public PersistentWapitiBug (XmlNode bug, Guid creatorID)
		{
			this.Level = int.Parse(bug.Attributes["level"].Value);
			
			foreach (XmlNode child in bug.ChildNodes)
			{
				switch(child.Name)
				{
				case "timestamp":
					this.Timestamp = child.InnerText;
					break;
				case "url":
					this.URL = child.InnerText;
					break;
				case "peer":
					foreach (XmlNode p in child.ChildNodes)
					{
						if (p.Name == "addr")
							this.Host = p.InnerText;
						else if (p.Name == "port")
							this.Port = int.Parse(p.InnerText);
					}
					break;
				case "parameter":
					this.Parameter = child.InnerText;
					break;
				case "info":
					this.Info = child.InnerText;
					break;
					
				}
				
			}
			
			SetCreationInfo(creatorID);
		}
		
		public PersistentWapitiBug(WapitiBug bug, Guid creatorID)
		{
			this.Level = bug.Level;
			this.Timestamp = bug.Timestamp;
			this.URL = bug.URL;
			this.Host = bug.Host;
			this.Port = bug.Port;
			this.Parameter = bug.Parameter;
			this.Info = bug.Info;
			this.Type = bug.Type;
			
			SetCreationInfo(creatorID);
		}
//		
//		public virtual  int Level { get; set; }
//		
//		public virtual DateTime Timestamp { get; set; }
//		
//		public virtual string URL { get; set; }
//		
//		public virtual string Host { get; set; }
//		
//		public virtual int Port { get; set; }
//		
//		public virtual string Parameter { get; set; }
//		
//		public virtual string Info { get; set; }
//		
//		public virtual string Type { get; set; }
		
		public virtual PersistentWapitiResults ParentResults { get; set; }
		
//		public virtual IList<WapitiReference> References { get; set; }
//		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual WapitiBug ToWapitiBug()
		{
			WapitiBug bug = new WapitiBug();
			
			bug.Level = this.Level;
			bug.Timestamp = this.Timestamp;
			bug.URL = this.URL;
			bug.Host = this.Host;
			bug.Port = this.Port;
			bug.Parameter = this.Parameter;
			bug.Info = this.Info;
			bug.Type = this.Type;
			
			return bug;
		}
		
		private void SetCreationInfo(Guid creatorID)
		{
			DateTime now = DateTime.Now;
			
			this.ID =  Guid.NewGuid();
			this.CreatedBy = creatorID;
			this.CreatedOn = now;
			this.LastModifiedBy = creatorID;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
	}
}

