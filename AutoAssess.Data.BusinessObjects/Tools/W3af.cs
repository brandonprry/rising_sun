using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class W3af : ITool
	{
		W3afToolOptions _options;
		
		public W3af ()
		{
		}
		
		public W3af(IToolOptions options)
		{
			if (options is W3afToolOptions)
				_options = (W3afToolOptions)options;
		}
		
		public string Name { get { return "w3af"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.Second; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new W3afToolOptions();
				
				return _options;
			}
			set 
			{
				if (value is W3afToolOptions)
					_options = (W3afToolOptions)value;
			}
		}
		
		
		public IToolResults Run (Guid scanID, Guid userID)
		{
			string cmd, output;
			
			cmd = "-s " + _options.ResourcePath;
			
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
			
			return new W3afToolResults(output);
		}
	}
}

