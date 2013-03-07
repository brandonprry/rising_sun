<%@ Page Language="C#" Inherits="AutoAssess.Web.RemediationDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>RemediationDetails</title>
</head>
<body>
	<form id="form1" runat="server">
			<asp:Label id="lblSummary" runat="server" />
			<br /><br /><br />
			<asp:Label id="lblScore" runat="server" />
			<br /><br />
			<h3><u>Risk:</u></h3>
			<div id="divRisks" runat="server" >
				<asp:Label id="lblAuthentication" runat="server" /><br />
				<asp:Label id="lblAvailabilityImpact" runat="server" /><br />
				<asp:Label id="lblComplexity" runat="server" />
				<asp:Label id="lblConfidentialityImpact" runat="server" /><br />
				<asp:Label id="lblIntegrityImpact" runat="server" /><br />
				<asp:Label id="lblVulnVector" runat="server" /><br />
			</div>
			<br />
			<h3><u><asp:Label id="lblPatchTitle" runat="server" /></u></h3>
			<div id="divPatchLinkList" runat="server" />
			<br /><br />
			<h3><u><asp:Label id="lblVendorTitle" runat="server" /></u></h3>
			<div id="divVendorLinkList" runat="server" />
			<br /><br />
			<h3><u><asp:Label id="lblOtherTitle" runat="server" /></u></h3>
			<div id="divOtherLinkList" runat="server" />
			<br /><br />
			
	</form>
</body>
</html>
