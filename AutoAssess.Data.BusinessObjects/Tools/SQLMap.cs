using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoAssess.Data.BusinessObjects
{
	public class SQLMap : ITool
	{
		SQLMapOptions _options;
		string _path = null;
		public SQLMap (IToolOptions options)
		{
			_options = options as SQLMapOptions;
		}
		
		
		
		public string Name { get { return "sqlmap"; }}
		
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.Second; }}
		
		public IToolOptions Options
		{
			get 
			{
				if (_options == null)
					_options = new SQLMapOptions();
				
				return _options;
			}
			set
			{
				if (value is SQLMapOptions)
					_options = value as SQLMapOptions;
				else
					throw new Exception("wrong options type");
			}
		}
		
		public IToolResults Run(WapitiBug bug)
		{
			string bugType = bug.Type;
			if (!bugType.StartsWith("SQL Injection"))
				return null;
							
			ProcessStartInfo si = new ProcessStartInfo();
			si.RedirectStandardOutput = true;
			si.UseShellExecute = false;
			
			Process proc = new Process();
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false;
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = "--purge-output";
			proc.Start();
				
			string output = proc.StandardOutput.ReadToEnd();
			
			string url = bug.URL;
			
			if (url.Contains(bug.Parameter))
			{
				//URL contains the parameters, most likely injection via GET verb
				
				//remove any offending data
				url = url.Replace("%BF%27%22%28", "abcd").Replace("%27+or+benchmark%2810000000%2CMD5%281%29%29%23", "abcd");
				
				List<string> skippedParams = new List<string>();
				foreach (string param in Regex.Split(bug.Parameter, "&"))
				{
					if (param.Contains("%BF%27%22%28") || param.Contains("or+benchmark"))
						continue;
					else
						skippedParams.Add(param.Split('=')[0]);
				}
				
				Console.WriteLine("Running GET SQL injection test on URL: " + bug.URL);
				
				string command = string.Empty;
				
				string host = url.Split('/')[2].Split(':')[0];
				
				command = " --disable-coloring -u \"" + url + "\" -o --fresh-queries --random-agent --flush-session --smart --batch";
				
				if (skippedParams.Count > 0)
					command = command + " --skip=\"" + String.Join(",", skippedParams) + "\"";
				
				command += (!string.IsNullOrEmpty(_options.DBMS) ? " --dbms=" + _options.DBMS : string.Empty);
				command += (_options.Level.HasValue ? " --level=" + _options.Level.Value.ToString() : string.Empty);
				command += (_options.Risk.HasValue ? " --risk=" + _options.Risk.Value : string.Empty);
				//command += (_options.TestForms ? " --forms" : string.Empty);

				proc = new Process();
				
				proc.StartInfo = si;
				proc.EnableRaisingEvents = false; 
				proc.StartInfo.FileName = _options.Path;
				proc.StartInfo.Arguments = command;
				proc.Start();
				
				output = proc.StandardOutput.ReadToEnd();				
				
				SQLMapResults results = new SQLMapResults(output, host);
				
				//this is a hack
				proc = new Process();
				proc.StartInfo = si;
				proc.EnableRaisingEvents = false;
				proc.StartInfo.FileName = _options.Path;
				proc.StartInfo.Arguments = "--purge-output";
				proc.Start();
				
				output = proc.StandardOutput.ReadToEnd();
				
				return results;
			}
			else
			{
				//URL does not contain the parameters, most likely injection via POST verb
									
				//remove any offending data
				url = url.Replace("%BF%27%22%28", "abcd").Replace("%27+or+benchmark%2810000000%2CMD5%281%29%29%23", "abcd");
				string data = bug.Parameter.Replace("%BF%27%22%28", "abcd").Replace("%27+or+benchmark%2810000000%2CMD5%281%29%29%23", "abcd");
				
				List<string> skippedParams = new List<string>();
				foreach (string param in Regex.Split(bug.Parameter, "&"))
				{
					if (param.Contains("%BF%27%22%28") || param.Contains("or+benchmark"))
						continue;
					else
						skippedParams.Add(param.Split('=')[0]);
				}
				
				Console.WriteLine("Running POST SQL injection test on URL: " + bug.URL);
				
				string host = url.Split('/')[2].Split(':')[0];
				string command = string.Empty;
				
				command = " -u \"" + url + "\" -o --fresh-queries --random-agent --flush-session --smart --batch";
				
				command += " --data=\"" + data + "\"";
				
				if (skippedParams.Count > 0)
					command = command + " --skip=\"" + String.Join(",", skippedParams) + "\"";
				
				command += (!string.IsNullOrEmpty(_options.DBMS) ? " --dbms=" + _options.DBMS : string.Empty);
				command += (_options.Level.HasValue ? " --level=" + _options.Level.Value.ToString() : string.Empty);
				command += (_options.Risk.HasValue ? " --risk=" + _options.Risk.Value : string.Empty);
				
				si = new ProcessStartInfo();
				
				si.RedirectStandardOutput = true;
				si.UseShellExecute = false;
				
				proc = new Process();
				
				proc.StartInfo = si;
				proc.EnableRaisingEvents = false; 
				proc.StartInfo.FileName = _options.Path;
				proc.StartInfo.Arguments = command;
				proc.Start();
				
				output = proc.StandardOutput.ReadToEnd();
				
				SQLMapResults results = new SQLMapResults(output, host);
				
				//this is a hack
				proc = new Process();
				proc.StartInfo = si;
				proc.EnableRaisingEvents = false;
				proc.StartInfo.FileName = _options.Path;
				proc.StartInfo.Arguments = "--purge-output";
				proc.Start();
				
				output = proc.StandardOutput.ReadToEnd();
				
				return results;
			}
		}
		
		
		public IToolResults Run()
		{
			string cmd, output;
			
			cmd = " -u \"" + _options.URL + ":" + _options.Port + "\" -o --fresh-queries --random-agent --flush-session --smart --batch --crawl=" + _options.CrawlLevel.ToString();
			
			cmd += (!string.IsNullOrEmpty(_options.DBMS) ? " --dbms=" + _options.DBMS : string.Empty);
			cmd += (_options.Level.HasValue ? " --level=" + _options.Level.Value.ToString() : string.Empty);
			cmd += (_options.Risk.HasValue ? " --risk=" + _options.Risk.Value : string.Empty);
			cmd += (_options.TestForms ? " --forms" : string.Empty);
			
			ProcessStartInfo si = new ProcessStartInfo();
			si.RedirectStandardOutput = true;
			si.UseShellExecute = false;
			
			Process proc = new Process();
			
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false; 
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = cmd;
			proc.Start();
			
			output = proc.StandardOutput.ReadToEnd();
			
			proc.WaitForExit();
			
			SQLMapResults results = new SQLMapResults(output, _options.URL);
			
			//this is a hack
			proc = new Process();
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false;
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = "--purge-output";
			proc.Start();
			
			return results;
		}
		
	}
}