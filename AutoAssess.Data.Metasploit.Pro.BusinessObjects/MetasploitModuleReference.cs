using System;

namespace AutoAssess.Data.Metasploit.Pro.BusinessObjects
{
	[Serializable]
	public class MetasploitModuleReference
	{
		public MetasploitModuleReference ()
		{
			
		}
		
		public virtual int RemoteID { get; set; }
		public virtual int RemoteModuleDetailID { get; set; }
		public virtual string Name { get; set; }
	}
}

