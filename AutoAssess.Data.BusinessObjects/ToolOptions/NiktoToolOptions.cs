using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class NiktoToolOptions : ToolOptions, IToolOptions
	{
		public NiktoToolOptions ()
		{
		}
		
		public string Host { get; set; }
		
		public int Port { get; set; }
		
		public string ReportPath { get; set; }
		
		public bool IsSSL { get; set; }
		
		public string Path { get; set; }
	}
}

