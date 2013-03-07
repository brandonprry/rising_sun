
using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using AutoAssess.Data;
using System.IO;

namespace AutoAssess.Web.API
{


	public class Global : System.Web.HttpApplication
	{

		protected virtual void Application_Start (Object sender, EventArgs e)
		{
		}

		protected virtual void Session_Start (Object sender, EventArgs e)
		{
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
			Exception exc = Server.GetLastError();
			
			this.WriteExceptionToFile(exc);
			
			if (exc.GetType() == typeof(HttpException))
			{
			    if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
				    return;
				
				Server.Transfer("HttpErrorPage.aspx");
			}
			
			Response.Write("<h2>Global Page Error: This error has been logged.</h2>\n");
			Response.Write("Return to the <a href='Default.aspx'>Default Page</a>\n");
			
			Server.ClearError();
		}
		
		private void WriteExceptionToFile(Exception e)
		{
			string exception = string.Empty;
			
			exception += DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n\r\n";
			exception += "Message: " + e.Message + "\r\n\r\n";
			exception += "Source: " + e.Source + "\r\n\r\n";
			exception += "StackTrace: " + e.StackTrace + "\r\n\r\n";
			exception += "Everything Else: " + e.ToString() + "\r\n\r\n";
			
			File.WriteAllText("/tmp/api_exception_" + Guid.NewGuid().ToString(), exception);
		}
		
		protected virtual void Session_End (Object sender, EventArgs e)
		{
		}

		protected virtual void Application_End (Object sender, EventArgs e)
		{
		}
	}
}

