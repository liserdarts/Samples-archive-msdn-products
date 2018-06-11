// Variable that will hold the SharePoint ClientContext object
var context;

// Variable that will hold the SharePoint App Web object
var web;

//Variable that will hold the SharePoint user
var user;

//Variable that will hold the SharePoint user name
var userName;
var employeeName;

// Variable that will hold various SharePoint List objects 
var list;
var eventList;
var assetList;
var attendeeCount;

// Variable that will hold various SharePoint ListItem objects
var currentItem;
var listItem;
var assetItem;
var empStatus;

//Variables that will hold the Sharepoint list ID
var assetsID;
var eventID;

// Variable that will hold the contents of a file selected by the user for uploading
var contents;

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () { 
    $('#newStartDate').datetimepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy",
        timeFormat: "hh:mm"
    });
    $('#newEndDate').datetimepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy",
        timeFormat: "hh:mm"
    });
    
    $('#editStartDate').datetimepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy",
        timeFormat: "hh:mm"
    });
    $('#editEndDate').datetimepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy",
        timeFormat: "hh:mm"
    });

    context = SP.ClientContext.get_current();
    web = context.get_web();
    userName = web.get_currentUser();
    userName.retrieve();
    context.load(web, 'EffectiveBasePermissions');

    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        if (web.get_effectiveBasePermissions().has(SP.PermissionKind.manageWeb)) {
            hideAllPanels();
            $('#Home').fadeIn(500, null);
            initializePeoplePicker('peoplePickerDiv');
        }
        else {
            $('#EmployeeHome').show();
            user = userName.get_title();
        }      
        user = userName.get_title();       
    },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Failed to get started. Error: " + args.get_message()));
        errArea.appendChild(divMessage);
        $('#Home').fadeIn(500, null);
    });
    });

// This function hides all main DIV elements. The caller is then responsible 
// for re-showing the one that needs to be displayed.
function hideAllPanels() {
    $('#AllEvents').hide();
    $('#AllAssets').hide();
    $('#AddEventDetails').hide();
    $('#editEventDetails').hide();
    $('#EmployeeHome').hide();
    $('#AllEnrllEvents').hide();
    $('#eventDetails').hide();
    $('#OtherEvents').hide();
}

// This function shows add asset dialog
function addAssets() {
    $('#AddAsset').dialog({
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

// This function clears the inputs on the add asset form and cancels it
function cancelNewAssets() {
    $('#addRoom').val("");
    $('#addProjectors').val("");
    $('#addStudentPC').val("");
    $('#addInstructorPC').val("");
    $('#AddAsset').dialog("close");
}

// This function saves the new asset
function saveNewAssets() {
    assetList = web.get_lists().getByTitle('Resources');
    var itemCreateInfo = new SP.ListItemCreationInformation();
    assetItem = assetList.addItem(itemCreateInfo);
    assetItem.set_item("Title", $('#addRoom').val());
    assetItem.set_item("Projectors", $('#addProjectors').val());
    assetItem.set_item("StudentPC", $('#addStudentPC').val());
    assetItem.set_item("InstructorPC", $('#addInstructorPC').val());
    assetItem.update();
    context.load(assetItem);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        cancelNewAssets();
        showTrainingAssets();
    },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Failure " + args.get_message());
    });
}

// This function shows the details for a specific asset 
function showEditAssets(assetID) {
    $('#EditAsset').dialog({
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
    assetList = web.get_lists().getByTitle('Resources');
    currentItem = assetList.getItemById(assetID);
    context.load(currentItem);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        $('#editRoom').val(currentItem.get_fieldValues()["Title"]);
        $('#editProjectors').val(currentItem.get_fieldValues()["Projectors"]);
        $('#editStudentPC').val(currentItem.get_fieldValues()["StudentPC"]);
        $('#editInstructorPC').val(currentItem.get_fieldValues()["InstructorPC"]);
    },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Error in getting assets: " + args.get_message());
    });
   
}

// This function cancels the editing of an existing asset
function cancelEditAssets() {
    $('#editRoom').val("");
    $('#editProjectors').val("");
    $('#editStudentPC').val("");
    $('#editInstructorPC').val("");
    $('#EditAsset').dialog("close");
}

// This function updates an existing asset's details
function saveEditAssets() {
    if ($('#editRoom').val() == "") {
        var errArea = document.getElementById("errAllAssets");
        // Remove all nodes from the errAllAssets <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Training Roome' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        currentItem.set_item("Title", $('#editRoom').val());
        currentItem.set_item("InstructorPC", $('#editInstructorPC').val());
        currentItem.set_item("StudentPC", $('#editStudentPC').val());
        currentItem.set_item("Projectors", $('#editProjectors').val());
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            // Success returned from executeQueryAsync
            $('#editEventDetails').fadeOut(500, null);
            showTrainingAssets();
            cancelEditAssets();
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error :"+args.get_message());
        });
    }
}

