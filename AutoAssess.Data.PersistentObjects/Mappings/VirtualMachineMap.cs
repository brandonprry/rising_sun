using System;
using AutoAssess.Data.PersistentObjects;
using FluentNHibernate.Mapping;

namespace Mappings
{
	public class VirtualMachineMap : ClassMap<PersistentVirtualMachine>
	{
		public VirtualMachineMap ()
	{
			Table ("virtualmachines");
			
			Id(vm => vm.ID).Column("virtualmachineid")
				.GeneratedBy.Assigned();
			
			Map (vm => vm.Name).Column("name");
			Map (vm => vm.Guid).Column("guid");
			Map (vm => vm.Username).Column("username");
			Map (vm => vm.Password).Column("password");
			Map (vm => vm.IsActive).Column("isactive");
			Map (vm => vm.CreatedBy).Column("createdby");
			Map (vm => vm.CreatedOn).Column("createdon");
			Map (vm => vm.LastModifiedBy).Column("lastmodifiedby");
			Map (vm => vm.LastModifiedOn).Column("lastmodifiedon");
			
			References(vm => vm.ParentScan)
				.Column("scanid");
		}
	}
}

