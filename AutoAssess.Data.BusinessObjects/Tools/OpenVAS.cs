using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AutoAssess.Data.OpenVAS.BusinessObjects;
using AutoAssess.Data.OpenVAS;

namespace AutoAssess.Data.BusinessObjects
{
	public class OpenVASTool
	{
		private OpenVASToolOptions _options;
		
		public OpenVASTool ()
		{
		}
		
		public OpenVASTool(OpenVASToolOptions options)
		{
			_options = options;
		}
		
		public string Name { get { return "OpenVAS"; } }
		public string Description { get { return string.Empty; }}
		
		public IToolOptions Options 
		{ 
			get
			{
				if (_options == null)
					_options = new OpenVASToolOptions();
				
				return _options;
			}
			
			set
			{
				if (value is OpenVASToolOptions)
					_options = (OpenVASToolOptions)value;
			}
		}
	}
}
