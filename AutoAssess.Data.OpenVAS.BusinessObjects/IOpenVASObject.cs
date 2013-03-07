using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	public interface IOpenVASObject
	{
		List<IOpenVASObject> Parse(XmlDocument response);
	}
}

