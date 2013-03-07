using System;
using System.Xml;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeHostTest : NexposeTest
	{
		public NexposeHostTest ()
		{
		}
		
		public NexposeHostTest(XmlNode test) : base (test)
		{
		}
	}
}

