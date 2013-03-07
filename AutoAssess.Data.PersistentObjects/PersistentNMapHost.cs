using System;
using System.Collections.Generic;
using System.Xml;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentNMapHost : NMapHost, IEntity
	{
		IList<PersistentPort> _ports;
		
		public PersistentNMapHost ()
		{
		}
		
		public PersistentNMapHost (XmlNode node)
		{
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "id")
					this.ID = new Guid(child.InnerText);
				else if (child.Name == "createdOn")
					this.CreatedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "createdBy")
					this.CreatedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedOn")
					this.LastModifiedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "lastModifiedBy")
					this.LastModifiedBy = new Guid(child.InnerText);
				else if (child.Name == "isActive")
					this.IsActive = Boolean.Parse(child.InnerText);
				else if (child.Name == "deviceType")
					this.DeviceType = child.InnerText;
				else if (child.Name == "profileHost")
					this.ProfileHost = new PersistentProfileHost(child);
				else if (child.Name == "hostname")
					this.Hostname = child.InnerText;
				else if (child.Name == "ipAddressV4")
					this.IPAddressv4 = child.InnerText;
				else if (child.Name == "ipAddressV6")
					this.IPAddressV6 = child.InnerText;
				else if (child.Name == "mac")
					this.MAC = child.InnerText;
				else if (child.Name == "networkDistance")
					this.NetworkDistance = child.InnerText;
				else if (child.Name == "os")
					this.OS = child.InnerText;
				else if (child.Name == "osDetails")
					this.OS_Details = child.InnerText;
				else if (child.Name == "ports")
				{
					this.PersistentPorts = new List<PersistentPort>();
					
					foreach (XmlNode c in child.ChildNodes)
						this.PersistentPorts.Add(new PersistentPort(c));
				}
			}
		}
		
		public PersistentNMapHost (NMapHost host)
		{
			this.DeviceType = host.DeviceType;
			this.Hostname = host.Hostname;
			this.IPAddressv4 = host.IPAddressv4;
			this.IPAddressV6 = host.IPAddressV6;
			this.MAC = host.MAC;
			this.NetworkDistance = host.NetworkDistance;
			this.OS = host.OS;
			this.OS_Details = host.OS_Details;
			
			this.PersistentPorts = new List<PersistentPort>();
			
			foreach (Port port in host.Ports)
			{
				PersistentPort pport = new PersistentPort(port);
				
				this.PersistentPorts.Add(pport);
			}
		}
		
		public virtual PersistentProfileHost ProfileHost { get; set; }
		
		public virtual IList<PersistentPort> PersistentPorts { get; set; }
		
		public virtual PersistentNMapResults ParentResults { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
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
		
		public virtual void PopulateNonPersistentPorts()
		{
			List<Port> ports = new List<Port>();
			
			foreach (PersistentPort port in this.PersistentPorts)
			{
				XmlDocument doc = new XmlDocument();
				string xml = port.ToBusinessXml();
				
				doc.LoadXml(xml);
				ports.Add(new Port(doc.FirstChild));
			}
			this.Ports = ports;
		}
		
		public virtual string ToPersistentXml()
		{				
			string xml = "<host>";
			
			xml = xml + "<id>" + this.ID.ToString() + "</id>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToShortDateString() + "</createdOn>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			xml = xml + "<parentResultsID>" + this.ParentResults.ID + "</parentResultsID>";
			//xml = xml + "<parentProfileID>" + this.ParentProfile.ID + "</parentProfileID>";
			//xml = xml + "<userID>" + this.User.ID + "</userID>";
			xml = xml + "<deviceType>" + this.DeviceType + "</deviceType>";
			xml = xml + "<hostname>" + this.Hostname + "</hostname>";
			xml = xml + "<ipAddressV4>" + this.IPAddressv4 + "</ipAddressV4>";
			xml = xml + "<ipAddressV6>" + this.IPAddressV6 + "</ipAddressV6>";
			xml = xml + "<mac>" + this.MAC + "</mac>";
			xml = xml + "<networkDistance>" + this.NetworkDistance + "</networkDistance>";
			xml = xml + "<os>" + this.OS + "</os>";
			xml = xml + "<osDetails>" + this.OS_Details + "</osDetails>";
			
			if (this.ProfileHost != null)
				xml = xml + this.ProfileHost.ToPersistentXML(false);
			
			xml = xml + "<ports>";
			
			foreach (PersistentPort port in this.PersistentPorts)
				xml = xml + port.ToPersistentXml();
			
			
			xml = xml + "</ports>";
			
			xml = xml + "</host>";
			
			return xml;
		}
	}
}

