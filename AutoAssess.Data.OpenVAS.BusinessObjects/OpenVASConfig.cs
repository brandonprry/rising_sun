using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASConfig : IOpenVASObject
	{
		public OpenVASConfig ()
		{
		}
		
		public OpenVASConfig(XmlNode config)
		{
			this.RemoteConfigID = new Guid(config.Attributes["id"].Value);
			
			foreach (XmlNode child in config.ChildNodes)
			{
				if (child.Name == "name")
					this.Name = child.InnerText;
				else if (child.Name == "comment")
					this.Comment = child.InnerText;
				else if (child.Name == "family_count")
				{
					this.FamilyCount = int.Parse(child.FirstChild.InnerText);
					this.FamilyCountIsGrowing = (child.LastChild.InnerText == "1" ? true : false);
				}
				else if (child.Name == "nvt_count")
				{	
					this.NVTCount = int.Parse(child.FirstChild.InnerText);
					this.NVTCountIsGrowing = (child.LastChild.InnerText == "1" ? true : false);
				}
				else if (child.Name == "in_use")
					this.InUse = (child.InnerText == "1" ? true : false);
				
			}
		}
		
		public virtual string Name { get; set; }
		
		public virtual string Comment { get; set; }
		
		public virtual int FamilyCount { get; set; }
		
		public virtual bool FamilyCountIsGrowing { get; set; }
		
		public virtual int NVTCount { get; set; }
		
		public virtual bool NVTCountIsGrowing { get; set; }
		
		public virtual bool InUse { get; set; }
		
		public virtual IList<OpenVASTask> Tasks { get; set; }
		
		public virtual IList<OpenVASNVTFamily> Families { get; set; }
		
		public virtual Guid RemoteConfigID { get; set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
	}
}

