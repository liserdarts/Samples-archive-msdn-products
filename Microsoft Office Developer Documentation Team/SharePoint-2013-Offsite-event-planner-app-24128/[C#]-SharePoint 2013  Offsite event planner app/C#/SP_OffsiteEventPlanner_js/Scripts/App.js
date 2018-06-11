
// Variable that will hold the SharePoint ClientContext object
var context;

// Variable that will hold the SharePoint App Web object
var web;

//Variable that will hold the SharePoint user
var user;

//Variable that will hold the SharePoint user name
var userName;

// Variable that will hold various SharePoint List objects 
var list;

//Variables that will hold the Sharepoint list ID
var offsiteID;
var empID;
var offID;
var actID;
var suggestionID;

// Variable that will hold various SharePoint ListItem objects
var currentItem;
var context = SP.ClientContext.get_current();
var user = context.get_web().get_currentUser();

// This function runs when the DOM is ready and wires up events to two file input elements.
// It also applies jQuery methods to turn various text input elements into date pickers.
// It also creates a context object which is needed to use the SharePoint object model.
$(document).ready(function () {
    $('#cancelNewOffsite').attr("title", "Cancel New Offsite");
    $('#saveNewOffsite').attr("title", "Save New Offsite");
    $('#cancelEditOffsite').attr("title", "Cancel Offsite");
    $('#saveEditOffsite').attr("title", "Save Offsite");
    $('#deleteEditOffsite').attr("title", "Delete Offsite");

    $('#newStartDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM/dd/yy"
    });
    $('#newEndDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM/dd/yy"
    });
    $('#editStartDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM/dd/yy"
    });
    $('#editEndDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM/dd/yy"
    });

    context = SP.ClientContext.get_current();
    web = context.get_web();
    userName = web.get_currentUser();
    userName.retrieve();
    context.load(web, 'EffectiveBasePermissions');

    context.executeQueryAsync(function () {
        if (web.get_effectiveBasePermissions().has(SP.PermissionKind.manageWeb)) {
            hideAllPanels();
            $('#AllOffsites').fadeIn(500, null);
            showOffsites();
            initializePeoplePicker('peoplePickerDiv');
        }
        else {
            $('#EmpOffsite').show();
            user = userName.get_title();
            showEmpOffsites();
        }
        user = userName.get_title();
    },
    function (sender, args) {

        // Failure returned from executeQueryAsync
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Failed to get started. Error: " + args.get_message()));
        errArea.appendChild(divMessage);
    });
});

// This function hides all main DIV elements. The caller is then responsible 
// for re-showing the one that needs to be displayed.
function hideAllPanels() {
    $('#AllOffsites').hide();
    $('#AddOffsite').hide();
    $('#OffsiteDetails').hide();
}

// This function retrieves all offsites
function showOffsites() {
    var errArea = document.getElementById("errAllOffsites");

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    var hasOffsites = false;
    hideAllPanels();
    var offsiteList = document.getElementById("AllOffsites");
    list = web.get_lists().getByTitle('Offsite');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var offsiteTable = document.getElementById("OffsiteList");

            // Remove all nodes from the offsite <DIV> so we have a clean space to write to
            while (offsiteTable.hasChildNodes()) {
                offsiteTable.removeChild(offsiteTable.lastChild);
            }

            // Iterate through the offsite list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the offsite name
                var offsite = document.createElement("div");
                var offsiteLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                offsite.appendChild(offsiteLabel);

                // Add an ID to the offsite DIV
                offsite.id = listItem.get_id();

                // Add an class to the offsite DIV
                offsite.className = "offsiteItem";

                // Add an onclick event to show the offsite details
                $(offsite).click(function (sender) {
                    showOffsiteDetails(sender.target.id);
                });

                // Add the offsite div to the UI
                offsiteTable.appendChild(offsite);
                hasOffsites = true;
            }
            if (!hasOffsites) {
                var noOffsites = document.createElement("div");
                noOffsites.appendChild(document.createTextNode("There are no offsites."));
                offsiteTable.appendChild(noOffsites);
            }
            $('#AllOffsites').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get offsites. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#OffsiteList').fadeIn(500, null);
        });
}

// This function shows the form for adding a new offsite
function addNewOffsite() {
    $('#AddOffsite').hide();
    $('#OffsiteDetails').hide();
    $('#AddOffsite').fadeIn(500, null);

}

