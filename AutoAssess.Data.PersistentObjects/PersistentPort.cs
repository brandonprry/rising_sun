using System;
using System.Xml;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	
	[Serializable]
	public class PersistentPort : Port, IEntity
	{
		public PersistentPort ()
		{
		}
		
		public PersistentPort(XmlNode node)
		{
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "id")
					this.ID = new Guid(child.InnerText);
				else if (child.Name == "createdOn")
					this.CreatedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "createdBy")
					this.CreatedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedBy")
					this.LastModifiedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedOn")
					this.LastModifiedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "isActive")
					this.IsActive = Boolean.Parse(child.InnerText);
				else if (child.Name == "deepScan")
					this.DeepScan = child.InnerText;
				else if (child.Name == "hydraServiceName")
					this.HydraServiceName = child.InnerText;
				else if (child.Name == "isTcp")
					this.IsTCP = Boolean.Parse(child.InnerText);
				else if (child.Name == "portNumber")
					this.PortNumber = int.Parse(child.InnerText);
				else if (child.Name == "service")
					this.Service = child.InnerText;
				else if (child.Name == "state")
					this.State = child.InnerText;
			}
		}
		
		public PersistentPort (Port port)
		{
			this.DeepScan = port.DeepScan;
			this.HydraServiceName = port.HydraServiceName;
			this.IsTCP = port.IsTCP;
			this.PortNumber = port.PortNumber;
			this.Service = port.Service;
			this.State = port.State;
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
		public virtual PersistentNMapHost ParentHost { get; set; }
		
		public virtual void SetCreationInfo(Guid userID)
		{
			this.ID = Guid.NewGuid();
			this.CreatedBy = userID;
			this.CreatedOn = DateTime.Now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = isActive;
		}
		
		public virtual string ToPersistentXml()
		{				
			string xml = "<port>";
			
			xml = xml + "<id>" + this.ID.ToString() + "</id>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			//xml = xml + "<userID>" + this.User.ID.ToString() + "</userID>";
			//xml = xml + "<parentProfileID>" + this.ParentProfile.ID.ToString() + "</parentProfileID>";
			xml = xml + "<parentHostID>" + this.ParentHost.ID.ToString() + "</parentHostID>";
			
			if (!string.IsNullOrEmpty(this.DeepScan)) //can be null if a port closes or is falsely considered open?
				xml = xml + "<deepScan>" +  this.DeepScan.Replace("&", "&amp;")
                             .Replace("<", "&lt;")
                             .Replace(">", "&gt;") + "</deepScan>";
			
			xml = xml + "<hydraServiceName>" + this.HydraServiceName + "</hydraServiceName>";
			xml = xml + "<isTcp>" + this.IsTCP + "</isTcp>";
			xml = xml + "<portNumber>" + this.PortNumber + "</portNumber>";
			xml = xml + "<service>" + this.Service + "</service>";
			xml = xml + "<state>" + this.State + "</state>"; 
			
			xml = xml + "</port>";

			return xml;
		}
	}
}

