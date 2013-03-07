<%@ Page Language="C#" Inherits="AutoAssess.Web.ViewPortDetails" MasterPageFile="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
	
	
	
	<div style="display:inline-block;width:400px;border:1px solid #000; background:#fff;margin-top:20px;" id="divNiktoResults" runat="server">
		<asp:Label id="lblNiktoResultsHeader" runat="server" />
		<asp:Label id="lblNiktoResults" runat="server" />
	</div>
	<div style="display:inline-block;width: 250px;vertical-align:top;">
	<div style="display:inline-block;vertical-align:top; width:450px;margin-left:30px;">
		<br /><br />
		<h3><u><asp:Label id="lblOpenVASPortResults" runat="server" /></u></h3>
		<asp:GridView id="gvOpenVASPortResults" Rows="5" runat="server" AllowPaging="true"  OnRowDataBound="gvOpenVASPortResults_RowDataBound" Style="background:#fff;" AutoGenerateColumns="false">
			<Columns>
				<asp:BoundField HeaderText="Threat" DataField="Threat" />
				<asp:BoundField HeaderText="Name" DataField="Name" />
			</Columns>
		</asp:GridView>
	</div>
	<div style="display:inline-block;vertical-align:top; width:450px;margin-left:30px;">
		<br /><br />
		<h3><u><asp:Label id="lblNessusPortResults" runat="server" /></u></h3>
		<asp:GridView id="gvNessusPortResults"  Rows="5" AllowPaging="true" runat="server" OnRowDataBound="gvNessusPortResults_RowDataBound" Style="background:#fff;" AutoGenerateColumns="false">
			<Columns>
				<asp:BoundField HeaderText="Threat" DataField="Threat" />
				<asp:BoundField HeaderText="Name" DataField="Name" />
			</Columns>
		</asp:GridView>
	</div>
	<div style="display:inline-block;vertical-align:top; width:450px;margin-left:30px;">
		<br /><br />
		<h3><u><asp:Label id="lblNexposePortResults" runat="server" /></u></h3>
		<asp:GridView id="gvNexposePortResults" AllowPaging="true" runat="server" OnRowDataBound="gvNexposePortResults_RowDataBound" Style="background:#fff;" AutoGenerateColumns="false">
			<Columns>
				<asp:BoundField HeaderText="PCI Compliance" DataField="Threat" />
				<asp:BoundField HeaderText="Info" DataField="Name"  />
			</Columns>
		</asp:GridView>
	</div>
	</div>
	<div  style="display:inline-block;width:400px;" id="divSNMPResults">
	<asp:Label id="lblSNMPResultsHeader" runat="server" />
		<asp:Label id="lblSNMPResults" runat="server" />
	</div>
	
	<div style="display:inline-block;width:400px;" id="divSMBScan">
	<asp:Label id="lblSMBScanHeader" runat="server" />
		<asp:Label id="lblSMBScan" runat="server" />
	</div>
	
	
		<h3><u><asp:Label id="lblSQLInjections" runat="server" Visible="false"/></u></h3>
		<asp:GridView id="gvSQLInjections"  AllowPaging="true" Style="background:#fff;"  runat="server" OnRowDataBound="gvSQLInjections_RowDataBound" AutoGenerateColumns="false">
			<Columns>
				<asp:TemplateField HeaderText="Method">
					<ItemTemplate>
						<asp:Label id="lblMethod" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Known Exploitable">
					<ItemTemplate>
						<asp:Label id="lblKnownExploitable" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="URL">
					<ItemTemplate>
						<asp:Label id="lblURL" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Parameter">
					<ItemTemplate>
						<asp:Label id="lblParameter" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<h3><u><asp:Label id="lblPossibleSQLInjections" runat="server" /></u></h3>
		<asp:GridView id="gvPossibleInjectionPoints"  Style="background:#fff;" AllowPaging="true" Rows="5" runat="server" OnRowDataBound="gvPossibleInjectionPoints_RowDataBound" AutoGenerateColumns="false">
			<Columns>
				<asp:TemplateField HeaderText="Method">
					<ItemTemplate>
						<asp:Label id="lblPossibleMethod" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="URL">
					<ItemTemplate>
						<asp:Label id="lblPossibleURL" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Parameter">
					<ItemTemplate>
						<asp:Label id="lblPossibleParameter" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<h3><u><asp:Label id="lblXSS" runat="server" /></u></h3>
		<asp:GridView id="gvXSS" AllowPaging="true" Rows="5" Style="background:#fff;"  runat="server" OnRowDataBound="gvXSS_RowDataBound" />
		<h3><u><asp:Label id="lblIncludes" runat="server" /></u></h3>
		<asp:GridView id="gvIncludes"  Style="background:#fff;" AllowPaging="true" Rows="5" runat="server" OnRowDataBound="gvIncludes_RowDataBound" />
		<h3><u><asp:Label id="lblCommandExecution" runat="server" /></u></h3>
		<asp:GridView id="gvCommandExecution" Style="background:#fff;" AllowPaging="true" Rows="5" runat="server" OnRowDataBound="gvCommandExecution_RowDataBound" />
	<br /><br /><br />
</asp:Content>