using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class SQLMapResults : ToolResults, IToolResults
	{
		public SQLMapResults()
		{}
		
		public SQLMapResults (string output, string host)
		{
			this.FullOutput = output;
			this.HostIPAddressV4 = host;
			ParseLog();
			
		}
		
		public virtual string Log { get; set; }
		
		public virtual string HostIPAddressV4 { get; set; }
		
		public virtual Port ParentHostPort { get; set; }
		
		public virtual int HostPort { get; set; }
		
		public virtual bool IsTCP { get; set; }
		
		public virtual bool IsUDP
		{
			get { return !IsTCP; }
			set { IsTCP = !value; }
		}
		
	
		public virtual ICollection<SQLMapVulnerability> Vulnerabilities { get; set; }
		
		private void ParseLog()
		{
			string logPath = "/home/bperry/tools/sqlmap/output/" + this.HostIPAddressV4 + "/log";
			string log = System.IO.File.ReadAllText(logPath);
			string[] results = Regex.Split(log, "---");
			
			this.Log = log;
			
			if (results.Length == 1)
				return;
			
			//---
			//Place: GET
			//Parameter: searchquery
			//    Type: UNION query
			//    Title: MySQL UNION query (NULL) - 4 columns
			//    Payload: action=search&searchquery=abcd' LIMIT 1,1 UNION ALL SELECT CONCAT(0x3a626b763a,0x66726b4c574566415773,0x3a74786e3a), NULL, NULL, NULL#
			//---
			
			this.Vulnerabilities = new List<SQLMapVulnerability>();
			
			foreach (string result in results.Skip(1))
			{
				if (string.IsNullOrWhiteSpace(result))
				    continue;
				    
				SQLMapVulnerability vuln  = new SQLMapVulnerability();
				
				foreach (string line in result.Split('\n'))
				{
					string l = line.Trim();
					
					if (l.StartsWith("Place"))
					{
						vuln.HTTPRequestType = l.Split(':')[1].Trim();
					}
					else if (l.StartsWith("Parameter"))
					{
						vuln.Parameter = l.Split(':')[1].Trim();
					}
					else if (l.StartsWith("Type"))
					{
						vuln.PayloadType  = l.Split(':')[1].Trim();
					}
					else if (l.StartsWith("Title"))
					{
						vuln.Title = l.Split(':')[1].Trim();
					}
					else if (l.StartsWith ("Payload"))
					{
						vuln.Payload  = l.Split(':')[1].Trim();
					}
				}
				
				this.Vulnerabilities.Add(vuln);
			}
		}
	}
}

