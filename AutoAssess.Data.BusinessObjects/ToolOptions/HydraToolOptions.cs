using System;
namespace AutoAssess.Data.BusinessObjects
{
	public class HydraToolOptions :ToolOptions,  IToolOptions
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AutoAssess.Data.HydraToolOptions"/> class.
		/// 
		/// </summary>
		public HydraToolOptions ()
		{
			
		}
		
		//derive port number from enum
		public HydraProtocol Protocol { get; set; }
		
		public string Host { get; set; }
		
		public string UsernameList { get; set; }
		
		public string PasswordList { get; set; }
		
		public int? Port { get; set; }
		
		public string Path { get; set; }
	}
	
	public enum HydraProtocol
	{
		AFP = 548,
		CVS = 2401,
		Firebird = 3050,
		FTP = 21,
		IMAP = 143,
		LDAP = 389,
		SecureLDAP = 636,
		MSSQL = 1433,
		NCP = 524,
		MySQL = 3306,
		NNTP = 119,
		OracleNetListener = 1521,
		POP3 = 110,
		SecurePOP3 = 995,
		PostgreSQL = 5432,
		RDP = 3389,
		RExec = 512,
		RLogin = 513,
		RSh = 514,
		SIP = 5060,
		SMB = 445,
		SMTP = 587,
		SNMP = 161,
		SOCKS5 = 27977,
		SSH = 22,
		Subversion = 3690,
		Telnet = 23,
		VNC = 5908
		
	}
}

