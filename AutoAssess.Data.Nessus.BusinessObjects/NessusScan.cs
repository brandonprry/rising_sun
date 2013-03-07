using System;
using System.Xml;
using AutoAssess.Data;
using System.Collections.Generic;

namespace AutoAssess.Data.Nessus.BusinessObjects
{
	[Serializable]
	public class NessusScan 
	{
		public NessusScan ()
		{
		}
		
		public NessusScan(XmlNode scan)
		{
			this.Hosts = new List<NessusReportHost>();
			foreach (XmlNode child in scan.LastChild.ChildNodes)
			{
				if (child.Name == "ReportHost")
					this.Hosts.Add(new NessusReportHost(child));
			}
		}
		
		public virtual string Name { get; set; }
		
		public virtual string Range { get; set; }
		
		public virtual string RemoteScanID { get; set; }
		
		public virtual DateTime StartTime { get; set; }
		
		public virtual string Owner { get; set; }
		
		public virtual NessusReport Report { get; set; }
		
		public virtual int UniqueReportNumber { get; set; }
		
		public virtual IList<NessusReportHost> Hosts { get; set; }
		
		public virtual void ParseReportForHosts(XmlNode scan)
		{	
			this.Hosts = new List<NessusReportHost>();
			foreach (XmlNode child in scan.LastChild.FirstChild.ChildNodes)
			{
				if (child.Name == "ReportHost")
					this.Hosts.Add(new NessusReportHost(child));
			}
		}
	}
}

