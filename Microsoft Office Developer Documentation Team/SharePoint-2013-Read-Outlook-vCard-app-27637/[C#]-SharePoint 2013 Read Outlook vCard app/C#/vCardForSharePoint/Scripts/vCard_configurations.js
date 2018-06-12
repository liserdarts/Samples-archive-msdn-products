

var userProperties = null;

jQuery(document).ready(function () {
    if (window.location.href.toLowerCase().indexOf("/pages/configurations.aspx") != -1) {
        appweburl = _spPageContextInfo.webAbsoluteUrl;
        var IsDlgMode = getvaluefromQueryString('IsDlg');
        clientContext = new SP.ClientContext.get_current();
        if (IsDlgMode) {
            jQuery.when(initializeSettings()).then(function (d) {
                settings = d;
                initializeConfigurations();
            }, function (err) {
                logError(err, false);
            });
        }
    }
});

// initialize the settings dialog
function initializeConfigurations() {
    try {
        jQuery.when(getUserProfilePropertyNames())
        .then(function (data) {
            userProperties = data.d.UserProfileProperties.results;
            updatefieldMarkup();
        }).fail(function (error) {
            var errorobj = jQuery.parseJSON(error.responseText);
            logError(errorobj.error.message.value, false);
        });
    } catch (e) {
        logError(e.message, false);
    }
}

// update the html controls in settings dialog with the preselected values
function updatefieldMarkup() {
    var picsetting = jQuery.grep(settings, function (s) {
        if (s.SettingName == "DOWNLOADPROFILEPIC") {
            return s;
        }
    });

    if (picsetting.length > 0 && picsetting[0].SettingValue != null && jQuery.trim(picsetting[0].SettingValue) != '') {
        if (picsetting[0].SettingValue == "true") {
            jQuery('#downloadProfilePic').attr("checked", "checked")
        }
        else {
            jQuery('#downloadProfilePic').removeAttr("checked");
        }
    }
    else {
        jQuery('#downloadProfilePic').attr("checked", "checked");
    }

    var sepsetting = jQuery.grep(settings, function (s) {
        if (s.SettingName == "VCFSEPERATOR") {
            return s;
        }
    });

    if (sepsetting.length > 0 && sepsetting[0].SettingValue != null) {
        jQuery('#propertySeperator').val(sepsetting[0].SettingValue);
    }
    else if (sepsetting.length > 0 && (sepsetting[0].SettingValue == null || jQuery.trim(sepsetting[0].SettingValue) == '')) {
        jQuery('#propertySeperator').val(' ');
    }

    var postToFeed = jQuery.grep(settings, function (s) {
        if (s.SettingName == "POSTNEWSFEED") {
            return s;
        }
    });

    if (postToFeed.length > 0 && postToFeed[0].SettingValue != null && jQuery.trim(postToFeed[0].SettingValue) != '') {
        if (postToFeed[0].SettingValue == "true") {
            jQuery('#postToFeed').attr("checked", "checked")
        }
        else {
            jQuery('#postToFeed').removeAttr("checked");
        }
    }
    else {
        jQuery('#postToFeed').attr("checked", "checked");
    }

    generateProfilePropMarkup();
}

// build the profile properties dropdown
function generateProfilePropMarkup() {
    var choicehtml = '<select id="profileprops" name="profileprops" multiple="multiple" style="width:250px;margin-left:0px">';
    try {
        var namesetting = jQuery.grep(settings, function (s) {
            if (s.SettingName == "VCFFILENAME") {
                return s;
            }
        });

        var preselectedvalue = new Array();
        if (namesetting.length > 0 && namesetting[0].SettingValue != null && jQuery.trim(namesetting[0].SettingValue) != '') {
            preselectedvalue = namesetting[0].SettingValue.split(";");
        }

        for (i = 0; i < userProperties.length; i++) {
            if (preselectedvalue.length > 0 && jQuery.inArray(userProperties[i].Key, preselectedvalue) != -1) {
                choicehtml += '<option selected="selected" value="' + userProperties[i].Key + '">' + userProperties[i].Key + '</option>';
            }
            else {
                choicehtml += '<option value="' + userProperties[i].Key + '">' + userProperties[i].Key + '</option>';
            }
        }

        choicehtml += '</select>&nbsp;<img id="expandicon" src="/_layouts/15/images/EXPAND.GIF" onclick="openSequenceSection();"/>';
        choicehtml += '&nbsp;<span>Reorder properties</span>';
        choicehtml += '<div style="padding-top:3px"><input class="ms-long" style="display:none;width:300px;" type="text" id="profilepropsvalue" value="' + preselectedvalue.join(';') + '"></input></div>';

        jQuery('#outervcffilediv').append(choicehtml);
        jQuery("#profileprops").multiselect();
        jQuery("#profileprops").bind("multiselectclose", function (event, ui) {
            updatePropValues();
        });
    } catch (e) {
        logError(e.message, false);
    }
}

// get the user profile property names
function getUserProfilePropertyNames() {
    return jQuery.ajax({
        url: appweburl + "/_api/SP.UserProfiles.PeopleManager/GetMyProperties",
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
    }).promise();
}

function updatePropValues() {
    var choiceTable = jQuery("input[id^='profilepropsvalue']");
    choiceTable.val('');
    var text = '';
    var value = jQuery('#profileprops').val();
    if (value != null) {
        for (i = 0; i < value.length; i++) {
            if (i == value.length - 1) {
                text += value[i];
            }
            else {
                text += value[i] + ";";
            }
        }

        choiceTable.val(text)
    }
}

// expand the text box to allow reordering the properties
function openSequenceSection() {
    var choiceTable = jQuery("input[id^='profilepropsvalue']");
    if (choiceTable.is(":visible")) {
        choiceTable.slideUp(500);
        jQuery("#expandicon").attr('src', '/_layouts/15/images/EXPAND.GIF');
    }
    else {
        jQuery("#expandicon").attr('src', '/_layouts/15/images/COLLAPSE.GIF');
        choiceTable.slideDown(500);
    }
}

function closeDialog() {
    //SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, 'Closed with OK result');
    SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.cancel, 'Cancelled');
}

