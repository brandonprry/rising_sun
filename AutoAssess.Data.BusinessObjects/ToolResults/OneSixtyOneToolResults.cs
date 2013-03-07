using System;
namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class OneSixtyOneToolResults : ToolResults,  IToolResults
	{
		public OneSixtyOneToolResults() {}
		
		public OneSixtyOneToolResults (string commandOutput)
		{
			this.FullOutput = commandOutput;
		}
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