// This function shows assets
function showTrainingAssets() {
    $('#EventsTile').css("background-color", "#0072C6");
    $('#AssetsTile').css("background-color", "orange");
    var errArea = document.getElementById("errAllAssets");
    // Remove all nodes from the errAllAssets <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasEvents = false;
    hideAllPanels();
    list = web.get_lists().getByTitle('Resources');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var eventTable = document.getElementById("assetList");
            // Remove all nodes from the assetList <DIV> so we have a clean space to write to
            while (eventTable.hasChildNodes()) {
                eventTable.removeChild(eventTable.lastChild);
            }
            // Iterate through the event list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the asset name

                var clear = document.createElement("div");
                clear.className = "clear";
                var title = listItem.get_fieldValues()["Title"];
                var assetDelete = document.createElement("span");
                assetDelete.appendChild(document.createTextNode("X"));
                assetDelete.className = "deleteAssetButton";
                assetDelete.id = listItem.get_id();

                $(assetDelete).click(function (sender) {
                    deleteEventforAsset(sender.target.id);
                });

                eventTable.appendChild(assetDelete);

                var event = document.createElement("div");
                var eventLabel = document.createTextNode(title);
                event.className = "roomElement";
                event.appendChild(eventLabel);
                event.id = listItem.get_id();

                $(event).click(function (sender) {
                    showEditAssets(sender.target.id);
                });

                eventTable.appendChild(event);
                eventTable.appendChild(clear);
                hasEvents = true;
            }
            if (!hasEvents) {
                var noEvents = document.createElement("div");
                noEvents.appendChild(document.createTextNode("There are no Assets. You can add a new Asset from here."));
                eventTable.appendChild(noEvents);
            }
            $('#AllAssets').fadeIn(500, null);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get assets. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#AssetsList').fadeIn(500, null);
        });
}

// This function deletes asset
function deleteAsset(assetID) {
    assetList = web.get_lists().getByTitle("Resources");
    var assetsItem = assetList.getItemById(assetID);
    assetsItem.deleteObject();
}

// This function deletes event when asset assigned to it is deleted 
function deleteEventforAsset(assetId) {
    list = web.get_lists().getByTitle("EventList");
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Resources' LookupId='TRUE' /><Value Type='Lookup'>"
        + assetId
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var listEnumerator = listItems.getEnumerator();
            while (listEnumerator.moveNext()) {
                var listItem1 = listEnumerator.get_current();
                var eventID = listItem1.get_id();
                deleteEmployee(eventID);
                deleteEvent(eventID);
            }
            deleteAsset(assetId);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error in getting events for this resource: "+ args.get_message());
        }
        );
}

// This function deletes the event
function deleteEvent(eventID) {
    list = web.get_lists().getByTitle("EventList");
    var eventItem = list.getItemById(eventID);
    eventItem.deleteObject();
}

// This function deletes the employees respective to event
function deleteEmployee(empid) {
    list = web.get_lists().getByTitle("Employee");
    var empItem = list.getItemById(empid);
    empItem.deleteObject();
}

// This function shows available resources in new event dialog
function checkReservedAssets(reserveAssets) {
    var location = document.getElementById("location");
    while (location.hasChildNodes()) {
        location.removeChild(location.lastChild);
    }
    var reservedAssets = "";
    var innerQ="";
    assetList = web.get_lists().getByTitle('Resources');
    var camlQuery = new SP.CamlQuery();
    for (var i = 0; i < reserveAssets.length; i++) {
        reservedAssets += "<Neq><FieldRef Name='ID'/><Value Type='Counter'>" + reserveAssets[i] + "</Value></Neq>";
        if (i >= 1)
        {
            innerQ = "<And>" + reservedAssets + "</And>";
            reservedAssets = innerQ;
        }                 
    }
    camlQuery.set_viewXml("<View><Query><Where>"
        + reservedAssets
        +"</Where></Query></View>");
        var listItems = assetList.getItems(camlQuery);
        context.load(listItems);
        context.executeQueryAsync(function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator1 = listItems.getEnumerator();
            while (listItemEnumerator1.moveNext()) {
                var listItem1 = listItemEnumerator1.get_current();
                var title = listItem1.get_fieldValues()["Title"];
                var id = listItem1.get_fieldValues()["ID"];
                var option = document.createElement('option');
                option.innerHTML = title;
                option.value = id;
                location.appendChild(option);
            }
        }, function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Failure " + args.get_message());
        });
}

// This function shows available resources in edit event dialog
function checkEditReservedAssets(reserveAssets) {
    var location = document.getElementById("editResources");
    while (location.hasChildNodes()) {
        location.removeChild(location.lastChild);
    }
    var reservedAssets = "";
    var innerQ = "";
    assetList = web.get_lists().getByTitle('Resources');
    var camlQuery = new SP.CamlQuery();
    for (var i = 0; i < reserveAssets.length; i++) {
        reservedAssets += "<Neq><FieldRef Name='Title'/><Value Type='Text'>" + reserveAssets[i] + "</Value></Neq>";
        if (i >= 1) {
            innerQ = "<And>" + reservedAssets + "</And>";
            reservedAssets = innerQ;
        }
    }
    camlQuery.set_viewXml("<View><Query><Where>"
        + reservedAssets
        + "</Where></Query></View>");
    var listItems = assetList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        var listItemEnumerator1 = listItems.getEnumerator();
        while (listItemEnumerator1.moveNext()) {
            $('#editResources').show();
            $('#fillEditOtherData').show();
            var listItem1 = listItemEnumerator1.get_current();
            var title = listItem1.get_fieldValues()["Title"];
            var id = listItem1.get_fieldValues()["ID"];
            var option = document.createElement('option');
            option.innerHTML = title;
            option.value = id;
            location.appendChild(option);
        }
    }, function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Failure " + args.get_message());
    });
}

