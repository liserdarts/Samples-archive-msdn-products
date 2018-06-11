﻿<%@ Page language="C#" MasterPageFile="~masterurl/default.master"    Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint,Version=14.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c"  %> <%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %> <%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderId="PlaceHolderLeftNavBar" runat="server"/>
<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">
	<SharePoint:UIVersionedContent UIVersion="4" runat="server"><ContentTemplate>
		<SharePoint:CssRegistration Name="forms.css" runat="server"/>
	</ContentTemplate></SharePoint:UIVersionedContent>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleLeftBorder" runat="server">
<table cellpadding="0" height="100%" width="100%" cellspacing="0">
 <tr><td class="ms-areaseparatorleft"><img src="/_layouts/images/blank.gif" width='1' height='1' alt="" /></td></tr>
</table>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleAreaClass" runat="server">
<script type="text/javascript" id="onetidPageTitleAreaFrameScript">
	if (document.getElementById("onetidPageTitleAreaFrame") != null)
	{
		document.getElementById("onetidPageTitleAreaFrame").className="ms-areaseparator";
	}
</script>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderBodyAreaClass" runat="server">
<style type="text/css">
.ms-bodyareaframe {
	padding: 8px;
	border: none;
}
</style>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderBodyLeftBorder" runat="server">
<div class='ms-areaseparatorleft'><img src="/_layouts/images/blank.gif" width='8' height='100%' alt="" /></div>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleRightMargin" runat="server">
<div class='ms-areaseparatorright'><img src="/_layouts/images/blank.gif" width='8' height='100%' alt="" /></div>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderBodyRightMargin" runat="server">
<div class='ms-areaseparatorright'><img src="/_layouts/images/blank.gif" width='8' height='100%' alt="" /></div>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleAreaSeparator" runat="server"/>
<asp:Content ContentPlaceHolderId="PlaceHolderPageImage" runat="server">
	<img src="/_layouts/images/blank.gif" width='1' height='1' alt="" />
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderUtilityContent" runat="server">
<script language=javascript>
function ULS_Upload(){if (ULS_Upload.caller){ULS_Upload.caller.ULSTeamName = "Windows SharePoint Services 4";ULS_Upload.caller.ULSFileName = "Upload.aspx";}}
var fCtl=false;
function EnsureUploadCtl()
{ULS_Upload();
	return browseris.ie5up && !browseris.mac &&
		null != document.getElementById("idUploadCtl");
}
function MultipleUploadView()
{ULS_Upload();
	if (EnsureUploadCtl())
	{
		treeColor = GetTreeColor();
		document.all.idUploadCtl.SetTreeViewColor(treeColor);
		if(!fCtl)
		{
			rowsArr = document.all.formTbl.rows;
			for(i=0; i < rowsArr.length; i++)
			{
				if ((rowsArr[i].id != "OverwriteField") &&
					(rowsArr[i].id != "trUploadCtl"))
				{
					rowsArr[i].removeNode(true);
					i=i-1;
				}
			}
			document.all.reqdFldTxt.removeNode(true);
			newCell = document.all.OverwriteField.insertCell();
			newCell.innerHTML = "ONET_NBSP";
			newCell.style.width="60%";
			document.all("dividMultipleView").style.display="inline";
			fCtl = true;
		}
	}
}
function RemoveMultipleUploadItems()
{ULS_Upload();
	if(browseris.nav || browseris.mac ||
		!EnsureUploadCtl()
	)
	{
		formTblObj = document.getElementById("formTbl");
		if(formTblObj)
		{
			rowsArr = formTblObj.rows;
			for(i=0; i < rowsArr.length; i++)
			{
				if (rowsArr[i].id == "trUploadCtl" || rowsArr[i].id == "diidIOUploadMultipleLink")
				{
					formTblObj.deleteRow(i);
				}
			}
		}
	}
}
function DocumentUpload()
{ULS_Upload();
	if (fCtl)
	{
		document.all.idUploadCtl.MultipleUpload();
	}
	else
	{
		ClickOnce();
	}
}
function GetTreeColor()
{ULS_Upload();
	var bkColor="";
	if(null != document.all("onetidNavBar"))
		bkColor = document.all.onetidNavBar.currentStyle.backgroundColor;
	if(bkColor=="")
	{
		numStyleSheets = document.styleSheets.length;
		for(i=numStyleSheets-1; i>=0; i--)
		{
			numRules = document.styleSheets(i).rules.length;
			for(ruleIndex=numRules-1; ruleIndex>=0; ruleIndex--)
			{
				if(document.styleSheets[i].rules.item(ruleIndex).selectorText==".ms-uploadcontrol")
					uploadRule = document.styleSheets[i].rules.item(ruleIndex);
			}
		}
		if(uploadRule)
			bkColor = uploadRule.style.backgroundColor;
	}
	return(bkColor);
}
</script>
<script type="text/javascript">
// <![CDATA[
	function _spBodyOnLoad()
	{ULS_Upload();
		var frm = document.forms[MSOWebPartPageFormName];
		frm.encoding="multipart/form-data";
	}
// ]]>
</script>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
		<WebPartPages:WebPartZone runat="server" FrameType="None" ID="Main" Title="loc:Main" />
	<input type="hidden" name="VTI-GROUP" value="0"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,upload_pagetitle_form%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
	<SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,upload_pagetitle_form%>" EncodeMethod='HtmlEncode'/>: <SharePoint:ListProperty Property="LinkTitle" runat="server"/>
</asp:Content>
