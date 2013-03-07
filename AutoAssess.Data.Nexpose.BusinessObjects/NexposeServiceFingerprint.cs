using System;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeServiceFingerprint
	{
		public NexposeServiceFingerprint()
		{
		}
		
		public NexposeServiceFingerprint (XmlNode print)
		{
			this.Certainty = decimal.Parse(print.Attributes["certainty"].Value);
			this.Product = print.Attributes["product"].Value;
			
			if (print.Attributes["family"] != null)
				this.Family = print.Attributes["family"].Value;
			
			if (print.Attributes["version"] != null)
				this.Version = print.Attributes["version"].Value;
			
			if (print.Attributes["vendor"] != null)
				this.Vendor = print.Attributes["vendor"].Value;
		}
		
		public virtual decimal Certainty { get; set; }
		
		public virtual string Family { get; set; }
		
		public virtual string Vendor { get; set; }
		
		public virtual string Product { get; set; }
		
		public virtual string Version { get; set; }
	}
}

