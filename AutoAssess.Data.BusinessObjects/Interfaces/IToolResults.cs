using System;
namespace AutoAssess.Data.BusinessObjects
{
	public interface IToolResults
	{
		string FullOutput { get; set; }
		
		string HostIPAddressV4 { get; set; }
		
		int HostPort { get; set; }
		
		bool IsTCP { get; set; }
		
		bool IsUDP { get; set; }
	}
}

