using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class NMapToolOptions : ToolOptions, IToolOptions
	{
		public NMapToolOptions ()
		{
		}
		
		public string Range { get; set; }
		
		public string Host { get; set; }
		
		public string Path { get; set; }
	}
}

