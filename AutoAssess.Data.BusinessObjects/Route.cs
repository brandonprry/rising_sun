using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class Route 
	{
		public Route ()
		{
		}
		public virtual TracerouteToolResults ParentResults { get; set; }
		
		public virtual int Hop { get; set; }
		
		public virtual string FirstIPAddress { get; set; }
		public virtual string FirstHostname { get; set; }
		public virtual string FirstResult { get; set; }
		
		public virtual string SecondIPAddress { get; set;}
		public virtual string SecondHostname {get; set; }
		public virtual string SecondResult { get; set; }
		
		public virtual string ThirdIPAddress { get; set; }
		public virtual string ThirdHostname { get; set; }
		public virtual string ThirdResult { get; set; }
	}
}

