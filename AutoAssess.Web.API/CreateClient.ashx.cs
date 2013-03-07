
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.BusinessObjects;
using AutoAssess.Data.PersistentObjects;
using NHibernate;
using NHibernate.Criterion;

namespace AutoAssess.Web.API
{


	public class CreateClient : ApiHttpHandler
	{

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
				PersistentClient newClient = new PersistentClient();
				
				newClient.HasAPIAccess = bool.Parse(context.Request["NewClientHasAPIAccess"]);
				newClient.LogoPath = context.Request["NewClientLogoPath"];
				newClient.Name = context.Request["NewClientName"];
				
				newClient.SetCreationInfo(userID);
				
				s.Save(newClient);
				
				try 
				{
					trans.Commit();
				}
				catch (Exception ex)
				{
					trans.Rollback();
					
					throw ex;
				}
			}
		}
	}
}