// Render and initialize the client-side People Picker.
function initializePeoplePicker(peoplePickerElementId) {

    // Create a schema to store picker properties, and set the properties.
    var schema = {};
    schema['PrincipalAccountType'] = 'User,DL,SecGroup,SPGroup';
    schema['SearchPrincipalSource'] = 15;
    schema['ResolvePrincipalSource'] = 15;
    schema['AllowMultipleValues'] = true;
    schema['MaximumEntitySuggestions'] = 50;
    schema['Width'] = '300px';

    // Render and initialize the picker. 
    // Pass the ID of the DOM element that contains the picker, an array of initial
    // PickerEntity objects to set the picker value, and a schema that defines
    // picker properties.
    this.SPClientPeoplePicker_InitStandaloneControlWrapper(peoplePickerElementId, null, schema);
}

// Query the picker for user information.
function getUserInfo(offsiteEventID) {

    // Get the people picker object from the page.
    var peoplePicker = this.SPClientPeoplePicker.SPClientPeoplePickerDict.peoplePickerDiv_TopSpan;
    // Get information about all users.
    empList = web.get_lists().getByTitle('Employee');
    users = peoplePicker.GetAllUserInfo();
    for (var i = 0; i < users.length; i++) {
        user = users[i];
        employeeName = user["DisplayText"];
        addEmployee(employeeName, offsiteEventID);
        showOffsites();
    }

}

// Query the picker for user information.
function getEditUserInfo(offsiteEventID) {

    // Get the people picker object from the page.
    var peoplePicker = this.SPClientPeoplePicker.SPClientPeoplePickerDict.editPeoplePicker_TopSpan;
    // Get information about all users.
    empList = web.get_lists().getByTitle('Employee');
    users = peoplePicker.GetAllUserInfo();
    for (var i = 0; i < users.length; i++) {
        user = users[i];
        employeeName = user["DisplayText"];
        addEditEmployee(employeeName, offsiteEventID);
        
    }

}

// This function adds employee to the Employee list
function addEditEmployee(employeeName, offsiteEventID) {
    empList = web.get_lists().getByTitle("Employee");
    //Create a CAML query that retrieves employee for offsite in question
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='OffsiteLookup' LookupId='TRUE' /><Value Type='Lookup'>"
            + offsiteEventID
            + "</Value></Eq></Where></Query></View>");
    var listItems = empList.getItems(camlQuery);
    var hasEmployee = false;
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var empl = listItem.get_fieldValues()["Title"];
                if (empl == employeeName) {
                    hasEmployee = true;
                }
            }
            if (!hasEmployee) {
                var itemCreateInfo = new SP.ListItemCreationInformation();
                var listItem = empList.addItem(itemCreateInfo);
                listItem.set_item("Title", employeeName);
                listItem.set_item("OffsiteLookup", offsiteEventID);
                listItem.update();
                context.load(listItem);
                context.executeQueryAsync(function () {
                    $('#AddOffsite').hide();

                },
                function (sender, args) { alert("Error in Saving Attendees: " + args.get_message()); });

            }
            showOffsites();
            showOffsiteDetails(offsiteEventID);
        },
        function (sender, args) {
            alert("Error: "+args.get_message());
        });
}

// This function adds employee to the Employee list
function addEmployee(employeeName, offsiteEventID) {
    empList = web.get_lists().getByTitle("Employee");
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = empList.getItems(camlQuery);
    var hasEmployee = false;
    context.load(listItems);
    context.executeQueryAsync(
       function () {
           var itemCreateInfo = new SP.ListItemCreationInformation();
           var listItem = empList.addItem(itemCreateInfo);
           listItem.set_item("Title", employeeName);
           listItem.set_item("OffsiteLookup", offsiteEventID);
           listItem.update();
           context.load(listItem);
           context.executeQueryAsync(function () {
               //Success returned from executeQueryAsync
               $('#AddOffsite').hide();
               
           },
           function (sender, args) {
               //Failure returned from executeQueryAsync
               alert("Error in Saving Attendees: " + args.get_message());
           });

       }, function () {
           //Failure returned from executeQueryAsync
           alert("Error: " + args.get_message());
       });
}

// This function cancels the offsite
function cancelNewOffsite() {
    clearNewOffsite();
}

// This function clears the inputs on the new offsite form
function clearNewOffsite() {
    var errArea = document.getElementById("errAllOffsites");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddOffsite').fadeOut(500, function () {
        $('#AddOffsite').hide();
        $('#newOffsite').val("");
        $('#newStartDate').val("");
        $('#newEndDate').val("");
        $('#newOffsiteBudget').val("");
        initializePeoplePicker('peoplePickerDiv');
    });
}

