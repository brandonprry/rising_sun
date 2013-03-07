using System;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeServiceTest : NexposeTest
	{
		public NexposeServiceTest ()
		{
		}
		
		public NexposeServiceTest (XmlNode test) : base(test)
		{
			
		}
	}
}

