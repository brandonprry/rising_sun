using System;
using FluentNHibernate.Mapping;

namespace AutoAssess.Data.OpenVAS.PersistentObjects
{
	public class OpenVASConfigMap : ClassMap<PersistentOpenVASConfig>
	{
		public OpenVASConfigMap ()
		{
			Table("openvasconfig");
			
			Id(c => c.ID).Column("openvasconfigid")
				.GeneratedBy.Assigned();
			
			Map(c => c.CreatedBy).Column("createdby");
			Map(c => c.CreatedOn).Column("createdon");
			Map(c => c.IsActive).Column("isactive");
			Map(c => c.LastModifiedBy).Column("lastmodifiedby");
			Map(c => c.LastModifiedOn).Column("lastmodifiedon");
			Map(c => c.Name).Column("name");
			Map(c => c.RemoteConfigID).Column("remoteconfigid");
			Map(c => c.Comment).Column("comment");
			Map(c => c.FamilyCount).Column("familycount");
			Map(c => c.FamilyCountIsGrowing).Column("familyisgrowing");
			Map(c => c.NVTCount).Column("nvtcount");
			Map(c => c.NVTCountIsGrowing).Column("nvtisgrowing");
			Map(c => c.InUse).Column("inuse");
		}
	}
}

