using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class WinexeToolOptions :ToolOptions, IToolOptions
	{
		public WinexeToolOptions ()
		{
		}
		
		public string Domain { get; set; }
		
		public string Username { get; set; }
		
		public byte[] Password { get; set; }
		
		public string Host { get; set; }
		
		public string RemotePath { get; set; }
		
		public string Path { get; set; }
	}
}

