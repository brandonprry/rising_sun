using System;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusScanTemplate
	{
		public NessusScanTemplate ()
		{
		}
		
		public NessusScanTemplate(XmlNode templateNode)
		{
			foreach (XmlNode c in templateNode.ChildNodes)
			{
				if (c.Name == "name")
					Name = c.Value;
				else if (c.Name == "policy_id")
					RemotePolicyID = int.Parse(c.Value);
				else if (c.Name == "readableName")
					ReadableName = c.Value;
				else if (c.Name == "owner")
					Owner = c.Value;
				else if (c.Name == "target")
					Target = c.Value;
			}
		}
		
		public virtual string Name { get; set; }
		
		public virtual string ReadableName { get; set; }
		
		public virtual int RemotePolicyID { get; set; }
		
		public virtual string Owner { get; set; }
		
		public virtual string Target { get; set; }
	}
}

