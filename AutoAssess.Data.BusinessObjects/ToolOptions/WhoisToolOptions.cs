using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class WhoisToolOptions : ToolOptions, IToolOptions
	{
		
		public WhoisToolOptions ()
		{
		}
		
		public string Host { get; set; }
		
		public Guid NMapHostID { get; set; }
		
		public string Path { get; set; }
	}
}

