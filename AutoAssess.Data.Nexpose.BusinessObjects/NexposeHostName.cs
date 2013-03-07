using System;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeHostName
	{
		public NexposeHostName ()
		{
		}
		
		public NexposeHostName(XmlNode name)
		{
			this.Name = name.InnerText;
		}
		
		public virtual string Name { get; set; }
	}
}

