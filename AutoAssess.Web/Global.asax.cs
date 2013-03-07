
using System;
using System.Web;
using AutoAssess.Web.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using AutoAssess.Data.PersistentObjects;
using System.Configuration;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace AutoAssess.Web
{


	public class Global : System.Web.HttpApplication//: AutoAssessWebApplication
	{

		
		protected virtual void Application_Start (Object sender, EventArgs e)
		{
		}

		protected virtual void Session_Start (Object sender, EventArgs e)
		{
			IPersistenceConfigurer config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString("Server=" + ConfigurationManager.AppSettings["PostgreSQL"] + ";Port=" + ConfigurationManager.AppSettings["PostgreSQLPort"] + ";Database=autoassess_web;User Id=" + ConfigurationManager.AppSettings["PostgreSQLUser"] + ";Password=" + ConfigurationManager.AppSettings["PostgreSQLPass"] + ";SSL=true;");
			
			
			ISessionFactory factory = Fluently.Configure()
				.Database(config)
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<WebUser>())
				.BuildSessionFactory();
				
			ISession s = factory.OpenSession();
			this.Session["Web?Session"] = s;
			
			config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString("Server=" + ConfigurationManager.AppSettings["PostgreSQL"] + ";Port=" + ConfigurationManager.AppSettings["PostgreSQLPort"] + ";Database=autoassess;User Id=" + ConfigurationManager.AppSettings["PostgreSQLUser"] + ";Password=" + ConfigurationManager.AppSettings["PostgreSQLPass"] + ";SSL=true;");
			
			factory = Fluently.Configure()
				.Database(config)
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentNessusScan>())
				.Mappings(m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentNexposeScan>())
				.Mappings(m => 
					    m.FluentMappings.AddFromAssemblyOf<PersistentMetasploitScan>())
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentOpenVASTask>())
				.Mappings(m => 
						m.FluentMappings.AddFromAssemblyOf<PersistentOneSixtyOneResults>())
				.BuildSessionFactory();
			
			
			this.Session["Scan?Session"] = factory.OpenSession();
		}

		protected virtual void Application_BeginRequest (Object sender, EventArgs e)
		{
		}

		protected virtual void Application_EndRequest (Object sender, EventArgs e)
		{
		}

		protected virtual void Application_AuthenticateRequest (Object sender, EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				

			}
		}

		protected virtual void Application_Error (Object sender, EventArgs e)
		{
		}

		protected virtual void Session_End (Object sender, EventArgs e)
		{
		}

		protected virtual void Application_End (Object sender, EventArgs e)
		{
		}
	}
}

