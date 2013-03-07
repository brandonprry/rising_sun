using System;
using System.Xml;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitNote
	{
		public MetasploitNote()
		{}
		
		public MetasploitNote (XmlNode note)
		{
			foreach (XmlNode child in note.ChildNodes)
			{
				if (child.Name == "id")
					this.RemoteID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "created-at")
					this.RemoteCreatedAt = child.InnerText;
				else if (child.Name == "ntype")
					this.NoteType = child.InnerText;
				else if (child.Name == "workspace-id")
					this.RemoteWorkspaceID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "service-id")
					this.RemoteServiceID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "host-id")
					this.RemoteHostID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "updated-at")
					this.RemoteUpdatedAt = child.InnerText;
				else if (child.Name == "critical")
				{}
				else if (child.Name == "seen")
				{}
				else if (child.Name == "data")
					this.Data = child.InnerText;
			}
		}
		
		public virtual int RemoteID { get; set; }
		
		public virtual string RemoteCreatedAt { get; set; }
		
		public virtual string NoteType { get; set; }
		
		public virtual int RemoteWorkspaceID { get; set; }
		
		public virtual int RemoteServiceID { get; set; }
		
		public virtual int RemoteHostID { get; set; }
		
		public virtual string RemoteUpdatedAt { get; set; }
		
		public virtual string Data { get; set; }
	}
}

