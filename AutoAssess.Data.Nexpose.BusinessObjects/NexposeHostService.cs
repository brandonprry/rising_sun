using System;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeHostService
	{
		public NexposeHostService ()
		{
		}
		
		public NexposeHostService(XmlNode service)
		{
			this.Name = service.Attributes["name"].Value;
			foreach (XmlNode child in service.ChildNodes)
			{
				if (child.Name == "fingerprints")
				{
					this.Fingerprints = new List<NexposeServiceFingerprint>();
					foreach (XmlNode print in child.ChildNodes)
						this.Fingerprints.Add(new NexposeServiceFingerprint(print));
				}
				else if (child.Name == "configuration")
				{
					this.Configurations = new List<NexposeServiceConfiguration>();
					foreach (XmlNode config in child.ChildNodes)
						this.Configurations.Add(new NexposeServiceConfiguration(config));
				}
				else if (child.Name == "tests")
				{
					this.ServiceTests = new List<NexposeServiceTest>();
					foreach (XmlNode test in child.ChildNodes)
						this.ServiceTests.Add(new NexposeServiceTest(test));
				}
				
			}
		}
		
		public virtual string Protocol { get; set; }
		
		public virtual int Port { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual IList<NexposeServiceFingerprint> Fingerprints { get; set; }
		
		public virtual IList<NexposeServiceConfiguration> Configurations { get; set; }
		
		public virtual IList<NexposeServiceTest> ServiceTests { get; set; }
	}
}

