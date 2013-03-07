using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.BusinessObjects;
using AutoAssess.Data.PersistentObjects;
using NHibernate;

namespace AutoAssess.Web.API
{
	public class CreateProfileHostVerification : ApiHttpHandler
	{
		
		public bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			ISession s = this.CurrentSession;
			
			using (ITransaction t = s.BeginTransaction())
			{
				Guid userID = new Guid(context.Request["UserID"]);
				Guid clientID = new Guid(context.Request["ClientID"]);
				
				PersistentUser user = s.Get<PersistentUser>(userID);
				PersistentClient client = s.Get<PersistentClient>(clientID);
				
				if (user == null || !user.HasAPIAccess)
					throw new Exception("no api access");
				
				if (client == null || !client.HasAPIAccess)
					throw new Exception("no api access");
				
				PersistentProfileHostVerification verification = new PersistentProfileHostVerification();
				verification.SetCreationInfo(Guid.Empty);
				
				verification.VerificationData = context.Request["VerificationData"];
				verification.VerificationFileName = context.Request["VerificationFileName"];
				verification.WhoisEmail = context.Request["WhoisRegex"];
				
				s.Save(verification);
				
				context.Response.Write(verification.ToBusinessXML());
			}
		}
	}
}

