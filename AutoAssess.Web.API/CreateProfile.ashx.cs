using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.PersistentObjects;
using NHibernate;
using NHibernate.Criterion;
using System.Net;

namespace AutoAssess.Web.API
{
	public class CreateProfile : ApiHttpHandler
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
				
				PersistentProfile profile = new PersistentProfile();
				
				string webUserID = context.Request["WebUserID"];
				
				profile.WebUserID = new Guid(context.Request["WebUserID"]);
				profile.Description = context.Request["ProfileDescription"];
				profile.Name = context.Request["ProfileName"];
				profile.Range = context.Request["ProfileDomain"];
				profile.Domain = context.Request["ProfileDomain"];
				profile.RunEvery = new TimeSpan(24*(int.Parse(context.Request["ProfileSchedule"])), 0, 0); //30 days
				profile.RunAfter = DateTime.Now;
				profile.HasRun = false;
				
				profile.SetCreationInfo(userID);
				
				s.Save(profile);
				
				foreach (string h in profile.Range.Split(','))
				{
					PersistentProfileHost host = new PersistentProfileHost(new Guid(context.Request["WebUserID"]));
					host.ParentProfile = profile;
					host.IPv4Address = Dns.GetHostEntry(h).AddressList[0].ToString();
					host.VerifiedByFile = true;
					host.VerifiedByWhois = true;
					host.VerifiedOn = DateTime.Now;
					host.WasManuallyVerified = false;
					host.IsVerified = true;
					
					s.Save(host);
				}
				
				try
				{
					t.Commit();
				}
				catch (Exception ex)
				{
					t.Rollback();
					
					throw ex;
				}
				
				string xml = profile.ToPersistentXml();
				
				context.Response.Write(xml);
			}
		}
	}
}

