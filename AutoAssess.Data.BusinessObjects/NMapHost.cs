using System;
using System.Collections.Generic;
using System.Xml;
using AutoAssess.Misc;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class NMapHost 
	{	
		public NMapHost ()
		{
		}
		
		public NMapHost(XmlNode node)
		{
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "deviceType")
					this.DeviceType = child.InnerText;
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
					this.Ports = new List<Port>();
					
					foreach (XmlNode c in child.ChildNodes)
						this.Ports.Add(new Port(c));
				}
			}
		}
		
		public virtual IList<Port> Ports {get; set; }
		
		public virtual string Hostname { get; set; }
		
		public virtual string IPAddressv4 { get; set; }

		public virtual string IPAddressV6 { get; set; }
		
		public virtual string MAC { get; set; }
		
		public virtual string DeviceType { get; set; }
		
		public virtual string OS { get; set; }
		
		public virtual string OS_Details { get; set; }
		
		public virtual string NetworkDistance { get; set; }

		public virtual void CleanHost()
		{
			this.DeviceType = StringUtils.CleanContent(this.DeviceType);
			this.Hostname = StringUtils.CleanContent(this.Hostname);
			this.IPAddressv4 = StringUtils.CleanContent(this.IPAddressv4);
			this.IPAddressV6 = StringUtils.CleanContent(this.IPAddressV6);
			this.MAC = StringUtils.CleanContent(this.MAC);
			this.NetworkDistance = StringUtils.CleanContent(this.NetworkDistance);
			this.OS = StringUtils.CleanContent(this.OS);
			this.OS_Details = StringUtils.CleanContent(OS_Details);
		}
		
		public virtual string ToBusinessXml()
		{
			string xml = "<host>";
				
			xml = xml + "<deviceType>" + this.DeviceType + "</deviceType>";
			xml = xml + "<hostname>" + this.Hostname + "</hostname>";
			xml = xml + "<ipAddressV4>" + this.IPAddressv4 + "</ipAddressV4>";
			xml = xml + "<ipAddressV6>" + this.IPAddressV6 + "</ipAddressV6>";
			xml = xml + "<mac>" + this.MAC + "</mac>";
			xml = xml + "<networkDistance>" + this.NetworkDistance + "</networkDistance>";
			xml = xml + "<os>" + this.OS + "</os>";
			xml = xml + "<osDetails>" + this.OS_Details + "</osDetails>";
			
			xml = xml + "<ports>";
			
			foreach (Port port in this.Ports)
			{
				port.CleanPort();
				xml = xml + port.ToBusinessXml();
			}
		
			
			xml = xml + "</ports>";
			
			xml = xml + "</host>";
			
			return xml;
		}
	}
}

