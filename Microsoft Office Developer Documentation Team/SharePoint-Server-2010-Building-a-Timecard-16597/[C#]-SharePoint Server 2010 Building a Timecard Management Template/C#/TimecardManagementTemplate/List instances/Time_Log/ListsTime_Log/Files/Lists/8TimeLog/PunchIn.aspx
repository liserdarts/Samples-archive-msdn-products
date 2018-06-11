<%@ Page Language="C#" masterpagefile="~masterurl/default.master" title="Punch In" inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" %>
<%@ Register tagprefix="WebPartPages" namespace="Microsoft.SharePoint.WebPartPages" assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register tagprefix="SharePoint" namespace="Microsoft.SharePoint.WebControls" assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content id="extras" runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
<script type="text/javascript">

_spBodyOnLoadFunctionNames.push("setFormValues");

function getTagFromIdentifierAndTitle(tagName, identifier, title) {
      var len = identifier.length;
      var tags = document.getElementsByTagName(tagName);
      for (var i=0; i < tags.length; i++) {
            var tempString = tags[i].id;
            if (tempString.indexOf(identifier) == tempString.length - len && tags[i].title == title) {
                  return tags[i];
            }
      }
      return null;
}

function changeSelect(displayName, newVal) {
      var theSelect = getTagFromIdentifierAndTitle("select","Choice",displayName);
      for (var j=0; j<theSelect.options.length; j++) {
            if (theSelect.options[j].value == newVal) {
                  theSelect.selectedIndex = j;
                  return;
            }
      }
}

function setFormValues() {
      changeSelect('Event', 'PunchIn'); 
}

</script>
<script type="text/javascript">
function GetSource(defaultSource) {
	return escapeProperly(STSPageUrlValidation(window.location.href));
}
</script>
</asp:Content>
<asp:Content id="Content1" runat="server" contentplaceholderid="PlaceHolderMain">

				<WebPartPages:SPProxyWebPartManager runat="server" id="ProxyWebPartManager">
	</WebPartPages:SPProxyWebPartManager>
	<WebPartPages:WebPartZone id="g_78900338B7FD48D4A7C9A41636970F66" runat="server" title="Main"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
</asp:Content>
<asp:Content id="Content2" runat="server" contentplaceholderid="PlaceHolderPageTitleInTitleArea">

		  Punch In</asp:Content>

