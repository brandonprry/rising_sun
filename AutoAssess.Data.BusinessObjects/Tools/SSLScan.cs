using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AutoAssess.Data.BusinessObjects
{
	public class SSLScan : ITool
	{
		SSLScanToolOptions _options;
		
		public SSLScan ()
		{
		}
		
		public SSLScan (IToolOptions options)
		{
			if (options is SSLScanToolOptions)
				_options = (SSLScanToolOptions)options;
		}
		
		public string Name { get { return "sslscan"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.Second; } }
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new SSLScanToolOptions();
				
				return _options;
			}
			set
			{
				if (value is SSLScanToolOptions)
					_options = (SSLScanToolOptions)value;
			}
		}
			
		
		public IToolResults Run ()
		{
			string cmd, output;
			
			cmd = "-w 10 -dd " + _options.Host + ":" + _options.Port;
			
			ProcessStartInfo si = new ProcessStartInfo();
			si.RedirectStandardOutput = true;
			si.UseShellExecute = false;
			
			Process proc = new Process();
			
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false; 
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = cmd;
			proc.Start();
			
			proc.WaitForExit();
		
			output = proc.StandardOutput.ReadToEnd();
			
			SSLScanToolResults results = new SSLScanToolResults(output);
			
			return results;
		}
	}
}

