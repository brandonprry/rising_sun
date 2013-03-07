using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class NBTScan : ITool
	{
		NBTScanToolOptions _options;
		
		public NBTScan ()
		{
		}
		
		public NBTScan(IToolOptions options)
		{
			if (options is NBTScanToolOptions)
				_options = (NBTScanToolOptions)options;
		}
		
		public ScanLevel Level { get { return ScanLevel.First; }}
		
		public string Name { get { return "nbtscan"; }}
		public string Description { get { return string.Empty; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new NBTScanToolOptions();
				
				return _options;
			}
			set
			{
				if (value is NBTScanToolOptions)
					_options = (NBTScanToolOptions)value;
			}
		}
		
		
		public IToolResults Run (Guid scanID, Guid userID)
		{			
			string cmd, output;
			
			cmd = "-r " + _options.Range;
			
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
			return new NBTScanToolResults(output);
		}
	}
}

