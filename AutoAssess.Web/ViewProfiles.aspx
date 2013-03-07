<%@ Page Language="C#" Inherits="AutoAssess.Web.ViewProfiles" MasterPageFile="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
	<h1><u>Current Active Profiles:</u></h1>	
	<div style="margin-left:auto;margin-right:auto; margin-top:30px;border:1px solid #000;background:#fff;width:90%; min-height: 500px;">	
		<asp:GridView Style="margin-left:auto;margin-right:auto;margin-top:10px;" id="gvProfiles" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProfiles_RowDataBound" >
			<Columns>
				<asp:BoundField HeaderText="Name" DataField="Name" />
				<asp:TemplateField HeaderText="View Profile History">
					<ItemTemplate>
						<asp:Button id="lnkViewProfile" runat="server" OnClick="lnkViewProfile_Click" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Schedule Run">
					<ItemTemplate>
						<asp:Button id="btnScheduleNewRun" runat="server" Text="Schedule New Run" OnClick="btnScheduleNewRun_Click" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<asp:Button id="btnCreateScanFromProfile" runat="server" Text="Create scan from profile" OnClick="btnCreateScanFromProfile_Click" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<asp:Button id="btnDeleteProfile" runat="server" Text="Delete profile" OnClick="btnDeleteProfile_Click" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</div>
</asp:Content>