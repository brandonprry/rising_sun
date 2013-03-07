using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.PersistentObjects.PersistentObjects
{
	public class ShareDetailsMap : ClassMap<PersistentShareDetails>
	{
		public ShareDetailsMap ()
		{
			Table("smbresultchild");
			
			Id(c => c.ID).Column("smbresultchildid").GeneratedBy.Assigned();
			
			Map(c => c.CreatedBy).Column("createdby");
			Map(c => c.CreatedOn).Column("createdon");
			Map(c => c.FilesOnShare).Column("filelist");
			Map(c => c.IsActive).Column("isactive");
			Map(c => c.LastModifiedBy).Column("lastmodifiedby");
			Map(c => c.LastModifiedOn).Column("lastmodifiedon");
			Map(c => c.ShareName).Column("sharename");
			
			References(c => c.ParentProfile)
				.Column("profileid");
			
			References(c => c.User)
				.Column("userid");
			
			References(c => c.ParentResults)
				.Column("smbresultid");
		}
	}
}

