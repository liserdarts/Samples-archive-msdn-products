<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>
<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" language="C#" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">

        <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.9.0.min.js"></script>  

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
    <script type="text/javascript" src="../Scripts/json.js"></script>

    <!-- The following script runs when the DOM is ready. The inline code uses a SharePoint feature to ensure -->
    <!-- The SharePoint script file sp.js is loaded and will then execute the sharePointReady() function in App.js -->
    <script type="text/javascript">
        $(document).ready(function () {
            SP.SOD.executeFunc('sp.js', 'SP.ClientContext', function () { sharePointReady(); });
        });
    </script>
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">

    <div>
<!-- main XSLT dataview for project items -->
<!-- datasource guids removed and replaced with ListName, CAML query included  -->
<!-- Param variables for server methods defined -->
<!-- template call dvt_1 > dvt_1.body > dvt_1.rowview -->
<!-- module style and layout defined in workli class show -->
<!-- hover module style and layout defined in workli class hide -->
<!-- project status icons defined at  <xsl:if test="not(normalize-space(@ProjectStatus) -->

<WebPartPages:DataFormWebPart runat="server" IsIncluded="True" AsyncRefresh="True" FrameType="None" NoDefaultStyle="TRUE" ViewFlag="8" Title="Project List" PageType="PAGE_NORMALVIEW" Default="FALSE" DisplayName="Project List" __markuptype="vsattributemarkup" __WebPartId="{242765A8-47A5-4480-9B93-95DC053349A4}" id="g_242765a8_47a5_4480_9b93_95dc053349a4">
             <DataSources>
                        <SharePoint:SPDataSource runat="server" DataSourceMode="List" UseInternalName="true" UseServerDataFormat="true" selectcommand="&lt;View&gt;&lt;/View&gt;" id="Project_x0020_List1"><SelectParameters><asp:Parameter Name="ListName" DefaultValue="Project List" /><asp:Parameter Name="StartRowIndex" DefaultValue="0"/><asp:Parameter Name="nextpagedata" DefaultValue="0"/><asp:Parameter Name="MaximumRows" DefaultValue="10"/>
                        	</SelectParameters>
                            <DeleteParameters><asp:Parameter Name="ListName" DefaultValue="Project List" /></DeleteParameters>
                            <UpdateParameters><asp:Parameter Name="ListName" DefaultValue="Project List" /></UpdateParameters>
                            <InsertParameters><asp:Parameter Name="ListName" DefaultValue="Project List" /></InsertParameters>
                        </SharePoint:SPDataSource>
            </DataSources>

            <ParameterBindings>
			<ParameterBinding Name="dvt_apos" Location="Postback;Connection"/>
			<ParameterBinding Name="ManualRefresh" Location="WPProperty[ManualRefresh]"/>
			<ParameterBinding Name="UserID" Location="CAMLVariable" DefaultValue="CurrentUserName"/>
			<ParameterBinding Name="Today" Location="CAMLVariable" DefaultValue="CurrentDate"/>
		</ParameterBindings>
            <datafields>@Title,Project Name;@ProjectState,Project State;@StartDate,Start Date;@_EndDate,End Date;@PercentComplete,% Complete;@ProjectType,Project Type;@ScrumNotes,Scrum Notes;@ProjectManager,Project Manager;@Developers,Developers;@Testers,Testers;@Activation,Activation;@ProjectDescription,Project Description;@ProjectDocuments,Project Documents;@ProjectStatus,Project Status;@Category1,Category;@ID,ID;@ContentType,Content Type;@Modified,Modified;@Created,Created;@Author,Created By;@Editor,Modified By;@_UIVersionString,Version;@Attachments,Attachments;@File_x0020_Type,File Type;@FileLeafRef,Name (for use in forms);@FileDirRef,Path;@FSObjType,Item Type;@_HasCopyDestinations,Has Copy Destinations;@_CopySource,Copy Source;@ContentTypeId,Content Type ID;@_ModerationStatus,Approval Status;@_UIVersion,UI Version;@Created_x0020_Date,Created;@FileRef,URL Path;@ItemChildCount,Item Child Count;@FolderChildCount,Folder Child Count;@AppAuthor,App Created By;@AppEditor,App Modified By;</datafields>

            <XSL>

