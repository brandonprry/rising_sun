using System;
using System.Diagnostics;
namespace AutoAssess.Data.BusinessObjects
{
	public class Nikto : ITool
	{
		
		NiktoToolOptions _options;
		
		public Nikto ()
		{
		}
		
		public Nikto(IToolOptions options)
		{
			if (options is NiktoToolOptions)
				_options = (NiktoToolOptions)options;
		}
		
		public string Name { get { return "nikto"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.Second; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new NiktoToolOptions();
				
				return _options;
			}
			set
			{
				if (value is NiktoToolOptions)
					_options = (NiktoToolOptions)value;
			}
		}
		
		
		public IToolResults Run ()
		{
			string cmd;
			
			cmd = "-h " + _options.Host + " -p " + _options.Port;
			
			if (_options.IsSSL)
				cmd = cmd + " -ssl";
			
			ProcessStartInfo si = new ProcessStartInfo();
			si.RedirectStandardOutput = true;
			si.UseShellExecute = false;
			
			Process proc = new Process();
			
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false; 
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = cmd;
			proc.Start();
			
			string output = proc.StandardOutput.ReadToEnd();
			
			proc.WaitForExit();
			
			NiktoToolResults results = new NiktoToolResults(output);
			
			return results;
		}
	}
}

