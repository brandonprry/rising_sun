using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class SMBClient : ITool
	{
		SMBClientToolOptions _options;
		public SMBClient ()
		{
		}
		
		public SMBClient (IToolOptions options)
		{
			if (options is SMBClientToolOptions)
				_options = (SMBClientToolOptions)options;
		}
		
		public string Name { get { return "smbclient"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.Second; } } 
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new SMBClientToolOptions();
				
				return _options;
			}
			set
			{
				if (value is SMBClientToolOptions)
					_options = (SMBClientToolOptions)value;
			}
		}
		
		
		public IToolResults Run ()
		{			
			string cmd, output;
			
			cmd = "-g -L " + _options.Host + " -U Administrator -N";
			
			ProcessStartInfo si = new ProcessStartInfo();
			si.RedirectStandardOutput = true;
			si.UseShellExecute = false;
			si.RedirectStandardError = true; //smbclient sends some good info to stderr
			
			Process proc = new Process();
			
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false; 
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = cmd;
			proc.Start();
			
			output = proc.StandardOutput.ReadToEnd();
			string errout = proc.StandardError.ReadToEnd();
			//parse output for shares
			
			proc.WaitForExit();
			
			SMBClientToolResults results = new SMBClientToolResults(errout + "\n\n\n" + output);

			
			if (_options.RecurseShares)
				results.RecurseShares(_options.Host);
			
			return results;
		}
	}
}

