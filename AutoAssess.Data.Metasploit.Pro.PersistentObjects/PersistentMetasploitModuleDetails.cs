using System;
using AutoAssess.Data.Metasploit.Pro.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.Metasploit.Pro.PersistentObjects
{
	[Serializable]
	public class PersistentMetasploitModuleDetails : MetasploitModuleDetails
	{
		public PersistentMetasploitModuleDetails ()
		{
		}
		
		public PersistentMetasploitModuleDetails (MetasploitModuleDetails deets)
		{
			this.DefaultTarget = deets.DefaultTarget;
			this.Description = deets.Description;
			this.DisclosureDate = deets.DisclosureDate;
			this.File = deets.File;
			this.FullName = deets.FullName;
			this.License = deets.License;
			this.ModuleType = deets.ModuleType;
			this.Name = deets.Name;
			this.Priviledged = deets.Priviledged;
			this.Rank = deets.Rank;
			this.Ready = deets.Ready;
			this.ReferenceName = deets.ReferenceName;
			this.RemoteID = deets.RemoteID;
			this.Stance = deets.Stance;
			
			this.PersistentActions = new List<PersistentMetasploitModuleAction>();
			this.PersistentArchitectures = new List<PersistentMetasploitModuleArchitecture>();
			this.PersistentAuthors = new List<PersistentMetasploitModuleAuthor>();
			this.PersistentMixins = new List<PersistentMetasploitModuleMixin>();
			this.PersistentPlatforms = new List<PersistentMetasploitModulePlatform>();
			this.PersistentReferences = new List<PersistentMetasploitModuleReference>();
			this.PersistentTargets = new List<PersistentMetasploitModuleTarget>();

			if (deets.Actions != null)
				foreach (var action in deets.Actions)
					this.PersistentActions.Add(new PersistentMetasploitModuleAction(action));
			
			if (deets.Architecures != null)
				foreach (var arch in deets.Architecures)
					this.PersistentArchitectures.Add(new PersistentMetasploitModuleArchitecture(arch));
			if (deets.Authors != null)
				foreach (var author in deets.Authors)
					this.PersistentAuthors.Add(new PersistentMetasploitModuleAuthor(author));
			if (deets.Mixins != null)
				foreach (var mixin in deets.Mixins)
					this.PersistentMixins.Add(new PersistentMetasploitModuleMixin(mixin));
			if (deets.Platforms != null)
				foreach (var platform in deets.Platforms)
					this.PersistentPlatforms.Add(new PersistentMetasploitModulePlatform(platform));
			if (deets.References != null)
				foreach (var reff in deets.References)
					this.PersistentReferences.Add(new PersistentMetasploitModuleReference(reff));
			if (deets.Targets != null)
				foreach (var target in deets.Targets)
					this.PersistentTargets.Add(new PersistentMetasploitModuleTarget(target));
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual IList<PersistentMetasploitModuleAuthor> PersistentAuthors { get; set; }
		public virtual IList<PersistentMetasploitModuleReference> PersistentReferences { get; set; }
		public virtual IList<PersistentMetasploitModuleArchitecture> PersistentArchitectures { get; set; }
		public virtual IList<PersistentMetasploitModulePlatform> PersistentPlatforms { get; set; }
		public virtual IList<PersistentMetasploitModuleTarget> PersistentTargets { get; set; }
		public virtual IList<PersistentMetasploitModuleAction> PersistentActions { get; set; }
		public virtual IList<PersistentMetasploitModuleMixin> PersistentMixins { get; set; }
		
		public virtual void SetCreationInfo(Guid owner)
		{
			DateTime now = DateTime.Now;
			
			this.ID = Guid.NewGuid();
			this.CreatedBy = owner;
			this.CreatedOn = now;
			this.LastModifiedBy = owner;
			this.LastModifiedOn = now;
			this.IsActive = true;
		}
		
		public virtual void SetCreationInfo(Guid owner, bool recursive)
		{
			this.SetCreationInfo(owner);
			
			if (recursive)
			{
				foreach (var author in this.PersistentAuthors)
					author.SetCreationInfo(owner);
				foreach(var reff in this.PersistentReferences)
					reff.SetCreationInfo(owner);
				foreach (var arch in this.PersistentArchitectures)
					arch.SetCreationInfo(owner);
				foreach (var platform in this.PersistentPlatforms)
					platform.SetCreationInfo(owner);
				foreach (var target in this.PersistentTargets)
					target.SetCreationInfo(owner);
				foreach (var action in this.PersistentActions)
					action.SetCreationInfo(owner);
				foreach (var mixin in this.PersistentMixins)
					mixin.SetCreationInfo(owner);
			}
		}
		
		public virtual void SetUpdateInfo(Guid modifier)
		{
			this.LastModifiedBy = modifier;
			this.LastModifiedOn = DateTime.Now;
		}
	}
}

