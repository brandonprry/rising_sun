using System;
using System.Web;
using System.Web.UI;
using NHibernate;
using NHibernate.Criterion;
using AutoAssess.Data.BusinessObjects;
using AutoAssess.Data.PersistentObjects;
using System.Collections.Generic;

namespace AutoAssess.Web.API
{
	public class CreateScan : ApiHttpHandler
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
				PersistentProfile parentProfile = s.CreateCriteria<PersistentProfile>()
					.Add(Restrictions.Eq("ID", new Guid(context.Request["ParentProfileID"])))
					.Add(Restrictions.Eq("IsActive", true))
					.UniqueResult<PersistentProfile>();
				
				PersistentScan scan = new PersistentScan();
				scan.SetCreationInfo(userID);
	
				scan.Name = context.Request["Name"];
				scan.ParentProfile = parentProfile;
				
				scan.ScanOptions = new PersistentScanOptions();
				scan.ScanOptions.SetCreationInfo(userID);
				scan.ParentProfile.VirtualMachines = new List<PersistentVirtualMachine>();
				
				if (context.Request["ScanVirtualMachines"] != null)
				{
					string[] machines = context.Request["ScanVirtualMachines"].Split(',');
					
					foreach (string machine in machines)
					{
						if (string.IsNullOrEmpty(machine))
							continue;
						
						PersistentVirtualMachine m = new PersistentVirtualMachine();
						m.SetCreationInfo(userID);
						m.Guid = Guid.Parse(machine);
						m.ParentProfile = scan.ParentProfile;
						scan.ParentProfile.VirtualMachines.Add(m);
						
						s.Update(scan.ParentProfile);
					}
				}
				
				scan.ScanOptions.ParentScan = scan;
				
				if (context.Request["ScanIsDSXS"] != null && context.Request["ScanIsDSXS"].ToLower() == "true")
					scan.ScanOptions.IsDSXS = true;
				if (context.Request["ScanIsSQLMap"] != null && context.Request["ScanIsSQLMap"].ToLower() == "true")
				{
					scan.ScanOptions.IsSQLMap = true;
					scan.ScanOptions.SQLMapOptions = new PersistentSQLMapOptions();
					scan.ScanOptions.SQLMapOptions.Level = 2;
					scan.ScanOptions.SQLMapOptions.SetCreationInfo(Guid.Empty);
					
					s.Save(scan.ScanOptions.SQLMapOptions);
				}
				if (context.Request["ScanIsOpenVAS"] != null && context.Request["ScanIsOpenVAS"].ToLower() == "true")
					scan.ScanOptions.IsOpenVASAssessment = true;
				if (context.Request["ScanIsNessus"] != null && context.Request["ScanIsNessus"].ToLower() == "true")
					scan.ScanOptions.IsNessusAssessment = true;
				if (context.Request["ScanIsNexpose"] != null && context.Request["ScanIsNexpose"].ToLower() == "true")
					scan.ScanOptions.IsNexposeAssessment = true;
				if (context.Request["ScanIsMetasploit"] != null && context.Request["ScanIsMetasploit"].ToLower() == "true")
				{
					scan.ScanOptions.IsMetasploitAssessment = true;
					scan.ScanOptions.MetasploitDiscovers = bool.Parse(context.Request["MetasploitDiscovers"]);
					scan.ScanOptions.MetasploitBruteforces = bool.Parse(context.Request["MetasploitBruteforces"]);
				}
				if (context.Request["ScanIsBasicBruteforce"] != null && context.Request["ScanIsBasicBruteforce"].ToLower() == "true")
					scan.ScanOptions.IsBruteForce = true;
				
				try
				{
				s.Save(scan);

					trans.Commit();
				}
				catch (Exception ex)
				{
					trans.Rollback();
					throw ex;
				}
				
				context.Response.Write(scan.ToPersistentXml());
			}
		}
	}
}

