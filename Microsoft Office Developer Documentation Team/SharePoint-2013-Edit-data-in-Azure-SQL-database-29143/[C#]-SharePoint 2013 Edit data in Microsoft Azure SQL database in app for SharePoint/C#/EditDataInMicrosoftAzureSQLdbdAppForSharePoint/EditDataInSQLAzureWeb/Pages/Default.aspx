<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EditDataInSQLAzureWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>How to edit data in Windows Azure SQL Databases from a Provider-hosted SharePoint application</title>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js"></script>
    <script type="text/javascript">
        var hostweburl;

        // Load the SharePoint resources.
        $(document).ready(function () {

            // Get the URI decoded app web URL.
            hostweburl = decodeURIComponent(getQueryStringParameter("SPHostUrl"));

            // The SharePoint js files URL are in the form:
            // web_url/_layouts/15/resource.js
            var scriptbase = hostweburl + "/_layouts/15/";

            // Load the js file and continue to the 
            // success handler.
            $.getScript(scriptbase + "SP.UI.Controls.js", renderChrome);
        });

        // Function to prepare the options and render the control.
        function renderChrome() {

            // The Help, Account, and Contact pages receive the 
            // same query string parameters as the main page.
            var options = {
                "appTitle": "How to edit data in Windows Azure SQL Databases from a Provider-hosted SharePoint application"
            };

            var nav = new SP.UI.Controls.Navigation("SharePointChromeControl", options);
            nav.setVisible(true);
        }

        // Function to retrieve a query string value.
        // For production purposes you may want to use
        // a library to handle the query string.
        function getQueryStringParameter(paramToRetrieve) {
            var params = document.URL.split("?").length > 1 ?
                document.URL.split("?")[1].split("&") : [];
            var strParams = "";
            for (var i = 0; i < params.length; i = i + 1) {
                var singleParam = params[i].split("=");
                if (singleParam[0] == paramToRetrieve)
                    return singleParam[1];
            }
        }
    </script>
    <link href="/Styles/Style.css" rel="stylesheet" />
</head>
<body style="overflow: auto">
    <div id="SharePointChromeControl"></div>

    <form id="form1" runat="server">
        <div style="margin-left: 45px">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CustomerID" DataSourceID="EntityDataSource1" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />
                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                    <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" />
                    <asp:BoundField DataField="ContactTitle" HeaderText="ContactTitle" SortExpression="ContactTitle" />
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                    <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                    <asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region" />
                    <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" SortExpression="PostalCode" />
                    <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
                </Columns>
            </asp:GridView>
            <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=SampleDBEntities" DefaultContainerName="SampleDBEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="Customers"></asp:EntityDataSource>
            <br />
            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="false" />
            <asp:Panel ID="panel1" runat="server" Visible="false">
                <table>
                    <tr>
                        <td>CompanyName</td>
                        <td>
                            <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCompanyName" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>ContactName</td>
                        <td>
                            <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>ContactTitle</td>
                        <td>
                            <asp:TextBox ID="txtContactTitle" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Address</td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
