using System;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeServiceConfiguration
	{
		public NexposeServiceConfiguration()
		{}
		
		public NexposeServiceConfiguration (XmlNode config)
		{
			this.Name = config.Attributes["name"].Value;
			this.Value = config.InnerText;
		}
		
		public virtual string Name { get; set; }
		
		public virtual string Value { get; set; }
	}
}

