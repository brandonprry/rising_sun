using System;

namespace AutoAssess.Data.BusinessObjects
{
	public class DsxsToolOptions : IToolOptions
	{
		public DsxsToolOptions ()
		{
		}

		public virtual string HostIPAddressV4 { get; set; }
		
		public virtual int HostPort { get; set; }
		
		public virtual bool IsSSL { get; set; }
		
		public virtual bool UseRandomAgent { get; set; }
		
		public virtual string URL { get; set; }
		
		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data to be POSTed. Ignored for GET.
		/// If Data == string.Empty && Data == null, GET is assumed. Otherwise, POST is assumed.
		/// </value>
		public virtual string Data { get; set; }
		
		public virtual string Path { get; set; }
	}
}

