using System;
using System.Xml;
using AutoAssess.Data.OpenVAS.BusinessObjects;

using System.Collections.Generic;

namespace openvas_examples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string xml = System.IO.File.ReadAllText("/home/bperry/openvas_report.xml");
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			
			var hosts = OpenVASReportParser.ParseHostsFromReport(doc);
			
			Console.WriteLine(hosts.Count);
		}
	}
}
