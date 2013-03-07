<%@ Page Language="C#" Inherits="AutoAssess.Web.CreditCardPayment" MasterPageFile="~/MasterPage.master" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ContentPlaceHolderID="main" ID="mainContent" runat="server">
<fieldset>
	<legend>Billing Information</legend>
	<p>First Name: <asp:TextBox id="txtFirstName" runat="server" />	</p>
	<p>Last Name: <asp:TextBox id="txtLastName" runat="server" /> </p>
	<p>Credit Card Number: <asp:TextBox id="txtCreditCard" runat="server" /> </p>
	<p>Security Code: <asp:TextBox id="txtSecurityCode" runat="server" /></p>
	<p>Expiry Date: <asp:TextBox id="txtExpiryDate" runat="server" /></p>
	<p>Street Address: <asp:TextBox id="txtStreetAddress" runat="server" /></p>
	<p>City:<asp:TextBox id="txtCity" runat="server" /></p>
	<p>State:<asp:TextBox id="txtState" runat="server" /></p>
	<p>ZIP Code: <asp:TextBox id="txtZIP" runat="server" /></p>
	<p><asp:Button id="btnSubmit" Text="Submit Payment" runat="server" /></p>
</fieldset>	
</asp:Content>


