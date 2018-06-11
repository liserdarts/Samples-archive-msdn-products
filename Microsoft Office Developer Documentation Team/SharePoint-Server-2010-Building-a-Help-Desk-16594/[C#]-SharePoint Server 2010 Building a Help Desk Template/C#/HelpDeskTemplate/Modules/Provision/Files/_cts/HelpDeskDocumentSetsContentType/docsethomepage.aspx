<%@ Register TagPrefix="wssuc" TagName="LinksTable" src="/_controltemplates/LinksTable.ascx" %> <%@ Register TagPrefix="wssuc" TagName="InputFormSection" src="/_controltemplates/InputFormSection.ascx" %> <%@ Register TagPrefix="wssuc" TagName="InputFormControl" src="/_controltemplates/InputFormControl.ascx" %> <%@ Register TagPrefix="wssuc" TagName="LinkSection" src="/_controltemplates/LinkSection.ascx" %> <%@ Register TagPrefix="wssuc" TagName="ButtonSection" src="/_controltemplates/ButtonSection.ascx" %> <%@ Register TagPrefix="wssuc" TagName="ActionBar" src="/_controltemplates/ActionBar.ascx" %> <%@ Register TagPrefix="wssuc" TagName="ToolBar" src="/_controltemplates/ToolBar.ascx" %> <%@ Register TagPrefix="wssuc" TagName="ToolBarButton" src="/_controltemplates/ToolBarButton.ascx" %> <%@ Register TagPrefix="wssuc" TagName="Welcome" src="/_controltemplates/Welcome.ascx" %>
<%@ Register Tagprefix="wssawc" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page language="C#" MasterPageFile="~masterurl/default.master"  inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint,Version=14.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" meta:webpartpageexpansion="full" meta:progid="SharePoint.WebPartPage.Document"     %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %> <%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="OfficeServer" Namespace="Microsoft.Office.Server.WebControls" Assembly="Microsoft.Office.DocumentManagement, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:EncodedLiteral runat="server" text="<%$Resources:dlcdm, DocSetHomepage_Title%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
	<span id="idParentListName">&#160;</span>
	<span id="idMiddleSection">
		<span id="idTitleSeparator">
			<SharePoint:ClusteredDirectionalSeparatorArrow runat="server"/>
		</span>
		<span id="idParentFolderName">&#160;</span>
	</span>
	<SharePoint:ClusteredDirectionalSeparatorArrow runat="server"/>
	<span id="idDocsetName">&#160;</span>
</asp:Content>
<asp:Content ContentPlaceholderID="PlaceHolderMain" runat="server">
	<OfficeServer:DocSetWelcomePageControl runat="server" ID="idDocSet"/>
	<table width="100%">
		<tr>
			<td width="15%" valign="top">
				<WebPartPages:WebPartZone runat="server" PartChromeType="None" id="WebPartZone_TopLeft" LayoutOrientation="vertical" AllowPersonalization="false" AllowCustomization="true"><ZoneTemplate>
				<WebPartPages:ImageWebPart runat="server" VerticalAlignment="Middle" AllowEdit="True" AllowConnect="True" ConnectionID="00000000-0000-0000-0000-000000000000" Title="Image" IsIncluded="True" Dir="Default" BackgroundColor="transparent" IsVisible="True" AlternativeText="" AllowMinimize="True" ExportControlledProperties="True" ZoneID="WebPartZone_TopLeft" ID="g_d8150632_ccd8_4c9f_af20_536d52ec1af3" HorizontalAlignment="Center" ImageLink="/_layouts/images/docset_welcomepage_big.png" ExportMode="All" AllowHide="True" SuppressWebPartChrome="False" DetailLink="" HelpLink="" FrameState="Normal" MissingAssembly="Cannot import this Web Part." PartImageSmall="" AllowRemove="True" HelpMode="Modeless" FrameType="Default" AllowZoneChange="True" PartOrder="2" Description="Use to display pictures and photos." PartImageLarge="" IsIncludedFilter="" __MarkupType="vsattributemarkup" __WebPartId="{D8150632-CCD8-4C9F-AF20-536D52EC1AF3}" WebPart="true" Height="" Width=""></WebPartPages:ImageWebPart>

</ZoneTemplate></WebPartPages:WebPartZone>
			</td>
			<td width="85%" valign="top">
				<WebPartPages:WebPartZone runat="server" PartChromeType="None" id="WebPartZone_Top" LayoutOrientation="vertical" AllowPersonalization="false" AllowCustomization="true"><ZoneTemplate>
<OfficeServer:DocumentSetPropertiesWebPart runat="server" __MarkupType="xmlmarkup" WebPart="true" __WebPartId="{39DDA903-94E0-44D6-8BBB-5A83F4400B95}" >
<WebPart xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://schemas.microsoft.com/WebPart/v2">
  <Title>Document Set Properties</Title>
  <FrameType>Default</FrameType>
  <Description>Displays the properties of the Document Set.</Description>
  <IsIncluded>true</IsIncluded>
  <ZoneID>WebPartZone_Top</ZoneID>
  <PartOrder>2</PartOrder><FrameState>Normal</FrameState><Height /><Width /><AllowRemove>true</AllowRemove><AllowZoneChange>true</AllowZoneChange><AllowMinimize>true</AllowMinimize><AllowConnect>true</AllowConnect><AllowEdit>true</AllowEdit><AllowHide>true</AllowHide><IsVisible>true</IsVisible><DetailLink /><HelpLink /><HelpMode>Modeless</HelpMode><Dir>Default</Dir><PartImageSmall /><MissingAssembly>Cannot import this Web Part.</MissingAssembly><PartImageLarge>/_layouts/images/msimagel.gif</PartImageLarge><IsIncludedFilter /><ExportControlledProperties>true</ExportControlledProperties><ConnectionID>00000000-0000-0000-0000-000000000000</ConnectionID><ID>g_39dda903_94e0_44d6_8bbb_5a83f4400b95</ID><DisplayText></DisplayText></WebPart>
</OfficeServer:DocumentSetPropertiesWebPart>
</ZoneTemplate></WebPartPages:WebPartZone>
			</td>
		</tr>
	</table>
	<table width="100%">
		<tr>
			<td>
				<WebPartPages:WebPartZone runat="server" PartChromeType="None" id="WebPartZone_CenterMain" LayoutOrientation="vertical" AllowPersonalization="false" AllowCustomization="true"><ZoneTemplate>
<OfficeServer:DocumentSetContentsWebPart runat="server" __MarkupType="xmlmarkup" WebPart="true" __WebPartId="{AD55E4CC-19AF-4434-964A-7481778020C7}" >
<WebPart xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://schemas.microsoft.com/WebPart/v2">
  <Title>Document Set Contents</Title>
  <FrameType>Default</FrameType>
  <Description>Displays the contents of the Document Set.</Description>
  <IsIncluded>true</IsIncluded>
  <ZoneID>WebPartZone_CenterMain</ZoneID>
  <PartOrder>2</PartOrder><FrameState>Normal</FrameState><Height /><Width /><AllowRemove>true</AllowRemove><AllowZoneChange>true</AllowZoneChange><AllowMinimize>true</AllowMinimize><AllowConnect>true</AllowConnect><AllowEdit>true</AllowEdit><AllowHide>true</AllowHide><IsVisible>true</IsVisible><DetailLink /><HelpLink /><HelpMode>Modeless</HelpMode><Dir>Default</Dir><PartImageSmall /><MissingAssembly>Cannot import this Web Part.</MissingAssembly><PartImageLarge>/_layouts/images/msimagel.gif</PartImageLarge><IsIncludedFilter /><ExportControlledProperties>true</ExportControlledProperties><ConnectionID>00000000-0000-0000-0000-000000000000</ConnectionID><ID>g_ad55e4cc_19af_4434_964a_7481778020c7</ID><DisplayText></DisplayText></WebPart>
</OfficeServer:DocumentSetContentsWebPart>
</ZoneTemplate></WebPartPages:WebPartZone>
			</td>
		</tr>
	</table>
	<table width="100%">
		<tr>
			<td>
				<WebPartPages:WebPartZone runat="server" PartChromeType="None" id="WebPartZone_Bottom" LayoutOrientation="vertical" AllowPersonalization="false" AllowCustomization="true"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
			</td>
		</tr>
	</table>
</asp:Content>