// This function saves the new offsite
function saveNewOffsite() {
    if ($('#newOffsite').val() == "") {
        var errArea = document.getElementById("errAllOffsites");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Offsite Name' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var offsiteList = web.get_lists().getByTitle('Offsite');
        var offsiteListItem = offsiteList.addItem(itemCreateInfo);
        offsiteListItem.set_item("Title", $('#newOffsite').val());
        offsiteListItem.set_item("EventDate", $('#newStartDate').val());
        offsiteListItem.set_item("EndDate", $('#newEndDate').val());
        offsiteListItem.set_item("Budget", $('#newOffsiteBudget').val());

        offsiteListItem.update();
        context.load(offsiteListItem);
        context.executeQueryAsync(function () {
            //Success returned from executeQueryAsync
            var offsiteEventID = offsiteListItem.get_id();
            getUserInfo(offsiteEventID);
            clearNewOffsite();

        },
            function (sender, args) {
                //Failure returned from executeQueryAsync
                var errArea = document.getElementById("errAllOffsites");
                // Remove all nodes from the error <DIV> so we have a clean space to write to
                while (errArea.hasChildNodes()) {
                    errArea.removeChild(errArea.lastChild);
                }
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode(args.get_message()));
                errArea.appendChild(divMessage);
            });
    }
}

// This function shows the details for a specific offsite
function showOffsiteDetails(itemID) {
   
    var errArea = document.getElementById("errAllOffsites");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
 
    $('#AddOffsite').hide();
    $('#OffsiteDetails').hide();
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            $('#editOffsite').val(currentItem.get_fieldValues()["Title"]);
            $('#editBudget').val(currentItem.get_fieldValues()["Budget"]);
            $('#editStartDate').val(new Date(currentItem.get_fieldValues()["EventDate"]).format("MM/dd/yyyy"));
            $('#editEndDate').val(new Date(currentItem.get_fieldValues()["EndDate"]).format("MM/dd/yyyy"));
            var ID = currentItem.get_id();
            $('#OffsiteDetails').fadeIn(500, null);
            offsiteID = itemID;
            getAttendees(ID);
            getAllSuggestions(itemID);

        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            var errArea = document.getElementById("errAllOffsites");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

//This function populates Attendees from Employee list
function getAttendees(itemID) {
    
    var employeeList = web.get_lists().getByTitle('Employee');
    var employeeNames = "";
    // Create a CAML query that retrieves the offsite
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='OffsiteLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = employeeList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var empList = document.getElementById("showAttendees");
            while (empList.hasChildNodes()) {
                empList.removeChild(empList.lastChild);
            }
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                employeeNames = listItem.get_fieldValues()["Title"];
                var empDelete = document.createElement("span");
                empDelete.appendChild(document.createTextNode("X"));
                empDelete.className = "deleteButton";
                empDelete.id = listItem.get_id();
                $(empDelete).click(function (sender) {
                    deleteEmployee(sender.target.id, itemID);
                });
                empList.appendChild(empDelete);
                var empLink = document.createElement("a");
                empLink.appendChild(document.createTextNode(employeeNames));
                empList.appendChild(empLink);
                
            }
            $('#editAttendees').val(employeeNames);
            
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            alert("Error: " + args.get_message());
        }
        );


}

//This function shows the new Attendee Dialog Box
function addNewAttendees() {
    $('#editAttendees').dialog({
        height: "auto",
        width: 530,
        modal: true,
        show: {
            effect: "Scale",
            duration: 1000
        },
        hide: {
            effect: "Explode",
            duration: 1000
        }
    });
    initializePeoplePicker('editPeoplePicker');
}

// This function Saves the Attendee in edit offsite form
function saveEditAttendee() {
    getEditUserInfo(offsiteID);
    cancelAttendee();
}

//This function closes the new Attendee Dialog box
function cancelAttendee() {
    $('#editAttendees').dialog("close");
    $('#editPeoplePicker').val("");
    var empList = document.getElementById("showAttendees");
    while (empList.hasChildNodes()) {
        empList.removeChild(empList.lastChild);
    }
    showOffsiteDetails(offsiteID);
}

