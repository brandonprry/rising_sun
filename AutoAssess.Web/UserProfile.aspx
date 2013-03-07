<%@ Page Language="C#" Inherits="AutoAssess.Web.UserProfile" MasterPageFile="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" runat="server">	
	<div class="userInfo">
		<a href="/EditUserProfile.aspx">Edit/Update your user information</a><br /><br />
		<span style="width: 200px;display: inline-block;">Username:</span><asp:Label id="lblUsername" runat="server" />
		<br />
		<span style="width: 200px;display: inline-block;">Email Address:</span><asp:Label id="lblEmailAddress" runat="server" />
		<br />
		<span style="width: 200px;display: inline-block;">First Name:</span><asp:Label id="lblFirstName" runat="server" />
		<br />
		<span style="width: 200px;display: inline-block;">Middle Name:</span><asp:Label id="lblMiddleName" runat="server" />
		<br />
		<span style="width: 200px;display: inline-block;">Last Name:</span><asp:Label id="lblLastName" runat="server" />
		<br />
		<span style="width: 200px;display: inline-block;">Primary Phone:</span><asp:Label id="lblPrimaryPhone" runat="server" />
		<br />
		<span style="width: 200px;display: inline-block;">Secondary Phone:</span><asp:Label id="lblSecondaryPhone" runat="server" />
		<br />
	</div>
</asp:Content>