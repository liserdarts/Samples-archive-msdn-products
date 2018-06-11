<%@ Page Language="C#" masterpagefile="~masterurl/default.master" title="My Hours Today" inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" %>
<%@ Register tagprefix="WebPartPages" namespace="Microsoft.SharePoint.WebPartPages" assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register tagprefix="SharePoint" namespace="Microsoft.SharePoint.WebControls" assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content id="Content1" runat="server" contentplaceholderid="PlaceHolderMain">

				<WebPartPages:SPProxyWebPartManager runat="server" id="ProxyWebPartManager">
	</WebPartPages:SPProxyWebPartManager>
	<WebPartPages:WebPartZone id="g_54DF79AAFA804AD391DA5FF84E2764DF" runat="server" title="Main"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
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
	color: #000000;
	font-size: 8pt;
	font-family: tahoma, sans-serif;
	height: 22px;
	font-weight: bold;
	text-align: right;
	border-top: 1px solid #f9f9f9;
	border-bottom: 1px solid #8ebbf5;
	padding-bottom: 3px;
	background-color: #ffffff;
}
</style>
</asp:Content>
<asp:Content id="Content3" runat="server" contentplaceholderid="PlaceHolderPageTitleInTitleArea">

		  My Hours Today</asp:Content>
