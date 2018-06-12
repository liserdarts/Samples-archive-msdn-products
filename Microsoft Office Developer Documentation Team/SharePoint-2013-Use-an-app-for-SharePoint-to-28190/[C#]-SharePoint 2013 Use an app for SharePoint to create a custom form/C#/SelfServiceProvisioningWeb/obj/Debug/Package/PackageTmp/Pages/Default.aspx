<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SelfServiceProvisioningWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Start a new site</title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AppStyles.css" />
    <script type="text/javascript" src="../Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="../Scripts/UXScript.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var isDialog = decodeURIComponent(getQueryStringParameter('IsDlg'));
            if (isDialog == '1') {
                MakeSSCDialogPageVisible();
                UpdateSSCDialogPageSize();
            }
            $('.liSiteType').click(function () {
                $('.liSiteType').removeClass('ms-core-listMenu-selected');
                $(this).addClass('ms-core-listMenu-selected');
                $('#divBasePath').html($(this).attr('data-BasePath'));
                $('#hdnSelectedTemplate').val($(this).attr('data-Template'));
            });

            $('#btnCreate').click(function () {
                $('#divWaitingPanel').css('display', 'block');
            });

            $('#txtTitle').keyup(function (event) {
                $('#txtUrl').val($('#txtTitle').val());
            });

            //initialize the divAdministrators box
            if ($('#hdnAdministratorDisplayNames').val().length > 0) {
                var names = $('#hdnAdministratorDisplayNames').val().split('!');
                var txt = '';
                for (i = 0; i < names.length; i++)
                    txt += '<span class="txtLookupSelected">' + names[i] + '</span>; ';
                $('#divAdministrators').html(txt);
            }

            //wire keyup on textbox to do user lookups
            $('#divAdministrators').keyup(function (event) {
                var txt = $('#divAdministrators').html();
                if (txt.length > 0) {
                    var searchText = txt;

                    //clean up search based on previous selections
                    if (txt.indexOf('<span') != -1) {
                        var items = txt.split('</span>;');
                        searchText = items[items.length - 1].trim();
                    }

                    var query = new SP.UI.ApplicationPages.ClientPeoplePickerQueryParameters();
                    query.set_allowMultipleEntities(false);
                    query.set_maximumEntitySuggestions(50);
                    query.set_principalType(1);
                    query.set_principalSource(15);
                    query.set_queryString(searchText);
                    var searchResult = SP.UI.ApplicationPages.ClientPeoplePickerWebServiceInterface.clientPeoplePickerSearchUser(context, query);
                    context.executeQueryAsync(function () {
                        var results = context.parseObjectFromJsonString(searchResult.get_value());
                        var txtResults = '';
                        if (results) {
                            if (results.length > 0) {
                                for (var i = 0; i < results.length; i++) {
                                    var item = results[i];
                                    var loginName = item['Key'];
                                    var displayName = item['DisplayText'];
                                    var title = item['EntityData']['Title'];
                                    txtResults += '<div class=\'ms-bgHoverable\' style=\'width: 200px; padding: 4px;\' onclick=\'javascript:recipientSelected(\"' + loginName + '\", \"' + displayName + '\")\'>' + displayName + '<br/>' + title + '</div>';
                                }
                            }
                            txtResults += '<div class=\'ms-emphasisBorder\' style=\'width: 200px; padding: 4px; border-left: none; border-bottom: none; border-right: none; cursor: default;\'>' + results.length + ' people matches</div>';
                            $('#divUserSearch').html(txtResults);

                            //display the suggestion box
                            $('#divUserSearch').css('display', 'block');
                        }
                        else {
                            //hide the suggestion box since results are null
                            $('#divUserSearch').css('display', 'none');
                        }
                    }, function () {
                        alert('Error performing user search');
                    });
                }
                else {
                    //hide the suggestion box
                    $('#divUserSearch').css('display', 'none');

                    //clear hidden fields
                    $('#hdnAdministratorLogins').val('');
                    $('#hdnAdministratorDisplayNames').val('');
                }
            });
        });

        //function that is fired when a user is selected from the suggestions dialog
        function recipientSelected(login, name) {
            $('#divUserSearch').css('display', 'none');

            if ($('#hdnAdministratorLogins').val().length == 0)
                $('#hdnAdministratorLogins').val(login)
            else
                $('#hdnAdministratorLogins').val($('#hdnAdministratorLogins').val() + '!' + login);

            if ($('#hdnAdministratorDisplayNames').val().length == 0)
                $('#hdnAdministratorDisplayNames').val(name)
            else
                $('#hdnAdministratorDisplayNames').val($('#hdnAdministratorDisplayNames').val() + '!' + name);

            var txt = $('#divAdministrators').html();
            var newTxt = '';
            if (txt.indexOf('<span') != -1) {
                var items = txt.split('</span>; ');
                for (i = 0; i < items.length - 1; i++)
                    newTxt += '<span class="txtLookupSelected">' + items[i].substring(32) + '</span>; ';
            }
            newTxt += '<span class="txtLookupSelected">' + name + '</span>; ';
            $('#divAdministrators').html(newTxt);
        }
    </script>
