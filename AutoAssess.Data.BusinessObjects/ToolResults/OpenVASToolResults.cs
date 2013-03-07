using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.BusinessObjects
{
	public class OpenVASToolResults :  ToolResults, IToolResults
	{
		public OpenVASToolResults (string commandOutput)
		{
			this.FullOutput = commandOutput;
		}
		
		public Guid ReportID { get; set; }
		
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