<xsl:stylesheet xmlns:x="http://www.w3.org/2001/XMLSchema" xmlns:d="http://schemas.microsoft.com/sharepoint/dsp" version="1.0" exclude-result-prefixes="xsl msxsl ddwrt" xmlns:ddwrt="http://schemas.microsoft.com/WebParts/v2/DataView/runtime" xmlns:asp="http://schemas.microsoft.com/ASPNET/20" xmlns:__designer="http://schemas.microsoft.com/WebParts/v2/DataView/designer" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:SharePoint="Microsoft.SharePoint.WebControls" xmlns:ddwrt2="urn:frontpage:internal">
            <xsl:output method="html" indent="no"/>
            <xsl:decimal-format NaN=""/>
            <xsl:param name="dvt_apos">'</xsl:param>
            <xsl:param name="ManualRefresh"></xsl:param>
            <xsl:variable name="dvt_1_automode">0</xsl:variable>

            <xsl:template match="/" xmlns:x="http://www.w3.org/2001/XMLSchema" xmlns:d="http://schemas.microsoft.com/sharepoint/dsp" xmlns:asp="http://schemas.microsoft.com/ASPNET/20" xmlns:__designer="http://schemas.microsoft.com/WebParts/v2/DataView/designer" xmlns:SharePoint="Microsoft.SharePoint.WebControls">
                        <xsl:choose>
                                    <xsl:when test="($ManualRefresh = 'True')">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                        <td valign="top">
                                                                            <!-- call to main templete -->
                                                                                    <xsl:call-template name="dvt_1"/>
                                                                        </td>
                                                                        <td width="1%" class="ms-vb" valign="top">
                                                                                    <img src="/_layouts/images/staticrefresh.gif" id="ManualRefresh" border="0" onclick="javascript: {ddwrt:GenFireServerEvent('__cancel')}" alt="Click here to refresh the dataview."/>
                                                                        </td>
                                                            </tr>
                                                </table>
                                    </xsl:when>
                                    <xsl:otherwise>
                                                 <div>Legend: <span style="display:inline-block">Contoso </span><div style="height: 10px; width: 10px; margin-left: 3px; background-color: #ED151E;display:inline-block"></div>
                                                    <span style="display:inline-block; padding-left: 5px;">Contoso Corp </span><div style="height: 10px; width: 10px; margin-left: 3px; background-color: #19a4e0;display:inline-block"></div>
                                                    <span style="display:inline-block; padding-left: 5px;">Contoso HR </span><div style="height: 10px; width: 10px; margin-left: 3px; background-color: #7f7f7f;display:inline-block"></div>
                                                </div>
                                                <xsl:call-template name="dvt_1"/>
                                    </xsl:otherwise>
                        </xsl:choose>
            </xsl:template>

            <xsl:template name="dvt_1">
                        <xsl:variable name="dvt_StyleName">RepForm3</xsl:variable>
                        <xsl:variable name="Rows" select="/dsQueryResponse/Rows/Row"/>
                        <xsl:variable name="dvt_RowCount" select="count($Rows)"/>
                        <xsl:variable name="IsEmpty" select="$dvt_RowCount = 0" />
                        <xsl:variable name="dvt_IsEmpty" select="$dvt_RowCount = 0"/>

                        <xsl:choose>
                                    <xsl:when test="$dvt_IsEmpty">
                                                <xsl:call-template name="dvt_1.empty"/>
                                    </xsl:when>

                                    <xsl:otherwise>
                                                <xsl:call-template name="dvt_1.body">
                                                            <xsl:with-param name="Rows" select="$Rows" />
                                                </xsl:call-template>
                                    </xsl:otherwise>
                        </xsl:choose></xsl:template>

            <xsl:template name="dvt_1.body">
                        <xsl:param name="Rows"/>
                        <xsl:for-each select="$Rows">
                                    <xsl:call-template name="dvt_1.rowview" />
                        </xsl:for-each>
            </xsl:template>

            <xsl:template name="dvt_1.rowview">
            <div class="containerdiv">
            <ul class="work" style="width: 260px; height: 200px; padding: 10px; margin: 0 0 10px 0; display: block; float: left; position: relative; overflow: hidden;">
            <div class="slide" >
                 <li class="workli" style="padding: 0px; margin: 0px;">
                 <a class="show" href="#" style="padding: 0px; margin: 0px;" title="{@Title}">

                               <div>
                                                                                    <xsl:attribute name="style">width: 240px; height: 180px; padding: 10px; margin: 0px 0 10px 0px; display: block; float: left; position: relative; overflow: hidden;
                                                                                                <xsl:if test="normalize-space(@Category1) = 'Contoso'">border: 3px solid #ED151E;</xsl:if>
                                                                                                <xsl:if test="normalize-space(@Category1) = 'Contoso Corp'">border: 3px solid #19a4e0;</xsl:if>
                                                                                                <xsl:if test="normalize-space(@Category1) = 'Contoso HR'">border: 3px solid #7f7f7f;</xsl:if>
                                                                                    </xsl:attribute>                                

                                                                                    <div style="display:block; vertical-align:middle ">

                                                                                    <div style="margin-bottom: 2px; display:inline-block; width: 69%"><span class="title">
                                                                                                <xsl:attribute name="style">font-weight: bold; font-size: 14pt; font-family: &apos;Segoe UI Light&apos;;
                                                                                                            <xsl:if test="normalize-space(@Category1) = 'VMC'">color: #000;</xsl:if>
                                                                                                            <xsl:if test="normalize-space(@Category1) = 'Other'">color: #000;</xsl:if>
                                                                                                            <xsl:if test="normalize-space(@Category1) = 'Microsoft'">color: #000;</xsl:if>
                                                                                                </xsl:attribute>

                                                                                                <xsl:value-of select="@Title" /></span></div>

                                                                                    	<xsl:if test="not(normalize-space(@ProjectStatus) = '')"><div class="status" style="display:inline-block; float:right; width: 29%; text-align:right ">

                                                                                                <xsl:if test="not(normalize-space(@ProjectStatus) = 'At-Risk' or normalize-space(@ProjectStatus) = 'Critical')"><img alt="" src="../images/green.png" width="20" height="20" border="0" /></xsl:if>

                                                                                                <xsl:if test="not(normalize-space(@ProjectStatus) = 'On-Target' or normalize-space(@ProjectStatus) = 'Critical')"><img alt="" src="../images/yellow.png" width="20" height="20" border="0" /></xsl:if>

                                                                                                <xsl:if test="not(normalize-space(@ProjectStatus) = 'On-Target' or normalize-space(@ProjectStatus) = 'At-Risk')"><img alt="" src="../images/red.png" width="20" height="20" border="0" /></xsl:if>

                                                                                                </div></xsl:if></div>

                                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tbody>
                                                                        <tr>
                                                                        <td>
                                                                        <div style="display:block;  width:100%; margin: 0px 10px 10px 0; font-size: 7pt !important">
                                                                                                <div style="display:inline-block; float:left; padding-right: 3px ">
                                                                                                <span style="color: black;">SD: <xsl:value-of select="ddwrt:FormatDate(string(@StartDate) ,1033 ,1)" /></span></div>
                                                                                                <div style="display:inline-block; float:left; padding-right: 3px  ">
                                                                                                <span style="color: black;"> | </span></div>
                                                                                                <div style="display:inline-block; float:left">
                                                                                                <span style="color: black; padding-right: 3px">ED: <xsl:value-of select="ddwrt:FormatDate(string(@_EndDate) ,1033 ,1)" /></span></div>                                                                                                 
																								<div style="float:left; margin-left:0; clear:both">           
                                                                                                <span style="color: black; text-transform:uppercase !important; font-size: 7pt !important ">Status: <xsl:value-of select="@ProjectStatus" /></span>
                                                                                                <span style="color: black; font-size: 7pt !important "> | <xsl:value-of select="@PercentComplete" /> COMPLETE</span></div>
                                                                        </div>                                                                                 
                                                                        </td>
                                                                        </tr>     
                                                                        <tr>
                                                                        <td>
                                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                        <td class="descriptionbodytd">
                                                                        <xsl:if test="not(normalize-space(@ProjectDescription) = '')"><div class="descriptionbody" style="color: black"><br /><xsl:value-of select="substring(@ProjectDescription, 0, 140)" disable-output-escaping="yes" />...

                                                                                                                                                                                                <xsl:if test="$dvt_1_automode = '1'" ddwrt:cf_ignore="1">

                                                                                    <span ddwrt:amkeyfield="ID" ddwrt:amkeyvalue="ddwrt:EscapeDelims(string(@ID))" ddwrt:ammode="view"></span></xsl:if>

                                                                                    </div></xsl:if>
                                                                        </td>
                                                                        </tr>
                                                                        </tbody>
                                                                        </table>                                                                       

                                                                    </div>                         

                                                                        <div class="hide" style="position: relative; margin: 0px;">

                                                                        <div class="dev" style=" position: absolute; ;bottom: 0; left: 0; padding: 10px;  " >

                                                                        <div style="margin-bottom: 2px; display:inline-block; width: 69%"><span class="title">

                                                                                                <xsl:attribute name="style">font-weight: bold; font-size: 14pt; font-family: &apos;Segoe UI Light&apos;;

                                                                                                            <xsl:if test="normalize-space(@Category1) = 'VMC'">color: #000;</xsl:if>

                                                                                                            <xsl:if test="normalize-space(@Category1) = 'Other'">color: #000;</xsl:if>

                                                                                                            <xsl:if test="normalize-space(@Category1) = 'Microsoft'">color: #000;</xsl:if>

                                                                                                </xsl:attribute>

                                                                                                <xsl:value-of select="@Title" /></span></div>

                                                                                    <xsl:if test="not(normalize-space(@ProjectStatus) = '')"><div class="status" style="display:inline-block; float:right; width: 29%; text-align:right ">

                                                                                                <xsl:if test="not(normalize-space(@ProjectStatus) = 'At-Risk' or normalize-space(@ProjectStatus) = 'Critical')"><img src="../images/green.png" border="0" height="20" title="Status: On-Target" /></xsl:if>

                                                                                                <xsl:if test="not(normalize-space(@ProjectStatus) = 'On-Target' or normalize-space(@ProjectStatus) = 'Critical')"><img src="../images/yellow.png" border="0" height="20" title="Status: At-Risk" /></xsl:if>

                                                                                                <xsl:if test="not(normalize-space(@ProjectStatus) = 'On-Target' or normalize-space(@ProjectStatus) = 'At-Risk')"><img src="../images/red.png" border="0" height="20" title="Status: Critical" /></xsl:if>

                                                                                                </div></xsl:if>

                                                                        <table cellpadding="0" cellspacing="0" width="100%">

                                                                        <tbody>

                                                                        <tr>

                                                                        <td>

                                                                        <div style="display:block;  width:100%; margin: 0px 10px 10px 0; font-size: 7pt !important">
                                                                                                <div style="display:inline-block; float:left; padding-right: 3px ">
                                                                                                <span style="color: black;">SD: <xsl:value-of select="ddwrt:FormatDate(string(@StartDate) ,1033 ,1)" /></span></div>
                                                                                                <div style="display:inline-block; float:left; padding-right: 3px  ">
                                                                                                <span style="color: black;"> | </span></div>
                                                                                                <div style="display:inline-block; float:left">
                                                                                                <span style="color: black; padding-right: 3px">ED: <xsl:value-of select="ddwrt:FormatDate(string(@_EndDate) ,1033 ,1)" /></span></div>                                                                                                 
																								<div style="float:left; margin-left:0; clear:both">           
                                                                                                <span style="color: black; text-transform:uppercase !important; font-size: 7pt !important ">Status: <xsl:value-of select="@ProjectStatus" /></span>
                                                                                                <span style="color: black; font-size: 7pt !important "> | <xsl:value-of select="@PercentComplete" /> COMPLETE</span></div>
                                                                        </div>

                                                                        </td>

                                                                        </tr>

                                                                        

                                                                        <tr>

                                                                        <td>

                                                                        </td>

                                                                        </tr>

                                                                        <tr>

                                                                        <td>

                                                                        <div style="color: black;"><br /><b>Manager:</b>  <span style="padding-left: 5px;" class="managers"><xsl:value-of select="@ProjectManager" /></span></div>

                                                                        </td>

                                                                        </tr>

                                                                        <tr>

                                                                        <td>

                                                                        <div style="color: black;"><b>Developers:</b>  <span style="padding-left: 5px;" class="devs"><xsl:value-of select="@Developers" disable-output-escaping="yes" /></span></div>

                                                                        </td>

                                                                        </tr>

                                                                        <tr>

                                                                        <td>

                                                                        <xsl:if test="not(normalize-space(@Testers) = '')"><div style="color: black;"><b>Testers:</b>  <span style="padding-left: 5px;" class="devs"><xsl:value-of select="@Testers" disable-output-escaping="yes" /></span></div></xsl:if>

                                                                        </td>

                                                                        </tr>

 

                                                                        </tbody>

                                                                        </table>

                        </div>

                        <div class="icons" style=" width: 100%; height:30px; position: absolute; bottom: -20px; left: 0;   ">                                                                        

                                                                        <div style="width: 33px; height: 30px; float:right; text-align:center; vertical-align:bottom; margin-right: 10px;"><a onClick="OpenPopUpPage('{@FileDirRef}/EditForm.aspx?ID={@ID}')" style="cursor:pointer">                                                

                                                                        <img src="../images/edit.jpg" border="0" height="22" title="Edit {@Title} Form" /></a></div><div style="width: 30px; height: 30px; float:right; text-align:center; vertical-align:bottom "><a href="{@ProjectDocuments}"><img src="../images/doc.jpg" border="0" height="22" title="{@Title} Doc Library" /></a></div><div style="width: 34px; height: 30px; float:right;  text-align:center;  vertical-align:bottom  " ><a href="mailto:{@Developers},{@Testers}?subject=VMC Project Status: {@Title}&amp;body=Hello Team&amp;cc={@ProjectManager}"><img src="../images/mail.jpg" title="E-mail {@Title} Staff and Customers" border="0" height="22" /></a></div><div style="width: 24px; height: 30px; float:right;  text-align:right;  vertical-align:bottom  " ><a onClick="OpenPopUpPage('{@FileDirRef}/DispForm.aspx?ID={@ID}')" style="cursor:pointer"><img src="../images/notes.png" title="Scrum Notes" border="0" height="22" /></a></div>

                                                                        </div>

                                                                        </div>
                                                </a>
                                                  </li>
                                      </div>
                                    </ul>
                                    </div>             
