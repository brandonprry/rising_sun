using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class Dsxs //: ITool
	{
		DsxsToolOptions _options;
		
		public Dsxs(IToolOptions options)
		{
			_options = options as DsxsToolOptions;
		}
		
		public IToolResults Run(WapitiBug bug)
		{
			if (bug.Type != "Cross Site Scripting")
				return null;
			
			DsxsToolResults results;
			ProcessStartInfo si = new ProcessStartInfo();
			
			si.RedirectStandardOutput = true;
			si.UseShellExecute = false;
			
			Process proc = new Process();
			
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false; 
			proc.StartInfo.FileName = _options.Path;
			
			string url = bug.URL.Replace("%3Cscript%3Ealert%28%22tv25fmf889%22%29%3C%2Fscript%3E", "abcd");
			string command = string.Empty;
			
			command = "-u \"" + url + "\" --random-agent"; //always use a random agent.
			
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
			results = new DsxsToolResults(output);
			
			results.HostIPAddressV4 = bug.Host;
			results.HostPort = bug.Port;
			return results as IToolResults;
		}
	}
}

