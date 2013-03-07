using System;
using System.Collections.Generic;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeSite
	{
		public NexposeSite ()
		{
		}
		
		public List<NexposeScan> Scans { get; set; }
		
		public string Name { get; set; }
		
		public List<NexposeAsset> Assets { get; set; }
	}
}

