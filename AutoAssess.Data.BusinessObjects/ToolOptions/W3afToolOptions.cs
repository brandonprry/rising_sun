using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class W3afToolOptions :ToolOptions,  IToolOptions
	{
		public W3afToolOptions ()
		{
		}
		
		public string ResourcePath { get; set; }
		
		public string Path { get; set; }
	}
}

