using System;
using System.Collections.Generic;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class Correlator
	{
		Correlators _correlators;
		public Correlator (Correlators correlators)
		{
			_correlators = correlators;
		}
		
		public Dictionary<string, Correlation> Correlate(List<IVuln> toolResults)
		{
			Dictionary<string, Correlation> correlations = new Dictionary<string, Correlation>();
			foreach (IVuln vuln in toolResults)
			{
				if (correlations[vuln.CVE] == null)
					correlations.Add (vuln.CVE, new Correlation { CVE = vuln.CVE });
				
				if (((_correlators & Correlators.Nessus) == Correlators.Nessus) && vuln.Assesser == "Nessus")
					correlations[vuln.CVE].Correlators = correlations[vuln.CVE].Correlators | Correlators.Nessus;
				else if (((_correlators & Correlators.Nexpose) == Correlators.Nexpose) && vuln.Assesser == "Nexpose")
					correlations[vuln.CVE].Correlators = correlations[vuln.CVE].Correlators | Correlators.Nexpose;
				else if (((_correlators & Correlators.OpenVAS) == Correlators.OpenVAS) && vuln.Assesser == "OpenVAS")
					correlations[vuln.CVE].Correlators = correlations[vuln.CVE].Correlators | Correlators.OpenVAS;
			}
			
			foreach (var pair in correlations)
			{
				decimal divisor, dividend;
				divisor = dividend = 0m;
				
				if ((_correlators & Correlators.Nessus) == Correlators.Nessus)
					divisor++;
				
				if ((_correlators & Correlators.Nexpose) == Correlators.Nexpose)
					divisor++;
				
				if ((_correlators & Correlators.OpenVAS) == Correlators.OpenVAS)
					divisor++;
				
				if (pair.Value.IsNessus.HasValue && pair.Value.IsNessus.Value)
					dividend++;
				
				if (pair.Value.IsNexpose.HasValue && pair.Value.IsNexpose.Value)
					dividend++;
				
				if (pair.Value.IsOpenVAS.HasValue && pair.Value.IsOpenVAS.Value)
					dividend++;
				
				if (divisor == 1m)
				{
					pair.Value.OddsOfFalsePositive = null;
					pair.Value.Information = "Not enough data to derive odds of false positive. Try using more vulnerability scanners.";
				}
				else
				{
					pair.Value.OddsOfFalsePositive = (1m - (dividend/divisor))*100m;
					pair.Value.Information = "The score was determined by the fact that " + dividend.ToString() + " out of " +
										 divisor.ToString() + " separate vulnerabilty assessment engines found this issue.";
				}
			}
			
			return correlations;
		}
		
	}
		
	public enum Correlators
	{
		Nessus,
		OpenVAS,
		Nexpose
	}
	
	[Serializable]
	public class Correlation
	{
		private bool? _isNessus = null;
		private bool? _isNexpose = null;
		private bool? _isOpenvas = null;
		
		public string CVE { get; set; }
		
		public bool? IsNessus { 
			get { return _isNessus; } 
			set { _isNessus = value; }
		}
		
		public bool? IsNexpose { 
			get { return _isNexpose; }
			set { _isNexpose = value; }
		}
		
		public bool? IsOpenVAS { 
			get { return _isOpenvas; }
			set { _isOpenvas = value; }
		}
		
		public Correlators Correlators { get; set; }
		
		public string Information { get; set; }
		
		public decimal? OddsOfFalsePositive { get; set; }
	}
}

