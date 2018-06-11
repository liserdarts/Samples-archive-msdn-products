<%@ Page language="C#" MasterPageFile="~masterurl/default.master"    Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint,Version=12.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %> <%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,multipages_homelink_text%>" EncodeMethod="HtmlEncode"/> 
	- 
	<SharePoint:ProjectProperty Property="Title" runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageImage" runat="server">
	<WebPartPages:SPProxyWebPartManager runat="server" id="ProxyWebPartManager"></WebPartPages:SPProxyWebPartManager><IMG SRC="/TimecardManagement/_layouts/images/blank.gif" width=1 height=1 alt=""></asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
			<label class="ms-hidden">
			<SharePoint:ProjectProperty Property="Title" runat="server"/></label>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleBreadcrumb" runat="server"/>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleAreaClass" runat="server">
<style type="text/css">
TD.ms-titleareaframe, .ms-pagetitleareaframe {
	height: 10px;
}
Div.ms-titleareaframe {
	height: 100%;
}
.ms-pagetitleareaframe table {
	background: none;
	height: 10px;
}
.style1 {
	margin-left: 40px;
}
</style>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">
	<META Name="CollaborationServer" Content="SharePoint Team Web Site">
	<script type="text/javascript">
	var navBarHelpOverrideKey = "wssmain";
	</script>
	<script type="text/javascript" src="dateutil.js"></script>
	<meta http-equiv="refresh" content="300">
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderSearchArea" runat="server">
	<SharePoint:DelegateControl runat="server"
		ControlId="SmallSearchInputBox"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderLeftActions" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageDescription" runat="server"/>
<asp:Content ContentPlaceHolderId="PlaceHolderBodyAreaClass" runat="server">
<style type="text/css">
.ms-bodyareaframe {
	padding: 0px;
}
</style>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
	<table cellspacing="0" border="0" width="100%">
	  <tr>
	   <td class="ms-pagebreadcrumb">
		<asp:SiteMapPath SiteMapProvider="SPContentMapProvider" id="ContentMap" SkipLinkText="" NodeStyle-CssClass="ms-sitemapdirectional" runat="server"/>
	   </td>
	  </tr>
	  <tr>
	   <td class="ms-webpartpagedescription">
		<SharePoint:ProjectProperty Property="Description" runat="server"/></td>
	  </tr>
	  <tr>
		<td>
		 <table width="100%" cellpadding=0 cellspacing=0 style="padding: 5px 10px 10px 10px;">
		  <tr>
		   <td valign="top" width="70%">
			   <WebPartPages:WebPartZone id="g_E874CBBBC8314291A40ACDDC5373C58C" runat="server" title="Main"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
		   </td>
		  </tr>
		 </table>
		</td>
	  </tr>
	</table>
</asp:Content>