// This function shows all resources
function checkResources() {
    var location = document.getElementById("location");
    while (location.hasChildNodes()) {
        location.removeChild(location.lastChild);
    }
    assetList = web.get_lists().getByTitle('Resources');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = assetList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
                var listItemEnumerator1 = listItems.getEnumerator();
                while (listItemEnumerator1.moveNext()) {
                    var listItem1 = listItemEnumerator1.get_current();
                    var title = listItem1.get_fieldValues()["Title"];
                    var id = listItem1.get_fieldValues()["ID"];
                    var option = document.createElement('option');
                    option.innerHTML = title;
                    option.value = id;
                    location.appendChild(option);
                }
        },
        function (sender, args) {
            alert("Error in Populating Assets: " + args.get_message());
        }
        );
}

// This function fills resources associated with selected location in new event dialog
function fillOtherData() {
    var assetID = $('#location').val();
    assetList = web.get_lists().getByTitle('Resources');
    currentItem = assetList.getItemById(assetID);
    context.load(currentItem);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        assetItem = assetID;
        $('#projectors').val(currentItem.get_fieldValues()["Projectors"]);
        $('#studentPCs').val(currentItem.get_fieldValues()["StudentPC"]);
        $('#newInstructors').val(currentItem.get_fieldValues()["InstructorPC"]);
    },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Error in getting assets: " + args.get_message());
    });
}

// This function fills resources associated with selected location in edit event dialog
function fillEditOtherData() {
    var assetID = $('#editResources').val();
    $('#saveEditEvent').show();
    assetList = web.get_lists().getByTitle('Resources');
    assetItem = assetList.getItemById(assetID);
    context.load(assetItem);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        $('#editProjector').val(assetItem.get_fieldValues()["Projectors"]);
        $('#editStudentPCs').val(assetItem.get_fieldValues()["StudentPC"]);
        $('#editInstructors').val(assetItem.get_fieldValues()["InstructorPC"]);
    },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Error in getting assets: " + args.get_message());
    });
}

// Render and initialize the client-side People Picker
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

// This function checks for reserved items in the resource list in new event dialog
function assignResource() {  
    if($('#newStartDate').val()!="")
    {
        var addRoom = document.getElementById('location');
        var eventCount = 0;
        var getAssets = [];
        var inputDateStr =$('#newStartDate').val();
        var inputDate = new Date(inputDateStr).format("MM dd, yy hh:mm");
        var camlQuery = SP.CamlQuery.createAllItemsQuery();
        list = web.get_lists().getByTitle('EventList');
        var listItems = list.getItems(camlQuery);
        context.load(listItems);
        context.executeQueryAsync(
            function () {
                // Success returned from executeQueryAsync
                eventCount = listItems.get_count();
                if (eventCount > 0) {
                    var listItemEnumerator = listItems.getEnumerator();
                    while (listItemEnumerator.moveNext()) {
                        var listItem = listItemEnumerator.get_current();
                        var startDate = listItem.get_fieldValues()["EventDate"].format("MM dd, yy hh:mm");
                        var endDate = listItem.get_fieldValues()["EndDate"].format("MM dd, yy hh:mm");
                        if (startDate <= inputDate && inputDate <= endDate) {
                            getAssets.push(listItem.get_fieldValues()["Resources"].get_lookupValue());
                        }
                    }
                    checkReservedAssets(getAssets);
                }
                else {
                    checkResources();
                }
            },
            function (sender, args) {
                // Failure returned from executeQueryAsync
                alert("Failure " + args.get_message());
            });
        $('#assignResource').hide();
        $('#resource').slideDown(500, null);
    }
    else{
        alert("Enter Start Date");
    }
}

// This function checks for reserved items in the resource list in edit event dialog
function assignEditResource() {
    $('#editEvent').removeAttr("disabled");
    $('#editStartDate').removeAttr("disabled");
    $('#editEndDate').removeAttr("disabled");
    $('#editLocation').hide();
    var editProjector = document.getElementById('editProjectors');
    var editRoom = document.getElementById('editLocation');
        var eventCount = 0;
        var getAssets = [];
        var inputDateStr = $('#editStartDate').val();
        var inputDate = new Date(inputDateStr).format("MM dd, yy hh:mm");
        var camlQuery = SP.CamlQuery.createAllItemsQuery();
        list = web.get_lists().getByTitle('EventList');
        var listItems = list.getItems(camlQuery);
        context.load(listItems);
        context.executeQueryAsync(
            function () {
                // Success returned from executeQueryAsync
                eventCount = listItems.get_count();
                if (eventCount > 0) {
                    var listItemEnumerator = listItems.getEnumerator();
                    while (listItemEnumerator.moveNext()) {
                        var listItem = listItemEnumerator.get_current();
                        var startDate = listItem.get_fieldValues()["EventDate"].format("MM dd, yy hh:mm");
                        var endDate = listItem.get_fieldValues()["EndDate"].format("MM dd, yy hh:mm");
                        if (startDate <= inputDate && inputDate <= endDate) {
                            getAssets.push(listItem.get_fieldValues()["Resources"].get_lookupValue());
                        }
                    }
                    checkEditReservedAssets(getAssets);                   
                }
                else {
                    checkResources();
                }
                $('#saveEditEvent').show();
            },
            function (sender, args) {
                // Failure returned from executeQueryAsync
                alert("Failure " + args.get_message());
            });
        $('#assignEditResource').hide();
        $('#editResource').slideDown(500, null);
}

