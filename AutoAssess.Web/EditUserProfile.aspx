<%@ Page Language="C#" Inherits="AutoAssess.Web.EditUserProfile" MasterPageFile="~/MasterPage.master" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" ID="mainContent" runat="server">
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
	</fieldset>
	<br />
	<fieldset style="margin-left: auto; margin-right:auto; width: 50%;">
		<legend>Personal Information</legend>
		<div style="display:inline-block;width: 200px;"><b>First Name:</b></div><asp:TextBox id="txtFirstName" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Middle Name:</b></div><asp:TextBox id="txtMiddleName" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Last Name:</b></div><asp:TextBox id="txtLastName" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Primary Phone:</b></div><asp:TextBox id="txtPrimaryPhone" runat="server" />
		<br />
		<div style="display:inline-block;width: 200px;"><b>Secondary Phone:</b></div><asp:TextBox id="txtSecondaryPhone" runat="server" />
	</fieldset>
	<asp:Button style="margin-left: 256px" id="btnEditUser" Text="Edit User" OnClick="btnEditUser_Click" runat="server" />
</asp:Content>


