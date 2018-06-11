<%@ Page Language="C#" masterpagefile="~masterurl/default.master" title="My Logged Hours" inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" %>
<%@ Register tagprefix="WebPartPages" namespace="Microsoft.SharePoint.WebPartPages" assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register tagprefix="SharePoint" namespace="Microsoft.SharePoint.WebControls" assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content id="Content1" runat="server" contentplaceholderid="PlaceHolderMain">

				<WebPartPages:SPProxyWebPartManager runat="server" id="ProxyWebPartManager">
	</WebPartPages:SPProxyWebPartManager>
	<WebPartPages:WebPartZone id="g_61F777F6D29D45CBBF952E1350EB329D" runat="server" title="Main"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
</asp:Content>
<asp:Content id="Content2" runat="server" contentplaceholderid="PlaceHolderAdditionalPageHead">

	<style type="text/css">
.style1 {
	color: #000000;
	font-size: 8pt;
	font-family: tahoma, sans-serif;
	vertical-align: top;
	text-align: right;
}
.style2 {
	font-weight: normal;
	color: #b2b2b2;
	text-align: right;
	text-decoration: none;
	vertical-align: top;
	font-size: 8pt;
	font-family: tahoma, sans-serif;
	white-space: nowrap;
	padding-top: 1px;
	padding-bottom: 0px;
	background-color: #f2f2f2;
	background-image: url('/TimecardManagementTemplate/_layouts/images/viewheadergrad.gif');
	background-repeat: repeat-x;
}
</style>
</asp:Content>
<asp:Content id="Content3" runat="server" contentplaceholderid="PlaceHolderPageTitleInTitleArea">

		  My Hours by Project</asp:Content>

