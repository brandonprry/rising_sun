using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitModuleDetails
	{
		public MetasploitModuleDetails()
		{}
		public MetasploitModuleDetails (XmlNode deets)
		{
			foreach (XmlNode child in deets.ChildNodes)
			{
				if (child.Name == "id")
					this.RemoteID = string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "mtime")
				{}
				else if (child.Name == "file")
					this.File = child.InnerText;
				else if (child.Name == "mtype")
					this.ModuleType = child.InnerText;
				else if (child.Name == "refname")
					this.ReferenceName = child.InnerText;
				else if (child.Name == "fullname")
					this.FullName = child.InnerText;
				else if (child.Name == "rank")
					this.Rank =  string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(child.InnerText);
				else if (child.Name == "description")
					this.Description = child.InnerText;
				else if (child.Name == "license")
					this.License = child.InnerText;
				else if (child.Name == "privileged")
				{}
				else if (child.Name == "disclosure-date")
					this.DisclosureDate = child.InnerText;
				else if (child.Name == "default-target")
					this.DefaultTarget = string.IsNullOrEmpty(child.InnerText) ? -1 :  int.Parse(child.InnerText);
				else if (child.Name == "default-action")
				{}
				else if (child.Name == "stance")
					this.Stance = child.InnerText;
				else if (child.Name == "ready")
					this.Ready = Boolean.Parse(child.InnerText);
				else if (child.Name == "module_authors")
				{
					this.Authors = new List<MetasploitModuleAuthor>();
					int i = 0;
					MetasploitModuleAuthor author = new MetasploitModuleAuthor();
					foreach (XmlNode info in child.ChildNodes)
					{
						if (info.Name == "id")
							author.RemoteID =  string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(info.InnerText);
						else if (info.Name == "module-detail-id")
							author.RemoteModuleDetailID =  string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(info.InnerText);
						else if (info.Name == "name")
							author.Name = info.InnerText;
						else if (info.Name == "email")
							author.Email = info.InnerText;
						
						i++;
						
						if (i == 3)
						{
							this.Authors.Add(author);
							author = new MetasploitModuleAuthor();
							i = 0;
						}
					}
				}
				else if (child.Name == "module_refs")
				{
					this.References = new List<MetasploitModuleReference>();
					int i = 0;
					MetasploitModuleReference refr = new MetasploitModuleReference();
					foreach (XmlNode reff in child.ChildNodes)
					{
						if (reff.Name == "id")
							refr.RemoteID = string.IsNullOrEmpty(child.InnerText) ? -1 :  int.Parse(reff.InnerText);
						else if (reff.Name == "module-detail-id")
							refr.RemoteModuleDetailID =  string.IsNullOrEmpty(child.InnerText) ? -1 : int.Parse(reff.InnerText);
						else if (reff.Name == "name")
							refr.Name = child.InnerText;
						
						i++;
						if (i == 3)
						{
							this.References.Add(refr);
							refr = new MetasploitModuleReference();
							i = 0;
						}
					}
				}
				
			}
		}
		
		public virtual int RemoteID { get; set; }
		public virtual string File { get; set; }
		public virtual string ModuleType { get; set; }
		public virtual string ReferenceName { get; set; }
		public virtual string FullName { get; set; }
		public virtual string Name { get; set; }
		public virtual int Rank { get;set; }
		public virtual string Description { get; set; }
		public virtual string License { get; set; }
		public virtual bool Priviledged { get; set; }
		public virtual string DisclosureDate { get; set; }
		public virtual int DefaultTarget { get; set; }
		public virtual string Stance { get; set; }
		public virtual bool Ready { get; set; }
		public virtual IList<MetasploitModuleAuthor> Authors { get; set; }
		public virtual IList<MetasploitModuleReference> References { get; set; }
		public virtual IList<MetasploitModuleArchitecture> Architecures { get; set; }
		public virtual IList<MetasploitModulePlatform> Platforms { get; set; }
		public virtual IList<MetasploitModuleTarget> Targets { get; set; }
		public virtual IList<MetasploitModuleAction> Actions { get; set; }
		public virtual IList<MetasploitModuleMixin> Mixins { get; set; }
	}
}

