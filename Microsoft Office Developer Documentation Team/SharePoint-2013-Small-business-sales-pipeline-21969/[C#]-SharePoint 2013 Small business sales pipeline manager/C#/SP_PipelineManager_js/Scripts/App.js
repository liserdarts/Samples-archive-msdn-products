'use strict';
// Variable that will hold the SharePoint ClientContext object
var context;

// Variable that will hold the SharePoint App Web object
var web;

// Variable that will hold various SharePoint List objects 
var list;

// Variable that will hold various SharePoint ListItem objects
var currentItem;

// Variable that will hold a file selected by the user for uploading
var file;

// Variable that will hold the contents of a file selected by the user for uploading
var contents;

//Variable that will hold the total lead amount in the pipeline
var leadAmount;

//Variable that will hold the total opportunity amount in the pipeline
var oppAmount;

//Variable that will hold the total sale amount in the pipeline
var saleAmount;

// This function runs when the DOM is ready and wires up events to two file input elements.
// It also applies jQuery methods to turn various text input elements into date pickers.
// It also creates a context object which is needed to use the SharePoint object model.
$(document).ready(function () {
    var errArea = document.getElementById("errGeneral");

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Wire up events to two file input elements.
    // NOTE: IE 8 does not support .addEventListener, so if that's
    // not supported use .attachEvent instead.
    var oppUpload = document.getElementById("oppUpload");
    if (!oppUpload.addEventListener) {
        oppUpload.attachEvent("onchange", oppAttach);
    }
    else {
        oppUpload.addEventListener("change", oppAttach, false);
    }
   

    // Reference and load the basic SharePoint objects needed to start with
    context = SP.ClientContext.get_current();
    web = context.get_web();
    context.load(web);
    context.executeQueryAsync(function () {

        // Success returned from executeQueryAsync
        hideAllPanels();
        $('#Home').fadeIn(500, null);
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
    $('#AllLeads').hide();
    $('#AddLead').hide();
    $('#LeadDetails').hide();
    $('#AllOpps').hide();
    $('#OppDetails').hide();
    $('#AllSales').hide();
    $('#SaleDetails').hide();
    $('#AllLostSales').hide();
    $('#LostSaleDetails').hide();
    $('#AllReports').hide();
    $('#amountPipeline').hide();
    $('#countPipeline').hide();
    $('#chartArea').hide();
    $('#drillDown').hide();
    $('#pipelineName').hide();
    $('#conversionName').hide();
}

// This function retrieves all leads.
function showLeads() {
    //Highlight the selected tile
    $('#LeadsTile').css("background-color", "orange");
    $('#OppsTile').css("background-color", "#0072C6");
    $('#SalesTile').css("background-color", "#0072C6");
    $('#LostSalesTile').css("background-color", "#0072C6");
    $('#ReportsTile').css("background-color", "#0072C6");
   
    var errArea = document.getElementById("errAllLeads");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    var hasLeads = false;
    hideAllPanels();
    var LeadList = document.getElementById("AllLeads");
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);

    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var leadTable = document.getElementById("LeadList");

            // Remove all nodes from the lead <DIV> so we have a clean space to write to
            while (leadTable.hasChildNodes()) {
                leadTable.removeChild(leadTable.lastChild);
            }

            // Iterate through the Prospects list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Lead") {


                    // Create a DIV to display the organization name 
                    var lead = document.createElement("div");
                    var leadLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    lead.appendChild(leadLabel);

                    // Add an ID to the lead DIV
                    lead.id = listItem.get_id();

                    // Add an class to the lead DIV
                    lead.className = "item";

                    // Add an onclick event to show the lead details
                    $(lead).click(function (sender) {
                        showLeadDetails(sender.target.id);
                    });

                    // Add the lead div to the UI
                    leadTable.appendChild(lead);
                    hasLeads = true;
                }
            }
            if (!hasLeads) {
                var noLeads = document.createElement("div");
                noLeads.appendChild(document.createTextNode("There are no leads. You can add a new lead from here."));
                leadTable.appendChild(noLeads);
            }
            $('#AllLeads').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get Leads. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#LeadList').fadeIn(500, null);
        });

}

//This function shows the form for adding a new lead
function addNewLead() {
    $('#LeadDetails').hide();
    $('#AddLead').fadeIn(500, null);
}

