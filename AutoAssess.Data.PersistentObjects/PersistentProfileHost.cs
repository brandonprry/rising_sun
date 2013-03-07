using System;
using AutoAssess.Data.BusinessObjects;
using System.Xml;
using System.Collections.Generic;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentProfileHost : ProfileHost
	{
		public PersistentProfileHost ()
		{
		}
		
		public PersistentProfileHost(Guid owner)
		{
			DateTime now = DateTime.Now;
			this.ID = Guid.NewGuid();
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedOn = now;
			this.LastModifiedBy = owner;
			this.IsActive = true;
		}
		
		public PersistentProfileHost(XmlNode host)
		{
			foreach (XmlNode child in host.ChildNodes)
			{
				if (child.Name == "parentProfileID")
					this.ParentProfileID = new Guid(child.InnerText);
				else if (child.Name == "profile")
					this.ParentProfile = new PersistentProfile(child);
				else if (child.Name == "id")
					this.ID = new Guid(child.InnerText);
				else if (child.Name == "name")
					this.Name = child.InnerText;
				else if (child.Name == "createdOn")
					this.CreatedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "createdBy")
					this.CreatedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedBy")
					this.LastModifiedBy = new Guid(child.InnerText);
				else if (child.Name == "lastModifiedOn")
					this.LastModifiedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "ipV4Address")
					this.IPv4Address = child.InnerText;
				else if (child.Name == "isVerified")
					this.IsActive = Boolean.Parse(child.InnerText);
				else if (child.Name == "verifiedByWhois")
					this.VerifiedByWhois = Boolean.Parse(child.InnerText);
				else if (child.Name == "verifiedByFile")
					this.VerifiedByFile = Boolean.Parse(child.InnerText);
				else if (child.Name == "verifiedOn")
					this.VerifiedOn = DateTime.Parse(child.InnerText);
				else if (child.Name == "wasManuallyVerified")
					this.WasManuallyVerified = Boolean.Parse(child.InnerText);
				else if (child.Name == "nmapHosts")
				{
					this.NmapHosts = new List<PersistentNMapHost>();
					foreach (XmlNode h in child.ChildNodes)
						this.NmapHosts.Add(new PersistentNMapHost(h));
				}
				else
					throw new Exception("I don't know how to deal with element: " + child.Name);
			}
		}
		
		public PersistentProfileHost(ProfileHost host, Guid owner)
		{
			if (host == null)
				throw new Exception("Host null.");
			
			this.IPv4Address = host.IPv4Address;
			this.IsVerified = host.IsVerified;
			this.VerifiedByFile = host.VerifiedByFile;
			this.VerifiedByWhois = host.VerifiedByWhois;
			this.VerifiedOn = host.VerifiedOn;
			this.WasManuallyVerified = host.WasManuallyVerified;
			this.Name = host.Name;
			
			DateTime now = DateTime.Now;
			this.ID = Guid.NewGuid();
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedOn = now;
			this.LastModifiedBy = owner;
			this.IsActive = true;
		}
		
		public virtual Guid ParentProfileID { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual IList<PersistentNMapHost> NmapHosts { get; set; }

		public virtual string ToPersistentXML (bool withNmapHosts)
		{
			string xml = "<profileHost>";
			
			xml += "<parentProfileID>" + this.ParentProfileID.ToString() + "</parentProfileID>";
			
			if (this.ParentProfile != null && withNmapHosts)
				xml += this.ParentProfile.ToPersistentXml();
			
			xml += "<id>" + this.ID.ToString() + "</id>";
			xml += "<createdOn>" + this.CreatedOn.ToString() + "</createdOn>";
			xml += "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml += "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml += "<lastModifiedOn>" + this.LastModifiedOn.ToString() + "</lastModifiedOn>";
			xml += "<ipV4Address>" + this.IPv4Address + "</ipV4Address>";
			xml += "<isVerified>" + this.IsVerified + "</isVerified>";
			xml += "<verifiedByFile>" + this.VerifiedByFile + "</verifiedByFile>";
			xml += "<verifiedByWhois>" + this.VerifiedByWhois + "</verifiedByWhois>";
			xml += "<verifiedOn>" + this.VerifiedOn + "</verifiedOn>";
			xml += "<wasManuallyVerified>" + this.WasManuallyVerified + "</wasManuallyVerified>";
			xml += "<name>" + this.Name + "</name>";
			
			if (withNmapHosts)
			{
				xml += "<nmapHosts>";
				
				foreach (PersistentNMapHost host in this.NmapHosts)
					xml += host.ToPersistentXml();
				
				xml += "</nmapHosts>";
			}
			
			xml += "</profileHost>";
			return xml;
		}
	}
}

