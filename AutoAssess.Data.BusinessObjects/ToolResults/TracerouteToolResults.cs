using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoAssess.Data.BusinessObjects
{
	public class TracerouteToolResults : ToolResults,  IToolResults
	{
		
		public TracerouteToolResults()
		{}
		
		public TracerouteToolResults (string commandOutput)
		{
			this.FullOutput = commandOutput;
			
			this.Routes = new List<Route>();
			
			ParseOutput(commandOutput);
		}
		
		
		public virtual int HostPort { get; set; }
		
		public virtual bool IsTCP { get; set; }
		
		public virtual bool IsUDP
		{
			get { return !IsTCP; }
			set { IsTCP = !value; }
		}
		
		public virtual IList<Route> Routes
		{
			get; set; 
		}
		
		public  virtual string HostIPAddressV4 { get; set; }
		private void ParseOutput(string output)
		{
			string pattern = @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
	                         @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
	                         @"([01]?\d\d?|2[0-4]\d|25[0-5])\." +
	                         @"([01]?\d\d?|2[0-4]\d|25[0-5])";
	        
			Regex ip = new Regex( pattern );
			
			int i = 0;
			
			foreach (string line in output.Split('\n'))
			{
				if (i == 0)
				{
					i++;
					continue;
				}
				
				if (string.IsNullOrEmpty(line))
					continue;
				
				Route r = new Route();
				
				//trim prepended and trailing spaces
				//then replace double spaces with commas
				//this makes splitting easier.
				string cleanLine = line
					.Trim()
					.Replace("  ", ",") //double spaces
					.Replace(" ms ", " ms,"); //handle load balancers
				
				string[] infos = cleanLine.Split(',');
				
				r.Hop = int.Parse(infos[0]);
				
				//Some routers don't return anything.
				if (infos[1] == "* * *")
				{
					r.FirstResult = null;
					r.FirstIPAddress = null;
					r.FirstHostname = null;
					
					r.SecondResult = null;
					r.SecondIPAddress = null;
					r.SecondHostname = null;
					
					r.ThirdResult = null;
					r.ThirdIPAddress = null;
					r.ThirdHostname = null;
				}
				else //whee, let's get complicated
				{//breaks when arbitrary host unreachable, !H / * *
					r.FirstHostname = infos[1].Split(' ')[0];
					r.FirstIPAddress = infos[1].Split(' ')[1];
					r.FirstResult = infos[2];
					
					if (ip.IsMatch(infos[3].Split(' ')[1].Replace("(", string.Empty).Replace(")", string.Empty)))
					{
						r.SecondHostname = infos[3].Split(' ')[0];
						r.SecondIPAddress = infos[3].Split(' ')[1].Replace("(", string.Empty).Replace(")", string.Empty);
						r.SecondResult = infos[4];
						
						if (ip.IsMatch(infos[5].Split(' ')[1].Replace("(", string.Empty).Replace(")", string.Empty)))
						{
							r.ThirdHostname = infos[5].Split(' ')[0];
							r.ThirdIPAddress = infos[5].Split(' ')[1].Replace("(", string.Empty).Replace(")", string.Empty);
							r.ThirdResult = infos[6];
						}
						else
						{
							r.ThirdHostname = r.SecondHostname;
							r.ThirdIPAddress = r.SecondIPAddress;
							r.ThirdResult = infos[5];
						}
					}
					else
					{
						r.SecondHostname = r.FirstHostname;
						r.SecondIPAddress = r.FirstIPAddress;
						r.SecondResult = infos[3];
						
						if (ip.IsMatch(infos[4].Split(' ')[1].Replace("(", string.Empty).Replace(")", string.Empty)))
						{
							r.ThirdHostname = infos[4].Split(' ')[0];
							r.ThirdIPAddress = infos[4].Split(' ')[1].Replace("(", string.Empty).Replace(")", string.Empty);
							r.ThirdResult = infos[5];
						}
						else
						{
							r.ThirdHostname = r.SecondHostname;
							r.ThirdIPAddress = r.SecondIPAddress;
							r.ThirdResult = infos[4];
						}
					}
				}
				
				r.ParentResults = this;
				
				this.Routes.Add(r);
				
				
			}
		}
	}
}