// This function retrieves all events
function showEvents() {
    $('#EventsTile').css("background-color", "orange");
    $('#AssetsTile').css("background-color", "#0072C6");
    var errArea = document.getElementById("errAllEvents");
     
    // Remove all nodes from the errAllEvents <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasEvents = false;
    hideAllPanels();
    var eventList = document.getElementById("AllEvents");
    list = web.get_lists().getByTitle('EventList');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var eventTable = document.getElementById("EventList");

            // Remove all nodes from the EventList <DIV> so we have a clean space to write to
            while (eventTable.hasChildNodes()) {
                eventTable.removeChild(eventTable.lastChild);
            }

            // Iterate through the event list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the event name
                var event = document.createElement("div");
                var eventLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                event.appendChild(eventLabel);

                // Add an ID to the event DIV
                event.id = listItem.get_id();

                // Add an class to the event DIV
                event.className = "item";

                // Add an onclick event to show the event details
                $(event).click(function (sender) {
                    showEventDetails(sender.target.id);
                });

                // Add the event div to the UI
                eventTable.appendChild(event);
                hasEvents = true;
            }
            if (!hasEvents) {
                var noEvents = document.createElement("div");
                noEvents.appendChild(document.createTextNode("There are no events. You can add a new event from here."));
                eventTable.appendChild(noEvents);
            }
            $('#AllEvents').fadeIn(500, null);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get events. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#EventList').fadeIn(500, null);
        });
}

// This function shows the details for a specific event
function showEventDetails(eventId) {
    eventID = eventId;
    $('#editResources').hide();
    $('#saveEditEvent').hide();
    $('#fillEditOtherData').hide();
    $('#editEvent').attr("disabled", "true");
    $('#editStartDate').attr("disabled", "true");
    $('#editEndDate').attr("disabled", "true");
    var errArea = document.getElementById("errAllEvents");
    // Remove all nodes from the errAllEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var editLocation = document.getElementById("editLocation");
    while (editLocation.hasChildNodes()) {
        editLocation.removeChild(editLocation.lastChild);
    }
    list = web.get_lists().getByTitle('EventList');
    currentItem = list.getItemById(eventID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            $('#editEvent').val(currentItem.get_fieldValues()["Title"]);
            $('#editStartDate').val(new Date(currentItem.get_fieldValues()["EventDate"]).format("MMMM dd, yyyy hh:mm"));
            $('#editEndDate').val(new Date(currentItem.get_fieldValues()["EndDate"]).format("MMMM dd, yyyy hh:mm"));
            var assetID= currentItem.get_fieldValues()["Resources"].get_lookupValue();
            assetList = web.get_lists().getByTitle('Resources');
            assetItem = assetList.getItemById(assetID);
            var assetContext= SP.ClientContext.get_current();
            assetContext.load(assetItem);
            context.executeQueryAsync(
                function () {
                    $('#editLocation').val(assetItem.get_fieldValues()["Title"]);
                    $('#editProjector').val(assetItem.get_fieldValues()["Projectors"]);
                    $('#editStudentPCs').val(assetItem.get_fieldValues()["StudentPC"]);
                    $('#editInstructors').val(assetItem.get_fieldValues()["InstructorPC"]);
                }
                );
            $('#editEventDetails').dialog(
         {
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
            getAttendees(eventID);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var errArea = document.getElementById("errAllEvents");
            // Remove all nodes from the errAllEvents <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
    $('#editLocation').show();
}

//This function populates Attendees from Employee list
function getAttendees(itemID) {
    var attendeeList = web.get_lists().getByTitle('Employee');
    attendeeCount = 0;
    var attendeeNames = "";
    // Create a CAML query that retrieves the employee
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='EventLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = attendeeList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var empList = document.getElementById("showAttendees");
            while (empList.hasChildNodes()) {
                empList.removeChild(empList.lastChild);
            }
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var status = listItem.get_fieldValues()["_Status"];
                if (status != "Wait") {
                    attendeeCount = attendeeCount + 1;
                    employeeNames = listItem.get_fieldValues()["Title"];
                    var empDelete = document.createElement("span");
                    empDelete.appendChild(document.createTextNode("X"));
                    empDelete.className = "deleteButton";
                    empDelete.id = listItem.get_id();
                    $(empDelete).click(function (sender) {
                        deleteEditEmployee(sender.target.id, itemID);
                    });
                    empList.appendChild(empDelete);
                    var empLink = document.createElement("a");
                    empLink.appendChild(document.createTextNode(employeeNames));
                    empList.appendChild(empLink);
                }
            }
            $('#editAttendees').val(employeeNames);

        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Failure " + args.get_message());
        });
}

// This function clears the inputs on the new event dialog
function clearNewEventForm() {
    var errArea = document.getElementById("errAllEvents");
    // Remove all nodes from the errAllEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#newEventDetails').dialog("close");
    $('#newEvent').val("");
    $('#newLocation').val("");
    $('#newInstructors').val("");
    $('#newStartDate').val("");
    $('#newEndDate').val("");
    $('#studentPCs').val("");
    $('#projectors').val("");
    initializePeoplePicker('peoplePickerDiv');
}

// This function shows the add new event dialog
function addNewEvent() {
    var location = document.getElementById("location");
    while (location.hasChildNodes()) {
        location.removeChild(location.lastChild);
    }
    assetList = web.get_lists().getByTitle('Resources');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = assetList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            if (listItems.get_count() > 0) {
                var listItemEnumerator1 = listItems.getEnumerator();
                while (listItemEnumerator1.moveNext()) {
                    var listItem1 = listItemEnumerator1.get_current();
                    var title = listItem1.get_fieldValues()["Title"];
                    var id = listItem1.get_fieldValues()["ID"];
                    var option = document.createElement('option');
                    option.innerHTML = title;
                    option.value = id;
                    location.appendChild(option);
                }
                $('#resource').hide();
                $('#assignResource').show();
                $('#AddEventDetails').dialog({
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
            }
            else {
                alert("There are no resources. Add some now");
                showTrainingAssets();
            }
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error in Populating Assets: " + args.get_message());
        }
        );
}

