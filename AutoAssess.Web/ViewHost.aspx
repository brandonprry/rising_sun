<%@ Page Language="C#" Inherits="AutoAssess.Web.ViewHost" MasterPageFile="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" runat="server" >
		
	<div class="currentProfileDetails">
		<div class="textboxLabel">Hostname:</div> <asp:Label id="lblHostname" runat="server" /><br />
		<div class="textboxLabel">IP Addressv4:</div> <asp:Label id="lblIPv4" runat="server" /><br />
		<div class="textboxLabel">Device Type:</div> <asp:Label id="lblDeviceType" runat="server" /><br />
		<div class="textboxLabel">OS:</div> <asp:Label id="lblOS" runat="server" /><br />
		<div class="textboxLabel">Network Distance:</div> <asp:Label id="lblNetworkDistance" runat="server" /><br />
	</div>
	<div style="margin-right:75px;border:1px solid #000;background:#fff;margin:20px;padding:5px;display:inline-block;vertical-align:top;" >
		<div class="dashboardItemHeader">Ports on host</div>
	<br />
		<asp:GridView id="gvPorts" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvPorts_RowDataBound" >
			<Columns>
				<asp:BoundField HeaderText="Service" DataField="ServiceName" />
				<asp:BoundField HeaderText="Port Number" DataField="Port" />
				<asp:BoundField HeaderText="OpenVAS Grade" DataField="OpenVASGrade" />
				<asp:BoundField HeaderText="Nexpose Grade" DataField="NexposeGrade" />
				<asp:BoundField HeaderText="Nessus Grade" DataField="NessusGrade" />
				<asp:BoundField HeaderText="Metasploit Credentials" DataField="MetasploitCredentials" />
				<asp:BoundField HeaderText="Metasploit Sessions" DataField="MetasploitExploits" />
				<asp:TemplateField HeaderText="View Port Details" >
						<ItemTemplate>
							<asp:Button id="btnViewPortDetails" runat="server" OnClick="btnViewPortDetails_Click" />
						</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</div>
</asp:Content>