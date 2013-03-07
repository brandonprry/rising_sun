
using System;
using System.Collections.Generic;

namespace AutoAssess.Data.BusinessObjects
{


	public class OpenVASToolOptions :ToolOptions,  IToolOptions
	{

		public OpenVASToolOptions ()
		{
		}
		
		public string ReportType { get; set; }
		public string ReportPath { get; set; }
		public string Server { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string HostsPath { get; set; }
		public string ServerName { get; set; }
	}
}