// persist the changes made in the configurations and close dialog
function acceptChangeAndClose() {
    try {
        if (validateforminput()) {
            for (k in settings) {
                switch (settings[k].SettingName) {
                    case "DOWNLOADPROFILEPIC":
                        if (jQuery('#downloadProfilePic').attr('checked') != null) {
                            settings[k].SettingValue = 'true';
                        }
                        else {
                            settings[k].SettingValue = 'false';
                        }

                        break;
                    case "VCFFILENAME":
                        var propval = jQuery.trim(jQuery('#profilepropsvalue').val());
                        while (propval.lastIndexOf(';') == (propval.length - 1)) {
                            propval = propval.substr(0, propval.lastIndexOf(';'));
                        }

                        settings[k].SettingValue = propval;

                        break;
                    case "VCFSEPERATOR":
                        settings[k].SettingValue = jQuery('#propertySeperator').val();

                        break;
                    case "POSTNEWSFEED":
                        if (jQuery('#postToFeed').attr('checked') != null) {
                            settings[k].SettingValue = 'true';
                        }
                        else {
                            settings[k].SettingValue = 'false';
                        }

                        break;
                }
            }

            updateConfigurations();
        }
    } catch (e) {
        logError(e.message, false);
    }
}

// perform validation on the setting values
function validateforminput() {
    var seperator = jQuery('#propertySeperator').val();
    var index = seperator.match(/[\\/:*?"<>|]/g);
    if (index != null) {
        alert('The field "VCF File Name: Seperator" cannot contain any of the following characters \ / : * ? " < > | ');

        return false;
    }

    var propval = jQuery.trim(jQuery('#profilepropsvalue').val());
    while (propval.lastIndexOf(';') == (propval.length - 1)) {
        propval = propval.substr(0, propval.lastIndexOf(';'));
    }

    var arr = propval.split(';');
    if (arr.length < 1 || arr[0] == '') {
        alert('The field "VCF File Name: Properties" cannot be empty.');

        return false;
    }
    else {
        for (k in arr) {
            var found = jQuery.grep(userProperties, function (s) {
                if (s.Key == arr[k]) {
                    return true;
                }
            });

            if (found.length < 1) {
                alert('The field VCF File Name: Properties contains an invalid property.');

                return false;
            }
        }
    }

    return true;
}

// save changes to the configurations list
function updateConfigurations() {
    var configList = clientContext.get_web().get_lists().getByTitle('configurations');
    for (k in settings) {
        this.listItem = configList.getItemById(settings[k].SettingID);
        listItem.set_item('Value', settings[k].SettingValue);
        listItem.update();
    }

    clientContext.executeQueryAsync(Function.createDelegate(this, this.onConfigUpdateSucceeded), Function.createDelegate(this, this.onConfigUpdateFailed));
}

function onConfigUpdateSucceeded() {
    //SP.SOD.executeFunc('sp.ui.dialog.js', 'SP.UI.ModalDialog.showModalDialog', function () { closeDialog(); });
    setTimeout(function () { window.frameElement.cancelPopUp(); }, 0);
}

function onConfigUpdateFailed(sender, args) {
    var error = 'Request failed. ' + args.get_message() + '\n' + args.get_stackTrace();
    logError(error, false);
}
