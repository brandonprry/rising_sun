using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class ARPScanToolOptions :ToolOptions,  IToolOptions
	{
		public ARPScanToolOptions ()
		{
		}
		
		public string Range { get; set; }
		
		public string Path { get; set; }
	}
}

