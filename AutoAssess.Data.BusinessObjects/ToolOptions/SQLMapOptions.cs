using System;

namespace AutoAssess.Data.BusinessObjects
{
	public class SQLMapOptions : IToolOptions
	{
		public SQLMapOptions ()
		{
			this.Risk = null;
			this.Level = 2;
			this.TestForms = false;
			this.CrawlLevel = 5;
			this.Threads = null;
			this.DBMS = null;
		}
		
		public virtual string Path { get; set; }
		
		public virtual int? Risk { get; set; }
		
		public virtual int? Level { get; set; }
		
		public virtual int CrawlLevel { get; set; }
		
		public virtual bool TestForms { get; set; }
		
		public virtual int? Threads { get; set; }
		
		public virtual string URL { get; set; }
		
		public virtual int Port { get; set; }
		
		public virtual string DBMS { get; set; }
		
		public virtual string ToBusinessXml()
		{
			string xml = "<sqlmapOptions>";
			
			xml = xml + "<risk>" + this.Risk + "</risk>";
			xml = xml + "<level>" + this.Level + "</level>";
			xml = xml + "<crawlLevel>" + this.CrawlLevel + "</crawlLevel>";
			xml = xml + "<testForms>" + this.TestForms + "</testForms>";
			xml = xml + "<threads>" + this.Threads + "</threads>";
			xml = xml + "<host>" + this.URL + "</host>";
			xml = xml + "<port>" + this.Port + "</port>";
			xml = xml + "<dbms>" + this.DBMS + "</dbms>";
			
			xml = xml + "</sqlmapOptions>";
			
			return xml;
		}
		
	}
}

