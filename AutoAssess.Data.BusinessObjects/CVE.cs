using System;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class CVE
	{
		public CVE ()
		{
		}
		
		public CVE (XmlNode cve)
		{
			if (cve.Attributes["name"] != null)
				this.Name = cve.Attributes["name"].Value;
			
			foreach (XmlNode child in cve.ChildNodes)
			{
				if (child.Name == "desc")
					this.Description = child.InnerText;
				else if (child.Name == "refs")
				{
					if (this.References == null)
						this.References = new List<CVEReference>();
					
					foreach (XmlNode reff in child.ChildNodes)
						this.References.Add(new CVEReference(reff));
				}
				else if (child.Name == "comments")
				{
					if (this.Comments == null)
						this.Comments = new List<CVEComment>();
					
					foreach (XmlNode comment in child.ChildNodes)
						this.Comments.Add(new CVEComment(comment));
				}
			}
		}
		
		public virtual string Name { get; set; }
		
		public virtual string Description { get; set; }
		
		public virtual List<CVEReference> References { get; set; }
		
		public virtual List<CVEComment> Comments { get; set; }
	}
	
	public class CVEReference
	{
		public CVEReference()
		{}
		
		public CVEReference(XmlNode reference)
		{
			this.Source = reference.Attributes["source"].Value;
			
			if (reference.Attributes["url"] != null)
				this.URL = reference.Attributes["url"].Value;
			
			this.Description = reference.InnerText;
		}
		
		public virtual string Source { get; set; }
		
		public virtual string URL { get; set; }
		
		public virtual string Description { get; set; }
	}
	
	public class CVEComment 
	{
		public CVEComment()
		{}
		
		public CVEComment(XmlNode comment)
		{
			this.Voter = comment.Attributes["voter"].Value;
			
			this.Comment = comment.InnerText;
		}
		public virtual string Voter { get; set; }
		
		public virtual string Comment { get; set; }
	}
}

