using System;
using System.Diagnostics;

namespace AutoAssess.Data.BusinessObjects
{
	public class Hydra : ITool
	{
		HydraToolOptions _options;
		
		public Hydra ()
		{
		}
		
		public Hydra (IToolOptions options)
		{
			if (options is HydraToolOptions)
				_options = (HydraToolOptions)options;
		}
		
		public string Name { get { return "hydra"; } }

		public string Description { get { return string.Empty; } }
		
		public ScanLevel Level { get { return ScanLevel.Second; } }
		
		public IToolOptions Options { 
			get {
				if (_options == null)
					_options = new HydraToolOptions ();
				
				return _options;
			}
			set {
				if (value is HydraToolOptions)
					_options = (HydraToolOptions)value;
			}
		}
		
		public IToolResults Run (Guid scanID, Guid userID)
		{
			string cmd, output;
			
			cmd = "-L " + _options.UsernameList + " -P " + _options.PasswordList + " -s " + 
				(_options.Port.HasValue ? _options.Port.Value : (int)_options.Protocol) + " " + _options.Host + " ";
			
			switch (_options.Protocol) {
			case HydraProtocol.AFP:
				cmd = cmd + "afp";
				break;
			case HydraProtocol.CVS:
				cmd = cmd + "cvs";
				break;
			case HydraProtocol.Firebird:
				cmd = cmd + "firebird";
				break;
			case HydraProtocol.FTP:
				cmd = cmd + "ftp";
				break;
			case HydraProtocol.IMAP:
				cmd = cmd + "imap";
				break;
			case HydraProtocol.LDAP:
				cmd = cmd + "ldap";
				break;
			case HydraProtocol.MSSQL:
				cmd = cmd + "mssql";
				break;
			case HydraProtocol.MySQL:
				cmd = cmd + "mysql";
				break;
			case HydraProtocol.NCP:
				cmd = cmd + "ncp";
				break;
			case HydraProtocol.NNTP:
				cmd = cmd + "nntp";
				break;
			case HydraProtocol.POP3:
				cmd = cmd + "pop3";
				break;
			case HydraProtocol.RDP:
				cmd = cmd + "rdp";
				break;
			case HydraProtocol.RExec:
				cmd = cmd + "rexec";
				break;
			case HydraProtocol.RLogin:
				cmd = cmd + "rlogin";
				break;
			case HydraProtocol.RSh:
				cmd = cmd + "rsh";
				break;
			case HydraProtocol.SIP:
				cmd = cmd + "sip";
				break;
			case HydraProtocol.SMB:
				cmd = cmd + "smb";
				break;
			case HydraProtocol.SMTP:
				cmd = cmd + "smtp";
				break;
			case HydraProtocol.PostgreSQL:
				cmd = cmd + "postgres";
				break;
			case HydraProtocol.SNMP:
				cmd = cmd + "snmp";
				break;
			case HydraProtocol.SOCKS5:
				cmd = cmd + "socks5";
				break;
			case HydraProtocol.SSH:
				cmd = cmd + "ssh2";
				break;
			case HydraProtocol.Subversion:
				cmd = cmd + "svn";
				break;
			case HydraProtocol.Telnet:
				cmd = cmd + "telnet";
				break;
			case HydraProtocol.VNC:
				cmd = cmd + "vnc";
				break;
			}
			
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
			
			return new HydraToolResults (output);
		}
	}
}

