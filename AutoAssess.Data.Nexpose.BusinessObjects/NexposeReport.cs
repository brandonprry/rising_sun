using System;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeReport
	{
		public NexposeReport ()
		{
		}
		
		public NexposeReportType NexposeReportType { get; set; }
		
		public string Base64EncodedReport { get; set; }
	}
	
	public enum NexposeReportType
	{
		XML  = 1,
		CSV  = 2,
		PDF  = 3,
		HTML = 4,
		RTF  = 5,
		Text = 6
	}
}

