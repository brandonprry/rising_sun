<%@ Page Language="C#" Inherits="AutoAssess.Web.Admin.CreateWebUser" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>CreateWebUser</title>
</head>
<body>
	<form id="form1" runat="server">
	
		Username: <asp:TextBox id="txtUsername" runat="server" /><br  />
		Password: <asp:TextBox id="txtPassword" runat="server" /><br />
		Email Addy: <asp:TextBox id="txtEmailAddress" runat="server" /><br />
		<asp:Button id="btnCreateUser" runat="server" OnClick="btnCreateUser_Click" />
	</form>
</body>
</html>
