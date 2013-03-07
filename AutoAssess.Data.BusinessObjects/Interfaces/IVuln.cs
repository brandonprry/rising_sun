using System;

namespace AutoAssess.Data.BusinessObjects
{
	public interface IVuln
	{
		string CVE { get; set; }
		string Assesser { get; set; }
	}
}

