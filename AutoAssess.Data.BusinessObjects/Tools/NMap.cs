using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AutoAssess.Data;

namespace AutoAssess.Data.BusinessObjects
{
	public class NMap : ITool
	{
		NMapToolOptions _options;
		public NMap ()
		{
		}
		
		public NMap (IToolOptions options)
		{
			if (options is NMapToolOptions)
				_options = (NMapToolOptions)options;
		}
		
		public ScanLevel Level { get { return ScanLevel.First; }}
		
		public string Name { get { return "nmap"; }}
		public string Description { get { return string.Empty; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new NMapToolOptions();
				
				return _options;
			}
			set
			{
				if (value is NMapToolOptions)
					_options = (NMapToolOptions)value;	
			}
		}
		
		
		public IToolResults Run (Profile profile)
		{
			string cmd, output;
			
			cmd = "-sT -sU -p T:1-65535,U:53,67,68,123,135,137-138,161,445,631,1434,5353 -O " + 
				(!string.IsNullOrEmpty(_options.Range) ? _options.Range : _options.Host);
			
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
			
			NMapToolResults results = new NMapToolResults(output);
			
			return results;
		}
		
		
		public string DeepScan(string host, string port)
		{
			string cmd, output;
			
			cmd = "-sT -A -PN -p " + port + " " + host;
			
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
			
			return output;
		}
		
	}
}

