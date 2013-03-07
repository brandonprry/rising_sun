using System;
using System.Collections.Generic;
using System.Xml;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentNMapResults : NMapToolResults, IEntity
	{
		IList<PersistentNMapHost> _hosts;
		
		public PersistentNMapResults ()
		{
		}
		
		public PersistentNMapResults (NMapToolResults results)
		{
			this.FullOutput = results.FullOutput;
			
			if (results.Hosts != null)
			{
				this.PersistentHosts = new List<PersistentNMapHost>();
				
				foreach (NMapHost host in results.Hosts)
				{
					PersistentNMapHost h = new PersistentNMapHost(host);
					
					this.PersistentHosts.Add(h);
				}
			}
		}
		
		public PersistentNMapResults(XmlNode node)
		{
			foreach (XmlNode child in node.FirstChild.ChildNodes)
			{
				if (child.Name == "id")
					this.ID = new Guid(child.InnerText);
				else if (child.Name == "createdOn")
					this.CreatedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "createdBy")
					this.CreatedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedBy")
					this.LastModifiedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedOn")
					this.LastModifiedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "isActive")
					this.IsActive = Boolean.Parse(child.InnerText);
				else if (child.Name == "hosts")
				{
					this.PersistentHosts = new List<PersistentNMapHost>();
					
					foreach (XmlNode c in child.ChildNodes)
						this.PersistentHosts.Add(new PersistentNMapHost(c));
				}
			}
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual IList<PersistentNMapHost> PersistentHosts { get; set; }
		
		public virtual  void SetCreationInfo(Guid userID)
		{
			this.ID = Guid.NewGuid();
			this.CreatedBy = userID;
			this.CreatedOn = DateTime.Now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = isActive;
		}
		
		public virtual void PopulateNonPersistentHosts()
		{
			IList<NMapHost> hosts = new List<NMapHost>();
			
			foreach (PersistentNMapHost phost in this.PersistentHosts)
			{
				phost.PopulateNonPersistentPorts();
				
				XmlDocument doc = new XmlDocument();
				string xml = phost.ToBusinessXml();
				
				doc.LoadXml(xml);
				
				hosts.Add(new NMapHost(doc.FirstChild));
			}
			
			this.Hosts = hosts;
		}
		
		public virtual string ToPersistentXml()
		{
			string xml = string.Empty;
			
			xml = xml + "<results>";
			
			xml = xml + "<id>" + this.ID.ToString() + "</id>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			//xml = xml + "<parentProfileID>" + this.ParentProfile.ID.ToString() + "</parentProfileID>";
			//xml = xml + "<userID>" + this.User.ID.ToString() + "</userID>";
			xml = xml + "<hostCount>" + this.PersistentHosts.Count + "</hostCount>";
			xml = xml + "<hosts>";
			
			foreach (PersistentNMapHost host in this.PersistentHosts)
				xml = xml + host.ToPersistentXml();
			
			xml = xml + "</hosts>";
			xml = xml + "</results>";
			
			return xml;
		}
	}
}

