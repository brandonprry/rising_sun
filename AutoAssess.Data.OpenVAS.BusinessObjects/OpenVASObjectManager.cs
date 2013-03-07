using System;
using System.Collections.Generic;
using System.Xml;
using AutoAssess.Data.OpenVAS;
using openvassharp;

namespace AutoAssess.Data.OpenVAS.BusinessObjects
{
	public class OpenVASObjectManager : OpenVASManager, IDisposable
	{
		private string _defaultFormat;
		private string _defaultSortOrder;
		private string _defaultSortField;
		private bool _useSpecialTimeout;

		public OpenVASObjectManager ()
		{
			_defaultFormat = string.Empty;
			_defaultSortField = string.Empty;
			_defaultSortOrder = string.Empty;
			_useSpecialTimeout = false;
		}
		
		public OpenVASObjectManager (OpenVASManagerSession session) : base (session)
		{
			_defaultFormat = string.Empty;
			_defaultSortField = string.Empty;
			_defaultSortOrder = string.Empty;
			_useSpecialTimeout = false;
		}
		
		public bool UseSpecialTimeout
		{
			get
			{
				return _useSpecialTimeout;
			}
			set
			{
				_useSpecialTimeout = value;
			}
		}
		
		public string DefaultFormat { 
			get
			{
				if (_defaultFormat == null)
					return string.Empty;
				
				return _defaultFormat;
			}
			set
			{
				_defaultFormat = value;
			}
		}
		
		public string DefaultSortOrder {
			get
			{
				if (_defaultSortOrder == null)
					return "asc";
				
				return _defaultSortOrder;
			}
			set
			{
				_defaultSortOrder = value;
			}
		}
		
		public string DefaultSortField {
			get 
			{
				if (_defaultSortField == null)
					return string.Empty;
				
				return _defaultSortField;
			}
			set
			{
				_defaultSortField = value;
			}
		}
		
		public OpenVASAgent CreateAgent(OpenVASAgent agent)
		{
			if (agent.RemoteAgentID != Guid.Empty)
				throw new Exception("This agent has already been created.");
			
			XmlDocument response = this.CreateAgent(agent.Installer, agent.Signature, agent.Name);
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "id")
					agent.RemoteAgentID = new Guid(attr.Value);
			}
			
