using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class NBTScanToolOptions : ToolOptions, IToolOptions
	{
		public NBTScanToolOptions ()
		{
		}
		
		public string Range { get; set;}
		
		public string Path { get; set; }
	}
}

