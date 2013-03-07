using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class SSLScanToolOptions : ToolOptions, IToolOptions
	{
		public SSLScanToolOptions ()
		{
		}
		
		public string Host { get; set; }
		
		public int Port { get; set; }
		
		public string Path { get; set; }
	}
}

