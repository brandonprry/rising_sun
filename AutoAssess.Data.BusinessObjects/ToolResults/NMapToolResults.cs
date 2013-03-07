using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoAssess.Data;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class NMapToolResults : ToolResults,  IToolResults
	{
		public NMapToolResults()
		{}
		
		public NMapToolResults (string commandOutput)
		{
			this.ParseOutput(commandOutput);
			
			this.FullOutput = commandOutput;
		}
		
		public virtual string HostIPAddressV4 { get; set; }
		
		public virtual int HostPort { get; set; }
		
		public virtual bool IsTCP { get; set; }
		
		public virtual bool IsUDP
		{
			get { return !IsTCP; }
			set { IsTCP = !value; }
		}
		
		
		public virtual IList<NMapHost> Hosts { get; set; }
		
		
		private void ParseOutput(string output)
		{
			List<NMapHost> hosts = new List<NMapHost>();
			Regex portRegex = new Regex(@"^[0-9]{1,5}/[tcp|udp]");
			NMapHost host = null;
				

			foreach(string line in output.Split('\n'))
			{
				if (string.IsNullOrEmpty(line))
				{
					if (host != null && !string.IsNullOrEmpty(host.IPAddressv4))
						hosts.Add(host);
		
					
					host = new NMapHost();
					host.Ports = new List<Port>();
					
					continue;
				}
				
				if (line.StartsWith("Device type: "))
				{
					host.DeviceType = line.Replace("Device type: ", string.Empty);
	
				}
				else if (line.StartsWith("Running: "))
				{
					host.OS = line.Replace("Running: ", string.Empty);

				}
				else if (line.StartsWith("OS details: "))
				{
					host.OS_Details = line.Replace("OS details: ", string.Empty);

				}
				else if (line.StartsWith("Network Distance: "))
				{
					host.NetworkDistance = line.Replace("Network Distance: ", string.Empty);

				}
				else if (line.StartsWith("MAC Address: "))
				{
					host.MAC = line.Replace("MAC Address: ", string.Empty);

				}
				else if (portRegex.IsMatch(line))
				{
					Port port = new Port();
					
					string l2 = Regex.Replace(line, @"\s{2,}", " ");
					
					string[] t = l2.Split(' ');
					
					string p = t[0]; //port in [0-9]{1,5}/[udp|tcp] form
					string st = t[1]; //state of port (open)
					string ser = t[2]; //service on port (http(s), ssh)
					
					//this also sets IsTCP since it returns !IsUDP
					port.IsUDP = (p.Split('/')[1] == "udp");
					
					port.Service = ser;
					port.State = st;
					port.PortNumber = int.Parse(p.Split('/')[0]);
					
					host.Ports.Add(port);
					
				}
				else if (line.StartsWith("Nmap scan report for "))
				{
					string l2 = line.Replace("Nmap scan report for ", string.Empty);
					
					string[] t = l2.Split(' ');
					
					if (t.Length == 1)
						host.IPAddressv4 = t[0];
					else
					{
						host.Hostname = t[0];
						
						string ip = t[1].Substring(1);
						ip = ip.Substring(0, ip.Length - 1);
						
						host.IPAddressv4 = ip;
					}
					
				}
				
			}
			
			this.Hosts = hosts;
		}
		
		public virtual string ToBusinessXml()
		{
			string xml = string.Empty;
			
			xml = xml + "<currentResults>";
				
				xml = xml + "<hostCount>" + this.Hosts.Count + "</hostCount>";
				
				foreach (NMapHost host in this.Hosts)
				
					xml = xml + host.ToBusinessXml();
				
				
				
				xml = xml + "</currentResults>";
			
			return xml;
		}
	}
}

