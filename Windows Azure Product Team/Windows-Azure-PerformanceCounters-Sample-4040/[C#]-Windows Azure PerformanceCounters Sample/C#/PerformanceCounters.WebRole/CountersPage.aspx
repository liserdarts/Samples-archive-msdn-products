<%@ Page Title="Performance Counters" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CountersPage.aspx.cs" Inherits="PerformanceCounters.WebRole.CountersPage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Performance Counters
    </h2>
    <p>Current Performance Counter sample rate (in seconds): 
    <asp:Label ID="LabelSampleRate" runat="server" Font-Bold="true" Text=""></asp:Label></p>
    <p>Current Performance Counter storage transfer interval (in minutes): 
    <asp:Label ID="LabelTransferInterval" runat="server" Font-Bold="true" Text=""></asp:Label></p>
    <p>
        The following table shows the most recent five minutes' worth of data 
        for the performance counters currently configured in the diagnostic monitor 
        for this role instance, as of the most recent transfer to Windows Azure storage in the 
        <strong>WADPerformanceCountersTable</strong>. The table will not appear until after 
        this role instance has completed a transfer to Windows Azure storage and the page
        is refreshed, which happens automatically about every thirty seconds. The 
        initial transfer may take a couple of minutes to complete.
    </p>
    <p>
        The EventTickCount column records the time the performance counter data was sampled. 
        The Timestamp records the time the data was transferred to the 
        <strong>WADPerformanceCountersTable</strong> in Windows Azure storage.
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" Height="161px"
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="EventTickCount" HeaderText="EventTickCount" SortExpression="EventTickCount">
                </asp:BoundField>
                <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" SortExpression="Timestamp">
                </asp:BoundField>
                <asp:BoundField DataField="CounterName" HeaderText="CounterName" SortExpression="CounterName" />
                <asp:BoundField DataField="CounterValue" HeaderText="CounterValue" SortExpression="CounterValue" />
            </Columns>
        </asp:GridView>
    </p>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="PerformanceCounters.WebRole.PerformanceCountersDataSource">
    </asp:ObjectDataSource>
</asp:Content>