</xsl:template>

            <xsl:template name="dvt_1.empty">

                        <xsl:variable name="dvt_ViewEmptyText">There are no projects entered, click New Item below and fill out form</xsl:variable>

                        <table border="0" width="100%">

                                    <tr>

                                                <td class="ms-vb">
                                                    <div style="height: 250px; width: 250px; border: red 2px solid; ">
                                                            <xsl:value-of select="$dvt_ViewEmptyText"/>
                                                    </div>
                                                </td>

                                    </tr>

                        </table>

            </xsl:template></xsl:stylesheet>         </XSL>

</WebPartPages:DataFormWebPart>

    </div>
    
    <hr />
    <!-- inline Project list instance view and form -->

<WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" 
ID="WebPartZone1" Title="loc:full" >
<WebPartPages:XsltListViewWebPart ID="XsltListViewWebPart1" 
runat="server" ListUrl="Lists/Project%20List" IsIncluded="True" 
NoDefaultStyle="TRUE" Title="Project List" PageType="PAGE_NORMALVIEW" 
Default="False" ViewContentTypeId="0x"> 
</WebPartPages:XsltListViewWebPart>
</WebPartPages:WebPartZone>
    <hr />
    
    <!-- inline Project list instance view and form -->
    <WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" 
ID="WebPartZone2" Title="loc:full" >
<WebPartPages:XsltListViewWebPart ID="XsltListViewWebPart2" 
runat="server" ListUrl="Lists/Project%20Documents" IsIncluded="True" 
NoDefaultStyle="TRUE" Title="Project Document" PageType="PAGE_NORMALVIEW" 
Default="False" ViewContentTypeId="0x"> 
</WebPartPages:XsltListViewWebPart>
</WebPartPages:WebPartZone>


<!-- jquery hover over function -->
<!--[if lte IE 7]>

<script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.9.0.min.js"></script> 

<script type="text/javascript">$(function() {

                                $('.work div').mouseover

                                                (function(){

                                                $(this).addClass('over');
                                });

                    
                                $('.work div&').mouseout

                                                (function(){

                                                $(this).removeClass('over');
                                });
});

</script>

<![endif]-->



</asp:Content>
