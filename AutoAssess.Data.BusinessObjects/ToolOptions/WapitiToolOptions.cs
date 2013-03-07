using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class WapitiToolOptions : ToolOptions, IToolOptions
	{
		public WapitiToolOptions ()
		{
		}
		
		public string Host { get; set; }
		
		public bool IsHTTPS { get; set; }
		
		public int Port { get; set; }
		
		public string Path { get; set; }
	}
}