// This function retrieves all suggestions for particular offsite
function getAllSuggestions(itemID) {
    var errArea = document.getElementById("errorAllSuggestions");
    var hasActivities = false;
    var activityList = web.get_lists().getByTitle('Activity');

    // Create a CAML query that retrieves the activities for the offsite
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='OffsiteLookup' LookupId='TRUE' /><Value Type='Lookup'>"
    + itemID
    + "</Value></Eq></Where><OrderBy><FieldRef Name='VoteCount' Ascending='FALSE'></FieldRef></OrderBy></Query></View>");
    var listItems = activityList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync.
            var activityTable = document.getElementById("allSuggestions");

            // Remove all nodes from the Activity <DIV> so we have a clean space to write to
            while (activityTable.hasChildNodes()) {
                activityTable.removeChild(activityTable.lastChild);
            }

            // Iterate through the Activity list.
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the Activity
                var activity = document.createElement("div");
                var activityLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                var activityBudget = listItem.get_fieldValues()["ActivityBudget"];
                var activityVotes = listItem.get_fieldValues()["VoteCount"];
                var activityStatus = listItem.get_fieldValues()["_Status"];

                if (activityStatus == "Not Approved") {
                    activity.appendChild(activityLabel);
                    // Add an ID to the Activity DIV
                    activity.id = listItem.get_id();
                    // Add an class to the Activity DIV
                    activity.className = "itemTable";
                    // Add an onclick event to show the Activity details
                    $(activity).click(function (sender) {
                        showSuggestionDetails(sender.target.id);

                    });
                    // Add the Activity div to the UI

                    activityTable.appendChild(activity);
                    $("#allSuggestions").append("<div class='clearWidth'>&nbsp;</div> <div class='itemTable'>" + activityBudget + "</div> <div class='clearWidth'>&nbsp;</div> <div class='itemTable'>  " + activityVotes + "</div><div class='clear'>&nbsp;</div>")
                    hasActivities = true;
                }
            }

            if (!hasActivities) {
                var noRevs = document.createElement("div");
                noRevs.appendChild(document.createTextNode("There are no suggestions."));
                activityTable.appendChild(noRevs);
            }
            $('#allSuggestions').fadeIn(500, null);
            getApprovedSuggestions(itemID);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get activities. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#allSuggestions').fadeIn(500, null);
        });
}

// This function shows the details for a specific suggestion
function showSuggestionDetails(activityID) {
    var actCount = 0;
    var actArray = new Array();
    var errArea = document.getElementById("errorAllSuggestions");
    suggestionID = activityID;
    
    var suggestList = web.get_lists().getByTitle('Activity');
    currentItem = suggestList.getItemById(activityID);
    context.load(currentItem);
    context.executeQueryAsync(function () {
        //Success returned from executeQueryAsync
        $('#allActivity').val(currentItem.get_fieldValues()["Title"]);
        $('#allBudget').val(currentItem.get_fieldValues()["ActivityBudget"]);
        $('#allLocation').val(currentItem.get_fieldValues()["Location"]);
        $('#allDescription').val(currentItem.get_fieldValues()["ActivityDescription"]);
        if (currentItem.get_fieldValues()["WebPage"] == null) {
            $('#allWebSite').val("");
        }
        else {
            $('#allWebSite').val(currentItem.get_fieldValues()["WebPage"].get_url());
        }

        var actVotes = currentItem.get_fieldValues()["Votes"];
        if (actVotes == null) {
            actCount = 0;
        }
        else {
            actArray = actVotes.split(";");
            for (var i = 0; i < actVotes.length; i++) {
                if (actVotes[i] == ";") {
                    actCount = actCount + 1;
                }
            }
        }
        $('#allVotes').val(actCount);
        $('#allSuggestionDetails').dialog(
         {
             height: "auto",
             width: "550px",
             modal: true,
             show: {
                 effect: "Scale",
                 duration: 1000
             },
             hide: {
                 effect: "Explode",
                 duration: 1000
             }

         });
        
    }, function (sender, args) {
        //Failure returned from executeQueryAsync
        alert("Failed to get the item. Error: " + args.get_message());
    });
}

