using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class MetasploitToolOptions :ToolOptions,  IToolOptions
	{
		public MetasploitToolOptions ()
		{
		}
		
		public string ResourceFilePath { get; set; }
		
		public string DatabaseDriver { get; set; }
		
		public string DatabaseAddress { get; set; }
		
		public string DatabaseUser { get; set; }
		
		public string DatabasePassword { get; set; }
		
		public string DatabaseName { get;set;}
		
		public string ClientName { get; set; }
		
		public string IPListPath { get; set; }
	
		public bool EnableNessus { get; set; }
		
		public string Path { get; set; }
	}
}
