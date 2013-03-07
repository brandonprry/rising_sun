using System;
using System.Xml;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusPolicy
	{
		public NessusPolicy ()
		{
		}
		
		public NessusPolicy(XmlNode policyNode)
		{
			foreach (XmlNode c in policyNode.ChildNodes)
			{
				if (c.Name	== "policyID")
					RemotePolicyID = int.Parse(c.InnerText);
				else if (c.Name == "policyName")
					Name = c.InnerText;
				else if  (c.Name == "policyOwner")
					Owner = c.InnerText;
				else if (c.Name == "visibility")
					Visibility = c.InnerText;
			}
		}
		
		public virtual int RemotePolicyID { get; set; }
		
		public virtual string Name { get;  set; }
		
		public virtual string Owner { get;  set; }
		
		public virtual string Visibility { get;  set; }
		
		public virtual string ToBusinessXml()
		{
			string xml = "<nessusPolicy>";
			
			xml = xml + "<policyName>" + this.Name + "</policyName>";
			xml = xml + "<policyOwner>" + this.Owner + "</policyOwner>";
			xml = xml + "<visibility>" + this.Visibility + "</visibility>";
			xml = xml + "<policyID>" + this.RemotePolicyID + "</policyID>";
			
			xml = xml + "</nessusPolicy>";
			
			return xml;
		}
	}
}

