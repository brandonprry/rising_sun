
using System;

namespace AutoAssess.Data.BusinessObjects
{
	public interface ITool
	{
		string Name { get; }
		string Description { get; }
		
		ScanLevel Level { get; }
		
		IToolOptions Options { get; set; }
		
		//IToolResults Run(Guid scanID, Guid userID);
	}
	
	public enum ScanLevel 
	{
		None = 0,
		First = 1,
		Second = 2,
		Third = 3,
		Fourth = 4,
	}
		
}
