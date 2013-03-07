<%@ Page Language="C#" Inherits="AutoAssess.Web.ViewScans" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>ViewScans</title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:GridView id="gvScans" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvScans_RowDataBound" >
			<Columns>
					<asp:BoundField DataField="Name" HeaderText="Name" />
					<asp:BoundField DataField="HasRun" HeaderText="Has Run" />
					<asp:TemplateField HeaderText="View Scan">
						<ItemTemplate>
							<asp:Button id="btnViewScan" Text="View scan..." runat="server" OnClick="btnViewScan_Click" />
						</ItemTemplate>
					</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</form>
</body>
</html>
