using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class OneSixtyOne : ITool
	{
		OneSixtyOneToolOptions _options;
		
		public OneSixtyOne ()
		{
		}
		
		public OneSixtyOne(IToolOptions options)
		{
			if (options is OneSixtyOneToolOptions)
				_options = (OneSixtyOneToolOptions)options;
		}
		
		public ScanLevel Level { get { return ScanLevel.Second; } }
		
		public string Name { get { return "onesixtyone"; }}
		public string Description { get { return string.Empty; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new OneSixtyOneToolOptions();
				
				return _options;
			}
			set
			{
				if (value is OneSixtyOneToolOptions)
					_options = (OneSixtyOneToolOptions)value;
			}
		}
		
		
		public IToolResults Run ()
		{			
			string cmd, output;
			
			cmd = "-w 10 -dd " +  _options.Host;
			
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
			
			OneSixtyOneToolResults results = new OneSixtyOneToolResults(output);
			
			return results;
		}
	}
}

