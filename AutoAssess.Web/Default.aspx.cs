
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using NHibernate;
using AutoAssess.Data.PersistentObjects;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace AutoAssess.Web
{
	public partial class Default : AutoAssessPage
	{
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			
			ISession s = this.CurrentScanSession;
			
			IList<PersistentEvent> events = s.CreateCriteria<PersistentEvent>()
				.Add(Restrictions.Eq("WebUserID", this.CurrentUser.ID))
				.List<PersistentEvent>();
			
			string html = "<ul>";
			
			foreach (var evnt in events.OrderByDescending(ev => ev.CreatedOn))
				html+="<li>" + evnt.Description + "</li>";
			
			html += "</ul>";
			
			divEvents.InnerHtml = html;
			
			IList<PersistentProfile> profiles = this.CurrentScanSession.CreateCriteria<PersistentProfile>()
				.Add(Restrictions.Eq ("WebUserID", this.CurrentUser.ID))
				.List<PersistentProfile>();
			
			lblNumberOfProfiles.Text = profiles.Count.ToString();
			lblProfilesHealth.Text = "Poor";
			lblCoverage.Text = "Excellent";
		}

	}
}