//This function shows the details for a specific lead
function showLeadDetails(itemID) {
var errArea = document.getElementById("errAllLeads");
// Remove all nodes from the error <DIV> so we have a clean space to write to
while (errArea.hasChildNodes()) {
    errArea.removeChild(errArea.lastChild);
}
$('#AddLead').hide();
$('#LeadDetails').hide();
$('#AllOpps').hide();
$('#OppDetails').hide();
$('#AllSales').hide();
$('#SaleDetails').hide();
currentItem = list.getItemById(itemID);
context.load(currentItem);
context.executeQueryAsync(
    function () {
        $('#editLead').val(currentItem.get_fieldValues()["Title"]);
        $('#editContactPerson').val(currentItem.get_fieldValues()["ContactPerson"]);
        $('#editContactNumber').val(currentItem.get_fieldValues()["ContactNumber"]);
        $('#editEmail').val(currentItem.get_fieldValues()["Email"]);
        $('#editPotentialAmount').val("$"+currentItem.get_fieldValues()["DealAmount"]);
        //Add an onclick event to the convertToOpp div
        $('#convertToOpp').click(function (sender) {
            convertToOpp(itemID);
        });
        $('#LeadDetails').fadeIn(500, null);
    },
    function (sender, args) {
        var errArea = document.getElementById("errAllLeads");
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

//This function saves the newly-entered lead
function saveNewLead() {
    if ($('#newPotentialAmount').val() == "" || $('#newLead').val() == "") {
        var errArea = document.getElementById("errAllLeads");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Organization Name or amount field is empty."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var listItem = list.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#newLead').val());
        listItem.set_item("ContactPerson", $('#newContactPerson').val());
        listItem.set_item("ContactNumber", $('#newContactNumber').val());
        listItem.set_item("Email", $('#newEmail').val());
        listItem.set_item("_Status", "Lead");

        listItem.set_item("DealAmount", $('#newPotentialAmount').val());
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            clearNewLeadForm();
            showLeads();
        },
            function (sender, args) {
                var errArea = document.getElementById("errAllLeads");
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

//This function updates an existing lead's details
function saveEditLead() {
    if ($('#editPotentialAmount').val() == "" || $('#editLead').val() == "") {
        var errArea = document.getElementById("errAllLeads");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Organization Name or amount field is empty."));
        errArea.appendChild(divMessage);
    }
    else {
        currentItem.set_item("Title", $('#editLead').val());
        currentItem.set_item("ContactPerson", $('#editContactPerson').val());
        currentItem.set_item("ContactNumber", $('#editContactNumber').val());
        currentItem.set_item("Email", $('#editEmail').val());
        currentItem.set_item("DealAmount", $('#editPotentialAmount').val());
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            clearEditLeadForm();
            showLeads();
        },
            function (sender, args) {
                var errArea = document.getElementById("errAllLeads");
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

//This function cancels the creation of a lead
function cancelNewLead() {
    clearNewLeadForm();
}

// This function clears the inputs on the new lead form
function clearNewLeadForm() {
    var errArea = document.getElementById("errAllLeads");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddLead').fadeOut(500, function () {
        $('#AddLead').hide();
        $('#newLead').val("");
        $('#newContactPerson').val("");
        $('#newContactNumber').val("");
        $('#newEmail').val("");
        $('#newPotentialAmount').val("");


    });

}

// This function cancels the editing of an existing lead's details
function cancelEditLead() {
    clearEditLeadForm();
}

// This function clears the inputs on the edit form for a lead
function clearEditLeadForm() {
    var errArea = document.getElementById("errAllLeads");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in future operations
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#LeadDetails').fadeOut(500, function () {
        $('#LeadDetails').hide();
        $('#editLead').val("");
        $('#editContactPerson').val("");
        $('#editContactNumber').val("");
        $('#editEmail').val("");
        $('#editPotentialAmount').val("");
    });
}

//This function converts a lead into opportunity and shows opportunity
function convertToOpp(itemID) {
    hideAllPanels();
    clearEditLeadForm();
    currentItem.set_item("_Status", "Opportunity");
    currentItem.update();
    context.load(currentItem);
    context.executeQueryAsync(function () {
        clearNewLeadForm();
        showOpps();
    }, function (sender, args) {
        var errArea = document.getElementById("errAllLeads");
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

//This retrieves all opportunities
function showOpps() {
    //Highlight the selected tile
    $('#LeadsTile').css("background-color", "#0072C6");
    $('#OppsTile').css("background-color", "orange");
    $('#SalesTile').css("background-color", "#0072C6");
    $('#LostSalesTile').css("background-color", "#0072C6");
    $('#ReportsTile').css("background-color", "#0072C6");
    var errArea = document.getElementById("errAllOpps");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasOpps = false;

    hideAllPanels();
    var oppList = document.getElementById("AllOpps");
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            var oppTable = document.getElementById("OppList");
            // Remove all nodes from the Opportunity <DIV> so we have a clean space to write to
            while (oppTable.hasChildNodes()) {
                oppTable.removeChild(oppTable.lastChild);
            }

            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Opportunity") {
                    // Create a DIV to display the organization name
                    var opp = document.createElement("div");
                    var oppLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    opp.appendChild(oppLabel);

                    // Add an ID to the opportunity DIV
                    opp.id = listItem.get_id();

                    // Add an class to the opportunity DIV
                    opp.className = "item";

                    // Add an onclick event to show the opportunity details
                    $(opp).click(function (sender) {
                        showOppDetails(sender.target.id);
                    });

                    //Add the opportunity div to the UI
                    oppTable.appendChild(opp);
                    hasOpps = true;
                }
            }
            if (!hasOpps) {
                var noOpps = document.createElement("div");
                noOpps.appendChild(document.createTextNode("There are no opportunity.  You can add a new Opportunity to an existing Lead."));
                oppTable.appendChild(noOpps);
            }
            $('#AllOpps').fadeIn(500, null);
        },

            function (sender, args) {

                // Failure returned from executeQueryAsync
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode("Failed to get opportunity. Error: " + args.get_message()));
                errArea.appendChild(divMessage);
                $('#OppList').fadeIn(500, null);
            });

}

//This function shows the details for a specific opportunity
function showOppDetails(itemID) {
    var errArea = document.getElementById("errAllOpps");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    $('#AddOpp').hide();
    $('#OppDetails').hide();
    $('#AddSale').hide();
    $('#SaleDetails').hide();

    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            $('#editOpp').val(currentItem.get_fieldValues()["Title"]);
            $('#editOppPerson').val(currentItem.get_fieldValues()["ContactPerson"]);
            $('#editOppNumber').val(currentItem.get_fieldValues()["ContactNumber"]);
            $('#editOppEmail').val(currentItem.get_fieldValues()["Email"]);
            $('#editOppAmount').val("$" + currentItem.get_fieldValues()["DealAmount"]);

            //Add an onclick event to the convertToSale div
            $('#convertToSale').click(function (sender) {
                convertToSale(itemID);
            });

            //Add an onclick event to the convertToLostSale div
            $('#convertToLostSale').click(function (sender) {
                convertToLostSale(itemID);
            });

            var oppList = document.getElementById("OppAttachments");
            while (oppList.hasChildNodes()) {
                oppList.removeChild(oppList.lastChild);
            }
            if (currentItem.get_fieldValues()["Attachments"] == true) {
                var attachmentFolder = web.getFolderByServerRelativeUrl("Lists/Prospects/Attachments/" + itemID);
                var attachments = attachmentFolder.get_files();
                context.load(attachments);
                context.executeQueryAsync(function () {
                    // Enumerate and list the Opp Attachments if they exist
                    var attachementEnumerator = attachments.getEnumerator();
                    while (attachementEnumerator.moveNext()) {
                        var attachment = attachementEnumerator.get_current();

                        var oppDelete = document.createElement("span");
                        oppDelete.appendChild(document.createTextNode("Delete"));
                        oppDelete.className = "deleteButton";
                        oppDelete.id = attachment.get_serverRelativeUrl();


                        $(oppDelete).click(function (sender) {
                            deleteOppAttachment(sender.target.id, itemID);
                        });
                        oppList.appendChild(oppDelete);
                        var oppLink = document.createElement("a");
                        oppLink.setAttribute("target", "_blank");
                        oppLink.setAttribute("href", attachment.get_serverRelativeUrl());
                        oppLink.appendChild(document.createTextNode(attachment.get_name()));
                        oppList.appendChild(oppLink);
                        oppList.appendChild(document.createElement("br"));
                        oppList.appendChild(document.createElement("br"));
                    }
                },
                function () {

                });
            }
            $('#OppDetails').fadeIn(500, null);

        },
        function (sender, args) {
            var errArea = document.getElementById("errAllOpps");
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

//This function converts an opportunity into sale and shows sales
function convertToSale(itemID) {
    $('#AllOpps').hide();
    $('#OppDetails').hide();
    clearEditOppForm();

    currentItem.set_item("_Status", "Sale");
    currentItem.update();
    context.load(currentItem);
    context.executeQueryAsync(function () {
        clearEditOppForm();
        showSales();
    }
    ,
    function (sender, args) {
        var errArea = document.getElementById("errAllLeads");
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

//This function converts an opportunity into lost opportunity
function convertToLostSale(itemID) {
    $('#AllOpps').hide();
    $('#OppDetails').hide();
    clearEditOppForm();

    currentItem.set_item("_Status", "Lost Sale");
    currentItem.update();
    context.load(currentItem);
    context.executeQueryAsync(function () {
        clearEditOppForm();
        showLostSales();
    }
    ,
    function (sender, args) {
        var errArea = document.getElementById("errAllLeads");
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

//This function updates an existing opportunity's details
function saveEditOpp() {

    if ($('#editOppAmount').val() == "" || $('#editOpp').val() == "") {
        var errArea = document.getElementById("errAllOpps");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Organization Name or amount field is empty."));
        errArea.appendChild(divMessage);
    }
    else {
        currentItem.set_item("Title", $('#editOpp').val());
        currentItem.set_item("ContactPerson", $('#editOppPerson').val());
        currentItem.set_item("ContactNumber", $('#editOppNumber').val());
        currentItem.set_item("Email", $('#editOppEmail').val());
        currentItem.set_item("DealAmount", $('#editOppAmount').val());
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            clearEditOppForm();
            showOpps();
        },
            function (sender, args) {
                var errArea = document.getElementById("errAllOpps");
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

// This function cancels the editing of an existing opportunity's details
function cancelEditOpp() {
    clearEditOppForm();
}

// This function clears the inputs on the edit form for a opportunity
function clearEditOppForm() {
    var errArea = document.getElementById("errAllOpps");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#OppDetails').fadeOut(500, function () {
        $('#OppDetails').hide();
        $('#editOpp').val("");
        $('#editOppPerson').val("");
        $('#editOppNumber').val("");
        $('#editOppEmail').val("");
        $('#editOppAmount').val("");
        $('#editOppDate').val("");
    });
}

//This function deletes an attachment from a Prospects list opportunity item and then refreshed the Prospects form
function deleteOppAttachment(url, itemID) {
    var attachment = web.getFileByServerRelativeUrl(url);
    attachment.deleteObject();
    showOppDetails(itemID);
}

// This function runs when a file is successfully loaded and read by the PO file input.
// It references SP.RequestExecutor.js which will upload the file as an attachment by using the REST API.
// NOTE: This is safer and more capabale (in terms of file size) than using JSOM file creation for uploading files as attachments.
function oppFileOnload(event) {
    contents = event.target.result;
    // The storePOAsAttachment function is called to do the actual work after we have a reference to SP.RequestExecutor.js
    $.getScript(web.get_url() + "/_layouts/15/SP.RequestExecutor.js", storeOppAsAttachment);
}

// This helper function ensures that the byte array passed in is returned in a format 
// that's required for the contents of a file sent to SharePoint for storage as a list item attachment.
function fixBuffer(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return binary;
}

// This function runs when the file input is used to uplaod a proposal document for a opportunity
function oppAttach(event) {
    var errArea = document.getElementById("errAllOpps");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if (!event.target) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("The FileSystem APIs are not supported in this browser."));
        errArea.appendChild(divMessage);
        return (false);
    }
    var files = event.target.files;
    if (!window.FileReader) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("The FileSystem APIs are not supported in this browser."));
        errArea.appendChild(divMessage);
        return (false);
    }
    if (files.length > 0) {

        // Get the first file. In this case, only one file can be selected but because the file input could support
        // multi-file selection in some browsers we still need to access the file as the 0th member of the files collection
        file = files[0];

        // Wire up the HTML5 FileReader to read the selected file
        var reader = new FileReader();
        reader.onload = oppFileOnload;
        reader.onerror = function (event) {
            var errArea = document.getElementById("errAllOpps");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Error reading file: " + event.target.error.code));
            errArea.appendChild(divMessage);
        };

        // Reading the file triggers the oppFileOnLoad function that was wired up above
        reader.readAsArrayBuffer(file);
    }
    return false;
}

// This function runs after we are sure we have a reference to SP.RequestExecutor.js.
// It uses the REST API to upload the file as an attachment 
function storeOppAsAttachment() {
    var fileContents = fixBuffer(contents);
    var createitem = new SP.RequestExecutor(web.get_url());
    createitem.executeAsync({
        url: web.get_url() + "/_api/web/lists/GetByTitle('Prospects')/items(" + currentItem.get_id() + ")/AttachmentFiles/add(FileName='" + file.name + "')",
        method: "POST",
        binaryStringRequestBody: true,
        body: fileContents,
        success: storeOppSuccess,
        error: storeOppFailure,
        state: "Update"
    });
    function storeOppSuccess(data) {

        // Success callback
        var errArea = document.getElementById("errAllOpps");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }

        // Workaround to clear the value in the file input.
        // What we really want to do is clear the text of the input=file element. 
        // However, we are not allowed to do that because it could allow malicious script to interact with the file system. 
        // So we’re not allowed to read/write that value in JavaScript (or jQuery)
        // So what we have to do is replace the entire input=file element with a new one (which will have an empty text box). 
        // However, if we just replaced it with HTML, then it wouldn’t be wired up with the same events as the original.
        // So we replace it with a clone of the original instead. 
        // And that’s what we need to do just to clear the text box but still have it work for uploading a second, third, fourth file.
        $('#oppUpload').replaceWith($('#oppUpload').val('').clone(true));
        var oppUpload = document.getElementById("oppUpload");
        oppUpload.addEventListener("change", oppAttach, false);
        showOppDetails(currentItem.get_id());
    }
    function storeOppFailure(data) {

        // Failure callback
        var errArea = document.getElementById("errAllOpps");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("File upload failed."));
        errArea.appendChild(divMessage);
    }
}

// This function retrieves all sales from Prospects list
function showSales() {
    //Highlight selected tile
    $('#LeadsTile').css("background-color", "#0072C6");
    $('#OppsTile').css("background-color", "#0072C6");
    $('#SalesTile').css("background-color", "orange");
    $('#LostSalesTile').css("background-color", "#0072C6");
    $('#ReportsTile').css("background-color", "#0072C6");

    var errArea = document.getElementById("errAllSales");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    var hasSales = false;
    hideAllPanels();
    var saleList = document.getElementById("AllSales");
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var saleTable = document.getElementById("SaleList");

            // Remove all nodes from the sale <DIV> so we have a clean space to write to
            while (saleTable.hasChildNodes()) {
                saleTable.removeChild(saleTable.lastChild);
            }
            // Iterate through the Propsects list
            var listItemEnumerator = listItems.getEnumerator();

            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Sale") {
                    // Create a DIV to display the organization name
                    var sale = document.createElement("div");
                    var saleLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    sale.appendChild(saleLabel);

                    // Add an ID to the sale DIV
                    sale.id = listItem.get_id();

                    // Add an class to the sale DIV
                    sale.className = "item";

                    // Add an onclick event to show the sale details
                    $(sale).click(function (sender) {
                        showSaleDetails(sender.target.id);
                    });

                    saleTable.appendChild(sale);
                    hasSales = true;
                }
            }
            if (!hasSales) {
                var noSales = document.createElement("div");
                noSales.appendChild(document.createTextNode("There are no sales. You can add a new sale from here."));
                saleTable.appendChild(noSales);
            }
            $('#AllSales').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get sales. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#SaleList').fadeIn(500, null);
        });
}

