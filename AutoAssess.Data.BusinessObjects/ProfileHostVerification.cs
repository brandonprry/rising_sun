using System;

namespace AutoAssess.Data.BusinessObjects
{
	[Serializable]
	public class ProfileHostVerification
	{
		public ProfileHostVerification ()
		{
			this.GenerateVerificationFile();
		}
		
		
		public virtual string VerificationFileName { get; set; } //I wish this setter were private tbh
	
		public virtual string WhoisEmail { get; set; }
		
		public virtual string VerificationData { get; set;}//I wish this setter were private tbh
		
		public virtual string VerificationError { get; set; }
		
		private void GenerateVerificationFile()	
		{
			this.VerificationFileName = Guid.NewGuid().ToString();
			this.VerificationData = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
		}
		
		public virtual string ToBusinessXML ()
		{
			string xml = string.Empty;
			
			xml += "<profileHostVerification>";
			xml += "<whoisEmail>" + this.WhoisEmail + "</whoisEmail>";
			xml += "<verificationData>" + this.VerificationData + "</verificationData>";
			xml += "<verificationError>" + this.VerificationError + "</verificationError>";
			xml += "</profileHostVerification>";
			
			return xml;
		}
	}
}

