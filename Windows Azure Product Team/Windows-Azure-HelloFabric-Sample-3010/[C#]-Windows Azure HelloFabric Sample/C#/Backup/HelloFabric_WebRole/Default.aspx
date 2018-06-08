<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Microsoft.Samples.ServiceHosting.HelloFabric._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hello Fabric</title>
</head>

<body style="font-family: Arial, Helvetica, sans-serif;">

    <h1>Hello Fabric</h1>

    <h3>General Information</h3>

    <p>There is a simple API for finding out whether an app runs inside the fabric, and whether SSL is enabled or not. 
    The current status is:</p>

    <form id="form1" runat="server" style="font-size: medium">
    <asp:ScriptManager runat="server" />
    <div>
        <asp:Label ID="Label3" runat="server" Text="Runing as ??"></asp:Label><br />
        <asp:Label ID="Label2" runat="server" Text="Secure connection ??"></asp:Label><br />
        <asp:Label ID="MemoryLabel" runat="server" Text="Physical memory: ??"></asp:Label>
    </div>    
    
    <hr />

    <h3>Configuration Settings</h3>
    
    <p>There is also an easy way for getting configuration settings that have been set in .cscfg files or Web.config files. The setting 
    called BannerText is currently configured as the following string:</p>
    
    <div>
        <asp:Label ID="Label1" runat="server" Text="Setting" ToolTip="This setting can be modified by updating the configuration settings and using the 'File->Update Configuration' menu of the devfabric."></asp:Label>
    </div>
    
    <hr />
    
    <h3>Diagnostics</h3>
    
    <p>Events can be logged so that they appear in the development fabric when running on the local machine. There is also a way to access logs 
    in the real fabric. When running inside a development fabric, please have a look at the HelloFabric_WebRole window inside the 
    development fabric application to see the messages you log here.</p>
    
    <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
        <asp:DropDownList ID="DropDownList1" runat="server" Width="20%" ToolTip="Set the level of the message to log.">
            <asp:ListItem>Error</asp:ListItem>
            <asp:ListItem>Warning</asp:ListItem>
            <asp:ListItem>Information</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server" Width="70%" ToolTip="Message to Log"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Log Event" OnClick="Button1_Click"
            ToolTip="Log an event at a known level." />
    </asp:Panel>
    
    <p>The Diagnostics feature also keeps track of failed web requests. Press 'Crash This Page!' to
    generate an error. Then, go back and press 'View Report' to view the corresponding Failed Request Tracing log file.  
    <br />
    <br />
    To configure Failed Request Tracing in your code, see the &lt;traceFailedRequests&gt; section in web.config.  Failed Request Tracing log files 
    are located in the 'DiagnosticStore' resource.  In the development fabric, right-click on a role instance, choose 'Open local store...', then 
    navigate to 'directory\DiagnosticStore' to view the log files locally.  The Diagnostics feature allows you to push these log files to your storage account.
    </p>
    
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Crash This Page!" 
            ToolTip="Push me to generate a web page crash and see what information is logged." />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="View Report" 
            ToolTip="Push me to view the error report." />
            
    </p>
    
    <p>In addition to explicit logging, Windows Azure Diagnostics also allows pushing various types of diagnostic data to the
    configured storage account. The example below illustrates how to do this from the service itself, however the common scenario
    would be for a client side tool to make the underlying calls from outside Windows Azure.</p>
        
    <asp:UpdatePanel runat="server" ID="LogTransferPanel">
        <ContentTemplate>
        <asp:Label ID="TransferErrorMessage" runat="server" ForeColor="Red" Visible="false" />
        <div id="TransferSetupPanel" runat="server">
            <p>Use the button below to transfer all Windows Azure logs from the last N minutes.</p>        
            <p>
                <asp:Button ID="PushAzure" runat="server" Text="Transfer Logs" onclick="PushAzure_Click" />
                in the last
                <asp:TextBox ID="PushAge" runat="server" Width="30%" Text="1" />
                minutes at level
                <asp:DropDownList ID="PushLevel" runat="server" Width="20%" ToolTip="Set the minimum level of event to push.">
                    <asp:ListItem>Critical</asp:ListItem>
                    <asp:ListItem>Error</asp:ListItem>
                    <asp:ListItem>Warning</asp:ListItem>
                    <asp:ListItem>Information</asp:ListItem>
                    <asp:ListItem Selected="True">Verbose</asp:ListItem>
                </asp:DropDownList>
                or above.
            </p>
            
            <p>Use the button below to schedule transfer of data from Performance Counters. In this sample the performance counter being collected is ‘\Proccesor(_Total)\% Processor Time’.</p>    
            <p>
                <asp:Button ID="PushPerf" runat="server" Text="Transfer Performance Counters" onclick="PushPerf_Click" />
                in the last
                <asp:TextBox ID="PushPerfAge" runat="server" Width="30%" Text="1" />
                minutes.
            </p>
            
            <p>Use the button below to transfer all IIS logs from the last N minutes.</p>
            <p>
                <asp:Button ID="PushIIS" runat="server" Text="Transfer IIS Logs" onclick="PushIIS_Click" />
                in the last
                <asp:TextBox ID="PushIISAge" runat="server" Width="30%" Text="1" />
                minutes.
            </p>
        </div>
        <div id="TransferProgressPanel" runat="Server" visible="false">
            <p><i>Transferring...</i></p>
            <asp:Timer ID="TransferProgressTimer" runat="server" Interval="1000" OnTick="TransferProgressTimer_Tick" />
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <hr />
    
    <h3>External Connections</h3>
    
    <p>Both Web and Worker roles in Windows Azure are allowed to make connections to external sites. Use the two buttons below to perform a test connection via HTTP or TCP.</p>
     <asp:Panel ID="DNSResolutionErrorPanel" runat="server" Visible="false">
        <asp:Label ID="DNSResolutionErrorLabel" runat="server" ForeColor="Red" />
    </asp:Panel>
    <p>Use the following form to submit an <a href="http://www.w3.org/Protocols/rfc2616/rfc2616.txt">HTTP</a> GET request to the specified URL.</p>
    <p>
        Address:
        <asp:TextBox ID="HttpAddress" runat="server" Width="70%">http://www.example.com/</asp:TextBox>
        <asp:Button ID="HttpButton" runat="server" OnClick="HttpButton_Click" Text="Make HTTP Request" />
    </p>
    <asp:Panel ID="HttpResponsePanel" runat="server" Visible="false">
        <p>Response from server:</p>
        <pre><asp:Label ID="HttpResponseLabel" runat="server" /></pre>
    </asp:Panel>
    
    <p>Use the form below to send an <a href="http://www.ietf.org/rfc/rfc977.txt">NNTP</a> command to the Microsoft public news server.</p>
    <p>
        NNTP Command:
        <asp:TextBox ID="NntpCommand" runat="server" Width="20%">help</asp:TextBox>
        <asp:Button ID="NntpButton" runat="server" OnClick="NntpButton_Click" Text="Make TCP Request" />
    </p>
    <asp:Panel ID="NntpResponsePanel" runat="server" Visible="false">
        <p>Response from server:</p>
        <pre><asp:Label ID="NntpResponseLabel" runat="server" /></pre>
    </asp:Panel>
    
    <hr />
    
    <h3>Local Storage</h3>    
    
    <p>Windows Azure also provides local storage for each role that is accessible via standard file I/O operations.
    This storage is persisted for the lifetime of the role. You can use the following textbox to store a message in local storage.
    This message in local storage one will be persisted until the role gets recycled, while the message in local storage two
    will persist across role recycles.</p>
    
    <asp:Panel ID="StoredMessagePanel1" runat="server">
        Message stored in local storage one: <pre><asp:Label ID="StoredMessageLabel1" runat="server"></asp:Label></pre>
    </asp:Panel>
    <asp:Panel ID="StoredMessageErrorPanel1" runat="server">
        <asp:Label ID="StoredMessageErrorLabel1" runat="server" ForeColor="Red"></asp:Label>
    </asp:Panel>
    
    <div>
        <asp:TextBox ID="InputMessageTextBox1" runat="server" Width="70%" ToolTip="Message to Log"></asp:TextBox>
        <asp:Button ID="StoreMessage1" runat="server" Text="Store Message" OnClick="StoreMessage1_Click"
            ToolTip="Store this message in the first local storage" />
    </div>

    <asp:Panel ID="StoredMessagePanel2" runat="server">
        Message stored in local storage two: <pre><asp:Label ID="StoredMessageLabel2" runat="server"></asp:Label></pre>
    </asp:Panel>
    <asp:Panel ID="StoredMessageErrorPanel2" runat="server">
        <asp:Label ID="StoredMessageErrorLabel2" runat="server" ForeColor="Red"></asp:Label>
    </asp:Panel>
    
    <div>
        <asp:TextBox ID="InputMessageTextBox2" runat="server" Width="70%" ToolTip="Message to Log"></asp:TextBox>
        <asp:Button ID="StoreMessage2" runat="server" Text="Store Message" OnClick="StoreMessage2_Click"
            ToolTip="Store this message in the second local storage" />
    </div>

    <hr />

    <h3>Internal Endpoints</h3>
    
    <p>Windows Azure roles can expose internal endpoints for inter-role communication. The worker role is exposing a
    simple TCP server that returns the current time:</p>
    
    <div>
        Time reported by worker role: <asp:Label ID="WorkerTime" runat="server" />
    </div>
    
    </form>
</body>
</html>
