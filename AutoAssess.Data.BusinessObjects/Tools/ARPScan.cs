	using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class ARPScan : ITool
	{
		ARPScanToolOptions _options;
		string _arpScanPath = "arp-scan"; //call from env...sane default
		
		public ARPScan ()
		{
		}
		
		public ARPScan(IToolOptions options)
		{
			if (options is ARPScanToolOptions)
				_options = (ARPScanToolOptions)options;
		}
		
		public string Name { get { return "arp-scan"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.First; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new ARPScanToolOptions();
				
				return _options;
			}
			set
			{
				if (value is ARPScanToolOptions)
					_options = (ARPScanToolOptions)value;
			}
		}
		
		public IToolResults Run (Guid scanID, Guid userID)
		{
			string cmd, output;
			
			cmd = _options.Range;
			
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
			
			return new ARPScanToolResults(output);
		}
	}
}

