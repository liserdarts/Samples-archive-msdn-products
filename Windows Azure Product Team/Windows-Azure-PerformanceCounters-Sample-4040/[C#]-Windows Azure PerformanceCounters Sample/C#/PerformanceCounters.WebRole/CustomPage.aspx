<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="CustomPage.aspx.cs" Inherits="PerformanceCounters.WebRole.CustomPage" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        // Once a button is clicked, don't allow more clicks until the page refreshes
        function DisableButtons() {
            document.getElementById("<%=Button1.ClientID %>").disabled = true;
            document.getElementById("<%=Button2.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButtons;
    </script>
    <h2>
        Custom Performance Counters
    </h2>
    <p>
        Use these buttons to generate user activity tracked in the custom performance counters. 
        If you load another copy of this page in the browser, you can see that 
        the totals reflect activity in both pages, but only for the running instance of 
        the web role:
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Button 1" 
            onclick="Button1_Click" />
    </p>
    <p>
        <asp:Button ID="Button2" runat="server" Text="Button 2" 
            onclick="Button2_Click" />
    </p>
    <p>
        Button 1 click total currently recorded in the local performance counter:
        <asp:Label ID="LabelLocal1" runat="server" Font-Bold="true" Text=""></asp:Label></p>
    <p>
        Button 2 click total currently recorded in the local performance counter:
        <asp:Label ID="LabelLocal2" runat="server" Font-Bold="true" Text=""></asp:Label></p>
    <p>
        The performance counter data for the custom counters is queried when the page updates. 
        This page updates automatically about every thirty seconds. The most recent values 
        from the diagnostics results in Windows Azure storage for the custom performance 
        counters are shown below. Note that the performance counter data does not change until 
        the <strong>WADPerformanceCountersTable</strong> table in the storage account for the diagnostic data 
        has been updated, which means there may be a lag of a couple of minutes or more before 
        the counters below display the most recent user activity data:</p>
    <p>
        Button 1 click total currently recorded in Windows Azure storage:
        <asp:Label ID="LabelButton1" runat="server" Font-Bold="true" Text=""></asp:Label></p>
    <p>
        Button 2 click total currently recorded in Windows Azure storage:
        <asp:Label ID="LabelButton2" runat="server" Font-Bold="true" Text=""></asp:Label></p>
</asp:Content>