// This function shows the details for a specific sale
function showSaleDetails(itemID) {
    var errArea = document.getElementById("errAllSales");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#SaleDetails').hide();
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            $('#editSale').val(currentItem.get_fieldValues()["Title"]);
            $('#editSalePerson').val(currentItem.get_fieldValues()["ContactPerson"]);
            $('#editSaleNumber').val(currentItem.get_fieldValues()["ContactNumber"]);
            $('#editSaleEmail').val(currentItem.get_fieldValues()["Email"]);
            $('#editSaleStatus').val(currentItem.get_fieldValues()["_Status"]);
            $('#editSaleAmount').val("$" + currentItem.get_fieldValues()["DealAmount"]);
            $('#SaleDetails').fadeIn(500, null);

               

        },
        function (sender, args) {
            var errArea = document.getElementById("errAllSales");
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

// This function clears the inputs on the edit form for a sale
function cancelEditSale() {
    clearEditSaleForm();
}

// This function clears the inputs on the edit form for a sale
function clearEditSaleForm() {
    var errArea = document.getElementById("errAllSales");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in future operations
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#SaleDetails').fadeOut(500, function () {
        $('#SaleDetails').hide();
        $('#editSaleName').val("");
        $('#editSalePerson').val("");
        $('#editSaleNumber').val("");
        $('#editSaleEmail').val("");
        $('#editSaleAmount').val("");
    });
}

