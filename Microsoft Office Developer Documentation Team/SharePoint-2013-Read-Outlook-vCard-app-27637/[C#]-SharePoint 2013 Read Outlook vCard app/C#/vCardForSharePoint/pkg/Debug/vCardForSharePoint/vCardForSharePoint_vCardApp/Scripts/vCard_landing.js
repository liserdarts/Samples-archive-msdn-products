var userProperties = null;
var userDrpSrc = new Array();
var hostweburl = null;
var appweburl = null;
var vcardlistid = null;
var listUrl = null;

jQuery(document).ready(function () {
    SP.SOD.executeFunc('sp.js', 'SP.ClientContext', startloading);
});

function startloading() {
    hostweburl = getvaluefromQueryString('SPHostUrl');
    appweburl = getvaluefromQueryString('SPAppWebUrl');
    jQuery("#ctl00_onetidHeadbnnr2").attr('src', '/DownloadvCard/Images/siteicon.png');
    var IsDlgMode = getvaluefromQueryString('IsDlg');
    if (!IsDlgMode) {
        getVCardListID();
        bindListItemForm();
    }
}

// get the vcard list id.
function getVCardListID() {
    var clientContext = new SP.ClientContext.get_current();
    var web = clientContext.get_web();
    var lists = web.get_lists();
    this.vcardlist = lists.getByTitle('vCard Properties');
    clientContext.load(this.vcardlist, 'Id');
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onListGetSucess),
        Function.createDelegate(this, this.onMethodFailed)
    );
}

function onListGetSucess(sender, args) {
    vcardlistid = this.vcardlist.get_id();
    listUrl = appweburl + "Lists/vCardProperties/AllItems.aspx"
}

function onMethodFailed(sender, args) {
    var err = 'Request failed. ' + args.get_message() +
        '\n' + args.get_stackTrace();
    logError(err, false);
}
// launch our custom new form
function newItemForm() {
    var launchOptions = {
        url: _spPageContextInfo.webAbsoluteUrl + "/vCardForms/NewForm.aspx?List=" + vcardlistid + "&APPHostUrl=" + _spPageContextInfo.webAbsoluteUrl,
        title: "New Item",
        allowMaximize: false,
        showClose: true,
        /*width: 420,
        height: 430,*/
        autoSize: true,
        dialogReturnValueCallback: landingDialogCallback
    };

    SP.UI.ModalDialog.showModalDialog(launchOptions);
}

// launch our custom view form
function viewItemForm(id) {
    var launchOptions = {
        url: _spPageContextInfo.webAbsoluteUrl + "/vCardForms/DispForm.aspx?ID=" + id + "&List=" + vcardlistid + "&APPHostUrl=" + _spPageContextInfo.webAbsoluteUrl,
        title: "View Item",
        allowMaximize: false,
        showClose: true,
        /*width: 420,
        height: 430,*/
        autoSize: true,
        dialogReturnValueCallback: landingDialogCallback
    };

    SP.UI.ModalDialog.showModalDialog(launchOptions);
}

// launch our custom edit form
function editItemForm(id) {
    var launchOptions = {
        url: _spPageContextInfo.webAbsoluteUrl + "/vCardForms/EditForm.aspx?ID=" + id + "&List=" + encodeURIComponent(vcardlistid),
        title: "Edit Item",
        allowMaximize: false,
        showClose: true,
        /*width: 420,
        height: 430,*/
        autoSize: true,
        dialogReturnValueCallback: landingDialogCallback
    };

    SP.UI.ModalDialog.showModalDialog(launchOptions);
}

function landingDialogCallback(dialogResult, data) {
    if (dialogResult == SP.UI.DialogResult.OK) {
        SP.UI.ModalDialog.RefreshPage(dialogResult);
    }
}

// ovveride the idHomePageNewItem anchor to launch the our dialog insted of navigating to the oob new form
function bindListItemForm() {
    var newitemanchor = jQuery("a[id='idHomePageNewItem']");
    if (!(newitemanchor === null)) {
        var html = jQuery('<div>').append(newitemanchor.clone()).html();
        var tdColumn = jQuery(".ms-list-addnew");
        tdColumn.html(html);
    }

    newitemanchor = jQuery("a[id='idHomePageNewItem']");
    if (!(newitemanchor === null)) {
        newitemanchor.attr("href", "");
        newitemanchor.removeAttr("onclick").unbind('click');
        newitemanchor.click(function () {
            newItemForm();
            return false;
        });
    }
}

// get query string values
function getvaluefromQueryString(param) {
    var pageURL = window.location.search.substring(1);
    var urlVariables = pageURL.split('&');
    for (var i = 0; i < urlVariables.length; i++) {
        var parameterName = urlVariables[i].split('=');
        if (parameterName[0] == param) {
            return decodeURIComponent(parameterName[1]);
        }
    }

    return null;
}