// This function saves the new event
function saveNewEvent() {
    var maxStudents = getPeopleCount();

    var eventContext = SP.ClientContext.get_current();
    var errArea = document.getElementById("errAllEvents");
    // Remove all nodes from the errAllEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if ($('#newEvent').val() == "") {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Event' field is required."));
        errArea.appendChild(divMessage);
    }
    else if (maxStudents <= $('#studentPCs').val()) {
        eventList = web.get_lists().getByTitle("EventList");
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var eventItem = eventList.addItem(itemCreateInfo);
        eventItem.set_item("Title", $('#newEvent').val());
        eventItem.set_item("EventDate", $('#newStartDate').val());
        eventItem.set_item("EndDate", $('#newEndDate').val());
        eventItem.set_item("Resources", assetItem);
        eventItem.set_item("StudentPC", $('#studentPCs').val());
        eventItem.set_item("InstructorPC", $('#newInstructors').val());
        eventItem.update();

        eventContext.load(eventItem);
        eventContext.executeQueryAsync(function () {
            // Success returned from executeQueryAsync
            eventID = eventItem.get_id();
            getUserInfo();
            cancelNewEvent();
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Failure " + args.get_message());
        });
    }
    else {
        alert("Entered attendees are more than available student PCs. Please delete some attendees");
    }
}

// This function updates an existing event's details
function saveEditEvent() {
    var maxStudents = getEditPeopleCount();

    var errArea = document.getElementById("errAllEvents");
    // Remove all nodes from the errAllEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if ($('#editEvent').val() == "") {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Event' field is required."));
        errArea.appendChild(divMessage);
    }
    else if (maxStudents <= $('#editStudentPCs').val()) {
        var selectedEventID = currentItem.get_id();
        currentItem.set_item("Title", $('#editEvent').val());
        currentItem.set_item("EventDate", $('#editStartDate').val());
        currentItem.set_item("EndDate", $('#editEndDate').val());
        currentItem.set_item("Resources", assetItem);
        currentItem.set_item("StudentPC", $('#editStudentPCs').val());
        currentItem.set_item("InstructorPC", $('#editInstructors').val());
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            //Success returned from executeQueryAsync
            $('#editEventDetails').fadeOut(500, null);
            showEvents();
            cancelEditEvent();
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Failure: " + args.get_message());
        });
    }
    else {
        alert("Entered attendees are more than available student PCs. Please delete some attendees");
    }
}

// This function cancels the new event dialog
function cancelNewEvent() {
    $('#AddEventDetails').dialog("close");
    clearNewEventForm();
}

// This function cancels the editing of an existing event
function cancelEditEvent() {
    $('#editEventDetails').dialog("close");
    clearEditEventForm();
}

// This function clears the inputs on the edit event dialog
function clearEditEventForm() {
    $('#editProjectors').attr('disabled', true);
    $('#editLocation').attr('disabled', true);
    $('#editInstructors').attr('disabled', true);
    $('#editStudentPCs').attr('disabled', true);
    var errArea = document.getElementById("errAllEvents");
    // Remove all nodes from the errAllEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#assignEditResource').show();
    $('#editResource').slideUp(500, null);
    $('#editEvent').val("");
    $('#editLocation').val("");
    $('#editInstructors').val("");
    $('#editStartDate').val("");
    $('#editEndDate').val("");
    $('#editStudentPCs').val("");
    $('#editProjectors').val("");
    initializePeoplePicker('editPeoplePicker');
}

// Query the picker for user information
function getUserInfo() {
    // Get the people picker object from the page.
    var peoplePicker = this.SPClientPeoplePicker.SPClientPeoplePickerDict.peoplePickerDiv_TopSpan;
    
    // Get information about all users.
    empList = web.get_lists().getByTitle('Employee');
    users = peoplePicker.GetAllUserInfo();
    for (var i = 0; i < users.length; i++) {
        user = users[i];
        employeeName = user["DisplayText"];
        addEmployee(employeeName);
    }
    $('#AddEventDetails').hide();
    showEvents();
}

// Query the picker for user count in new event dialog
function getPeopleCount() {
    var peoplePicker = this.SPClientPeoplePicker.SPClientPeoplePickerDict.peoplePickerDiv_TopSpan;

    // Get information about all users
    users = peoplePicker.GetAllUserInfo();
    return users.length;
}

// Query the picker for user count in edit event dialog 
function getEditPeopleCount() {
    var peoplePicker = this.SPClientPeoplePicker.SPClientPeoplePickerDict.editPeoplePicker_TopSpan;
    // Get information about all users.
    users = peoplePicker.GetAllUserInfo();
    return users.length;
}

// This function adds employee to the Employee list
function addEmployee(employeeName) {
    empList = web.get_lists().getByTitle("Employee");
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = empList.getItems(camlQuery);
    var hasEmployee = false;
    context.load(listItems);
    context.executeQueryAsync(
       function () {
           // Success returned from executeQueryAsync
            var itemCreateInfo = new SP.ListItemCreationInformation();
            var listItem = empList.addItem(itemCreateInfo);
            listItem.set_item("Title", employeeName);
            listItem.set_item("EventLookup", eventID);
            listItem.set_item("_Status", "Enrolled");
            listItem.update();
            context.load(listItem);
            context.executeQueryAsync(function () {
                
            });
       },
       function (sender, args) {
           // Failure returned from executeQueryAsync
           alert("Failure " + args.get_message());
       });
}