</head>
<body style="display: none;">
    <form id="form1" runat="server">
    <div id="divSPChrome"></div>
    <div id="divWaitingPanel" style="position: absolute; z-index: 3; background: rgb(255, 255, 255); width: 100%; display: none; bottom: 0px; top: 0px;">
        <div style="top: 40%; position: absolute; left: 50%; margin-left: -150px;">
            <img alt="Working on it" src="data:image/gif;base64,R0lGODlhEAAQAIAAAFLOQv///yH/C05FVFNDQVBFMi4wAwEAAAAh+QQFCgABACwJAAIAAgACAAACAoRRACH5BAUKAAEALAwABQACAAIAAAIChFEAIfkEBQoAAQAsDAAJAAIAAgAAAgKEUQAh+QQFCgABACwJAAwAAgACAAACAoRRACH5BAUKAAEALAUADAACAAIAAAIChFEAIfkEBQoAAQAsAgAJAAIAAgAAAgKEUQAh+QQFCgABACwCAAUAAgACAAACAoRRACH5BAkKAAEALAIAAgAMAAwAAAINjAFne8kPo5y02ouzLQAh+QQJCgABACwCAAIADAAMAAACF4wBphvID1uCyNEZM7Ov4v1p0hGOZlAAACH5BAkKAAEALAIAAgAMAAwAAAIUjAGmG8gPW4qS2rscRPp1rH3H1BUAIfkECQoAAQAsAgACAAkADAAAAhGMAaaX64peiLJa6rCVFHdQAAAh+QQJCgABACwCAAIABQAMAAACDYwBFqiX3mJjUM63QAEAIfkECQoAAQAsAgACAAUACQAAAgqMARaol95iY9AUACH5BAkKAAEALAIAAgAFAAUAAAIHjAEWqJeuCgAh+QQJCgABACwFAAIAAgACAAACAoRRADs=" style="width: 32px; height: 32px;" />
            <span class="ms-accentText" style="font-size: 36px;">&nbsp;Working on it...</span>
        </div>
    </div>
    <div style="left: 50%; width: 450px; margin-left: -225px; position: absolute;">
        <div id="divFieldTemplate" style="display: table;">
            <h3 class="ms-core-form-line">Pick a template</h3>
            <div class="ms-core-form-line">
                <asp:Repeater ID="repeaterTemplate" runat="server">
                    <HeaderTemplate>
                        <ul style="list-style-type: none; height: 100px;">
                    </HeaderTemplate>
                    <ItemTemplate>
                            <li class="liSiteType ms-core-menu-item" data-BasePath='<%# DataBinder.Eval(Container.DataItem, "BasePath") %>' data-Template='<%# DataBinder.Eval(Container.DataItem, "Title") %>'>
                                <div class="liSiteTypeContainer">
                                    <img src='<%# DataBinder.Eval(Container.DataItem, "ImageUrl") %>' alt='<%# DataBinder.Eval(Container.DataItem, "Title") %>' class="imgSiteType" />
                                    <h3 class="ms-core-form-line liSiteTypeTitle"><%# DataBinder.Eval(Container.DataItem, "Title") %></h3>
                                </div>
                            </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField ID="hdnSelectedTemplate" runat="server" />
            </div>
        </div>
        <div id="divFieldTitle" style="display: table;">
            <h3 class="ms-core-form-line">Give it a name</h3>
            <div class="ms-core-form-line">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="ms-fullWidth"></asp:TextBox>
            </div>
            <div style="float: left; white-space: nowrap; padding-bottom: 10px; width: 450px;">
                <div style="width: 320px; font-size: 13px; float: left; padding-top: 2px;" id="divBasePath">
                    https://richdizzcom.sharepoint.com/sites/Communities/
                </div>
                <div style="width: 130px; float: left;">
                    <asp:TextBox ID="txtUrl" runat="server" CssClass="ms-fullWidth"></asp:TextBox>
                </div>
            </div>
        </div>
        <div id="divFieldDescription">
            <h3 class="ms-core-form-line">Give it a description</h3>
            <div class="ms-core-form-line">
                <asp:TextBox ID="txtDescription" runat="server" CssClass="ms-fullWidth" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </div>
        </div>
        <div id="divFieldOwners">
            <h3 class="ms-core-form-line">Add additional owners (everyone needs a backup)</h3>
            <div class="ms-core-form-line">
                <div contenteditable="true" id="divAdministrators" class="divUserLookup txtLookup ms-fullWidth"></div>
                <div id="divUserSearch" class="ms-emphasisBorder" style="position: absolute; z-index: 2; display: none; cursor: pointer; background-color: #fff;"></div>
                <asp:HiddenField ID="hdnAdministratorLogins" runat="server" />
                <asp:HiddenField ID="hdnAdministratorDisplayNames" runat="server" />
            </div>
        </div>
        <div id="divButtons" style="float: right;">
            <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="ms-ButtonHeightWidth" OnClick="btnCreate_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ms-ButtonHeightWidth" />
        </div>
    </div>
    </form>
</body>
</html>