// This function retrieves all approved suggestions for particular offset
function getApprovedSuggestions(itemID) {
    var errArea = document.getElementById("errorAppSuggestion");
    offID = itemID;
    var hasSuggestions = false;
    var suggestList = web.get_lists().getByTitle('Activity');
    //Create a CAML query that retrieves the offsite
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='OffsiteLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var getSuggestions = suggestList.getItems(camlQuery);
    context.load(getSuggestions);
    context.executeQueryAsync(function () {
        //Success returned from executeQueryAsync
        var suggestionTable = document.getElementById("approvedSuggestion");

        // Remove all nodes from the Activity <DIV> so we have a clean space to write to
        while (suggestionTable.hasChildNodes()) {
            suggestionTable.removeChild(suggestionTable.lastChild);
        }

        // Iterate through the Activity list
        var listItemEnumerator = getSuggestions.getEnumerator();
        while (listItemEnumerator.moveNext()) {
            var listItem = listItemEnumerator.get_current();

            // Create a DIV to display the Activity name
            var suggestion = document.createElement("div");
            var suggestionLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
            var suggestionBudget = listItem.get_fieldValues()["ActivityBudget"];
            var suggestionVotes = listItem.get_fieldValues()["VoteCount"];
            var suggestionStatus = listItem.get_fieldValues()["_Status"];
            if (suggestionStatus == "Approved") {
                suggestion.appendChild(suggestionLabel);

                // Add an ID to the Activity DIV
                suggestion.id = listItem.get_id();

                // Add an class to the Activity DIV
                suggestion.className = "itemTable";

                // Add an onclick event to show the Activity details
                $(suggestion).click(function (sender) {
                    showAppSuggestionDetails(sender.target.id);
                });

                // Add the Activity div to the UI
                suggestionTable.appendChild(suggestion);
                $("#approvedSuggestion").append("<div class='clearWidth'>&nbsp;</div> <div class='itemTable'>" + suggestionBudget + "</div> <div class='clearWidth'>&nbsp;</div> <div class='itemTable'>  " + suggestionVotes + "</div><div class='clear'>&nbsp;</div>");
                hasSuggestions = true;
            }
        }
        if (!hasSuggestions) {
            var noSuggestions = document.createElement("div");
            noSuggestions.appendChild(document.createTextNode("There are no approved Suggestions."));
            suggestionTable.appendChild(noSuggestions);
        }
        $('#approvedSuggestion').fadeIn(500, null);
    },
    function (sender, args) {
        //Failure returned from executeQueryAsync
        var errArea = document.getElementById("errorAllSuggestions");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Failed to get Suggestions. Error: " + args.get_message()));
        errArea.appendChild(divMessage);
    });

}

// This function shows the details for a specific approved suggestion
function showAppSuggestionDetails(activityID) {
    var actCount = 0;
    var actArray = new Array();
    var errArea = document.getElementById("errorAppSuggestion");
    suggestionID = activityID;
    var suggestList = web.get_lists().getByTitle('Activity');
    currentItem = suggestList.getItemById(activityID);
    context.load(currentItem);
    context.executeQueryAsync(function () {
        //Success returned from executeQueryAsync
        $('#appActivity').val(currentItem.get_fieldValues()["Title"]);
        $('#appBudget').val(currentItem.get_fieldValues()["ActivityBudget"]);
        $('#appLocation').val(currentItem.get_fieldValues()["Location"]);
        $('#appDescription').val(currentItem.get_fieldValues()["ActivityDescription"]);
        if (currentItem.get_fieldValues()["WebPage"] == null) {
            $('#appWebSite').val("");
        }
        else {
            $('#appWebSite').val(currentItem.get_fieldValues()["WebPage"].get_url());
        }
        var actVotes = currentItem.get_fieldValues()["Votes"];
        if (actVotes == null) {
            actCount = 0;
        }
        else {
            actArray = actVotes.split(";");
            for (var i = 0; i < actVotes.length; i++) {
                if (actVotes[i] == ";") {
                    actCount = actCount + 1;
                }
            }
        }
        $('#appVotes').val(actCount);
        $('#appSuggestionDetails').dialog(
         {
             height: "auto",
             width: "550px",
             modal: true,
             show: {
                 effect: "Scale",
                 duration: 1000
             },
             hide: {
                 effect: "Explode",
                 duration: 1000
             }

         });
    }, function (sender, args) {
        //Failure returned from executeQueryAsync
        alert("Failed to get the item. Error: " + args.get_message());
    }
        );
}

// This function approves the suggested activity
function approveActivity() {
    currentItem.set_item("_Status", "Approved");
    currentItem.update();
    context.load(currentItem);
    context.executeQueryAsync(function () {
        //Success returned from executeQueryAsync
        getAllSuggestions(offID);
        closeApproveDialog();      
    }, function (sender, args) {
        //Failure returned from executeQueryAsync
        alert("Can't Approve this items. Error:" + args.get_message());
    });
}

// This function cancels the editing of an existing offsite
function cancelEditOffsite() {
    clearEditOffsite();
}

// This function clears the inputs on the edit offsite form
function clearEditOffsite() {
    var errArea = document.getElementById("errAllOffsites");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in future operations
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#OffsiteDetails').fadeOut(500, function () {
        $('#OffsiteDetails').hide();
        $('#editOffsite').val("");
        $('#editBudget').val("");
        $('#editStartDate').val("");
        $('#editEndDate').val("");
    });
}