// This function retrieves enrolled events
function showEnrllEvents() {
    
    var enrollTable = document.getElementById("EnrllEventList");
    while (enrollTable.hasChildNodes()) {
        enrollTable.removeChild(enrollTable.lastChild);
    }
    var hasEvent = false;
    $('#AllEnrllEvents').show();
    $('#OtherEvents').hide();
    $('#showEnrllEvents').css("background-color", "orange");
    $('#showAllEvents').css("background-color", "#0072C6");
    list = web.get_lists().getByTitle("Employee");
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"
        + user
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var enrollClear = document.getElementById("AllEventsList");
                // Remove all nodes from the AllEventsList <DIV> so we have a clean space to write to
                while (enrollClear.hasChildNodes()) {
                    enrollClear.removeChild(enrollClear.lastChild);
                }
                var listItem = listItemEnumerator.get_current();
                var empEventID = listItem.get_fieldValues()["EventLookup"].get_lookupValue();
                empStatus = listItem.get_fieldValues()["_Status"];
                hasEvent = true;
                getEventName(empEventID, empStatus);
            }
            if (!hasEvent) {
            var noEvents = document.createElement("div");
            noEvents.appendChild(document.createTextNode("There are no enrolled Events."));
            enrollTable.appendChild(noEvents);
            }
            $('#AllEventsList').fadeIn(500, null);
        }, function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Failure " + args.get_message());
        });
}

// This function gives events for an employee
function getEventName(empEventID, empStatus) {
    var context1 = SP.ClientContext.get_current();
    var errArea = document.getElementById("errEvents");
    // Remove all nodes from the errEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    list = web.get_lists().getByTitle("EventList");
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Text'>"
        + empEventID
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context1.load(listItems);
    context1.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();                     
                var eventTable = document.getElementById("AllEventsList");

                if (empStatus != "Wait") {
                    var event = document.createElement("div");
                    var eventLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);

                    event.appendChild(eventLabel);

                    // Add an ID to the event DIV
                    event.id = listItem.get_id();

                    // Add an class to the event DIV
                    event.className = "item";

                    // Add an onclick event to show the event details
                    $(event).click(function (sender) {
                        showEmpEventDetails(sender.target.id, empStatus);
                    });

                    // Add the employee div to the UI
                    eventTable.appendChild(event);
                }
            }
        }, function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Failure " + args.get_message());
        });
}

// This function shows the details for a specific event assigned to current user
function showEmpEventDetails(itemID, status) {
    var errArea = document.getElementById("errEvents");
    // Remove all nodes from the errEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            if (status == "Self-Enrolled") {
                $('#deleteEnroll').show();
            }
            else {
                $('#deleteEnroll').hide();
            }
            $('#eventName').val(currentItem.get_fieldValues()["Title"]);
            assetsID = currentItem.get_fieldValues()["Resources"].get_lookupValue();
            $('#eventLocation').val(assetsID);
            $('#eventStartDate').val(new Date(currentItem.get_fieldValues()["EventDate"]).format("MMMM dd, yyyy"));
            $('#eventEndDate').val(new Date(currentItem.get_fieldValues()["EndDate"]).format("MMMM dd, yyyy"));
            $('#eventDetails').dialog(
         {
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
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var errArea = document.getElementById("errEvents");
            // Remove all nodes from the errEvents <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function cancels the event dialog
function cancelEvent() {
    $('#eventDetails').dialog("close");
}

// This function cancels the enrolled event dialog
function cancelEnrollEvent() {
    $('#enrollEventDetails').dialog("close");
}

// This function retrieves all events not assigned to current user
function showAllEvents() {
    $('#AllEnrllEvents').hide();
    $('#OtherEvents').show();
    $('#showEnrllEvents').css("background-color", "#0072C6");
    $('#showAllEvents').css("background-color", "orange");
    var enrollClear = document.getElementById("AllEventsList");
    // Remove all nodes from the AllEventsList <DIV> so we have a clean space to write to
    while (enrollClear.hasChildNodes()) {
        enrollClear.removeChild(enrollClear.lastChild);
    }
    var newArr = [];
    var origLen;
    var found;
    var x;
    var y;
    var flag = false;
    var empINEventID = new Array();
    var removeID = new Array();
    list = web.get_lists().getByTitle("Employee");
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                var eventLookup = listItem.get_fieldValues()["EventLookup"].get_lookupValue();
                var empName = listItem.get_fieldValues()["Title"];
                var status = listItem.get_fieldValues()["_Status"];
                empINEventID.push(eventLookup);
                if (empName == user && status!= "Wait") {
                    empINEventID.pop();
                    removeID.push(eventLookup); 
                }
            }
            for (var i in empINEventID) {
                for (var j in removeID) {
                    if (empINEventID[i] == removeID[j]) {    
                        delete empINEventID[i];           
                    }
                }
            }

            origLen = empINEventID.length;
            //Removes duplicate elements from empINEventID array
            for (x = 0; x < origLen; x++) {
                found = undefined;
                for (y = 0; y < newArr.length; y++) {
                    if (empINEventID[x] == newArr[y]) {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    newArr.push(empINEventID[x]);
            }
            empINEventID = newArr;
            var eventTable = document.getElementById("AllEventsList");
            while (eventTable.hasChildNodes()) {
                eventTable.removeChild(eventTable.lastChild);
            }
            if (empINEventID.length == 0|| empINEventID=="") {
                var noEvents = document.createElement("div");
                noEvents.appendChild(document.createTextNode("There are no Events to enroll."));
                eventTable.appendChild(noEvents);
            }
            else {
                for (var i in empINEventID) {
                    getAllEventName(empINEventID[i]);
                }
            }
        }, function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Failure " + args.get_message());
        });
}

