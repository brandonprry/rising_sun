using System;
using System.Collections.Generic;
using System.Net;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class Client
	{	
		public Client ()
		{
		}
		
		public virtual string Name { get; set; }
		
		public virtual string LogoPath { get; set; }
		
		public virtual bool HasAPIAccess { get; set; }
		
		public virtual IList<User> Users { get; set; }

	}
}
