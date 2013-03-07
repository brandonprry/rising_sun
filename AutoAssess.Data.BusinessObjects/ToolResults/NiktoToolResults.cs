using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class NiktoToolResults : ToolResults,  IToolResults
	{
		public NiktoToolResults()
		{
		}
		
		public NiktoToolResults (string commandOutput)
		{
			this.FullOutput = commandOutput;
		}
		
		public virtual Guid HostPortID { get; set; }
		public virtual string HostIPAddressV4 { get; set; }
		
		public virtual int HostPort { get; set; }
		
		public virtual bool IsTCP { get; set; }
		
		public virtual bool IsUDP
		{
			get { return !IsTCP; }
			set { IsTCP = !value; }
		}
		
		public virtual IEnumerable<string> ParseOutput()
		{
			string[] tmp = Regex.Split(this.FullOutput, "---------------------------------------------------------------------------");
			
			foreach (string section in tmp)
			{
				foreach (string data in section.Split('\n'))
				{
					if (data.StartsWith("+ Start Time:"))
						continue;
					if (data.StartsWith("+ End Time:"))
						continue;
					if (data.StartsWith("+ No CGI Directories found"))
						continue;
					if (data.StartsWith("+ 1 host(s) tested"))
						continue;
					if (data.StartsWith("- Nikto v2.1.5"))
						continue;
					
					yield return data;
				}
			}

		}
	}
}

