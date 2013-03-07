<%@ Page Language="C#" Inherits="AutoAssess.Web.Registration" MasterPageFile="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
	<fieldset style="margin-left: auto; margin-right:auto; width: 50%;">
		<legend>User Information</legend>	
		<div style="display:inline-block;width: 200px;"><b>Username:</b></div><asp:TextBox id="txtUsername" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Password:</b></div><asp:TextBox id="txtPassword" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Confirm Password:</b></div><asp:TextBox id="txtConfirmPassword" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Email Address:</b></div><asp:TextBox id="txtEmailAddress" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Number of Hosts:</b></div>
		<asp:DropDownList id="ddlNumberOfHosts" runat="server">
			<asp:ListItem Value="5">0-5</asp:ListItem>
			<asp:ListItem Value="25">6-25</asp:ListItem>
			<asp:ListItem Value="100">26-100</asp:ListItem>
		</asp:DropDownList>
		<br />
		<div style="display:inline-block;width: 200px;"><b>Type of Provider:</b></div>
		<asp:DropDownList id="ddlProvider" runat="server">
			<asp:ListItem Value="VPS">VPS</asp:ListItem>
			<asp:ListItem Value="Managed">Managed Colo</asp:ListItem>
			<asp:ListItem Value="Unmanaged">Unmanaged Colo</asp:ListItem>
			<asp:ListItem Value="Mixed">Mixed</asp:ListItem>
			<asp:ListItem Value="Other">Other</asp:ListItem>
		</asp:DropDownList>
		<br />
		<div style="display:inline-block;width: 200px;"><b>Main Security Concern</b></div>
		<asp:DropDownList id="ddlMainConcern" runat="server">
			<asp:ListItem Value="WebVulns">Web Vulnerabilities (OWASP Top 10)</asp:ListItem>
			<asp:ListItem Value="UnpatchedHosts">Unpatched Hosts</asp:ListItem>
			<asp:ListItem Value="Both">Both</asp:ListItem>
			<asp:ListItem Value="Other">Other</asp:ListItem>
		</asp:DropDownList>
	</fieldset>
	<br />
	<fieldset style="margin-left: auto; margin-right:auto; width: 50%;">
		<legend>Personal Information</legend>
		<div style="display:inline-block;width: 200px;"><b>First Name:</b></div><asp:TextBox id="txtFirstName" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Last Name:</b></div><asp:TextBox id="txtLastName" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Primary Website:</b></div><asp:TextBox id="txtPrimaryWebsite" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Primary Phone:</b></div><asp:TextBox id="txtPrimaryPhone" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Secondary Phone:</b></div><asp:TextBox id="txtSecondaryPhone" runat="server" />
	</fieldset>
	<br />
	<div style="margin-left: auto; margin-right:auto; width: 50%;">
		<asp:Button Style="float:right;" id="btnCreateUser" Text="Create User" OnClick="btnCreateUser_Click" runat="server" />
	</div>
</asp:Content>