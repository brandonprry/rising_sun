using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using AutoAssess.Data;
using AutoAssess.Misc;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]

	public class Port 
	{
		public Port ()
		{
		}
		
		public Port (XmlNode node)
		{
			foreach (XmlNode child in node.ChildNodes)
			{
				if (child.Name == "portNumber")
					this.PortNumber = int.Parse(child.InnerText);
				else if (child.Name == "IsTcp")
					this.IsTCP = bool.Parse(child.InnerText);
				else if (child.Name == "service")
					this.Service = child.InnerText;
				else if (child.Name == "hydraServiceName")
					this.HydraServiceName = child.InnerText;
				else if (child.Name == "deepScan")
					this.DeepScan = child.InnerText;
				else if (child.Name == "state")
					this.State = child.InnerText;
			}
		}
		
		/// <summary>
		/// The port Service is listening on
		/// </summary>
		public virtual int PortNumber { get; set; }
		
		/// <summary>
		/// Is the service on this port UDP or TCP?
		/// </summary>
		public virtual bool IsUDP { get; set; }
		
		//this may seem redundant, but it will make code readability a bit better IMO
		/// <summary>
		/// Equal to !IsUDP
		/// </summary>
		public virtual bool IsTCP 
		{ 
			get
			{
				return !this.IsUDP;
			}
			set 
			{
				IsUDP = !value;
			}
		}
		
		public virtual string DeepScan { get; set; }
		
		/// <summary>
		/// Name of the service listening on the port
		/// </summary>
		public virtual string Service { get; set; }
		
		/// <summary>
		/// Name of the service when using hydra to bruteforce weak credentials
		/// </summary>
		public virtual string HydraServiceName { get; set; }
		
		/// <summary>
		/// State of the port (open, filtered, closed)
		/// Could probably be an enum...
		/// </summary>
		public virtual string State { get; set; }
		
		public virtual string ParentIPAddress { get; set; }
		
		public virtual void CleanPort()
		{
			this.DeepScan = StringUtils.CleanContent(this.DeepScan);
			this.HydraServiceName = StringUtils.CleanContent(this.HydraServiceName);
			this.Service = StringUtils.CleanContent(this.Service);
			this.State = StringUtils.CleanContent(this.State);
		}
		
		public virtual string ToBusinessXml()
		{
			string xml = "<port>";
			
			xml = xml + "<deepScan>" + this.DeepScan + "</deepScan>";
			xml = xml + "<hydraServiceName>" + this.HydraServiceName + "</hydraServiceName>";
			xml = xml + "<isTcp>" + this.IsTCP + "</isTcp>";
			xml = xml + "<portNumber>" + this.PortNumber + "</portNumber>";
			xml = xml + "<service>" + this.Service + "</service>";
			xml = xml + "<state>" + this.State + "</state>"; 
			
			xml = xml + "</port>";
			
			return xml;
		}	
	}
	
	
	public enum PortState
	{
		Open = 1,
		Filtered = 2,
		Closed = 3
	}
}

