using System;
using System.Linq;
using System.Collections.Generic;

namespace AutoAssess.Data.Nexpose.BusinessObjects
{
	[Serializable]
	public static class NexposeParser
	{
		
//		public static List<NexposeAsset> ParseFromCSVReport(string csv)
//		{
//			if (string.IsNullOrEmpty(csv))
//				return null;
//			
//			List<NexposeAsset> assets = new List<NexposeAsset>();
//			int i = 0;
//			
//			foreach (string line in csv.Split('\n'))
//			{
//				if (i == 0)
//				{
//					i++;
//					continue; //header
//				}
//				string[] assetInfo = line.Split(',');
////				
////				try {
////				if (assetInfo[2] == "something")
////						;
////				}
////				catch
////				{
////					Console.Write("bfdsa");
////				}
//				if (assetInfo.Length > 2 && assetInfo[2] != "nv") // /*&& (assetInfo[2] == "sv" || assetInfo[2] == "ve")*/)
//				{
//					NexposeAsset asset = assets.Where(a => a.IPAddressV4 == assetInfo[0]).FirstOrDefault();
//					bool isNew = false;
//					
//					if (asset == null)
//					{
//						asset = new NexposeAsset();
//						asset.IPAddressV4 = assetInfo[0];
//						asset.Vulnerabilities = new List<NexposeVulnerability>();
//						
//						isNew = true;
//					}
//					
//					NexposeVulnerability vuln = new NexposeVulnerability();
//					
//					vuln.Port = assetInfo[1];
//					vuln.RemoteVulnerabilityID = assetInfo[3];
//					vuln.CVEID = assetInfo[4];
//					vuln.Severity = assetInfo[5];
//					vuln.Title = assetInfo[6];
//					
//					asset.Vulnerabilities.Add(vuln); 
//					
//					if (isNew)
//						assets.Add(asset);
//				}
//			}
//			
//			return assets;
//			
//		}
	}
}

