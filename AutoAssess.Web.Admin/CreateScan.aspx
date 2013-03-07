<%@ Page Language="C#" Inherits="AutoAssess.Web.Admin.CreateScan" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Create Scan</title>
</head>
<body>
	<form id="form1" runat="server">
		Hosts: <asp:TextBox id="txtHosts" runat="server" />
		<br />
		<asp:ListBox id="lstHosts" SelectionMode="Multiple" runat="server" Height="300" />
		<br />
		Profile Name: <asp:TextBox id="txtProfileName" runat="server" />
		<br /><br />
		<h3>Scan Options</h3>
		<br />
		<asp:CheckBox Text="Bruteforce" id="chkBruteforce" runat="server" /><br />
		<asp:CheckBox Text="SQLMap" id="chkSQLMap" runat="server" /><br />
		<asp:CheckBox Text="DSXS (Damn Small XSS Scanner)" id="chkDSXS" runat="server" /><br />
		<asp:CheckBox Text="OpenVAS" runat="server" id="chkOpenVAS" /><br />
		<asp:CheckBox Text="Nessus" runat="server" id="chkNessus" /><br />
		<asp:CheckBox Text="Nexpose" runat="server" id="chkNexpose" /><br />
		<asp:CheckBox Text="Metasploit" runat="server" id="chkMetasploit" /><br />
		<asp:CheckBox Text="Metasploit Discovers" runat="server" id="chkMetasploitDiscovers" /><br />
		<asp:CheckBox Text="Metasploit Bruteforces" runat="server" id="chkMetasploitBruteforces" /><br />
		<br />
		Scan Name: <asp:TextBox id="txtScanName" runat="server" />
		<br />
		User to own: <asp:DropDownList id="ddlUser" runat="server" />
		<br />
		<asp:Button id="btnCreateScan" runat="server" OnClick="btnCreateScan_Click" Text="Create Scan" />
	</form>
</body>
</html>
