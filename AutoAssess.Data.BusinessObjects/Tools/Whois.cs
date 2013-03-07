using System;
using System.Diagnostics;
namespace AutoAssess.Data.BusinessObjects
{
	public class Whois : ITool
	{
		WhoisToolOptions _options;

		public Whois ()
		{
		}

		public Whois (IToolOptions options)
		{
			if (options is WhoisToolOptions)
				_options = (WhoisToolOptions)options;
		}

		public string Name {
			get { return "whois"; }
		}
		
		public ScanLevel Level { get { return ScanLevel.First; }}

		public string Description {
			get { return string.Empty; }
		}

		public IToolOptions Options {
			get {
				if (_options == null)
					_options = new WhoisToolOptions ();
				
				return _options;
			}
			set {
				if (value is WhoisToolOptions)
					_options = (WhoisToolOptions)value;
			}
		}

		public IToolResults Run ()
		{
			string cmd, output;
			
			cmd = _options.Host;
			
			ProcessStartInfo si = new ProcessStartInfo ();
			si.RedirectStandardOutput = true;
			si.UseShellExecute = false;
			
			Process proc = new Process ();
			
			proc.StartInfo = si;
			proc.EnableRaisingEvents = false;
			proc.StartInfo.FileName = _options.Path;
			proc.StartInfo.Arguments = cmd;
			proc.Start ();
			
			output = proc.StandardOutput.ReadToEnd ();
			
			proc.WaitForExit ();
			
			WhoisToolResults results = ParseOutput (output);
			
			return results;
		}

		private WhoisToolResults ParseOutput (string output)
		{
			//There is no standard way afaict to parse whois results, this will do for now.
			//Even though I could just do this up in Run(), putting it in it's own method now
			//will keep things simple later if I need to make changes and add more logic.
			WhoisToolResults results =  new WhoisToolResults(output);
			
			return results;
		}
	}
}