			return agent;
		}
        
		public OpenVASConfig CreateConfigByCopy(OpenVASConfig config)
		{
			if (config.RemoteConfigID != Guid.Empty)
				throw new Exception("This config has already been created.");
			
			XmlDocument response = this.CreateConfigByCopy(config.Name, config.RemoteConfigID.ToString());
			
			OpenVASConfig newConfig = new OpenVASConfig();
			newConfig.Name = config.Name;
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "id")
					newConfig.RemoteConfigID = new Guid(attr.Value);
			}
			
			return newConfig;
		}
		
		public OpenVASEscalator CreateEscalator(OpenVASEscalator escalator)
		{
			if (escalator.RemoteEscalatorID != Guid.Empty)
				throw new Exception("This escalator has already been created.");
			
			XmlDocument response = this.CreateEscalator(escalator.Name, escalator.Condition.Condition, escalator.Condition.ConditionData, escalator.Condition.ConditionName);
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "id")
					escalator.RemoteEscalatorID = new Guid(attr.Value);
			}
			
			return escalator;
		}
		
		public OpenVASLSCCredential CreateLSCCredential(OpenVASLSCCredential credentials)
		{
			if (credentials.RemoteCredentialsID != Guid.Empty)
				throw new Exception("This set of credentials as already been created.");
			
			XmlDocument response = this.CreateLSCCredential(credentials.Name, credentials.LoginUsername, credentials.Password, credentials.Comment);
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "id")
					credentials.RemoteCredentialsID = new Guid(attr.Name);
			}
			
			return credentials;
		}
		
		public OpenVASNote CreateNote(OpenVASNote note)
		{
			if (note.RemoteNoteID != Guid.Empty)
				throw new Exception("This note has already been created.");
			
			XmlDocument response = this.CreateNote(note.Content, note.NVT.OID, note.Comment, note.Hosts, note.Port.ToString(), note.Report.RemoteReportID.ToString(), note.Task.RemoteTaskID.ToString(), note.ThreatLevel);
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "id")
					note.RemoteNoteID = new Guid(attr.Value);
			}
			
			return note;
		}
		
		public OpenVASOverride CreateOverride(OpenVASOverride or)
		{
			if (or.RemoteOverrideID != Guid.Empty)
				throw new Exception("This override has already been created.");
			
			XmlDocument response = this.CreateOverride(or.Content, or.NVT.OID, or.Comment, or.Hosts, or.NewThreat, or.Port.ToString(), or.Report.RemoteReportID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "id")
					or.RemoteOverrideID = new Guid(attr.Value);
			}
			
			return or;
		}
		
		public OpenVASTarget CreateTarget(OpenVASTarget target)
		{
			if (target.RemoteTargetID != Guid.Empty)
				throw new Exception("This target has already been created.");
			
			XmlDocument response = this.CreateTarget(target.Name, target.Comment, target.Hosts, target.SMBCredentials.RemoteCredentialsID.ToString(), target.SSHCredentials.RemoteCredentialsID.ToString(), target.PortRange);
			
			foreach (XmlAttribute attr in response.FirstChild.Attributes)
			{
				if (attr.Name == "id")
					target.RemoteTargetID = new Guid(attr.Value);
			}
			
			return target;
		}
		
		public OpenVASTask CreateTask(OpenVASTask task)
		{
			if (task.RemoteTaskID != Guid.Empty)
				throw new Exception("This task has been created already.");
			
			string name, comment, configID, targetID, escalatorID, scheduleID, slaveID;
			
			name = task.Name;
			comment = task.Comment;
			configID = task.Config == null ? string.Empty : task.Config.RemoteConfigID.ToString();
			targetID = task.Target == null ? string.Empty : task.Target.RemoteTargetID.ToString();
			escalatorID = task.Escalator == null ? string.Empty : task.Escalator.RemoteEscalatorID.ToString();
			scheduleID = task.Schedule == null ? string.Empty : task.Schedule.RemoteScheduleID.ToString();
			slaveID = task.Slave == null ? string.Empty : task.Slave.RemoteSlaveID.ToString();
			
			XmlDocument response = this.CreateTask(name, comment, configID, targetID, escalatorID, scheduleID, slaveID);
			
			foreach (XmlAttribute attr in response.FirstChild.Attributes)
			{
				if (attr.Name == "id")
					task.RemoteTaskID = new Guid(attr.Value);
			}
			
			return task;
		}
		
		public void DeleteConfig(OpenVASConfig config)
		{
			if (config.RemoteConfigID == Guid.Empty)
				throw new Exception("Can't delete config that hasn't been created.");
			
			XmlDocument response = this.DeleteConfig(config.RemoteConfigID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting config failed");
			}
			
		}
		
		public void DeleteEscalator(OpenVASEscalator escalator)
		{
			if (escalator.RemoteEscalatorID == Guid.Empty)
				throw new Exception("Can't delete config that hasn't been created.");
			
			XmlDocument response = this.DeleteEscalator(escalator.RemoteEscalatorID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting escalator failed");
			}
		}
		
		public void DeleteLSCCredential(OpenVASLSCCredential credentials)
		{
			if (credentials.RemoteCredentialsID == Guid.Empty)
				throw new Exception("Can't delete credentials that haven't been created.");
			
			XmlDocument response = this.DeleteLSCCredential(credentials.RemoteCredentialsID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting credentials failed");
			}
		}
		
		public void DeleteNote(OpenVASNote note)
		{
			if (note.RemoteNoteID == Guid.Empty)
				throw new Exception("Can't delete note that hasn't been created");
			
			XmlDocument response = this.DeleteNote(note.RemoteNoteID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting note failed.");
			}
		}
		
		public void DeleteOverride(OpenVASOverride or)
		{
			if (or.RemoteOverrideID == Guid.Empty)
				throw new Exception("Can't delete override that hasn't been created.");
			
			XmlDocument response = this.DeleteOverride(or.RemoteOverrideID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting Override Failed.");
			}
		}
		
		public void DeleteReport(OpenVASScan report)
		{
			if (report.RemoteReportID == Guid.Empty)
				throw new Exception("Can't delete report that hasn't been created.");
			
			XmlDocument response = this.DeleteReport(report.RemoteReportID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting report failed.");
			}
		}
		
		public void DeleteReportFormat(OpenVASReportFormat format)
		{
			if (format.RemoteReportFormatID == Guid.Empty)
				throw new Exception("Can't delete report format that hasn't been created.");
			
			XmlDocument response = this.DeleteReportFormat(format.RemoteReportFormatID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting report format failed.");
			}	
		}
		
		public void DeleteSchedule(OpenVASSchedule schedule)
		{
			if (schedule.RemoteScheduleID == Guid.Empty)
				throw new Exception("Can't delete schedule that hasn't been created.");
			
			XmlDocument response = this.DeleteSchedule(schedule.RemoteScheduleID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting schedule failed.");
			}
		}
		
		public void DeleteSlave(OpenVASSlave slave)
		{
			if (slave.RemoteSlaveID == Guid.Empty)
				throw new Exception("Can't delete slave that hasn't been created.");
			
			XmlDocument response = this.DeleteSlave(slave.RemoteSlaveID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting slave failed.");
			}
		}
		
		public void DeleteTarget(OpenVASTarget target)
		{
			if (target.RemoteTargetID == Guid.Empty)
				throw new Exception("Can't delete target that hasn't been created.");
			
			XmlDocument response = this.DeleteTarget(target.RemoteTargetID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting target failed.");
			}
		}
		
		public void DeleteTask(OpenVASTask task)
		{
			if (task.RemoteTaskID == Guid.Empty)
				throw new Exception("Can't delete task that hasn't been created.");
			
			XmlDocument response = this.DeleteTask(task.RemoteTaskID.ToString());
			
			foreach (XmlAttribute attr in response.Attributes)
			{
				if (attr.Name == "status" && attr.Value != "200")
					throw new Exception("Deleting task failed.");
			}
		}
		
		public List<OpenVASAgent> GetAllAgents()
		{
			XmlDocument response = this.GetAgents(string.Empty, this.DefaultFormat, this.DefaultSortOrder, this.DefaultSortField);
			
			List<OpenVASAgent> agents = new List<OpenVASAgent>();
			
			foreach (XmlNode node in response.ChildNodes)
			{
				if (node.Name == "agent")
					agents.Add(new OpenVASAgent(node));
			}
			
			return agents;
		}
		
		public OpenVASAgent GetAgent(Guid agentID)
		{
			XmlDocument response = this.GetAgents(agentID.ToString(), this.DefaultFormat, this.DefaultSortOrder, this.DefaultSortField);
			
			foreach (XmlNode node in response.ChildNodes)
			{
				if (node.Name == "agent")
					return new OpenVASAgent(node);
			}
			
			return null;
		}
		
		public List<OpenVASConfig> GetAllConfigs()
		{
			XmlDocument configXml = base.GetConfigs(string.Empty, false, false, string.Empty, string.Empty);
			List<OpenVASConfig> configs = new List<OpenVASConfig>();
			
			foreach (XmlNode node in configXml.FirstChild.ChildNodes)
			{
				if (node.Name == "config")
					configs.Add(new OpenVASConfig(node));
			}
			
			return configs;
		}
		
		public List<OpenVASNVT> GetAllNVTs(bool getPreferences, bool getPreferenceCount, bool getDetails, string family, string configID)
		{
			XmlDocument response = this.GetNVTs(string.Empty, getDetails, getPreferences, getPreferenceCount, this.UseSpecialTimeout, configID, family, this.DefaultSortOrder, this.DefaultSortField);
			List<OpenVASNVT> nvts = new List<OpenVASNVT>();
			
			foreach (XmlNode node in response.FirstChild.ChildNodes)
			{
				if (node.Name == "nvt")
					nvts.Add(new OpenVASNVT(node));
			}
			
			return nvts;
		}
		
		public OpenVASNVT GetNVT(string oid, bool getPreferences, bool getPreferenceCount, bool getDetails)
		{
			XmlDocument response = this.GetNVTs(oid, getDetails, getPreferences, getPreferenceCount, this.UseSpecialTimeout, string.Empty, string.Empty, this.DefaultSortOrder, this.DefaultSortField);
			
			foreach (XmlNode node in response.ChildNodes)
			{
				if (node.Name == "nvt")
					return new OpenVASNVT(node);
			}
			
			return null;
		}
		
		public List<OpenVASNVTFamily> GetAllNVTFamilies()
		{
			XmlDocument response = this.GetNVTFamilies(this.DefaultSortOrder);
			
			List<OpenVASNVTFamily> families = new List<OpenVASNVTFamily>();
			
			foreach (XmlNode node in response.FirstChild.ChildNodes)
			{
				if (node.Name == "families")
					foreach (XmlNode c in node.ChildNodes)
						families.Add(new OpenVASNVTFamily(c));
			}
			
			return families;
		}
		
		public void Dispose()
		{
		}
	}
}

