using System;
using System.Web;
using System.Web.UI;
using NHibernate;
using AutoAssess.Data.PersistentObjects;
using System.Net;

namespace AutoAssess.Web.API
{
	public class CreateProfileHost : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{			
			ISession s = this.CurrentSession;
			
			using (ITransaction t = s.BeginTransaction()) {
				Guid userID = new Guid (context.Request ["UserID"]);
				Guid clientID = new Guid (context.Request ["ClientID"]);
				
				PersistentUser user = s.Get<PersistentUser> (userID);
				PersistentClient client = s.Get<PersistentClient> (clientID);
				
				if (user == null || !user.HasAPIAccess)
					throw new Exception ("no api access");
				
				if (client == null || !client.HasAPIAccess)
					throw new Exception ("no api access");
				
				PersistentProfile p = s.Get<PersistentProfile>(new Guid(context.Request["ProfileID"]));
				DateTime now = DateTime.Now;
				PersistentProfileHost host = new PersistentProfileHost (new Guid (context.Request ["WebUserID"]));
				host.ParentProfile = p;
				host.Name = context.Request["HostSubDomain"];
				host.IPv4Address = Dns.GetHostEntry (context.Request["HostSubDomain"]).AddressList [0].ToString ();
				host.VerifiedByFile = true;
				host.VerifiedByWhois = true;
				host.VerifiedOn = DateTime.Now;
				host.WasManuallyVerified = false;
				host.IsVerified = true;
				host.CreatedBy = Guid.Empty;
				host.CreatedOn = now;
				host.LastModifiedBy = Guid.Empty;
				host.LastModifiedOn = now;
				host.IsActive = true;
				
				p.Range += " " + host.IPv4Address;
				p.SetUpdateInfo(Guid.Empty, true);
				
				s.Save (p);
				s.Save (host);
				
				try {
					t.Commit ();
				} catch (Exception ex) {
					t.Rollback ();
					
					throw ex;
				}
				
				string xml = host.ToPersistentXML(false/*include nmap hosts? no, because none exist right now*/);
				
				context.Response.Write (xml);
			}
		}
	}
}

