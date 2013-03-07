using System;
using System.Xml;
using System.Collections.Generic;
using AutoAssess.Data.BusinessObjects;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace wapiti_to_sqlmap_example
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Need a wapiti report to import");
				return;
			}
			
			string wapiti = System.IO.File.ReadAllText(args[0]);
			string host = string.Empty;
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(wapiti);
			
			var nodes = doc.SelectNodes("/report/bugTypeList/bugType");
			
			List<WapitiBug> bugs = new List<WapitiBug>();
			foreach (XmlNode node in nodes)
			{
				string bugType = node.Attributes["name"].Value;
				
				var bugElements = node.SelectNodes("bugList/bug");
				
				foreach (XmlNode bug in bugElements)
					bugs.Add(new WapitiBug(bugType, bug));
			}
			
			foreach (WapitiBug bug in bugs)
			{
				if (!bug.Type.Contains("SQL Injection"))
					continue;
				
				if (bug.URL.Contains(bug.Parameter))
				{
					//URL contains the parameters, most likely injection via GET verb
					
					//remove any offending data
					string url = bug.URL.Replace("%3Cscript%3Ealert%28%22tv25fmf889%22%29%3C%2Fscript%3E", "abcd");
					
					List<string> skippedParams = new List<string>();
					foreach (string param in Regex.Split(bug.Parameter, "&amp;"))
					{
						if (param.Contains("%3Cscript%3Ealert%28%22tv25fmf889%22%29%3C%2Fscript%3E"))
							continue;
						else
							skippedParams.Add(param.Split('=')[0]);
					}
					
					Console.WriteLine("Running GET XSS test on URL: " + bug.URL);
					
					string command = string.Empty;
					
					command = command + " -u " + url;
					command = command + " --smart";
					
					if (skippedParams.Count > 0)
						command = command + " --skipped=\"" + String.Join(",", skippedParams) + "\"";
					
					command = command + "  --technique=EUS --flush-session --fresh-queries --level=2 --batch";
					
					ProcessStartInfo si = new ProcessStartInfo();
					
					si.RedirectStandardOutput = true;
					si.UseShellExecute = false;
					
					Process proc = new Process();
					
					proc.StartInfo = si;
					proc.EnableRaisingEvents = false; 
					proc.StartInfo.FileName = "/home/bperry/tools/sqlmap/sqlmap.py";
					proc.StartInfo.Arguments = command;
					proc.Start();
					
					//string output = proc.StandardOutput.ReadToEnd();
					
				}
				else
				{
					//URL does not contain the parameters, most likely injection via POST verb
										
					//remove any offending data

					string url = bug.URL.Replace("%3Cscript%3Ealert%28%22tv25fmf889%22%29%3C%2Fscript%3E", "abcd");
					string data = bug.Parameter.Replace("%3Cscript%3Ealert%28%22tv25fmf889%22%29%3C%2Fscript%3E", "abcd");
					
					List<string> skippedParams = new List<string>();
					foreach (string param in Regex.Split(bug.Parameter, "&amp;"))
					{
						if (param.Contains("%3Cscript%3Ealert%28%22tv25fmf889%22%29%3C%2Fscript%3E"))
							continue;
						else
							skippedParams.Add(param.Split('=')[0]);
					}
					
					Console.WriteLine("Running POST SQL injection test on URL: " + bug.URL);
					
					string command = string.Empty;
					
					command = command + " -u " + url;
					command = command + " --smart";
					command = command + " --data=" + data;
					
					if (skippedParams.Count > 0)
						command = command + " --skipped=\"" + String.Join(",", skippedParams) + "\"";
					
					command = command + "  --technique=EUS --flush-session --fresh-queries --level=2 --batch";
					
					ProcessStartInfo si = new ProcessStartInfo();
					
					si.RedirectStandardOutput = true;
					si.UseShellExecute = false;
					
					Process proc = new Process();
					
					proc.StartInfo = si;
					proc.EnableRaisingEvents = false; 
					proc.StartInfo.FileName = "/home/bperry/tools/sqlmap/sqlmap.py";
					proc.StartInfo.Arguments = command;
					proc.Start();
					
					//string output = proc.StandardOutput.ReadToEnd();
					//Console.WriteLine(output);
				}
			}
		}
	}
}