//This function updates an existing sale's details
function saveEditSale() {

    if ($('#editSaleAmount').val() == "" || $('#editSale').val() == "") {
        var errArea = document.getElementById("errAllSales");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Organization Name or amount field is empty."));
        errArea.appendChild(divMessage);
    }
    else {
        currentItem.set_item("Title", $('#editSale').val());
        currentItem.set_item("ContactPerson", $('#editSalePerson').val());
        currentItem.set_item("ContactNumber", $('#editSaleNumber').val());
        currentItem.set_item("Email", $('#editSaleEmail').val());
        currentItem.set_item("DealAmount", $('#editSaleAmount').val());
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            showSales();
        },
            function (sender, args) {
                var errArea = document.getElementById("errAllSales");
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

// This function retrieves all lost sales from Prospects list
function showLostSales() {
    $('#LeadsTile').css("background-color", "#0072C6");
    $('#OppsTile').css("background-color", "#0072C6");
    $('#SalesTile').css("background-color", "#0072C6");
    $('#LostSalesTile').css("background-color", "orange");
    $('#ReportsTile').css("background-color", "#0072C6");

    var errArea = document.getElementById("errAllLostSales");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    var haslostSales = false;
    hideAllPanels();
    var saleList = document.getElementById("AllLostSales");
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var saleTable = document.getElementById("LostSaleList");

            // Remove all nodes from the lostSale <DIV> so we have a clean space to write to
            while (saleTable.hasChildNodes()) {
                saleTable.removeChild(saleTable.lastChild);
            }
            // Iterate through the Propsects list
            var listItemEnumerator = listItems.getEnumerator();

            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Lost Sale") {
                    // Create a DIV to display the organization name
                    var lostSale = document.createElement("div");
                    var saleLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    lostSale.appendChild(saleLabel);

                    // Add an ID to the lostSale DIV
                    lostSale.id = listItem.get_id();

                    // Add an class to the lostSale DIV
                    lostSale.className = "item";

                    // Add an onclick event to show the lostSale details
                    $(lostSale).click(function (sender) {
                        showLostSaleDetails(sender.target.id);
                    });

                    saleTable.appendChild(lostSale);
                    haslostSales = true;
                }
            }
            if (!haslostSales) {
                var noLostSales = document.createElement("div");
                noLostSales.appendChild(document.createTextNode("There are no lost sales."));
                saleTable.appendChild(noLostSales);
            }
            $('#AllLostSales').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get lost sales. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#LostSaleList').fadeIn(500, null);
        });
}

