using System;
using System.Text.RegularExpressions;

namespace AutoAssess.Misc
{
	public class StringUtils
	{
		public StringUtils ()
		{
		}
		
		public static string CleanContent(string content)
        {
			if (!string.IsNullOrEmpty(content))
			{
	            content = content.Replace("&", "&amp;")
	                             .Replace("<", "&lt;")
	                             .Replace(">", "&gt;");
			}
			
            return content;
        }
	}
}

