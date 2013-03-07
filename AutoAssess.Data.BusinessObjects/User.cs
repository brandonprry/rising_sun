using System;
namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class User 
	{
		public User ()
		{
		}
		
		public virtual string FullName { get; set; }
		
		public virtual string Username { get; set; }
		
		public virtual int UserLevel { get; set;}
		
		public virtual string ToBusinessXml ()
		{
			return string.Empty;
		}
		
	}
}