// This function shows the details for a specific lost sale
function showLostSaleDetails(itemID) {
    var errArea = document.getElementById("errAllLostSales");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#LostSaleDetails').hide();
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            $('#lostSale').val(currentItem.get_fieldValues()["Title"]);
            $('#lostSalePerson').val(currentItem.get_fieldValues()["ContactPerson"]);
            $('#lostSaleNumber').val(currentItem.get_fieldValues()["ContactNumber"]);
            $('#lostSaleEmail').val(currentItem.get_fieldValues()["Email"]);
            $('#lostSaleStatus').val(currentItem.get_fieldValues()["_Status"]);
            $('#lostSaleAmount').val("$" + currentItem.get_fieldValues()["DealAmount"]);
            $('#LostSaleDetails').fadeIn(500, null);
        },
        function (sender, args) {
            var errArea = document.getElementById("errAllLostSales");
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

// This function closes the lost sale detail form
function cancelLostSale() {
    var errArea = document.getElementById("errAllSales");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in future operations
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#LostSaleDetails').fadeOut(500, function () {
        $('#LostSaleDetails').hide();
    });
}

// This function creates report graphs
function createGraph(allPipe, allLead, allOpp, allSale, allCurrentPipe, allLost) {
    $('#pipelineName').show();
    $('#conversionName').show();
    $('#pipeLead').width((allLead / allCurrentPipe) * 800);
    $('#pipeOpportunity').width((allOpp / allCurrentPipe) * 800);
    $('#pipeSale').width((allSale / allCurrentPipe) * 800);
    $('#leadText').width((allLead / allCurrentPipe) * 800);
    $('#blankopp').width((allOpp / allCurrentPipe) * 800);
    $('#blankLead').width((allLead / allCurrentPipe) * 800);
    $('#oppText').width((allOpp / allCurrentPipe) * 800);
    $('#saleText').width((allSale / allCurrentPipe) * 800);
    // Calculate the widths for conversion rate
    var total = allLost + allSale;
    $('#wonOpp').width((allSale / total) * 660);
    $('#lostOpp').width((allLost / total) * 660);
}

// This function shows lead count and total deal amount in pipeline on mouseover
function showLeadAmount(leadAmount) {
    var getdiv = document.getElementById("hoverLead");
    getdiv.innerText = "";
    var leadAmtLabel = document.createElement("DIV");
    leadAmtLabel.className = "chartBarLabel";
    leadAmtLabel.appendChild(document.createTextNode("$" + leadAmount.toLocaleString()));
    $('#hoverLead').append(leadAmtLabel);
}

// This function shows opportunities count and total deal amount in pipeline on mouseover
function showOppAmount(oppAmount) {
    var getdiv = document.getElementById("hoverOpp");
    getdiv.innerText = "";
    var oppAmtLabel = document.createElement("DIV");
    oppAmtLabel.className = "chartBarLabel";
    oppAmtLabel.appendChild(document.createTextNode("$" + oppAmount.toLocaleString()));
    $('#hoverOpp').append(oppAmtLabel);
}

