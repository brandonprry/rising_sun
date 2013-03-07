using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class SQLMapVulnerability
	{
		public SQLMapVulnerability () {} //empty ctor
		
		
		public virtual SQLMapResults ParentResults { get; set; }
		
		public virtual string Target { get; set; }
		
		public virtual string Parameter { get; set; }
		
		public virtual string PayloadType { get; set; }
		
		public virtual string HTTPRequestType { get; set; }
		
		public virtual string Payload { get; set; }
		
		public virtual string Title { get; set; }
	}
}

