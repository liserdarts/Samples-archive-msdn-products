<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SOAPManagement.Default" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SOAP Management Sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <h3>Click a report in the left pane to view it in the right pane.</h3>
    <br />
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridViewRSItems" EventName="RowCommand" />
            </Triggers>                            
            <ContentTemplate>        
                <table id="tbl" cellpadding="0px" cellspacing="0px">            
                    <tr>
                        <td style="border:solid 1px black" valign="top">
                            <div style="overflow:auto;width:400px;height:450px;">
                                <asp:GridView 
                                    ID="GridViewRSItems" runat="server" 
                                    EmptyDataText="No items under this path." 
                                    GridLines="None" Width="100%" BorderStyle="None" 
                                    OnRowDataBound="GridViewRSItems_RowDataBound" 
                                    OnRowCommand="GridViewRSItems_RowCommand" 
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                    <HeaderStyle HorizontalAlign="Left" BackColor="BurlyWood" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:LinkButton 
                                                    ID="LinkButtonItemPath" runat="server" 
                                                    Font-Underline="false" 
                                                    ForeColor="black" /> 
                                            </ItemTemplate>     
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelType" runat="server" />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSize" runat="server" />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                        <td style="border:solid 1px black" valign="top">
                            <div style="overflow:auto;width:500px;height:450px;">
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" 
                                    Width="100%" Font-Names="Verdana" Font-Size="8pt" 
                                    InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" 
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                    <ServerReport ReportServerUrl="" />
                                </rsweb:ReportViewer>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>             
    </div>
    </form>
</body>
</html>