// This function shows customer count and total deal amount in pipeline on mouseover
function showSaleAmount(saleAmount) {
    var saleAmtLabel = document.createElement("DIV");
    saleAmtLabel.className = "chartBarLabel";
    saleAmtLabel.appendChild(document.createTextNode("$" + saleAmount.toLocaleString()));
    var getdiv = document.getElementById("hoverSale");
    getdiv.innerText = "";
    $('#hoverSale').append(saleAmtLabel);
    var getwondiv = document.getElementById("wonOpp");
    getwondiv.innerText = "";
    var wonAmtLabel = document.createElement("DIV");
    wonAmtLabel.className = "wonLostLabel";
    wonAmtLabel.appendChild(document.createTextNode("$" + saleAmount.toLocaleString()));
    $('#wonOpp').append(wonAmtLabel);
}

// This function shows lost opportunity count and total deal amount in graph on mouseover
function showLostAmount(lostAmount) {
    var getdiv = document.getElementById("lostOpp");
    getdiv.innerText = "";
    var lostAmtLabel = document.createElement("DIV");
    lostAmtLabel.className = "wonLostLabel";
    lostAmtLabel.appendChild(document.createTextNode("$" + lostAmount.toLocaleString()));
    $('#lostOpp').append(lostAmtLabel);
}

// This function shows lead count and total deal amount in pipeline on mouseover
function showLeadCount(leadCount) { 
    var getDiv = document.getElementById("hoverLead");
    getDiv.innerText = leadCount;
}

// This function shows opportunities count and total deal amount in pipeline on mouseover
function showOppCount(oppCount) {      
    var getDiv = document.getElementById("hoverOpp");
    getDiv.innerText = oppCount; 
}

// This function shows customer count and total deal amount in pipeline on mouseover
function showSaleCount(saleCount) {
    //$('#pipeSale').mouseenter(function () {
    var getDiv = document.getElementById("hoverSale");
    getDiv.innerText = saleCount;

    var getwondiv = document.getElementById("wonOpp");
    getwondiv.innerText = "";
    var wonCountLabel = document.createElement("DIV");
    wonCountLabel.className = "wonLostLabel";
    wonCountLabel.appendChild(document.createTextNode(saleCount));
    $('#wonOpp').append(wonCountLabel);   
}

// This function shows lost opportunity count and total deal amount in graph on mouseover
function showLostSaleCount(lostCount) {
    var getdiv = document.getElementById("lostOpp");
    getdiv.innerText = "";
    var lostCountLabel = document.createElement("DIV");
    lostCountLabel.className = "wonLostLabel";
    lostCountLabel.appendChild(document.createTextNode(lostCount));
    $('#lostOpp').append(lostCountLabel);
}

// This function shows the conversion rate report and pipeline
function showReports() {
    $('#LeadsTile').css("background-color", "#0072C6");
    $('#OppsTile').css("background-color", "#0072C6");
    $('#SalesTile').css("background-color", "#0072C6");
    $('#LostSalesTile').css("background-color", "#0072C6");
    $('#ReportsTile').css("background-color", "orange");
    hideAllPanels();
    $('#AllReports').fadeIn(500, null);
    $('#amountPipeline').show();
    $('#countPipeline').show();
}

// This function gets deal amount for leads, opportunities, sales and lost opportunities
function getAmount() {
    $("#wonLostDrillDown").hide();
    $("#pipeDrillDown").hide();
    $("#drillTable").hide();
    leadAmount = 0;
    oppAmount = 0;
    saleAmount = 0;

    var lostSaleAmount = 0;
    var allProspect = 0;
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Iterate through the list items in the Invoice list
            var listItemEnumerator = listItems.getEnumerator();

            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Sum up all deal amounts
                if (listItem.get_fieldValues()["_Status"] == "Lead") {
                    leadAmount += parseFloat(listItem.get_fieldValues()["DealAmount"]);
                }
                if (listItem.get_fieldValues()["_Status"] == "Opportunity") {
                    oppAmount += parseFloat(listItem.get_fieldValues()["DealAmount"]);
                }
                if (listItem.get_fieldValues()["_Status"] == "Sale") {
                    saleAmount += parseFloat(listItem.get_fieldValues()["DealAmount"]);
                }
                if (listItem.get_fieldValues()["_Status"] == "Lost Sale") {
                    lostSaleAmount += parseFloat(listItem.get_fieldValues()["DealAmount"]);
                }
                if (listItem.get_fieldValues()) {
                    allProspect += parseFloat(listItem.get_fieldValues()["DealAmount"]);
                }
                var pipelineAmount = allProspect - lostSaleAmount;
            }
            showLeadAmount(leadAmount);
            showOppAmount(oppAmount);
            showSaleAmount(saleAmount);
            showLostAmount(lostSaleAmount);
            $('#chartArea').fadeIn(500, null);
            createGraph(allProspect, leadAmount, oppAmount, saleAmount, pipelineAmount, lostSaleAmount);
        }, function () { alert("failure in get amount"); });
}