// This function gives all events for an employee
function getAllEventName(empEventID) {

    var context1 = SP.ClientContext.get_current();
    var eventArray = new Array();
    var errArea = document.getElementById("errEvents");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
        list = web.get_lists().getByTitle("EventList");
        var camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Text'>"
            + empEventID
            + "</Value></Eq></Where></Query></View>");
        var listItems = list.getItems(camlQuery);
        context1.load(listItems);
        context1.executeQueryAsync(
            function () {
                // Success returned from executeQueryAsync
                var listItemEnumerator = listItems.getEnumerator();
                while (listItemEnumerator.moveNext()) {
                    var listItem = listItemEnumerator.get_current();
                    var eventTable = document.getElementById("AllEventsList");

                    var event = document.createElement("div");
                    var eventLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    var eventid = listItem.get_fieldValues()["ID"];
                    event.appendChild(eventLabel);

                    // Add an ID to the event DIV
                    event.id = listItem.get_id();

                    // Add an class to the event DIV
                    event.className = "item";

                    // Add an onclick event to show the event details
                    $(event).click(function (sender) {
                        showAllEventDetails(sender.target.id);
                    });

                    // Add the event div to the UI
                    eventTable.appendChild(event);
            
                }
                $('#AllEventsList').fadeIn(500, null);
            });
}

// This function shows the details for a specific event to employee
function showAllEventDetails(itemID) {
    var errArea = document.getElementById("errEvents");
    // Remove all nodes from the errEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    enrollID = itemID;
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            assetsID = currentItem.get_fieldValues()["Resources"].get_lookupValue();
            $('#enrollEventName').val(currentItem.get_fieldValues()["Title"]);
            $('#enrollEventLocation').val(assetsID);
            $('#enrollStartDate').val(new Date(currentItem.get_fieldValues()["EventDate"]).format("MMMM dd, yyyy"));
            $('#enrollEndDate').val(new Date(currentItem.get_fieldValues()["EndDate"]).format("MMMM dd, yyyy"));
            $('#enrollEventDetails').dialog({
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
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var errArea = document.getElementById("errEvents");
            // Remove all nodes from the errEvents <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function checks for enrollment
function enrollEvent()
{
    var errArea = document.getElementById("errEvents");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    empList = web.get_lists().getByTitle("Employee");
    currentItem = list.getItemById(enrollID);
    var maxCount = currentItem.get_fieldValues()["StudentPC"];
    context.load(currentItem);
    var enrolledCount = 0;
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var camlQuery = new SP.CamlQuery();
            camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='EventLookup' LookupId='TRUE' /><Value Type='Lookup'>"
                + enrollID
                + "</Value></Eq></Where></Query></View>");
            var listItems = empList.getItems(camlQuery);
            context.load(listItems);
            someCount = context.executeQueryAsync(
                function () {
                    var listItemEnumerator = listItems.getEnumerator();
                    while (listItemEnumerator.moveNext()) {
                        enrolledCount = enrolledCount + 1;
                    }
                    enrollEmployee(maxCount, enrolledCount, enrollID);
                });
        }, function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Failure " + args.get_message());
        });
}

// This function enrolls employee to the event
function enrollEmployee(maxCount, enrolledCount, enrollID) {
    var isWaiting = false;
    if (enrolledCount < maxCount) {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        listItem = empList.addItem(itemCreateInfo);
        listItem.set_item("Title", user);
        listItem.set_item("EventLookup", enrollID);
        listItem.set_item("_Status", "Self-Enrolled");
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            cancelEnrollEvent();
            showAllEvents();
        },
        function (sender, args) {
            alert("Failure " + args.get_message());
        });
    }
    else {        
        empList = web.get_lists().getByTitle("Employee");
        var camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='EventLookup' LookupId='TRUE' /><Value Type='Lookup'>"
            + enrollID
            + "</Value></Eq></Where></Query></View>");
        var listItems = empList.getItems(camlQuery);
        context.load(listItems);
        context.executeQueryAsync(
           function () {
               // Success returned from executeQueryAsync
               var listItemEnumerator = listItems.getEnumerator();
               while (listItemEnumerator.moveNext()) {
                   var listItem = listItemEnumerator.get_current();
                   if (listItem.get_fieldValues()["_Status"] == "Wait" && listItem.get_fieldValues()["Title"] == user) {
                       isWaiting = true;
                       alert("You are already in Waiting List.");
                   }
               }
               if (!isWaiting) {
                   var itemCreateInfo = new SP.ListItemCreationInformation();
                   listItem = empList.addItem(itemCreateInfo);
                   listItem.set_item("Title", user);
                   listItem.set_item("EventLookup", enrollID);
                   listItem.set_item("_Status", "Wait");
                   listItem.update();
                   context.load(listItem);
                   context.executeQueryAsync(function () {
                       alert("There is no free space. You are placed in Wait List");
                   });
               }
           },
           function (sender, args) {
               // Failure returned from executeQueryAsync
               alert("Failure " + args.get_message());
           });
    }
}

