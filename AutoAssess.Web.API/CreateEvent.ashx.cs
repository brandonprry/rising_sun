using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.PersistentObjects;
using NHibernate;
using NHibernate.Criterion;

namespace AutoAssess.Web.API
{
	public class CreateEvent : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			Guid userID = new Guid(context.Request["UserID"]);
			//Guid clientID = new Guid(context.Request["ClientID"]);
			
			ISession s = this.CurrentSession;
			
			PersistentUser user = s.CreateCriteria<PersistentUser>()
				.Add(Restrictions.Eq("ID", userID))
				.Add(Restrictions.Eq("IsActive", true))
				.UniqueResult<PersistentUser>();
		
			if (user == null || !user.HasAPIAccess)
				throw new Exception("no api access");
			
			if (!user.Client.HasAPIAccess)
				throw new Exception("no api access");
			
			using (ITransaction trans = s.BeginTransaction())
			{
				DateTime now = DateTime.Now;
				
				PersistentEvent e = new PersistentEvent();
				e.ID = Guid.NewGuid();
				e.CreatedBy = userID;
				e.CreatedOn = now;
				e.LastModifiedBy = userID;
				e.LastModifiedOn = now;
				e.IsActive = true;
				e.ProfileID = new Guid(context.Request["ProfileID"]);
				e.Description = context.Request["Description"];
				e.Severity = int.Parse(context.Request["Severity"]);
				e.Timestamp = now;
				
				s.Save(e);
				
				try
				{
					trans.Commit();
				}
				catch(Exception ex)
				{
					trans.Rollback();
					throw ex;
				}
				
				context.Response.Write( e.ToPersistentXml());
			}
		}
	}
}