// This function updates an existing offsite's details
function saveEditOffsite() {
    if ($('#editOffsite').val() == "") {
        var errArea = document.getElementById("errAllOffsites");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Offsite Name' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        currentItem.set_item("Title", $('#editOffsite').val());
        currentItem.set_item("Budget", $('#editBudget').val());
        currentItem.set_item("EventDate", $('#editStartDate').val());
        currentItem.set_item("EndDate", $('#editEndDate').val());
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            //Success returned from executeQueryAsync
            clearEditOffsite();
            showOffsites();
        },
            function (sender, args) {
                //Failure returned from executeQueryAsync
                var errArea = document.getElementById("errAllOffsites");
                // Remove all nodes from the error <DIV> so we have a clean space to write to
                while (errArea.hasChildNodes()) {
                    errArea.removeChild(errArea.lastChild);
                }
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode(args.get_message()));
                errArea.appendChild(divMessage);
            });
    }
}

// This function deletes the selected offsite
function deleteEditOffsite() {
    var errArea = document.getElementById("errAllOffsites");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var OffsiteList = web.get_lists().getByTitle('Employee');
    //Create a CAML query that retrieves the offsite
    var offsiteQuery = new SP.CamlQuery();
    offsiteQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='OffsiteLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var OffsiteListItems = OffsiteList.getItems(offsiteQuery);
    context.load(OffsiteListItems);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var listItemEnumerator = OffsiteListItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var empid=listItem.get_id();
                deleteEmployee(empid);
            }
            deleteActivities(currentItem.get_id());
            currentItem.deleteObject();
            clearEditOffsite();
            showOffsites();
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to delete offsite. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        }
        );
}

// This function deletes the employee respective to offsite
function deleteEmployee(empid, itemID) {
    list=web.get_lists().getByTitle("Employee");
    var empItem = list.getItemById(empid);
    empItem.deleteObject();
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var empList = document.getElementById("showAttendees");
            while (empList.hasChildNodes()) {
                empList.removeChild(empList.lastChild);
            }
            showOffsites();
            showOffsiteDetails(itemID);
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            alert("Error in Deleting Employee:"+ args.get_message());
        });
}

// This function deletes activities respective to the offsite
function deleteActivities(offID) {
    var activityList = web.get_lists().getByTitle('Activity');
    // Create a CAML query that retrieves the activities for the offsite in question
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='OffsiteLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + offID
        + "</Value></Eq></Where></Query></View>");
    var listItems = activityList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync

            // Iterate through the activity list.
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                deleteActivity(listItem.get_id());
            }  
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            alert("Error: "+args.get_message());
        });
}

// This function deletes an activity
function deleteActivity(actID) {
    var activityList = web.get_lists().getByTitle('Activity');
    var actItem = activityList.getItemById(actID);
    actItem.deleteObject();
    context.executeQueryAsync(
        function () {
        },
        function (sender, args) {
            alert("Error in Deleting Employee:" + args.get_message());
        });
}

// This function retrieves all offsites of an employee
function showEmpOffsites() {
    var hasOffsites = false;
    var enrollClear = document.getElementById("EmpOffsiteList");
    list = web.get_lists().getByTitle("Employee");
    //Create a CAML query that retrieves the employee
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"
        + user
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                
                // Remove all nodes from the error <DIV> so we have a clean space to write to
                while (enrollClear.hasChildNodes()) {
                    enrollClear.removeChild(enrollClear.lastChild);
                }
                var listItem = listItemEnumerator.get_current();
                empID = listItem.get_id();
                
                var empEventID = listItem.get_fieldValues()["OffsiteLookup"].get_lookupValue();
                getEventName(empEventID);
                hasOffsites = true;
            }
            if (!hasOffsites) {
                var noOffsites = document.createElement("div");
                noOffsites.appendChild(document.createTextNode("There are no Offsites for you."));
                enrollClear.appendChild(noOffsites);
            }
        });
}

// This function shows the details for a specific offsite
function showEmpOffDetails(itemID) {
    var errArea = document.getElementById("errEmpOffsite");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    list = web.get_lists().getByTitle("Offsite");
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            $('#empOffsiteName').val(currentItem.get_fieldValues()["Title"]);
            $('#empOffsiteBudget').val(currentItem.get_fieldValues()["Budget"]);
            $('#empStartDate').val(new Date(currentItem.get_fieldValues()["EventDate"]).format("MM/dd/yyyy"));
            $('#empEndDate').val(new Date(currentItem.get_fieldValues()["EndDate"]).format("MM/dd/yyyy"));
            var ID = currentItem.get_id();
            $('#empOffsiteDetails').fadeIn(500, null);
            showActivities(itemID);

        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            var errArea = document.getElementById("errEmpOffsite");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function gives offsites for an employee
function getEventName(empEventID) {
    var context1 = SP.ClientContext.get_current();
    var errArea = document.getElementById("errEmpOffsite");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    offsiteID = empEventID;
    list = web.get_lists().getByTitle("Offsite");
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Text'>"
        + empEventID
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context1.load(listItems);
    context1.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();

            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                // Success returned from executeQueryAsync
                var offsiteTable = document.getElementById("EmpOffsiteList");

                // Remove all nodes from the offsite <DIV> so we have a clean space to write to

                var offsite = document.createElement("div");
                var offsiteLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);

                offsite.appendChild(offsiteLabel);

                // Add an ID to the offsite DIV
                offsite.id = listItem.get_id();

                // Add an class to the offsite DIV
                offsite.className = "offsiteItem";

                // Add an onclick event to show the offsite details
                $(offsite).click(function (sender) {
                    showEmpOffDetails(sender.target.id);
                });

                // Add the offsite div to the UI
                offsiteTable.appendChild(offsite);

                $('#EmpOffsiteList').fadeIn(500, null);
            }

        });
}

