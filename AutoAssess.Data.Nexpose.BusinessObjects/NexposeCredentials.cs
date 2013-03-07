using System;
using System.Collections.Generic;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public class NexposeCredentials
	{
		public NexposeCredentials ()
		{
		}
		
		/// <summary>
		/// Gets or sets the credentialed objects.
		/// </summary>
		/// <value>
		/// The credentialed objects. Since credentials can be a public key, or a username and password
		/// or another variety of credentials, store the information in a dictionary and reference by key/value
		/// 
		/// Username
		/// Password
		/// Public Key
		/// </value>
		public Dictionary<string, string> CredentialedObjects { get; set; }
		
		public NexposeCredentialType CredentialType { get; set; }
	}
	
	public enum NexposeCredentialType
	{
		CVS  = 1,
		FTP  = 2,
		HTTP = 3,
		HTMLForm = 4,
		HTTPHeaders = 5,
		AS400 = 6,
		LotusNotes = 7,
		TDS = 8,
		Sybase = 9,
		CIFS = 10,
		Oracle = 11,
		MySQL = 12,
		DB2 = 13,
		PostgreSQL = 14,
		POP = 15,
		RExec = 16,
		SNMP = 17,
		SSH = 18,
		SSHPublicKey = 19,
		Telnet = 20
		
	}
}

