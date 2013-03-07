using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class Winexe : ITool
	{
		WinexeToolOptions _options;
		
		public Winexe ()
		{
		}
		
		public Winexe (IToolOptions options)
		{
			if (options is WinexeToolOptions)
				_options = (WinexeToolOptions)options;
		}
		
		public string Name { get { return "winexe"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.Second; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new WinexeToolOptions();
				
				return _options;
			}
			set
			{
				if (value is WinexeToolOptions)
					_options = (WinexeToolOptions)value;
			}
		}
		
		
		public IToolResults Run (Guid scanID, Guid userID)
		{
			System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
			string cmd, output;
			
			cmd = "--uninstall -U \"" + _options.Username + "\"%\"" + enc.GetString(_options.Password) + "\" //" + _options.Host + "\"" + _options.RemotePath + "\"";
			
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
			return new WinexeToolResults(output);
		}
	}
}

