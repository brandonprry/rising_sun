using System;
using System.Xml;
using System.Collections.Generic;
namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class WapitiToolResults : ToolResults, IToolResults
	{
		public WapitiToolResults() {}
		
		public WapitiToolResults (string commandOutput)
		{
			this.FullOutput = commandOutput;
		}
		
		public virtual Guid HostPortID { get; set; }
		
		public virtual string HostIPAddressV4 { get; set; }
		
		public virtual int HostPort { get; set; }
		
		public virtual bool IsTCP { get; set; }
		
		public virtual IList<WapitiBug> Bugs { get; set; }
		
		public virtual bool IsUDP
		{
			get { return !IsTCP; }
			set { IsTCP = !value; }
		}
		
		public static IToolResults Parse(string path)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(System.IO.File.ReadAllText(path));
			
			IToolResults results = new WapitiToolResults();
			(results as WapitiToolResults).Bugs = new List<WapitiBug>();
			
			foreach (XmlNode node in doc.LastChild.LastChild.ChildNodes)
			{
				string bugType = node.Attributes["name"].Value;
			
				foreach (XmlNode bug in node.ChildNodes)
				{
					if (bug.Name == "description" || bug.Name == "solution" || bug.Name == "references")
						continue;
					
					foreach (XmlNode n in bug.ChildNodes)
						(results as WapitiToolResults).Bugs.Add(new WapitiBug(bugType, n));
					
				}
			}
			
			return results;
		}
	}
	
	[Serializable]
	public class WapitiBug
	{
		public WapitiBug()
		{}
		
		public WapitiBug(string type, XmlNode bug)
		{
			this.Type = type;
			
			foreach (XmlNode node in bug.ChildNodes)
			{
				if (node.Name == "timestamp")
					this.Timestamp = node.InnerText;
				else if (node.Name == "url")
					this.URL = node.InnerText;
				else if (node.Name == "peer") {
					foreach (XmlNode peerInfo in node.ChildNodes) {
						if (peerInfo.Name == "addr")
							this.Host = peerInfo.InnerText;
						else if (peerInfo.Name == "port")
							this.Port = int.Parse (peerInfo.InnerText);
					}
				} else if (node.Name == "parameter")
					this.Parameter = node.InnerText;
				else if (node.Name == "info")
					this.Info = node.InnerText;

			}
		}
		
		public virtual int Level { get; set; }
		
		public virtual string Timestamp { get; set; }
		
		public virtual string URL { get; set; }
		
		public virtual string Host { get; set; }
		
		public virtual int Port { get; set; }
		
		public virtual string Parameter { get; set; }
		
		public virtual string Info { get; set; }
		
		public virtual string Type { get; set; }
		
		public virtual IList<WapitiReference> References { get; set; }
	}
	
	[Serializable]
	public class WapitiReference
	{
		public WapitiReference(XmlNode node)	
		{
		}
		
		public virtual string Title { get; set; }
		
		public virtual string URL { get; set; }
	}
}

