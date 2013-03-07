using System;
using System.Net;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class Website
	{
		public Website ()
		{
		}
		
		public string URL { get; set; }
		
		public IPAddress IPv4Address { get; set; }
	}
}

