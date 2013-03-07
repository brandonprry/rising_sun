using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Web;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.Nessus.PersistentObjects;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using AutoAssess.Data.OpenVAS.PersistentObjects;

namespace AutoAssess.Web.API
{
	public abstract class ApiHttpHandler : System.Web.IHttpHandler
	{
		public abstract void ProcessRequest(HttpContext context);
		
		public ApiHttpHandler ()
		{

		}
		
		public ISession CurrentSession { 
			get{
				return this.GetSession();
			}
		}
		
		private ISession GetSession()
		{
			string conn = "Server=" + ConfigurationManager.AppSettings["PostgreSQL"] + ";";
			conn += "Port=" + ConfigurationManager.AppSettings["PostgreSQLPort"] + ";";
			conn += "Database=autoassess;";
			conn += "User Id=" + ConfigurationManager.AppSettings["PostgreSQLUser"] + ";";
			conn += "Password=" + ConfigurationManager.AppSettings["PostgreSQLPass"] + ";";
			conn += "SSL=true;";
			
			IPersistenceConfigurer config = PostgreSQLConfiguration
				.PostgreSQL82.ConnectionString(conn);
			
			ISessionFactory factory = Fluently.Configure()
				.Database(config)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersistentNessusScan>())
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersistentProfile>())
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersistentOpenVASTask>())
				.BuildSessionFactory();
				
			return factory.OpenSession();
		}
		
		public bool IsReusable
		{
			get { return false; }
		}
		
		
	}
}

