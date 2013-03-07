using System;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeHostFingerprint
	{
		public NexposeHostFingerprint ()
		{
		}
		
		public NexposeHostFingerprint(XmlNode print)
		{
			this.Certainty = decimal.Parse(print.Attributes["certainty"].Value);
			
			if (print.Attributes["device-class"] != null)
				this.DeviceClass = print.Attributes["device-class"].Value;
			if ( print.Attributes["vendor"] != null)
				this.Vendor = print.Attributes["vendor"].Value;
			if (print.Attributes["family"] != null)
				this.Family = print.Attributes["family"].Value;
			if (print.Attributes["product"] != null)
				this.Product = print.Attributes["product"].Value;
		}
		
		public virtual decimal Certainty { get; set; }
		public virtual string DeviceClass { get; set; }
		public virtual string Vendor { get; set; }
		public virtual string Family { get; set; }
		public virtual string Product { get; set; }
	}
}

