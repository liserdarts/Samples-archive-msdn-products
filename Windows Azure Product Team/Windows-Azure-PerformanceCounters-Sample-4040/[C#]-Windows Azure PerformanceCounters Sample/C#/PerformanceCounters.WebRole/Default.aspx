<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PerformanceCounters.WebRole._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
      Performance Counter Configuration
    </h2>
    <p>
        This application demonstrates the following techniques for the configuration 
        and monitoring of performance counters within a role using the Windows Azure 
        diagnostic monitor:</p>
    <ul>
        <li>Configuring performance counters with the ServiceConfiguration.cscfg file </li>
        <li>Changing the performance counters that are monitored </li>
        <li>Changing the performance counter sampling rate </li>
        <li>Changing the interval for transferring diagnostic data to Windows Azure storage </li>
        <li>Creating, updating, and monitoring custom performance counters </li>
        <li>Reporting on the performance counter diagnostic data in Windows Azure storage </li>
    </ul>
    <p>
        Using the ServiceConfiguration.cscfg file allows changes to be made dynamically 
        without stopping running instances of the role. A web role or worker 
        role instance must be run with elevated privileges to create custom performance 
        counters within the instance, though this is not required to make use of custom 
        performance counters that are created by an elevated startup task. The demonstrated custom 
        performance counter creation technique is more suited to internal facing roles 
        than externally accessible web roles, given the security exposure of an elevated process. 
    </p>
    <p>
        Use the <a href="CountersPage.aspx">Counters</a> page tab to see the most recent performance counters information
        transferred to Windows Azure storage.
    </p>
    <p>
        Use the <a href="CustomPage.aspx">Custom</a> page tab to see the use of custom performance counters to track
        user generated events.
    </p>
    <p>
        Currently configured Performance counters:
    </p>
    <p><asp:ListBox ID="ListBox1" runat="server" Width="540px" Height="218px"></asp:ListBox></p>
    <p>
        For more information on this topic, see <a href="http://msdn.microsoft.com/en-us/library/gg433120.aspx">
        Monitoring Hosted Services and Logging Data in Windows Azure</a>. For the managed
        API reference, see the <a href="http://msdn.microsoft.com/en-us/library/microsoft.windowsazure.diagnostics.aspx">
        Microsoft.WindowsAzure.Diagnostics Namespace</a> documentation. 
    </p>
</asp:Content>
