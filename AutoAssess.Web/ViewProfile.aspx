<%@ Page Language="C#" Inherits="AutoAssess.Web.ViewProfile"  MasterPageFile="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
				
	<h2><u>Hosts</u></h2>
	<a href="/AddHostToProfile.aspx">Add new host to profile</a>
	<div style="background:#fff;">
	<asp:GridView id="gvHosts" runat="server" AutoGenerateColumns="false" Style="width:100%;" OnRowDataBound="gvHosts_OnRowDataBound">
		<Columns>
			<asp:TemplateField HeaderText="Host">
				<ItemTemplate>
					<asp:Button id="btnGotoHost" runat="server" OnClick="btnGotoHost_Click" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Host Name">
				<ItemTemplate>
					<asp:Label id="lblHostname" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Overall Risk">
				<ItemTemplate>
					<asp:Label id="lblOverallRisk" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Nessus Grade">
				<ItemTemplate>
					<asp:Label id="lblNessusGrade" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Nexpose Grade">
				<ItemTemplate>
					<asp:Label id="lblNexposeGrade" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="OpenVAS Grade">
				<ItemTemplate>
					<asp:Label id="lblOpenVASGrade" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Metasploit Grade">
				<ItemTemplate>
					<asp:Label id="lblMetasploitGrade" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Exploitable">
				<ItemTemplate>
					<asp:Label id="lblExploitable" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
		</div>
</asp:Content>