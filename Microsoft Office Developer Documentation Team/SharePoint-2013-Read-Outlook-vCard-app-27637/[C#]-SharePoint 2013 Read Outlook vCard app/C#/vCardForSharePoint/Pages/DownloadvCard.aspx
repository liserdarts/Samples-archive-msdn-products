<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" />


<html>
<head>
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="../Scripts/vCard_helper.js"></script>
    <script type="text/javascript" src="../Scripts/vCard_vcfCreator.js"></script>
    <link rel="stylesheet" type="text/css" href="../Content/App.css" />
    <script type="text/javascript">
        'use strict';

        // Set the style of the client web part page to be consistent with the host web.
        (function () {
            var hostUrl = '';
            if (document.URL.indexOf('?') != -1) {
                var params = document.URL.split('?')[1].split('&');
                for (var i = 0; i < params.length; i++) {
                    var p = decodeURIComponent(params[i]);
                    if (/^SPHostUrl=/i.test(p)) {
                        hostUrl = p.split('=')[1];
                        document.write('<link rel="stylesheet" href="' + hostUrl + '/_layouts/15/defaultcss.ashx" />');
                        break;
                    }
                }
            }
            if (hostUrl == '') {
                document.write('<link rel="stylesheet" href="/_layouts/15/1033/styles/themable/corev15.css" />');
            }
        })();
    </script>
</head>
<body>
    <div style="color: #0072c6; font-size: 1.46em; font-family: Segoe UI Semilight,Segoe UI,Segoe,Tahoma,Helvetica,Arial,sans-serif;">Download my vCard</div>
    <div align="center">
        <div id="loadingcontainer" class="loadingContainer">
            <span>
                <img src="../Images/arrow_animation.gif" alt="loading" style="margin-top: 17%" />
            </span>
        </div>
        <a id="vcflink" href="#" onclick="javascript:initiatedownload();">
            <img src="../Images/AppIcon.png" width="70" title="Download my vCard" /></a>
        <a id="vcflinkhiden" href="#" hidden="hidden" style="display: none"></a>
    </div>
    <input style="display: none" id="hiddenvcfbldbtn" type="button" onclick="javascript: injectScripts();" value="Button" />
</body>
</html>
