using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class TcpDumpToolOptions : ToolOptions, IToolOptions
	{
		public TcpDumpToolOptions ()
		{
		}
		
		public string Host { get; set; }
		
		public string Interface { get; set ;}
		
		public string PCAPPath { get; set; }
		
		public string Path { get; set; }
	}
}

