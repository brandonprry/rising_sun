using System;
using AutoAssess.Data.BusinessObjects;

namespace AutoAssess.Data.PersistentObjects
{
	[Serializable]
	public class PersistentPackage : Package
	{
		public PersistentPackage ()
		{
		}
		
		public virtual Guid ID { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual DateTime CreatedOn { get;set;}
		
		public virtual DateTime LastModifiedOn { get; set; }
		
	}
}

