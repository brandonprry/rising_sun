using System;
using System.Net;

namespace AutoAssess.Data.BusinessObjects
{
	public class ARPScanToolResults : ToolResults, IToolResults
	{
		public ARPScanToolResults() {}
		
		public ARPScanToolResults (string commandOutput)
		{
			this.FullOutput = commandOutput;
		}
		
		public IPAddress Host { get; set; }
		
		public string MAC { get; set; }
		
		public string Description { get; set; }
		
		public string HostIPAddressV4 { get; set; }
		
		public int HostPort { get; set; }
		
		public bool IsTCP { get; set; }
		public bool IsUDP
		{
			get { return !IsTCP; }
			set { IsTCP = !value; }
		}
		
		
	}
}

