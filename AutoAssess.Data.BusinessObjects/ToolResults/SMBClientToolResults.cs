using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoAssess.Data;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class SMBClientToolResults :  ToolResults, IToolResults
	{
		IList<string> _shares;
		
		public SMBClientToolResults ()
		{}
		
		public SMBClientToolResults (string commandOutput)
		{
			this.FullOutput = commandOutput;
			
			this.ParseOutput(commandOutput);
		}
		
		
		public virtual int HostPort { get; set; }
		
		public virtual bool IsTCP { get; set; }
		
		public virtual bool IsUDP
		{
			get { return !IsTCP; }
			set { IsTCP = !value; }
		}
		
		public virtual string HostIPAddressV4 { get; set; }
		public virtual Port ParentPort { get; set; }
		
		public virtual IList<ShareDetails> ShareDetails
		{
			get; set; 
		}
		
		private void ParseOutput(string output)
		{
			_shares = new List<string>();
			
			foreach (string line in output.Split('\n'))
			{
				if (line.StartsWith("Disk|"))
				{
					string sharename = line.Split('|')[1];
					
					if (!sharename.EndsWith("$"))
						_shares.Add(sharename);
				}
			}
		}
		
		public virtual void RecurseShares(string host)
		{
			this.ShareDetails = new List<ShareDetails>();
			
			foreach (string share in _shares)
			{
				ProcessStartInfo si = new ProcessStartInfo();
				si.RedirectStandardOutput = true;
				si.UseShellExecute = false;
				si.RedirectStandardError = false; 
				
				Process proc = new Process();
				
				proc.StartInfo = si;
				proc.EnableRaisingEvents = false; 
				proc.StartInfo.FileName = "smbclient";
				
				string cmd = @"'\\\\" + host + @"\\" + share + @"' -U Administrator -N -c 'recurse;ls'";
				
				proc.StartInfo.Arguments = cmd;
				proc.Start();
				
				string output = proc.StandardOutput.ReadToEnd();
				
				proc.WaitForExit();
				
				ShareDetails details = new ShareDetails();
				
				details.ParentResults = this;
				details.ShareName = share;
				details.FilesOnShare = output;
				
				this.ShareDetails.Add(details);
			}
		}
	}
}

