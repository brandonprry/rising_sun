<%@ Page Language="C#" Inherits="AutoAssess.Web.Login" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="contentLogin" runat="server" ContentPlaceHolderID="main">
	<div class="login">	
		<asp:Label id="lblLoginError" runat="server" ForeColor="DarkRed" />
		<br /><br />
		<span style="width: 60px;display: inline-block;">Username:</span><asp:TextBox id="txtUsername" runat="server" />
		<br />
		<span style="width: 60px;display: inline-block;">Password:</span><asp:TextBox id="txtPassword" runat="server" TextMode="Password" />
		<br />
		<asp:Button id="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login"/>
		<br />
		<a href="Registration.aspx">New? Click here to register for an account</a>
	
	</div>
	
</asp:Content>