<%@ Assembly Name="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WikiEditPage"
    MasterPageFile="~masterurl/default.master" meta:webpartpageexpansion="full" meta:progid="SharePoint.WebPartPage.Document" %>

<%@ Import Namespace="Microsoft.SharePoint.WebPartPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:ProjectProperty ID="ProjectProperty1" Property="Title" runat="server" />
    -
    <SharePoint:ListItemProperty ID="ListItemProperty1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <span>
        <SharePoint:DocumentFolderName runat="server" ID="PageFolderName" AppendSeparatorArrow="true" />
    </span><span class="ms-WikiPageNameEditor-Display" id="wikiPageNameDisplay" runat="server">
        <SharePoint:ListItemProperty ID="ListItemProperty2" runat="server" />
    </span><span class="ms-WikiPageNameEditor-Edit" style="display: none;" id="wikiPageNameEdit"
        runat="server">
        <asp:TextBox ID="wikiPageNameEditTextBox" runat="server" />
    </span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <SharePoint:UIVersionedContent ID="UIVersionedContent1" runat="server" UIVersion="4">
        <contenttemplate>
			<SharePoint:ProjectProperty Property="Description" runat="server"/>
		</contenttemplate>
    </SharePoint:UIVersionedContent>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderPageImage" runat="server">
    <SharePoint:AlphaImage ID="onetidtpweb1" Src="/_layouts/images/wiki.png" Width="145"
        Height="54" Alt="" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <meta name="CollaborationServer" content="SharePoint Team Web Site" />
    <script type="text/javascript">
        var navBarHelpOverrideKey = "WSSEndUser";
    </script>
    <SharePoint:RssLink ID="RssLink1" runat="server" />
    <SharePoint:UIVersionedContent ID="UIVersionedContent2" UIVersion="4" runat="server">
        <contenttemplate>
		<SharePoint:CssRegistration runat="server" Name="wiki.css" />
	</contenttemplate>
    </SharePoint:UIVersionedContent>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderMiniConsole" runat="server">
    <SharePoint:FormComponent TemplateName="WikiMiniConsole" ControlMode="Display" runat="server"
        ID="WikiMiniConsole" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="PlaceHolderLeftActions" runat="server">
    <SharePoint:RecentChangesMenu runat="server" ID="RecentChanges" />
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:UIVersionedContent runat="server" UIVersion="3" ID="PlaceHolderWebDescription">
        <contenttemplate>
			<div class="ms-webpartpagedescription"><SharePoint:ProjectProperty ID="ProjectProperty2" Property="Description" runat="server"/></div>
		</contenttemplate>
    </SharePoint:UIVersionedContent>
    <asp:UpdatePanel id="updatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <contenttemplate>
			<SharePoint:VersionedPlaceHolder ID="VersionedPlaceHolder1" UIVersion="4" runat="server">
				<SharePoint:SPRibbonButton
					id="btnWikiEdit"
					RibbonCommand="Ribbon.WikiPageTab.EditAndCheckout.SaveEdit.Menu.SaveEdit.Edit"
					runat="server"
				    Text="edit"/>
				<SharePoint:SPRibbonButton
					id="btnWikiSave"
					RibbonCommand="Ribbon.WikiPageTab.EditAndCheckout.SaveEdit.Menu.SaveEdit.SaveAndStop"
					runat="server"
				    Text="edit"/>
				<SharePoint:SPRibbonButton
					id="btnWikiRevert"
					RibbonCommand="Ribbon.WikiPageTab.EditAndCheckout.SaveEdit.Menu.SaveEdit.Revert"
				    runat="server"
					Text="Revert"/>
			</SharePoint:VersionedPlaceHolder>
			<SharePoint:EmbeddedFormField id="WikiField" FieldName="WikiField" ControlMode="Display" runat="server"><div class="ExternalClass4F305357B46C4906A3EA6C9ED768EDF1"><table id="layoutsTable" style="width:100%"><tbody><tr style="vertical-align:top"><td style="width:100%"><div class="ms-rte-layoutszone-outer" style="width:100%"><div class="ms-rte-layoutszone-inner">
	
    <!-- Silverlight Custom Web partial -->	
    <WebPartPages:SPUserCodeWebPart 
        runat="server" 
        Description="MusicBrainzSilverlightApplication Custom Web Part" 
        Title="MusicBrainzSilverlightApplication Custom Web Part" 
        AssemblyFullName="MusicBrainzSharePoint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0e7cadb4616debc8" 
        SolutionId="e3c61b27-c9fd-4df0-8fda-a5bd202868ea" 
        ID="g_26a3b1a0_7d67_4064_a3ba_ee267bc3d5d2" 
        TypeFullName="MusicBrainzSharePoint.SandboxedVisualWebPart1.vstemplate.SandboxedVisualWebPart1" 
        __MarkupType="vsattributemarkup" 
        __WebPartId="{C1178B91-CFAC-4BD7-82E7-5359C1E83A14}" 
        WebPart="true" 
        __designer:IsClosed="false">
    </WebPartPages:SPUserCodeWebPart>


</div></div></td></tr></tbody></table>
<span id="layoutsData" style="display:none">false,false,1</span></div></SharePoint:EmbeddedFormField>
			<WebPartPages:WebPartZone runat="server" ID="Bottom" Title="loc:Bottom"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
	</contenttemplate>
        <triggers>
	    <asp:PostBackTrigger ControlID="WikiField" />
	    <asp:PostBackTrigger ControlID="btnWikiRevert" />
	    <asp:PostBackTrigger ControlID="btnWikiSave" />
	</triggers>
    </asp:UpdatePanel>
</asp:Content>