// This function gets count for leads, opportunities, sales and lost opportunities
function getCount() {
    $("#wonLostDrillDown").hide();
    $("#pipeDrillDown").hide();
    $("#drillTable").hide();

    var leadCount = 0;
    var oppCount = 0;
    var saleCount = 0;
    var lostSaleCount = 0;
    var allCount = 0;
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Iterate through the list items in the Invoice list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Sum up all counts
                if (listItem.get_fieldValues()["_Status"] == "Lead") {
                    leadCount = parseInt(leadCount) + 1;
                }
                if (listItem.get_fieldValues()["_Status"] == "Opportunity") {
                    oppCount = parseInt(oppCount) + 1;
                }
                if (listItem.get_fieldValues()["_Status"] == "Sale") {
                    saleCount = parseInt(saleCount) + 1;
                }
                if (listItem.get_fieldValues()["_Status"] == "Lost Sale") {
                    lostSaleCount = parseInt(lostSaleCount) + 1;
                }
                if (listItem.get_fieldValues()) {
                    allCount = parseInt(allCount) + 1;
                }
                var current = allCount - lostSaleCount;
            }
            showLeadCount(leadCount);
            showOppCount(oppCount);
            showSaleCount(saleCount);
            showLostSaleCount(lostSaleCount);
            $('#chartArea').fadeIn(500, null);
            createGraph(allCount, leadCount, oppCount, saleCount, current, lostSaleCount);

        }, function () { alert("failure in getCount"); });
}

// This function gives leads details in the pipe drill down
function drillLead() {
    var hasLeads = false;
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    var type = "Lead";
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var leadTable = document.getElementById("drillTable");

            // Remove all nodes from the drillTable <DIV> so we have a clean space to write to
            while (leadTable.hasChildNodes()) {
                leadTable.removeChild(leadTable.lastChild);
            }

            // Iterate through the Prospects list
            var listItemEnumerator = listItems.getEnumerator();

            var listItem = listItemEnumerator.get_current();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Lead") {
                    // Get information for each Lead
                    var leadTitle = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    var leadPerson = document.createTextNode(listItem.get_fieldValues()["ContactPerson"]);
                    var leadNumber = document.createTextNode(listItem.get_fieldValues()["ContactNumber"]);
                    var leadEmail = document.createTextNode(listItem.get_fieldValues()["Email"]);
                    var leadPotentialAmt = document.createTextNode(listItem.get_fieldValues()["DealAmount"]);
                    drillTable(leadTitle.textContent, leadPerson.textContent, leadNumber.textContent,leadEmail.textContent, leadPotentialAmt.textContent, type, leadAmount.toLocaleString());
                }
                hasLeads = true;
            }

            if (!hasLeads) {
                // Text to display if there are no leads
                var noLeads = document.createElement("div");
                noLeads.appendChild(document.createTextNode("There are no leads. You can add a new lead from here."));
                leadTable.appendChild(noLeads);
            }
            $('#drillDown').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get Leads. Error: " + args.get_message()));
            errArea.appendChild(divMessage);

        });
}

// This function gives opportunity details in the pipe drill down
function drillOpp() {
    var hasOpp = false;
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    var type = "Opportunities";
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var oppTable = document.getElementById("drillTable");

            // Remove all nodes from the drillTable <DIV> so we have a clean space to write to
            while (oppTable.hasChildNodes()) {
                oppTable.removeChild(oppTable.lastChild);
            }

            // Iterate through the Prospects list
            var listItemEnumerator = listItems.getEnumerator();

            var listItem = listItemEnumerator.get_current();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Opportunity") {
                    // Get information for each Opportunity
                    var oppTitle = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    var oppPerson = document.createTextNode(listItem.get_fieldValues()["ContactPerson"]);
                    var oppNumber = document.createTextNode(listItem.get_fieldValues()["ContactNumber"]);
                    var oppEmail = document.createTextNode(listItem.get_fieldValues()["Email"]);
                    var oppAmt = document.createTextNode(listItem.get_fieldValues()["DealAmount"]);
                    drillTable(oppTitle.textContent, oppPerson.textContent, oppNumber.textContent, oppEmail.textContent, oppAmt.textContent, type, oppAmount);
                }
                hasOpp = true;
            }

            if (!hasOpp) {
                // Text to display if there are no Opportunities
                var noOpps = document.createElement("div");
                noOpps.appendChild(document.createTextNode("There are no Opportunities."));
                oppTable.appendChild(noOpps);
            }
            $('#drillDown').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get Opportunities. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function gives sale details in the pipe drill down
function drillSale() {
    var hasSale = false;
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    var type = "Sale";
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var saleTable = document.getElementById("drillTable");

            // Remove all nodes from the drillTable <DIV> so we have a clean space to write to
            while (saleTable.hasChildNodes()) {
                saleTable.removeChild(saleTable.lastChild);
            }

            // Iterate through the Prospects list
            var listItemEnumerator = listItems.getEnumerator();

            var listItem = listItemEnumerator.get_current();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Sale") {
                    // Get information for each Sale
                    var saleTitle = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    var salePerson = document.createTextNode(listItem.get_fieldValues()["ContactPerson"]);
                    var saleNumber = document.createTextNode(listItem.get_fieldValues()["ContactNumber"]);
                    var saleEmail = document.createTextNode(listItem.get_fieldValues()["Email"]);
                    var saleAmt = document.createTextNode(listItem.get_fieldValues()["DealAmount"]);


                    drillTable(saleTitle.textContent, salePerson.textContent, saleNumber.textContent, saleEmail.textContent, saleAmt.textContent, type, saleAmount);
                }
                hasSale = true;
            }

            if (!hasSale) {
                // Text to display if there are no Sales
                var noSales = document.createElement("div");
                noSales.appendChild(document.createTextNode("There are no Sales."));
                saleTable.appendChild(noSales);
            }
            $('#drillDown').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get Sales. Error: " + args.get_message()));
            errArea.appendChild(divMessage);

        });
}

