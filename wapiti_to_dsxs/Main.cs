using System;
using System.Xml;
using AutoAssess.Data.BusinessObjects;
using System.Collections.Generic;
using System.Diagnostics;

namespace wapiti_to_dsxs
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string dsxsPath = "/home/bperry/tools/dsxs/dsxs.py";
			string wapitiReport = "/tmp/wapiti_55fdd419-800a-4ba1-b591-5f2d6001a72f.xml";
			string xml = System.IO.File.ReadAllText(wapitiReport);
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			
			var nodes = doc.SelectNodes("/report/bugTypeList/bugType");
			
			List<WapitiBug> bugs = new List<WapitiBug>();
			foreach (XmlNode node in nodes)
			{
				string bugType = node.Attributes["name"].Value;
				XmlNodeList bugElements = node.SelectNodes("bugList/bug");
				
				foreach (XmlNode bug in bugElements)
					bugs.Add(new WapitiBug(bugType, bug));
			}
			
			foreach (WapitiBug bug in bugs)
			{
				
				if (bug.Type != "Cross Site Scripting")
					continue;
				
				ProcessStartInfo si = new ProcessStartInfo();
				
				si.RedirectStandardOutput = true;
				si.UseShellExecute = false;
				
				Process proc = new Process();
				
				proc.StartInfo = si;
				proc.EnableRaisingEvents = false; 
				proc.StartInfo.FileName = dsxsPath;
				
				string url = bug.URL.Replace("%3Cscript%3Ealert%28%22tv25fmf889%22%29%3C%2Fscript%3E", "abcd");
				string command = string.Empty;
				
				command = "-u \"" + url + "\" --random-agent";
				
				if (bug.URL.Contains(bug.Parameter))
				{
					//the XSS is in a GET request
					proc.StartInfo.Arguments = command;
					Console.WriteLine ("Performing GET XSS test on URL: " + url);
				}
				else
				{
					//the XSS is in a POST request
					string data = bug.Parameter.Replace("%3Cscript%3Ealert%28%22tv25fmf889%22%29%3C%2Fscript%3E", "abcd");
					command = command + " --data=\"" + data + "\"";
					proc.StartInfo.Arguments = command;
					Console.WriteLine ("Performing POST XSS test on URL: " + url);
				}
			
				proc.Start();
				
				string output = proc.StandardOutput.ReadToEnd();
				Console.WriteLine(output);
			}
		}
	}
}
