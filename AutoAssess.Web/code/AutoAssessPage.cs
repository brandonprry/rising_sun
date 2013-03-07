using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Web;
using AutoAssess.Data;
using AutoAssess.Web.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using AutoAssess.Data.PersistentObjects;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using System.Configuration;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.OpenVAS.PersistentObjects;

namespace AutoAssess.Web
{
	public abstract class AutoAssessPage : System.Web.UI.Page
	{
		public AutoAssessPage ()
		{

		}
		
		public ISession CurrentWebSession { 
			get{
				if (this.Session["Web?Session"] == null)
					GetWebSession();
				
				return this.Session["Web?Session"] as ISession;
			}
		}
		
		public ISession CurrentScanSession {
			get { 
				return this.Session["Scan?Session"] as ISession;
			}
		}
		
		public PersistentProfile CurrentProfile {
			get {
				return this.Session["User?CurrentProfile"] as PersistentProfile;
			}
			set { 
				this.Session["User?CurrentProfile"] = value;
			}
		}
		
		public PersistentClient CurrentClient { get; set; }
		
		public WebUser CurrentUser 
		{ 
			get
			{
				return Session["User"] as WebUser;
			}
		}
		
		protected override void OnLoad (EventArgs e)
		{

		
		}
		
		public void GetServiceSession()
		{
			IPersistenceConfigurer config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString("Server=" + ConfigurationManager.AppSettings["PostgreSQL"] + ";Port=" + ConfigurationManager.AppSettings["PostgreSQLPort"] + ";Database=autoassess;User Id=" + ConfigurationManager.AppSettings["PostgreSQLUser"] + ";Password=" + ConfigurationManager.AppSettings["PostgreSQLPass"] + ";SSL=true;");
			
			ISessionFactory factory = Fluently.Configure()
				.Database(config)
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentNessusScan>())
				.Mappings(m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentNexposeScan>())
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentOpenVASTask>())
				.Mappings(m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentOneSixtyOneResults>())
				.BuildSessionFactory();
			
			
			this.Session["Scan?Session"] = factory.OpenSession();
		}

		public void GetWebSession ()
		{
			IPersistenceConfigurer config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString("Server=" + ConfigurationManager.AppSettings["PostgreSQL"] + ";Port=" + ConfigurationManager.AppSettings["PostgreSQLPort"] + ";Database=autoassess_web;User Id=" + ConfigurationManager.AppSettings["PostgreSQLUser"] + ";Password=" + ConfigurationManager.AppSettings["PostgreSQLPass"] + ";SSL=true;");
			
			
			ISessionFactory factory = Fluently.Configure()
				.Database(config)
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<WebUser>())
				.BuildSessionFactory();
				
			ISession s = factory.OpenSession();
			this.Session["Web?Session"] = s;
		}
		
		protected override void OnUnload (EventArgs e)
		{
			base.OnUnload (e);
			
		}
	}
}

