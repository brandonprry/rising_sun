
using System;

namespace AutoAssess.Data.BusinessObjects
{

	
	[Serializable]
	public class Assessment
	{
		public Assessment ()
		{
		}
		
		//public Guid AssessmentID { get { return this.ID; } set { this.ID = value; } }
		
		//public ZipFile Archive { get; set; }
		
		public DateTime DateOf { get; set; }
		
		public Client Client { get; set; }
	}
}