//This function cancels the self-enrolled event
function deleteEnrollEvent() {
    var emplist = web.get_lists().getByTitle('Employee');
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='EventLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentItem.get_id()
        + "</Value></Eq><</Where></Query></View>");
    var listItems = emplist.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        var listItemEnumerator = listItems.getEnumerator();
        while (listItemEnumerator.moveNext()) {
            var listItem = listItemEnumerator.get_current();
            if (listItem.get_fieldValues()["_Status"] == "Self-Enrolled" && listItem.get_fieldValues()["Title"] == user) {
                var empid = listItem.get_id();
                deleteEnrollEmployee(empid);
            }
        }
        
    }, function (sender, args) {
        // Faliure returned from executeQueryAsync
        alert("Failure " + args.get_message());
    });
}

//This function deletes the employee respective to the event
function deleteEnrollEmployee(empid) {
    list = web.get_lists().getByTitle("Employee");
    var empItem = list.getItemById(empid);
    empItem.deleteObject();
    cancelEvent();
    autoEnroll();
}

//This function automatically enrolls the employee who is in wait list to the event in the first come first serve manner
function autoEnroll() {
    var emplist = web.get_lists().getByTitle('Employee');
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='EventLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var listItems = emplist.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Wait") {
                    listItem.set_item("_Status", "Self-Enrolled");
                    listItem.update();
                    context.load(listItem);
                    context.executeQueryAsync(function () {  }, function (sender,args) { alert("Error:"+args.get_message())});
                    break;
                }
            }
            showEnrllEvents();
        },
        function (sender, args) {
            // Faliure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        }
        );
}

//This function shows the new Attendee dialog
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

// This function saves the Attendees in edit event form
function saveEditAttendee() {
    var maxStudents = getEditPeopleCount();
    maxStudents = maxStudents + attendeeCount;
    if (maxStudents <= $('#editStudentPCs').val()) {
        getEditUserInfo(eventID);
        cancelAttendee();
    }
    else {
        alert("Number of attendees exceeded. Please remove some attendees");
    }
}

//This function closes the new Attendee dialog
function cancelAttendee() {
    $('#editAttendees').dialog("close");
    $('#editPeoplePicker').val("");
    $('#assignEditResource').show();
    var empList = document.getElementById("showAttendees");
    while (empList.hasChildNodes()) {
        empList.removeChild(empList.lastChild);
    }
    showEventDetails(eventID);
}

// Query the picker for user information.
function getEditUserInfo(editEventID) {

    // Get the people picker object from the page.
    var peoplePicker = this.SPClientPeoplePicker.SPClientPeoplePickerDict.editPeoplePicker_TopSpan;
    // Get information about all users.
    empList = web.get_lists().getByTitle('Employee');
    users = peoplePicker.GetAllUserInfo();
    for (var i = 0; i < users.length; i++) {
        user = users[i];
        employeeName = user["DisplayText"];
        addEditEmployee(employeeName, editEventID);

    }
    attendeeCount = users.length;
}

// This function adds employee to the Employee list
function addEditEmployee(employeeName, editEventID) {
    empList = web.get_lists().getByTitle("Employee");
    //Create a CAML query that retrieves employees for event
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='EventLookup' LookupId='TRUE' /><Value Type='Lookup'>"
            + editEventID
            + "</Value></Eq></Where></Query></View>");
    var listItems = empList.getItems(camlQuery);
    var hasEmployee = false;
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
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
                listItem.set_item("EventLookup", editEventID);
                listItem.update();
                context.load(listItem);
                context.executeQueryAsync(function () {
                },
                function (sender, args) { alert("Error in Saving Attendees: " + args.get_message()); });

            }
            showEvents();
            showEventDetails(editEventID);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error: " + args.get_message());
        });
}

// This function deletes the employee from event
function deleteEditEmployee(empid, itemID) {
    list = web.get_lists().getByTitle("Employee");
    var empItem = list.getItemById(empid);
    empItem.deleteObject();
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var empList = document.getElementById("showAttendees");
            while (empList.hasChildNodes()) {
                empList.removeChild(empList.lastChild);
            }
            showEvents();
            showEventDetails(itemID);
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            alert("Error in Deleting Employee:" + args.get_message());
        });
}

// This function deletes the employee respective to event that is deleted
function removeEmployee(empid) {
    list = web.get_lists().getByTitle("Employee");
    var empItem = list.getItemById(empid);
    empItem.deleteObject();
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var empList = document.getElementById("showAttendees");
            while (empList.hasChildNodes()) {
                empList.removeChild(empList.lastChild);
            }
            showEvents();
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            alert("Error in Deleting Employee:" + args.get_message());
        });
}

// This function deletes the selected event
function deleteEditEvent() {
    $('#editProjectors').attr('disabled',true);
    $('#editLocation').attr('disabled', true);
    $('#editInstructors').attr('disabled', true);
    $('#editStudentPCs').attr('disabled', true);

    var errArea = document.getElementById("errAllEvents");
    // Remove all nodes from the errAllEvents <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var empList = web.get_lists().getByTitle('Employee');
    //Create a CAML query that retrieves the employees
    var empQuery = new SP.CamlQuery();
    empQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='EventLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var empListItems = empList.getItems(empQuery);
    context.load(empListItems);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            var listItemEnumerator = empListItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var empid = listItem.get_id();
                removeEmployee(empid);
            }
            currentItem.deleteObject();
            cancelEditEvent();
            showEvents();
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        }
        );
}