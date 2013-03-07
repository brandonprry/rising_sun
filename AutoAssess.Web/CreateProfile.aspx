<%@ Page Language="C#" Inherits="AutoAssess.Web.CreateProfile"  MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
	<div class="createProfileExplanationActive" id="divCreateProfileExplanation" runat="server">
		<p>
			When you are creating a profile, you are creating a small set of one or more assets that are related to each other.
			For instance, two web servers acting as load balancers and their SQL database would be a great fit for a profile.
		</p>
		<p>
			When adding an asset to a profile, you must verify ownership of both the domain, and the server itself.
			This is done in two ways. 
			The first way verifies that you own the domain. You must verify a key sent to an email contained in the WHOIS information of the URL.
			If this cannot be done, the asset will not be able to be scanned by our services.
		</p>
		<p>
			The second method of verification requires you to create a specific file containing specific data at the root of your main server (whatever server your domain name points directly to).
			Once the file is created, you will click the 'Verify File' button and we will check the file and its contents.
			If we cannot verify the file on the root of your main server, the asset will not be able to be scanned by our services.
		</p>
		<p>
			This process is timed at 20 minutes for security purposes and must be done all at once.
			If you leave the page or log out before finishing the verification, you must start over in 24 hours.
		</p>
		<p>
			<asp:Button Style="float: right" id="btnStartVerification" runat="server" Text="Start Verification" OnClick="btnStartVerification_Click" />
			<br /><br />
		</p>
	</div>	
	<div name="profileDetails" class="profileDetailsInactive" id="divProfileDetails" runat="server">
		<h2><u>Profile Details</u></h2>
		<br />
		<div class="textboxLabel">Profile name:</div><asp:TextBox id="txtProfileName" runat="server" Enabled="false" />
		<br />
		<div class="textboxLabel" style="vertical-align:top;">Profile description:</div><br />
		<asp:TextBox id="txtProfileDescription" runat="server" TextMode="MultiLine" Width="325" Rows="6" Enabled="false" />
		<br /><br />
		<h3>Profile Schedule</h3>
		<p>You profile schedule is how often we will run a profile scan to refresh our data and ensure no new vulnerabilities have appeared. Every 30 days is fine for most hosts.</p>
		<fieldset>
			<legend>Profile Schedule</legend>
			<asp:RadioButtonList id="rblProfileSchedule" runat="server" Enabled="false">
				<asp:ListItem Text="Every 7 days" Value="7" />
				<asp:ListItem Text="Every 30 days" Value="30" Selected="true" />
				<asp:ListItem Text="Every 90 days" Value="90" />
			</asp:RadioButtonList>
		</fieldset>
		<br />
		<asp:Button Style="float:right;" id="btnSetProfileDetails" runat="server" Text="Set Profile Details" OnClick="btnSetProfileDetails_Click" Enabled="false" />
		<br /><br />
	</div>
	<div name="addDomain" class="addHostContainerInactive" id="divAddHostContainer" runat="server">
		<h2><u>Add Domain</u></h2>
		<p>Example: <b>example.com</b></p>
		<p>Domain must <b>not</b> have any subdomains! (No www)</p>
		<br /><br />
		<div class="textboxLabel">Domain:</div><asp:TextBox ID="txtHostURL"  runat="server" Enabled="false" />
		<br />
		<asp:Button Style="float:right;" id="btnAddHost" runat="server" CssClass="profileAddHostButton" OnClick="btnAddHost_Click" Text="Get Verification Details" Enabled="false" />	
		<br />
		<br />
	</div>
	<div name="verifyEmail" class="multiEmailContainerInactive" id="divMultiEmailContainer" runat="server">
		<h2><u>Email verification</u></h2>	
		<br /><br />
		<div class="multiEmailContainerHeader"><asp:Label ID="lblMultiEmailContainerHeader" runat="server" Enabled="false" /></div>
		<asp:DropDownList id="ddlWhoisEmail" Width="300px" runat="server" Enabled="false" />
		<br />
		<asp:Button id="btnSendVerification"  Enabled="false" runat="server" Text="Send Verification Email" OnClick="btnSendVerification_Click" />
		<br /><br />
		<div class="textboxLabel">Paste key:</div><asp:TextBox id="txtEmailKey" runat="server" Enabled="false" />
		<br />
		<asp:Button Style="float:right;" id="btnVerifyEmailKey" runat="server" Text="Verify Key" OnClick="btnVerifyEmailKey_Click" Enabled="false" />
		<br />
		<br />
	</div>
	<div name="verifyFile" class="verificationFileContainerInactive" id="divVerificationFileContainer" runat="server">
		<h2><u>File Verification</u></h2>	
		<br /><br />
		<div class="verificationFileContainerHeader"><asp:Label id="lblVerificationFileHeader" runat="server" /></div>
		<div class="verificationFileDetails">
			<div class="textboxLabel">Full URI:</div><asp:Label id="lblVerificationURL" runat="server" />
			<br />
			<div class="textboxLabel">Filename:</div><asp:Label id="lblFilename" runat="server" />
			<br />
			<div class="textboxLabel">File Contents:</div><asp:Label id="lblFileContents" runat="server" />
			<br />
			<asp:Button Style="float:right;" id="btnVerifyFile" runat="server" Text="Verify File" OnClick="btnVerifyFile_Click" Enabled="false" />
			<br />
			<br />
		</div>	
	</div>
	<div name="finishUp" class="finishUpContainerInactive" id="divFinishUpContainer" runat="server">
		<h2><u>Finish up</u></h2>
		<p>You have successfully verified the domain and main web server. Once you save, you will be able to add more hosts to this profile.</p>
		<br />
		<asp:Button Style="float:right;" id="btnCreateProfile" runat="server" Text="Create and Go To Profile" OnClick="btnCreateProfile_Click" Enabled="false" />
		<br />
		<br /><br />
	</div>
	
</asp:Content>