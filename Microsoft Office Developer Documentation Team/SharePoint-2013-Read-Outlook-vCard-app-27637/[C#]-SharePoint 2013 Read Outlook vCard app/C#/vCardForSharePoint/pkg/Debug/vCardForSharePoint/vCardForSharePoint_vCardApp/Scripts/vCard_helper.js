
// setting objects
var settings = null;
var profileProperties = null;
var appweburl = null;
var accountName = null;

// initialize the settings object
function initializeSettings() {
    var deferred = jQuery.Deferred();

    try {
        var confsettings = [
                       {
                           SettingID: '',
                           SettingName: 'DOWNLOADPROFILEPIC',
                           SettingValue: ''
                       },
                       {
                           SettingID: '',
                           SettingName: 'VCFFILENAME',
                           SettingValue: ''
                       },
                       {
                           SettingID: '',
                           SettingName: 'VCFSEPERATOR',
                           SettingValue: ''
                       },
                       {
                           SettingID: '',
                           SettingName: 'POSTNEWSFEED',
                           SettingValue: ''
                       }];

        jQuery.when(getappSettings())
            .then(function (data) {
                var results = data.d.results;
                for (k in confsettings) {
                    switch (confsettings[k].SettingName) {
                        case "DOWNLOADPROFILEPIC":
                            jQuery.grep(results, function (s) {
                                if (s.Title == "DOWNLOADPROFILEPIC") {
                                    confsettings[k].SettingValue = s.Value;
                                    confsettings[k].SettingID = s.ID;
                                }
                            });

                            break;
                        case "VCFFILENAME":
                            jQuery.grep(results, function (s) {
                                if (s.Title == "VCFFILENAME") {
                                    confsettings[k].SettingValue = s.Value;
                                    confsettings[k].SettingID = s.ID;
                                }
                            });

                            break;
                        case "VCFSEPERATOR":
                            jQuery.grep(results, function (s) {
                                if (s.Title == "VCFSEPERATOR") {
                                    confsettings[k].SettingValue = s.Value;
                                    confsettings[k].SettingID = s.ID;
                                }
                            });

                            break;
                        case "POSTNEWSFEED":
                            jQuery.grep(results, function (s) {
                                if (s.Title == "POSTNEWSFEED") {
                                    confsettings[k].SettingValue = s.Value;
                                    confsettings[k].SettingID = s.ID;
                                }
                            });

                            break;
                    }
                }
            })
            .done(function () {
                deferred.resolve(confsettings);
            })
            .fail(function (error) {
                var errorobj = jQuery.parseJSON(error.responseText);
                //logError(errorobj.error.message.value, false);
                deferred.reject(errorobj.error.message.value);
            });
    }
    catch (e) {
        logError(e.message, false);
    }

    return deferred.promise();
}

// fetch the settings from the configurations list
function getappSettings() {
    return jQuery.ajax({
        url: appweburl + "/_api/web/lists/getbytitle('configurations')/items",
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" }
        //success: function (data) {
        //    success(data);
        //},
        //error: function (data) {
        //    failure(data);
        //}
    }).promise();
}

// get the vcard property mappings
function getVcardPropertyMappings() {
    var query = "?$top=100&$select=UserProfilePropertyName,OutlookFieldName,FormatFunction";
    var deferred = jQuery.Deferred()
    jQuery.ajax({
        url: appweburl + "/_api/web/lists/getbytitle('vCard Properties')/items" + query,
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            var configurations = new Array();
            var objArr = data.d.results;
            for (i = 0; i < objArr.length; i++) {
                var configobject = new Object();
                configobject.OutlookProperty = objArr[i].OutlookFieldName;
                configobject.UPPropertyNameArray = objArr[i].UserProfilePropertyName.split(';');
                configobject.FormatCallback = objArr[i].FormatFunction;
                configurations.push(configobject);
            }

            deferred.resolve(configurations);
        },
        error: function (error) {
            var errorobj = JSON.parse(error.responseText);
            //logError(errorobj.error.message.value, false);
            deferred.reject(errorobj.error.message.value);
        }
    });

    return deferred.promise();
}