// This function shows the dialog for adding a new activity
function addNewActivity() {
    $('#NewActivityDetails').dialog(
         {
             height: "auto",
             width: "auto",
             modal: true,
             show: {
                 effect: "Scale",
                 duration: 1000
             },
             hide: {
                 effect: "Explode",
                 duration: 1000
             }
         });
}

// This function cancels the activity
function cancelNewActivity() {
    $('#NewActivityDetails').dialog("close");
    clearNewActivityForm();
}

// This function cancels the editing of an existing activity
function cancelEditActivity() {
    $('#EditActivityDetails').dialog("close");
    clearEditActivityForm();
}

// This function clears the inputs on the edit activity form
function clearEditActivityForm() {
    var errArea = document.getElementById("errEmpOffsite");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#EditActivityDetails').fadeOut(500, function () {
        $('#EditActivityDetails').hide();
        $('#editActName').val("");
        $('#editDescription').val("");
        $('#editLocation').val("");
        $('#editWebSite').val("");
        $('#editActBudget').val("");
    });
}

// This function clears the inputs on the new activity form
function clearNewActivityForm() {
    var errArea = document.getElementById("errEmpOffsite");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewActivityDetails').fadeOut(500, function () {
        $('#NewActivityDetails').hide();
        $('#activityName').val("");
        $('#description').val("");
        $('#location').val("");
        $('#website').val("");
        $('#activityBudget').val("");     
    });

}

// This function closes the approved activity dialog
function closeAppSuggestDialog() {
    $('#appSuggestionDetails').dialog("close");
}

// This function closes the activity dialog
function closeApproveDialog() {
    $('#allSuggestionDetails').dialog("close");
}

// This function saves the newly added activity
function saveNewActivity() {
    if ($('#activityName').val() == "") {
        var errArea = document.getElementById("errEmpOffsite");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Activity Name' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        var website = $('#website').val();
        if (website != "") {
            if (website.indexOf("http://") == -1 && website.indexOf(":") == -1 && website.indexOf("/") == -1) {
                website = "http://" + website;
            }
        }
        var budget=$('#activityBudget').val();
        if(budget==""){
            budget=0;
        }
        offsiteID = currentItem.get_id();
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var activityList = web.get_lists().getByTitle('Activity');
        var activityItem = activityList.addItem(itemCreateInfo);
        activityItem.set_item("Title", $('#activityName').val());
        activityItem.set_item("ActivityBudget", budget);
        activityItem.set_item("Location", $('#location').val());       
        activityItem.set_item("WebPage", website);
        activityItem.set_item("ActivityDescription", $('#description').val());
        activityItem.set_item("VoteCount", 0);
        activityItem.set_item("_Status", "Not Approved");
        activityItem.set_item("OffsiteLookup", offsiteID);
        activityItem.update();
        context.load(activityItem);
        context.executeQueryAsync(function () {
            // Success returned from executeQueryAsync
            cancelNewActivity();
            showActivities(offsiteID);
        },
            function (sender, args) {
                // Failure returned from executeQueryAsync
                alert("Error: "+args.get_message());
                var errArea = document.getElementById("errAllOffsites");
                // Remove all nodes from the error <DIV> so we have a clean space to write to
                while (errArea.hasChildNodes()) {
                    errArea.removeChild(errArea.lastChild);
                }
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode(args.get_message()));
                errArea.appendChild(divMessage);
            });
    }
}

