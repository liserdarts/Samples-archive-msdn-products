<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ApplyThemeAppWeb.Pages.Default" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script 
        src="//ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js" 
        type="text/javascript">
    </script>
    <script 
        type="text/javascript" 
        src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js">
    </script>      
    <script 
        type="text/javascript"
        src="ChromeLoader.js">
    </script>
    <script type="text/javascript">
        "use strict";

        var hostweburl;

        //load the SharePoint resources
        $(document).ready(function () {
            //Get the URI decoded URL.
            hostweburl =
                decodeURIComponent(
                    getQueryStringParameter("SPHostUrl")
            );

            // The SharePoint js files URL are in the form:
            // web_url/_layouts/15/resource
            var scriptbase = hostweburl + "/_layouts/15/";

            // Load the js file and continue to the 
            //   success handler
            $.getScript(scriptbase + "SP.UI.Controls.js", renderChrome)
        });

        // Callback for the onCssLoaded event defined
        //  in the options object of the chrome control
        function chromeLoaded() {
            // When the page has loaded the required
            //  resources for the chrome control,
            //  display the page body.
            $("body").show();
        }

        //Function to prepare the options and render the control
        function renderChrome() {
            // The Help, Account and Contact pages receive the 
            //   same query string parameters as the main page
            var options = {
                //"appIconUrl": "siteicon.png",
                "appTitle": "Apply Theme Sample App",
                // The onCssLoaded event allows you to 
                //  specify a callback to execute when the
                //  chrome resources have been loaded.
                "onCssLoaded": "chromeLoaded()"
            };

            var nav = new SP.UI.Controls.Navigation(
                                    "chrome_ctrl_placeholder",
                                    options
                                );
            nav.setVisible(true);
        }

        // Function to retrieve a query string value.
        // For production purposes you may want to use
        //  a library to handle the query string.
        function getQueryStringParameter(paramToRetrieve) {
            var params =
                document.URL.split("?")[1].split("&");
            var strParams = "";
            for (var i = 0; i < params.length; i = i + 1) {
                var singleParam = params[i].split("=");
                if (singleParam[0] == paramToRetrieve)
                    return singleParam[1];
            }
        }
</script>
    <title></title>
</head>
<body style="display:none">
    <form id="form1" runat="server">
    <!-- Chrome control placeholder -->
    <div id="chrome_ctrl_placeholder"></div>
    <div id="MainContent" style="margin-left:40px;">
    <div>
            <h2 class="ms-accentText">Status</h2>
            <asp:TextBox runat="server" ReadOnly="true" TextMode="MultiLine" Rows="20" Width="500" ID="StatusMessage" />
    </div>

        <div style="margin-top: 20px;">
            <asp:Button ID="GetThemeInfoButton" Text="Get Theme Info" runat="server" OnClick="GetThemeInfo_Click" />
            <asp:Button ID="ApplyThemeButton" Text="Apply New Theme" runat="server" OnClick="ApplyTheme_Click" />
        </div>
    </div>
    </form>
</body>
</html>