// utility function to aggregrate all properties in an array
function getflatPropertyList() {
    var propstring = new Array();
    for (i = 0; i < profileProperties.length; i++) {
        if (profileProperties[i].UPPropertyNameArray.length == 1) {
            propstring.push(profileProperties[i].UPPropertyNameArray[0]);
        }
        else {
            for (j = 0; j < profileProperties[i].UPPropertyNameArray.length; j++) {
                propstring.push(profileProperties[i].UPPropertyNameArray[j]);
            }
        }
    }

    return propstring;
}

// query the user profile service for the given account name and fetch the values for the required properties
function getUserProfilePropertyValues() {
    this.deferred = jQuery.Deferred();

    var peopleManager = new SP.UserProfiles.PeopleManager(clientContext);
    var profilePropertyNames = getflatPropertyList();
    var userProfilePropertiesForUser =
        new SP.UserProfiles.UserProfilePropertiesForUser(
            clientContext,
            accountName,
            profilePropertyNames);

    userProfilePropertyValues = peopleManager.getUserProfilePropertiesFor(userProfilePropertiesForUser);
    clientContext.load(userProfilePropertiesForUser);
    clientContext.executeQueryAsync(Function.createDelegate(this, onPropertiesGetSuccess), Function.createDelegate(this, onPropertiesGetFail));

    return this.deferred.promise();
}

function onPropertiesGetSuccess() {
    try {
        var cuindex = -1;
        for (j = 0; j < profileProperties.length; j++) {
            profileProperties[j].UPPropertyValueArray = new Array();
            if (profileProperties[j].UPPropertyNameArray.length > 1) {
                for (k = 0; k < profileProperties[j].UPPropertyNameArray.length; k++) {
                    cuindex++;
                    var value = userProfilePropertyValues[cuindex];
                    if (value == null) {
                        value = '';
                    }

                    if (jQuery.trim(value) != '' && profileProperties[j].FormatCallback != null && jQuery.trim(profileProperties[j].FormatCallback) != '') {
                        profileProperties[j].UPPropertyValueArray[k] = runFormatFunction(value, profileProperties[j].FormatCallback)
                    }
                    else {
                        profileProperties[j].UPPropertyValueArray[k] = value;
                    }
                }
            }
            else {
                cuindex++;
                var value = userProfilePropertyValues[cuindex];
                if (value == null) {
                    value = '';
                }

                if (jQuery.trim(value) != '' && profileProperties[j].FormatCallback != null && jQuery.trim(profileProperties[j].FormatCallback) != '') {
                    profileProperties[j].UPPropertyValueArray[0] = runFormatFunction(value, profileProperties[j].FormatCallback)
                }
                else {
                    profileProperties[j].UPPropertyValueArray[0] = value;
                }
            }
        }

        this.deferred.resolve(profileProperties);
    } catch (e) {
        this.deferred.reject(e.message);
    }
}

// execute the formatting rule on the given value
function runFormatFunction(value, callbackText) {
    var lastcolon = callbackText.lastIndexOf(";");
    if (lastcolon != -1 && callbackText.length != lastcolon + 1) {
        callbackText = callbackText + ";"
    }

    callbackText = callbackText + "return d;";
    try {
        var func = new Function("d", callbackText);
        var formattedData = func(value);
        return formattedData;
    }
    catch (err) {
        logAppErrorToSharePoint(err.message);
        return value;
    }
}

function onPropertiesGetFail(sender, args) {
    this.deferred.reject(args.get_message());
}

function logError(message, uiOnly) {
    if (!uiOnly) {
        logAppErrorToSharePoint(message);
    }

    alert("Error occured in vCard Download App: " + message);
}

//This funtion to log custom app error mesagges
function logAppErrorToSharePoint(message) {
    SP.Utilities.Utility.logCustomAppError(clientContext, message);
    clientContext.executeQueryAsync(
      function () { },
      function () { alert("Could not write to error log.") });
}

/*
    String format utility function
*/
String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }

    return s;
}

