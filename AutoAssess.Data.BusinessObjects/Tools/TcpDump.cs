using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class TcpDump : ITool
	{
		TcpDumpToolOptions _options;
		
		public TcpDump ()
		{
		}
		
		public TcpDump(IToolOptions options)
		{
			if (options is TcpDumpToolOptions)
				_options = (TcpDumpToolOptions)options;
		}
		
		public ScanLevel Level { get { return ScanLevel.None; }}
		
		public string Name { get { return "tcpdump"; }}
		public string Description { get { return string.Empty; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new TcpDumpToolOptions();
				
				return _options;
			}
			set
			{
				 if (value is TcpDumpToolOptions)
					_options = (TcpDumpToolOptions)value;
			}
		}
		
		
		public IToolResults Run (Guid scanID, Guid userID)
		{
			string cmd;
			
			cmd = "-i " + _options.Host + " -w " + _options.PCAPPath;
			
			Process proc = new Process();
			
			proc.EnableRaisingEvents = false; 
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = cmd;
			proc.Start();
			
			string output = proc.StandardOutput.ReadToEnd();
			
			proc.WaitForExit();
			
			return new TcpDumpToolResults(output);
		}
	}
}

