<%@ Page Language="C#" Inherits="AutoAssess.Web.ViewTraceroute" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>ViewTraceroute</title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:GridView id="gvRoutes" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvRoutes_RowDataBound" >
			<Columns>
				<asp:BoundField HeaderText="First Hostname" DataField="FirstHostname" />
				<asp:BoundField HeaderText="First IP Address" DataField="FirstIPAddress" />
				<asp:BoundField HeaderText="First Time Result" DataField="FirstResult" />	
				<asp:BoundField HeaderText="Second Hostname" DataField="SecondHostname" />
				<asp:BoundField HeaderText="Second IP Address" DataField="SecondIPAddress" />
				<asp:BoundField HeaderText="Second Time Result" DataField="SecondResult" />	
				<asp:BoundField HeaderText="Third Hostname" DataField="ThirdHostname" />
				<asp:BoundField HeaderText="Third IP Address" DataField="ThirdIPAddress" />
				<asp:BoundField HeaderText="Third Time Result" DataField="ThirdResult" />	
			</Columns>
		</asp:GridView>
	</form>
</body>
</html>
