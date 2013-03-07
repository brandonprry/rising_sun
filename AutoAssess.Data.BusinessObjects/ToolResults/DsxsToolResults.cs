using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AutoAssess.Data.BusinessObjects
{
	public class DsxsToolResults : IToolResults
	{
		public DsxsToolResults ()
		{
		}
		
		public DsxsToolResults(string output)
		{
			this.FullOutput = output;
			
			foreach (string line in output.Split('\n'))
			{
				if (line.StartsWith(" (i) "))
				{
					string[] tmp = Regex.Split(Regex.Split(line, "parameter")[1], "appears");
					string param = tmp[0];
					string technique = Regex.Split (tmp[1], "vulnerable")[1];
					
					DsxsItem item = new DsxsItem();
					item.Parameter = param;
					item.PossibleTechnique = technique;
					
					if (this.Items == null)
						this.Items = new List<DsxsItem>();
					
					this.Items.Add(item);
				}
			}
		}

		#region IToolResults implementation
		public virtual string FullOutput {get; set; }
		
		public virtual string HostIPAddressV4 {get; set; }

		public virtual int HostPort {get; set; }
		
		public virtual List<DsxsItem> Items { get; set; }
		
		public virtual bool IsTCP {get; set; }

		public virtual bool IsUDP {
			get { return !this.IsTCP; } 
			set { this.IsTCP = !value; }
		}
		#endregion
		
		public virtual void ParseItems(string output)
		{
			
		}
	}
}

