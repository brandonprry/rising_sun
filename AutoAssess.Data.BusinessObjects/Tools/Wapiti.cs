using System;
using System.Diagnostics;
using System.Threading;
using System.Xml;
namespace AutoAssess.Data.BusinessObjects
{
	public class Wapiti : ITool
	{
		WapitiToolOptions _options;
		public Wapiti (IToolOptions options)
		{
			if (options is WapitiToolOptions)
				_options = (WapitiToolOptions)options;
		}
		
		public string Name { get { return "wapiti"; }}
		public string Description { get { return string.Empty; }}
		
		public ScanLevel Level { get { return ScanLevel.Second; } }
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new WapitiToolOptions();
				
				return _options;
			}
			set
			{
				if (value is WapitiToolOptions)
					_options = (WapitiToolOptions)value;
			}
		}
		
		
		public IToolResults Run (TimeSpan longest)
		{
			string cmd;
			
			cmd = (_options.IsHTTPS ? "https://" : "http://") + _options.Host + ":" + _options.Port;
			
			Guid uid = Guid.NewGuid();
			cmd += " -m \"-blindsql\" -f xml -o \"/tmp/wapiti_" + uid.ToString() + ".xml\"";
			
			ProcessStartInfo si = new ProcessStartInfo();
			si.RedirectStandardOutput = true;
			si.UseShellExecute = false;
			
			Process proc = new Process();
			
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false; 
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = cmd;
			proc.Start();
			
			DateTime start = DateTime.Now;
			
			proc.StandardOutput.ReadToEnd();
			
			//while (!proc.HasExited)
			//{
			//	if ((DateTime.Now - start) > longest)
			//	{
			//		Console.WriteLine("Killing wapiti, taking too long");
			//		proc.Kill();
			//		return null;
			//	}
			//}
			
			WapitiToolResults results = WapitiToolResults.Parse("/tmp/wapiti_" + uid.ToString() + ".xml") as WapitiToolResults;
			results.FullOutput = System.IO.File.ReadAllText("/tmp/wapiti_" + uid.ToString() + ".xml");
			
			return results;
		}
	}
}