// This function retrieves all activities of an offsite
function showActivities(offsiteID) {
    var errArea = document.getElementById("errEmpOffsite");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    hideAllPanels();

    var hasActivities = false;
    var activityList = web.get_lists().getByTitle('Activity');

    // Create a CAML query that retrieves the activities for the offsite in question
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='OffsiteLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + offsiteID
        + "</Value></Eq></Where></Query></View>");
    var listItems = activityList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync.
            var activityTable = document.getElementById("activityList");

            // Remove all nodes from the activity <DIV> so we have a clean space to write to
            while (activityTable.hasChildNodes()) {
                activityTable.removeChild(activityTable.lastChild);
            }

            // Iterate through the activity list.
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the activity
                var activity = document.createElement("div");
                var activityLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);

                activity.appendChild(activityLabel);

                // Add an ID to the activity DIV
                activity.id = listItem.get_id();

                // Add an class to the activity DIV
                activity.className = "item";

                // Add an onclick event to show the activity details
                $(activity).click(function (sender) {
                    showActivityDetails(sender.target.id);

                });

                // Add the activity div to the UI
                activityTable.appendChild(activity);
                hasActivities = true;
            }
    
            if (!hasActivities) {
                var noRevs = document.createElement("div");
                noRevs.appendChild(document.createTextNode("There are no activities."));
                activityTable.appendChild(noRevs);
            }
            
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get activities. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#reviewForm').fadeIn(500, null);
        });
}

// This function shows the details for a specific activity
function showActivityDetails(activityID) {
    $('#EditActivityDetails').dialog(
         {
             height: "auto",
             width: "auto",
             modal: true,
             show: {
                 effect: "Scale",
                 duration: 1000
             },
             hide: {
                 effect: "Explode",
                 duration: 1000
             }

         });

    var errArea = document.getElementById("errEmpOffsite");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    list = web.get_lists().getByTitle("Activity");
    currentItem = list.getItemById(activityID);
    actID = activityID;
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            $('#editActName').val(currentItem.get_fieldValues()["Title"]);
            $('#editDescription').val(currentItem.get_fieldValues()["ActivityDescription"]);
            $('#editLocation').val(currentItem.get_fieldValues()["Location"]);
            if (currentItem.get_fieldValues()["WebPage"] == null) {
                $('#editWebSite').val("");
            }
            else {
                $('#editWebSite').val(currentItem.get_fieldValues()["WebPage"].get_url());
            }

            $('#editActBudget').val(currentItem.get_fieldValues()["ActivityBudget"]);
            $('#EditActivityDetails').fadeIn(500, null);
            
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var errArea = document.getElementById("errEmpOffsite");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function updates an existing activity's details
function saveEditActivity() {
    if ($('#editActName').val() == "") {
        var errArea = document.getElementById("errEmpOffsite");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Activity Name' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        var website = $('#editWebSite').val();
        if (website != "") {
            if (website.indexOf("http://") == -1 && website.indexOf(":") == -1 && website.indexOf("/") == -1) {
                website = "http://" + website;
            }
        }
        var activityid = currentItem.get_id();
        currentItem.set_item("Title", $('#editActName').val());
        currentItem.set_item("ActivityDescription", $('#editDescription').val());
        currentItem.set_item("Location", $('#editLocation').val());
        currentItem.set_item("WebPage", website);
        currentItem.set_item("ActivityBudget", $('#editActBudget').val());
        currentItem.set_item("OffsiteLookup", offsiteID);
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            // Success returned from executeQueryAsync
            cancelEditActivity();
            showActivities(offsiteID);
        },
            function (sender, args) {
                // Failure returned from executeQueryAsync
                alert("Error: "+args.get_message());
                var errArea = document.getElementById("errAllOffsites");
                // Remove all nodes from the error <DIV> so we have a clean space to write to
                while (errArea.hasChildNodes()) {
                    errArea.removeChild(errArea.lastChild);
                }
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode(args.get_message()));
                errArea.appendChild(divMessage);
            });
    }
}

// This function increments the vote count
function addVotes() {
    var actCount=0;
    var actArray = [];
    var hasVoted=false;
    list = web.get_lists().getByTitle("Activity");
    currentItem = list.getItemById(actID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var actVotes = currentItem.get_fieldValues()["Votes"];

            if (actVotes == null) {
                currentItem.set_item("Votes", empID + ";");
                currentItem.set_item("VoteCount", actCount+1);
            }
            else {   
                actArray = actVotes.split(";");
                for (var i = 0; i < actArray.length; i++) {
                    if (actArray[i] == empID) {
                        hasVoted = true;
                    }
                }
                actCount = actArray.length;
                

                if (!hasVoted) {
                    currentItem.set_item("Votes", actVotes + empID + ";");
                    currentItem.set_item("VoteCount", actCount);
                }
                else {
                    alert("You have already voted");
                }   
            }         
        }
        ,
        function (sender, args) {
            //Failure returned from executeQueryAsync
            alert("Error: "+args.get_message());
        }
    );
}
