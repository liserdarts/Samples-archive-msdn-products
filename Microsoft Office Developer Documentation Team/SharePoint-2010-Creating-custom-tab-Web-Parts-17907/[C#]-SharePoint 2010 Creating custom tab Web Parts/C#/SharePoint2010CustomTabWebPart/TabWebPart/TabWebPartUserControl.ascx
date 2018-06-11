<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabWebPartUserControl.ascx.cs" Inherits="SharePoint2010CustomTabWebPart.TabWebPart.TabWebPartUserControl" %>


<link type="text/css" rel="stylesheet" href="{0}" />

<script type="text/javascript">
    $(function () {
        $("#tabs<%= this.ClientID%>").tabs();
    });
</script>


<div id="tabs<%= this.ClientID%>">

	<ul>
        <asp:Repeater runat="server" ID="TabRepeater">
            <ItemTemplate>
                <li>
                    <a href="#tabs<%# this.ClientID%>-<%# Container.ItemIndex + 1 %>"><%# Container.DataItem%></a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
	</ul>

    <asp:Repeater runat="server" ID="TabContainerRepeater" onitemdatabound="TabContainerRepeater_ItemDataBound">
        <ItemTemplate>
	        <div id="tabs<%# this.ClientID%>-<%# Container.ItemIndex + 1 %>" class="ui-tabs-hide">
                <asp:Panel runat="server" ID="TabContainer"></asp:Panel>
	        </div>
        </ItemTemplate>
    </asp:Repeater>

</div>