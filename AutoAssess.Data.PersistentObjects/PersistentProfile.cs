using System;
using System.Collections.Generic;
using System.Xml;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentProfile : Profile, IEntity
	{
		PersistentNMapResults _currentResults;
		
		public PersistentProfile ()
		{
		}
		
		public PersistentProfile(XmlNode node)
		{
			foreach (XmlNode child in node.ChildNodes)
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
				else if (child.Name == "currentResults")
				{
					var r = new PersistentNMapResults(child);
					this.CurrentResults = r;
				}
				else if (child.Name == "name")
					this.Name = child.InnerText;
				else if (child.Name == "duration")
					this.Duration = child.InnerText;
				else if (child.Name == "description")
					this.Description = child.InnerText;
				else if (child.Name == "range")
					this.Range = child.InnerText;
				else if (child.Name == "allResults")
				{
					this.AllResults = new List<PersistentNMapResults>();
					
					foreach (XmlNode c in child.ChildNodes)
					{
						this.AllResults.Add(new PersistentNMapResults(c));
					}
				}
			}
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid WebUserID { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual string BadgeState { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual IList<PersistentEvent> Events { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual IList<PersistentProfileHost> ProfileHosts { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual IList<PersistentVirtualMachine> VirtualMachines { get; set; }
		
		public virtual PersistentNMapResults CurrentResults 
		{ 
			get 
			{ 
				if (_currentResults == null && base.CurrentResults != null)
					_currentResults = new PersistentNMapResults(base.CurrentResults);
				
				return _currentResults; 
			}
			set 
			{ 
				_currentResults = value;
				base.CurrentResults = value as NMapToolResults;
			} 
		}
		
		public virtual IList<PersistentNMapResults> AllResults 
		{ 
			get { return base.AllResults as IList<PersistentNMapResults>; }
			set { base.AllResults = value as IList<NMapToolResults>; }
		}
		
		public virtual void SetCreationInfo(Guid userID)
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
		
		public virtual string ToPersistentXml()
		{
			string xml = "<profile>";
			
			xml = xml + "<id>" + this.ID.ToString() + "</id>";
			xml = xml + "<createdBy>" + this.CreatedBy.ToString() + "</createdBy>";
			xml = xml + "<createdOn>" + this.CreatedOn.ToLongDateString() + "</createdOn>";
			xml = xml + "<lastModifiedBy>" + this.LastModifiedBy.ToString() + "</lastModifiedBy>";
			xml = xml + "<lastModifiedOn>" + this.LastModifiedOn.ToLongDateString() + "</lastModifiedOn>";
			xml = xml + "<isActive>" + this.IsActive + "</isActive>";
			xml = xml + "<daysBetweenScan>" + this.DaysBetweenScan + "</daysBetweenScan>";
			xml = xml + "<hasRun>" + this.HasRun + "</hasRun>";
			xml = xml + "<name>" + this.Name + "</name>";
			xml = xml + "<duration>" + this.Duration + "</duration>";
			xml = xml + "<description>" + this.Description + "</description>";
			xml = xml + "<range>" + this.Range + "</range>";
			xml = xml + "<runAfter>" + this.RunAfter.ToLongDateString() + "</runAfter>";
			xml = xml + "<runEvery>" + this.RunEvery.Days + " days</runEvery>";
			
			if (this.AllResults != null)
				xml = xml + "<resultCount>" + this.AllResults.Count + "</resultCount>";
			
			if (this.CurrentResults != null)
			{
				xml = xml + "<currentResults>";
				xml = xml + this.CurrentResults.ToPersistentXml();
				xml = xml + "</currentResults>";
			}
			
			xml = xml + "</profile>";
			
			return xml;
			
		}
	}
}

