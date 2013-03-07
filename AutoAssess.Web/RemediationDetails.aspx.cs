using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AutoAssess.Data.PersistentObjects;
using AutoAssess.Data.BusinessObjects;
using System.Web.UI.WebControls;
namespace AutoAssess.Web
{
	public partial class RemediationDetails : AutoAssessPage
	{
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			
			//if (string.IsNullOrEmpty(Request["cveid"]))
			//	return;
				
			PersistentNVD nvd = this.CurrentScanSession.CreateCriteria<PersistentNVD>()
				.Add(NHibernate.Criterion.Restrictions.Eq("CVEID", Request["cveid"]))
				.List<PersistentNVD>()
				.SingleOrDefault();
			
			if (nvd == null)
			{
				lblSummary.Text="Example Vulnerability Summary";
				
				lblAuthentication.Text = "<div style=\"display:inline-block;width:220px;\">Authentication required:</div>&nbsp;<b>" + "NONE" + "</b>";
			
				lblAvailabilityImpact.Text = "<div style=\"display:inline-block;width:220px;\">Availability Impact:</div>&nbsp;<b>" + "COMPLETE" + "</b>";
			
				lblComplexity.Text = "<div style=\"display:inline-block;width:220px;\">Vulnerability Complexity:</div>&nbsp;<b>" + "NOVICE" + "</b>";
			
				lblIntegrityImpact.Text = "<div style=\"display:inline-block;width:220px;\">Integrity Impact:</div>&nbsp;<b>" + "COMPLETE" + "</b>";
			
				lblVulnVector.Text = "<div style=\"display:inline-block;width:220px;\">Vulnerability vector:</div>&nbsp;<b>" + "NETWORK" + "</b>";
			
				lblScore.Text = "<div style=\"display:inline-block;width:220px;\">CVSS Score:</div>&nbsp;<b>" + "10" + "</b>";
				
				return;
			}
			
			lblSummary.Text = nvd.Summary;
			
			
			if (nvd.CVSS != null)
			{
				if (!string.IsNullOrEmpty(nvd.CVSS.Authentication))
					lblAuthentication.Text = "<div style=\"display:inline-block;width:200px;\">Authentication required:</div>&nbsp;<b>" + nvd.CVSS.Authentication + "</b>";
				
				if (!string.IsNullOrEmpty(nvd.CVSS.AvailabilityImpact))
					lblAvailabilityImpact.Text = "<div style=\"display:inline-block;width:200px;\">Availability Impact:</div>&nbsp;<b>" + nvd.CVSS.AvailabilityImpact + "</b>";
				
				if (!string.IsNullOrEmpty(nvd.CVSS.Complexity))
					lblComplexity.Text = "<div style=\"display:inline-block;width:200px;\">Vulnerability Complexity:</div>&nbsp;<b>" + nvd.CVSS.Complexity + "</b>";
				
				if (!string.IsNullOrEmpty(nvd.CVSS.IntegrityImpact))
					lblIntegrityImpact.Text = "<div style=\"display:inline-block;width:200px;\">Integrity Impact:</div>&nbsp;<b>" + nvd.CVSS.IntegrityImpact + "</b>";
				
				if (!string.IsNullOrEmpty(nvd.CVSS.Vector))
					lblVulnVector.Text = "<div style=\"display:inline-block;width:200px;\">Vulnerability vector:</div>&nbsp;<b>" + nvd.CVSS.Vector + "</b>";
				
				//score is a double, it can't be null (not double?)
				if (nvd.CVSS.Score != 0d)
					lblScore.Text = "CVSS Score:&nbsp;" + nvd.CVSS.Score.ToString();
			}
			
			foreach (PersistentNVDReference reference in nvd.References)
			{
				if (reference.Type == "VENDOR_ADVISORY")
				{
					lblVendorTitle.Text = "<h3><u>Vendor Advisories:</u></h3>";
					
					string html = "<p>" + reference.Source;
					
					html = html + ":&nbsp;<a href=\"" + reference.URL + "\">" + reference.Description + "</a></p>";
					divVendorLinkList.Controls.Add(new Label() { Text = html });
				}
				else if (reference.Type == "PATCH")
				{
					lblPatchTitle.Text = "<h3><u>Patches:</u></h3>";
					
					string html = "<p>" + reference.Source;
					
					html = html + ":&nbsp;<a href=\"" + reference.URL + "\">" + reference.Description + "</a></p>";
					divPatchLinkList.Controls.Add(new Label() { Text = html });
				}
				else if (reference.Type == "OTHER")
				{
				}
				else if (reference.Type == "UNKNOWN")
				{
					
				}
			}
			
		}
	}
}

