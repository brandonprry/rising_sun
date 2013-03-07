using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class TracerouteToolOptions : ToolOptions, IToolOptions
	{
		public TracerouteToolOptions ()
		{
		}
		
		public string Host { get; set; }
		
		public Guid NMapHostID { get; set; }
		
		public string Path { get; set; }
		
	}
}

