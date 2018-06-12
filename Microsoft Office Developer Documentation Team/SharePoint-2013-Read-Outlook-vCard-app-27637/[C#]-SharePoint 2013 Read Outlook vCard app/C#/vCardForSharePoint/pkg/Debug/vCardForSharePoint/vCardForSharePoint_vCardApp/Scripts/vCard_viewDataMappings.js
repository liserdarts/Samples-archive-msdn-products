
var accountName = null;
var person;

jQuery(document).ready(function () {
    clientContext = new SP.ClientContext.get_current();
    appweburl = _spPageContextInfo.webAbsoluteUrl;
    loadPeoplePicker();
});

//function to load the people picker
function loadPeoplePicker(peoplePickerElementId) {
    window.EnsurePeoplePickerRefinementInit = function (peoplePickerElementId) {
        try {
            //JSON dictionary to use as a schema that stores picker-specific properties
            var schema = new Array();
            schema["PrincipalAccountType"] = "User";
            schema["AllowMultipleValues"] = false;
            schema["Width"] = 100;
            schema["OnUserResolvedClientScript"] = function () {
                var pickerObj = SPClientPeoplePicker.SPClientPeoplePickerDict.peoplePicker_TopSpan;
                var users = pickerObj.GetAllUserInfo();
                person = users[0];
                var userInfo = '';

                // Get user information
                for (var userProperty in person) {
                    userInfo += userProperty + ':  ' + person[userProperty] + '<br>';
                }

                jQuery('#resolvedUser').html(userInfo);
                if (person != undefined || person != null && peron.Key != null) {
                    jQuery('#userKey').html(person.Key);
                }

                jQuery("#postButton").click(function (event) {
                    postText = document.getElementById("message").value;
                    postToFeed();
                });
            };

            SP.SOD.executeFunc("clienttemplates.js", "SPClientTemplates", function () {
                SP.SOD.executeFunc("clientforms.js", "SPClientPeoplePicker_InitStandaloneControlWrapper", function () {
                    SPClientPeoplePicker_InitStandaloneControlWrapper("peoplePicker", null, schema);
                });
            });
        }

        catch (e) { logError(e.message, false); }
    }

    EnsurePeoplePickerRefinementInit("peoplePicker");
}

// generate the grid populated with the selected profile property values for the selected user
function executeViewScripts() {
    jQuery("#propertytable").html('')
    if (person == undefined || person == null) {
        alert("Please enter a valid user");
        return;
    }
    else {
        accountName = person.Key;
        jQuery.when(getVcardPropertyMappings())
            .then(function (k) {
                profileProperties = k;
            })
            .then(getUserProfilePropertyValues)
            .done(function () {
                // using jsrender to bind the property value collection with the defined template
                jQuery("#propertytable").html(jQuery("#propertytemplate").render(profileProperties));
            }).fail(function (err) {
                logError(err, false);
            });
    }
}