using System;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusHostProperties
	{
		public NessusHostProperties ()
		{
		}
		
		public NessusHostProperties (XmlNode props)
		{
			foreach (XmlNode tag in props.ChildNodes)
			{
				if (tag.Attributes["name"].Value == "HOST_END")
					this.HostEnd = tag.InnerText;
				else if (tag.Attributes["name"].Value == "system-type")
					this.SystemType = tag.InnerText;
				else if (tag.Attributes["name"].Value == "operating-system")
					this.OperatingSystem = tag.InnerText;
				else if (tag.Attributes["name"].Value == "host-ip")
					this.HostIP = tag.InnerText;
				else if (tag.Attributes["name"].Value == "host-fqdn")
					this.HostFQDN = tag.InnerText;
				else if (tag.Attributes["name"].Value == "HOST_START")
					this.HostBegin = tag.InnerText;
			}
		}
		
		public virtual string HostBegin { get; set; }
		public virtual string HostEnd { get; set; }
		public virtual string SystemType { get; set; }
		public virtual string OperatingSystem { get; set; }
		public virtual string HostIP { get; set; }
		public virtual string HostFQDN { get; set; }
	}
}

