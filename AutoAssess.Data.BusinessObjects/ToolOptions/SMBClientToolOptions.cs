using System;
using System.Collections.Generic;

namespace AutoAssess.Data.BusinessObjects
{
	public class SMBClientToolOptions : ToolOptions, IToolOptions
	{
		public SMBClientToolOptions ()
		{
		}
		
		public string Host { get; set; }
		
		public bool RecurseShares { get; set; }
		
		public string Path { get; set; }
	}
}

