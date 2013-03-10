using System;

namespace AutoAssess.Data.Virtualbox
{
	public class VirtualMachine
	{
		public VirtualMachine ()
		{
		}
		
		public VirtualMachine (string vboxLine)
		{
			string[] tmp = System.Text.RegularExpressions.Regex.Split(vboxLine, "\" ");
		
			if (tmp.Length == 2)
			{
				this.Name = System.Text.RegularExpressions.Regex.Replace(tmp[0], "\"", string.Empty);
				this.Guid = Guid.Parse(tmp[1]);
			}
		}
		
		public VirtualMachine (string name, Guid guid)
		{
			this.Name = name;
			this.Guid = guid;
		}
		
		public virtual string Name { get; set; }
		public virtual Guid Guid { get; set; }
		public virtual string Username { get; set; }
		public virtual string Password { get; set; }
	}
}