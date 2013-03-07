using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	[Serializable]
	public class OpenVASEscalator
	{
		public OpenVASEscalator ()
		{
		}
		
		public virtual string Name { get; set; }
		
		public virtual EscalatorCondition Condition { get; set; }
		
		public virtual EscalatorEvent Event { get; set; }
		
		public virtual EscalatorMethod Method { get; set; }
		
		public virtual Guid RemoteEscalatorID { get; set; }
		
		public virtual List<IOpenVASObject> Parse(XmlDocument response)
		{
			List<IOpenVASObject> objects = new List<IOpenVASObject>();
			
			return objects;
		}
		 
	}
	
	public class EscalatorCondition 
	{
		public virtual string Condition { get; set; }
		
		public virtual string ConditionData { get; set; }
		
		public virtual string ConditionName { get; set; }
	}
	
	public class EscalatorEvent 
	{
		
		public virtual string Event { get; set; }
		
		public virtual string EventData { get; set; }
		
		public virtual string EventName { get; set; }
	}
	
	public class EscalatorMethod 
	{
		public virtual string Method { get; set; }
		
		public virtual  List<EscalatorMethodData> MethodData { get; set; }
		
		public virtual string MethodName { get; set; }
	}
	
	public class EscalatorMethodData 
	{
		
		public virtual string Data { get; set; }
		
		public virtual string Name { get; set; }
	}
	
	
	internal static class EscalatorExtensions
	{
		public static string ToXML<T>(this List<T> methodData)
		{
			if (typeof(T) != typeof(EscalatorMethodData))
				throw new Exception("For escalator method data only");
			
			return string.Empty;
		}
	}
}

