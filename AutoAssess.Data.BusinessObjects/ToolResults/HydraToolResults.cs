using System;
using System.Collections;

namespace AutoAssess.Data.BusinessObjects
{
	public class HydraToolResults : ToolResults,  IToolResults
	{
		public HydraToolResults() {}
		
		public HydraToolResults (string commandOutput)
		{
			this.FullOutput = commandOutput;
		}
		
		public virtual string[] Credentials { get; private set; }
		public virtual Guid HostPortID { get; set; }
		public virtual string HostIPAddressV4 { get; set; }
		
		
		public virtual int HostPort { get; set; }
		
		public virtual bool IsTCP { get; set; }
		
		public virtual bool IsUDP
		{
			get { return !IsTCP; }
			set { IsTCP = !value; }
		}
		
		
	}
}

