using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class OneSixtyOneToolOptions : ToolOptions, IToolOptions
	{
		public OneSixtyOneToolOptions ()
		{
		}
		
		public string Host { get; set; }
		
		public string Path { get; set; }
	}
}

