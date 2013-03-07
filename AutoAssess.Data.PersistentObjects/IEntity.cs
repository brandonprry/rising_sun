using System;

namespace AutoAssess.Data.PersistentObjects
{
	public interface IEntity
	{
		Guid ID { get; set; }
		
		PersistentUser User { get; set; }
		
		bool IsActive { get; set; }
		
		DateTime CreatedOn { get; set; }
		
		Guid CreatedBy { get; set; }
		
		DateTime LastModifiedOn { get; set; }
		
		Guid LastModifiedBy { get; set; }
		
		void SetCreationInfo(Guid userID);
		
		void SetUpdateInfo(Guid userID, bool isActive);
		
		string ToPersistentXml();
	}
}

