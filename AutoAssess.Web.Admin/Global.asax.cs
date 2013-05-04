using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using AutoAssess.Web.Data;
using System.Configuration;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using AutoAssess.Data.OpenVAS.PersistentObjects;
using AutoAssess.Data.Nexpose.PersistentObjects;
using AutoAssess.Data.Metasploit.Pro.PersistentObjects;

namespace AutoAssess.Web.Admin
{
	public class Global : System.Web.HttpApplication
	{
		
		protected virtual void Application_Start (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Session_Start (Object sender, EventArgs e)
		{
			IPersistenceConfigurer config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString("Server=" + ConfigurationManager.AppSettings["PostgreSQL"] + ";Port=5432;Database=rising_sun_web;User Id=postgres;Password=password;SSL=true;");
			
			ISessionFactory factory = Fluently.Configure()
				.Database(config)
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<WebUser>())
				.BuildSessionFactory();
				
			this.Session["User?Session"] = factory.OpenSession();
			
			config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString("Server=" + ConfigurationManager.AppSettings["PostgreSQL"] + ";Port=5432;Database=rising_sun;User Id=postgres;Password=password;SSL=true;");
			
			factory = Fluently.Configure()
				.Database(config)
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentScan>())
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentNessusScan>())
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentOpenVASScan>())
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentNexposeScan>())
				.Mappings(m =>
						m.FluentMappings.AddFromAssemblyOf<PersistentMetasploitScan>())
				.BuildSessionFactory();
			
			this.Session["Service?Session"] = factory.OpenSession();
		}
		
		protected virtual void Application_BeginRequest (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_EndRequest (Object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_AuthenticateRequest (Object sender, EventArgs e)
		{
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

