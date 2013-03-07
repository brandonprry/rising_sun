using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AutoAssess.Data.BusinessObjects
{
	public class Traceroute : ITool
	{
		TracerouteToolOptions _options;
		
		public Traceroute ()
		{
		}
		
		public Traceroute(IToolOptions options)
		{
			_options = options as TracerouteToolOptions;
		}
		
		public string Name { get { return "traceroute"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.First; } }
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new TracerouteToolOptions();
				
				return _options;
			}
			set
			{
				if (value is TracerouteToolOptions)
					_options = (TracerouteToolOptions)value;
			}
		}
		
		
		public IToolResults Run ()
		{
			string cmd, output;
			
			cmd = _options.Host;
			
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
			
			TracerouteToolResults results = new TracerouteToolResults(output);
			
			return results;
		}
		
	}
}

