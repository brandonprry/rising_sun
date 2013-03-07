using System;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.PersistentObjects;
using NHibernate;
using NHibernate.Criterion;

namespace AutoAssess.Web.API
{
	public class CreateUser : ApiHttpHandler
	{
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public override void ProcessRequest (HttpContext context)
		{
			Guid userID = new Guid(context.Request["UserID"]);
			Guid clientID = new Guid(context.Request["ClientID"]);
			
			
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
				PersistentUser newUser = new PersistentUser();
				
				newUser.Client = s.CreateCriteria<PersistentClient>()
					.Add(Restrictions.Eq("ID", new Guid(context.Request["NewUserClientID"])))
					.Add(Restrictions.Eq("IsActive", true))
					.UniqueResult<PersistentClient>();
				
				newUser.HasAPIAccess = bool.Parse(context.Request["NewUserHasAPIAccess"]);
				newUser.FullName = context.Request["NewUserFullName"];
				newUser.Username = context.Request["NewUserUsername"];
				
				newUser.SetCreationInfo(userID);
				
				s.Save(newUser);
				
				try
				{
					trans.Commit();
				}
				catch (Exception ex)
				{
					trans.Rollback();
					throw ex;
				}
				
				context.Response.Write(newUser.ToPersistentXml());
			}
		}
	}
}

