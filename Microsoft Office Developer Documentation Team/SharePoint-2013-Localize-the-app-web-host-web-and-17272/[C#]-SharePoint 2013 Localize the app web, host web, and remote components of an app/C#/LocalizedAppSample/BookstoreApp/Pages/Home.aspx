<%@ Page language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink name="sp.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <link rel="Stylesheet" type="text/css" href="../Styles/App.css" />
    <script type="text/javascript" src="../Scripts/App.js"></script>
    
    <!-- The following script tag loads the script file containing localized strings -->
    <script 
        type="text/javascript" 
        src="../Scripts/Resources.<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,language_value%>' EncodeMethod='HtmlEncode' />.js">
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
    <WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" ID="full" Title="loc:full" />
    <h2 id="instructionsheading">INVARIANT Instructions</h2>
    <ol>
        <li id="step01">Go to any document library in the host web.</li>
        <li id="step02">Go to the Library tab.</li>
        <li id="step03">Click "Request a book" in the Settings group.</li>
        <li id="step04">Click the contextual menu in any document.</li>
        <li id="step05">Click "Buy this book" in the contextual menu.</li>
        <li id="step06">Go to any SharePoint page in the host web and add the Bookstore orders app part.</li>
        <li id="step07">Review the localized <a href="../Lists/Orders">Orders</a> and <a href="../Lists/Order status">Order status</a> custom lists."</li>
    </ol>

    <!-- Use the localized strings in the resource JavaScript file -->
    <script type="text/javascript">
        window.onload = function () {
            if (typeof instructionstitle != 'undefined') {
                document.getElementById("instructionsheading").innerText = instructionstitle;
                document.getElementById("step01").innerText = step01;
                document.getElementById("step02").innerText = step02;
                document.getElementById("step03").innerText = step03;
                document.getElementById("step04").innerText = step04;
                document.getElementById("step05").innerText = step05;
                document.getElementById("step06").innerText = step06;
                document.getElementById("step07").innerHTML = step07;
            }
        }
    </script>
</asp:Content>