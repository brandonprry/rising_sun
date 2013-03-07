<%@ Page Language="C#" Inherits="AutoAssess.Web.ThankYou" MasterPageFile="~/MasterPage.master" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" ID="mainContent" runat="server">
<h1><u>Your account has been successfully charged and your scan has been scheduled!</u></h1>
<asp:Button id="btnGoHome" runat="server" Text="Go Home" OnClick="btnGoHome_Click" />
</asp:Content>


