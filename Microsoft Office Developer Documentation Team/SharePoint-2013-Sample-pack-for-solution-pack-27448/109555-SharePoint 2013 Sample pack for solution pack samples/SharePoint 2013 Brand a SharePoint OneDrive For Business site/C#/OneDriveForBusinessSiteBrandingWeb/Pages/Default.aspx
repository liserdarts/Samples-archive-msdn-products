<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PersonalSiteBrandingEditWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script
        type="text/javascript"
        src="../Scripts/jquery-1.8.2.js"> 
    </script>
    <script
        type="text/javascript"
        src="../Scripts/ChromeLoader.js"> 
    </script>
    <link rel="Stylesheet" type="text/css" href="../Styling/app.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Chrome control placeholder -->
        <div id="chrome_ctrl_placeholder"></div>
        <div id="ContentArea">
            <p>App should be added to public my site host or other web site associated to user profiles. After this a App part should be added to host web, which actually modifyes the site when it exists.</p>
            <p>
                This demo is used only for modifying already created personal sites when they are available and actual creation 
            process is still using out of the box timer based approach. You could however also override this model, if needed.
            </p>
            <p>
                <i>note. This is proof of concept code, whcih is not to be applied to production as such, rather to be used as example on the CAM model.</i>
            </p>
        </div>
    </form>
</body>
</html>