// This function gives sales details in the conversion rate drill down
function drillWon() {
    var hasWon = false;
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    // Get the type of prospect and its percentage
    var type = "Won";
    var getDiv = document.getElementById("wonOpp");
    var getWidth = parseFloat((getDiv.clientWidth / 800) * 100);
    getWidth = getWidth.toFixed(2);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var wonTable = document.getElementById("drillTable");

            // Remove all nodes from the drillTable <DIV> so we have a clean space to write to
            while (wonTable.hasChildNodes()) {
                wonTable.removeChild(wonTable.lastChild);
            }

            // Iterate through the Prospects list
            var listItemEnumerator = listItems.getEnumerator();

            var listItem = listItemEnumerator.get_current();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Sale") {
                    // Get information for each Sale
                    var saleTitle = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    var salePerson = document.createTextNode(listItem.get_fieldValues()["ContactPerson"]);
                    var saleNumber = document.createTextNode(listItem.get_fieldValues()["ContactNumber"]);
                    var saleEmail = document.createTextNode(listItem.get_fieldValues()["Email"]);
                    var saleAmt = document.createTextNode(listItem.get_fieldValues()["DealAmount"]);
                    drillConvert(saleTitle.textContent, salePerson.textContent, saleNumber.textContent, saleEmail.textContent, saleAmt.textContent, getWidth, type);
                }
                hasWon = true;
            }

            if (!hasWon) {
                // Text to display if there are no won Sales
                var noWon = document.createElement("div");
                noWon.appendChild(document.createTextNode("There are no Sales."));
                wonTable.appendChild(noWon);
            }
            $('#drillDown').fadeIn(500, null);
        });
}

// This function gives lost sales details in the conversion rate drill down
function drillLost() {
    var hasLost = false;
    list = web.get_lists().getByTitle('Prospects');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    // Get the type of prospect and its percentage
    var type = "Lost Sale";
    var getDiv = document.getElementById("lostOpp");
    var getWidth = parseFloat((getDiv.clientWidth / 800) * 100);
    getWidth = getWidth.toFixed(2);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var lostSaleTable = document.getElementById("drillTable");

            // Remove all nodes from the drillTable <DIV> so we have a clean space to write to
            while (lostSaleTable.hasChildNodes()) {
                lostSaleTable.removeChild(lostSaleTable.lastChild);
            }

            // Iterate through the Prospects list
            var listItemEnumerator = listItems.getEnumerator();

            var listItem = listItemEnumerator.get_current();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                if (listItem.get_fieldValues()["_Status"] == "Lost Sale") {
                    // Get information for each Lost Sale
                    var lostSaleTitle = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    var lostSalePerson = document.createTextNode(listItem.get_fieldValues()["ContactPerson"]);
                    var lostSaleNumber = document.createTextNode(listItem.get_fieldValues()["ContactNumber"]);
                    var lostSaleEmail = document.createTextNode(listItem.get_fieldValues()["Email"]);
                    var lostSaleAmt = document.createTextNode(listItem.get_fieldValues()["DealAmount"]);
                    drillConvert(lostSaleTitle.textContent, lostSalePerson.textContent, lostSaleNumber.textContent, lostSaleEmail.textContent, lostSaleAmt.textContent, getWidth.toString(), type.toString());

                }
                hasLost = true;
            }

            if (!hasLost) {
                // Text to display if there are no Lost Sales
                var noLostSales = document.createElement("div");
                noLostSales.appendChild(document.createTextNode("There are no Lost Sales."));
                lostSaleTable.appendChild(noLostSales);
            }
            $('#drillDown').fadeIn(500, null);
        });
}

// This function creates summary table for the pipeline drill down
function drillTable(orgName, contactPerson, contactNumber, email, amount, type, total) {
    // Check if some of the non required fields are blank
    if (contactPerson == "null") {
        contactPerson = "-";
    }
    if (contactNumber == "null") {
        contactNumber = "-";
    }
    if (email == "null") {
        email = "-";
    }
    $("#wonLostDrillDown").hide();
    $("#pipeSummary").hide();
    // Shows the pipeline summary
    $("#pipeDrillDown").fadeIn(500, function () {
        var getDiv = document.getElementById("pipeSummary");
        getDiv.innerText = "Total " + type + " amount is :  $" + total.toLocaleString();
        $("#pipeSummary").show();
    });    
    $("#drillTable").show();
    $("#drillTable").append("<div class='clear'>&nbsp;</div> <div class='reportLabel'>" + orgName + "</div>  <div class='reportLabel'> " + contactPerson + "</div> <div class='reportLabel'> " + contactNumber + "</div>  <div class='reportLabel'> " + email + "</div>  <div class='amountLabel' > $" + amount.toLocaleString() + "</div>");
  
}

// This function creates summary table for the Won/Lost drill down
function drillConvert(orgName, contactPerson, contactNumber, email, amount, rate, type) {
    // Check if some of the non required fields are blank
    if (contactPerson == "null") {
        contactPerson = "-";
    }
    if (contactNumber == "null") {
        contactNumber = "-";
    }
    if (email == "null") {
        email = "-";
    }
    $("#pipeDrillDown").hide();
    $("#conversionSummary").hide();
    // Shows the won lost summary
    $("#wonLostDrillDown").fadeIn(500, function () {
        var getDiv = document.getElementById("conversionSummary");
        getDiv.innerText = type + " Percentage is :  " + rate + "%";
        $("#conversionSummary").show();
    });
    
    $("#drillTable").show();
    $("#drillTable").append("<div class='clear'>&nbsp;</div> <div class='reportLabel'>" + orgName + "</div>  <div class='reportLabel'> " + contactPerson + "</div> <div class='reportLabel'> " + contactNumber + "</div>  <div class='reportLabel'> " + email + "</div>  <div class='amountLabel' id='drillamount'> $" + amount.toLocaleString() + "</div>");
}
