using System;
using System.Collections.Generic;
using System.Xml;
using AutoAssess.Data;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASAgent  : IOpenVASObject
	{
		public OpenVASAgent()
		{}
		
		public OpenVASAgent(XmlNode node)
		{
			if (node.Name != "agent")
				throw new Exception("Not an agent node.");
			
			this.RemoteAgentID = new Guid(node.Attributes["id"].Value);
			
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "name")
					this.Name = child.Value;
				else if (child.Name == "comment")
					this.Comment = child.Value;
				else if (child.Name == "in_use")
					this.InUse = (child.Value == "1" ? true : false);
			}
		}
		
		public virtual string Installer { get;  set; }
		
		public virtual string Signature { get;  set; }
		
		public virtual string Comment { get; set; }
		
		public virtual bool InUse { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual Guid RemoteAgentID { get;  set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
	}
}

