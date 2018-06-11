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
    var poUpload = document.getElementById("poUpload");
    if (!poUpload.addEventListener) {
        poUpload.attachEvent("onchange", poAttach);
    }
    else {
        poUpload.addEventListener("change", poAttach, false);
    }
    var invUpload = document.getElementById("invUpload");
    if (!invUpload.addEventListener) {
        invUpload.attachEvent("onchange", invAttach);
    }
    else {
        invUpload.addEventListener("change", invAttach, false);
    }

    // Turn various text inputs into calendars by using jQuery UI methods
    $('#newPODate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });
    $('#newPODueDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });
    $('#editPODate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });
    $('#editPODueDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });
    $('#newInvoiceDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });
    $('#editInvoiceDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });

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
    $('#AllCustomers').hide();
    $('#AddCustomer').hide();
    $('#CustomerDetails').hide();
    $('#AllPOs').hide();
    $('#AddPO').hide();
    $('#PODetails').hide();
    $('#AllInvoices').hide();
    $('#AddInvoice').hide();
    $('#InvoiceDetails').hide();
    $('#AllReports').hide();
}

// This function retrieves all customers.
function showCustomers() {
    var errArea = document.getElementById("errAllCustomers");

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasCustomers = false;
    hideAllPanels();
    var customerList = document.getElementById("AllCustomers");
    list = web.get_lists().getByTitle('Customer');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var customerTable = document.getElementById("CustomerList");

            // Remove all nodes from the customer <DIV> so we have a clean space to write to
            while (customerTable.hasChildNodes()) {
                customerTable.removeChild(customerTable.lastChild);
            }

            // Iterate through the Customer list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the customer name
                var customer = document.createElement("div");
                var customerLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                customer.appendChild(customerLabel);

                // Add an ID to the customer DIV
                customer.id = listItem.get_id();
                
                // Add an class to the customer DIV
                customer.className = "item";

                // Add an onclick event to show the customer details
                $(customer).click(function (sender) {
                    showCustomerDetails(sender.target.id);
                });

                // Add the customer div to the UI
                customerTable.appendChild(customer);
                hasCustomers = true;
            }
            if (!hasCustomers) {
                var noCustomers = document.createElement("div");
                noCustomers.appendChild(document.createTextNode("There are no customers. You can add a new customer from here."));
                customerTable.appendChild(noCustomers);
            }
            $('#AllCustomers').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get customers. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#CustomerList').fadeIn(500, null);
        });
}

// This function retrieves all purchase orders.
function showPOs() {
    var errArea = document.getElementById("errAllPOs");

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasPOs = false;
    hideAllPanels();
    var poList = document.getElementById("AllPOs");
    list = web.get_lists().getByTitle('Purchase Order');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var poTable = document.getElementById("POList");

            // Remove all nodes from the PO <DIV> so we have a clean space to write to
            while (poTable.hasChildNodes()) {
                poTable.removeChild(poTable.lastChild);
            }

            // Iterate through the PO list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the PO number
                var po = document.createElement("div");
                var poLabel = document.createTextNode(listItem.get_fieldValues()["PONumber"]);
                po.appendChild(poLabel);

                // Add an ID to the PO DIV
                po.id = listItem.get_id();

                // Add an class to the PO DIV
                po.className = "item";

                // Add an onclick event to show the PO details
                $(po).click(function (sender) {
                    showPODetails(sender.target.id);
                });

                // Add the PO div to the UI
                poTable.appendChild(po);
                hasPOs = true;
            }
            if (!hasPOs) {
                var noPOs = document.createElement("div");
                noPOs.appendChild(document.createTextNode("There are no purchase orders. You can add a new PO to an existing customer."));
                poTable.appendChild(noPOs);
            }
            $('#AllPOs').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get purchase orders. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#AllPOs').fadeIn(500, null);
        });
}

// This function retrieves all purchase orders for a specific customer.
function showPOsForCustomer(itemID, customer) {
    var errArea = document.getElementById("errAllPOs");

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasPOs = false;
    hideAllPanels();
    var poList = document.getElementById("AllPOs");
    list = web.get_lists().getByTitle('Purchase Order');

    // Create a CAML query that retrieves the POs for the customer in question
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='POCustomer' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync 
            var poTable = document.getElementById("POList");

            // Remove all nodes from the PO <DIV> so we have a clean space to write to
            while (poTable.hasChildNodes()) {
                poTable.removeChild(poTable.lastChild);
            }

            // Add a clickable label of the customer's name at the top of the list.
            // This effectively enables the user to go back to the customer details
            // that they were looking at before they clicked 'View Purchase Orders'
            // for the customer in question.
            var custLabel = document.createElement("div");
            custLabel.className = "clicker";
            custLabel.id = itemID;
            $(custLabel).click(function (sender) {
                showCustomers();
                showCustomerDetails(sender.target.id);
            });
            custLabel.appendChild(document.createTextNode(customer));
            poTable.appendChild(custLabel);

            // Iterate through the PO list items
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the PO number
                var po = document.createElement("div");
                var poLabel = document.createTextNode(listItem.get_fieldValues()["PONumber"]);
                po.appendChild(poLabel);

                // Add an ID to the PO DIV
                po.id = listItem.get_id();

                // Add an class to the PO DIV
                po.className = "item";

                // Add an onclick event to show the PO details
                $(po).click(function (sender) {
                    showPODetails(sender.target.id);
                });

                //Add the PO div to the UI
                poTable.appendChild(po);
                hasPOs = true;
            }
            if (!hasPOs) {
                var noPOs = document.createElement("div");
                noPOs.appendChild(document.createTextNode("There are no purchase orders for this customer."));
                poTable.appendChild(noPOs);
            }
            $('#AllPOs').fadeIn(500, null);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get purchase orders. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#AllPOs').fadeIn(500, null);
        });
}

// This function retrieves all invoices.
function showInvoices() {
    var errArea = document.getElementById("errAllInvoices");

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasInvoices = false;
    hideAllPanels();
    var InvoiceList = document.getElementById("AllInvoices");
    list = web.get_lists().getByTitle('Invoice');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync.
            var invTable = document.getElementById("InvoiceList");

            // Remove all nodes from the Invoice <DIV> so we have a clean space to write to
            while (invTable.hasChildNodes()) {
                invTable.removeChild(invTable.lastChild);
            }

            // Iterate through the Invoice list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                //Create a DIV to display the Invoice number
                var inv = document.createElement("div");
                var invLabel = document.createTextNode(listItem.get_fieldValues()["InvoiceNumber"]);
                inv.appendChild(invLabel);

                //Add an ID to the Invoice DIV
                inv.id = listItem.get_id();

                //Add an class to the Invoice DIV
                inv.className = "item";

                //Add an onclick event to show the Invoice details
                $(inv).click(function (sender) {
                    showInvoiceDetails(sender.target.id);
                });

                //Add the Invoice div to the UI
                invTable.appendChild(inv);
                hasInvoices = true;
            }
            if (!hasInvoices) {
                var noInvs = document.createElement("div");
                noInvs.appendChild(document.createTextNode("There are no invoices. You can add a new invoice to an existing purchase order."));
                invTable.appendChild(noInvs);
            }
            $('#AllInvoices').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get invoices. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#AllInvoices').fadeIn(500, null);
        });
}

// This function retrieves all invoices for a specific customer.
function showInvoicesForCustomer(itemID, customer) {
    var errArea = document.getElementById("errAllInvoices");

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasInvoices = false;
    hideAllPanels();
    var InvoiceList = document.getElementById("AllInvoices");
    list = web.get_lists().getByTitle('Invoice');

    // Create a CAML query that retrieves the invoices for the customer in question
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Customer' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync.
            var invTable = document.getElementById("InvoiceList");

            // Remove all nodes from the Invoice <DIV> so we have a clean space to write to
            while (invTable.hasChildNodes()) {
                invTable.removeChild(invTable.lastChild);
            }

            // Add a clickable label of the customer's name at the top of the list.
            // This effectively enables the user to go back to the customer details
            // that they were looking at before they clicked 'View Invoices'
            // for the customer in question.
            var custLabel = document.createElement("div");
            custLabel.className = "clicker";
            custLabel.id = itemID;
            $(custLabel).click(function (sender) {
                showCustomers();
                showCustomerDetails(sender.target.id);
            });
            custLabel.appendChild(document.createTextNode(customer));
            invTable.appendChild(custLabel);

            // Iterate through the invoice list.
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the Invoice number
                var inv = document.createElement("div");
                var invLabel = document.createTextNode(listItem.get_fieldValues()["InvoiceNumber"]);
                inv.appendChild(invLabel);

                // Add an ID to the PO DIV
                inv.id = listItem.get_id();

                // Add an class to the PO DIV
                inv.className = "item";

                // Add an onclick event to show the Invoice details
                $(inv).click(function (sender) {
                    showInvoiceDetails(sender.target.id);
                });

                // Add the Invoice div to the UI
                invTable.appendChild(inv);
                hasInvoices = true;
            }
            if (!hasInvoices) {
                var noInvs = document.createElement("div");
                noInvs.appendChild(document.createTextNode("There are no invoices for this customer."));
                invTable.appendChild(noInvs);
            }
            $('#AllInvoices').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get invoices. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#AllInvoices').fadeIn(500, null);
        });
}

// This function retireves all invoices for a specifc purchase order
function showInvoicesForPO(itemID, po) {
    var errArea = document.getElementById("errAllInvoices");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasInvoices = false;

    hideAllPanels();
    var InvoiceList = document.getElementById("AllInvoices");
    list = web.get_lists().getByTitle('Invoice');
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='PONumber' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            var invTable = document.getElementById("InvoiceList");
            // Remove all nodes from the Invoice <DIV> so we have a clean space to write to
            while (invTable.hasChildNodes()) {
                invTable.removeChild(invTable.lastChild);
            }
            var poLabel = document.createElement("div");
            poLabel.className = "clicker";
            poLabel.id = itemID;
            $(poLabel).click(function (sender) {
                showPOs();
                showPODetails(sender.target.id);
            });
            //poLabel.onclick = function (sender) {
            //    showPOs();
            //    showPODetails(sender.target.id);
            //};
            poLabel.appendChild(document.createTextNode(po));
            invTable.appendChild(poLabel);
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                //Create a DIV to display the Invoice number
                var inv = document.createElement("div");
                var invLabel = document.createTextNode(listItem.get_fieldValues()["InvoiceNumber"]);
                inv.appendChild(invLabel);
                //Add an ID to the PO DIV
                inv.id = listItem.get_id();
                //Add an class to the PO DIV
                inv.className = "item";
                //Add an onclick event to show the PO details
                $(inv).click(function (sender) {
                    showInvoiceDetails(sender.target.id);
                });
                //inv.onclick = function (sender) { showInvoiceDetails(sender.target.id); };

                //Add the Invoice div to the UI
                invTable.appendChild(inv);
                hasInvoices = true;
            }
            if (!hasInvoices) {
                var noInvs = document.createElement("div");
                noInvs.appendChild(document.createTextNode("There are no invoices for this purchase order."));
                invTable.appendChild(noInvs);
            }
            $('#AllInvoices').fadeIn(500, function () {

            });
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get invoices. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#AllInvoices').fadeIn(500, function () {

            });
        }
        );
}

// This function shows the reports about invoices/payments
function showReports() {

    // Reset the chart
    var totalInvoiced = 0;
    var totalReceived = 0;
    var totalBecomingDue = 0;
    var totalOverdue = 0;
    $('#totalInvoicedBar').width(0);
    $('#totalReceivedBar').width(0);
    $('#totalDueBar').width(0);
    $('#totalOverdueBar').width(0);
    $('#totalInvoicedBar').empty();
    $('#totalReceivedBar').empty();
    $('#totalDueBar').empty();
    $('#totalOverdueBar').empty();

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    var errArea = document.getElementById("errReports");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    hideAllPanels();

    // Get the Invoice list
    list = web.get_lists().getByTitle('Invoice');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            var invTable = document.getElementById("InvoiceList");
            // Remove all nodes from the PO <DIV> so we have a clean space to write to
            while (invTable.hasChildNodes()) {
                invTable.removeChild(invTable.lastChild);
            }

            // Iterate through the list items in the Invoice list
            var listItemEnumerator = listItems.getEnumerator();

            // Get today's date
            var today = new Date();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Sum up all invoices that are either 'Open' or 'Paid'
                if ((listItem.get_fieldValues()["PaymentStatus"] != "Rejected") && (listItem.get_fieldValues()["PaymentStatus"] != "Canceled")) {
                    totalInvoiced += parseFloat(listItem.get_fieldValues()["InvoiceAmount"]);
                }

                // Sum up all invoices that are 'Paid'
                if (listItem.get_fieldValues()["PaymentStatus"] == "Paid") {
                    totalReceived += parseFloat(listItem.get_fieldValues()["InvoiceAmount"]);
                }

                // If the status is Open, we'll need to look at invoice dates and payment terms
                if (listItem.get_fieldValues()["PaymentStatus"] == "Open")
                {
                    var iDate = new Date(listItem.get_fieldValues()["InvoiceDate"]);
                    var dDate = new Date(iDate);
                    dDate.setDate(iDate.getDate() + parseInt(listItem.get_fieldValues()["PaymentTerms"]));
                    if(dDate>=today)
                    {
                        // Payment terms have not been exceeded yet
                        totalBecomingDue += parseFloat(listItem.get_fieldValues()["InvoiceAmount"]);
                    }
                    else
                    {
                        // Payment terms HAVE been exceeded
                        totalOverdue += parseFloat(listItem.get_fieldValues()["InvoiceAmount"]);
                    }
                }
            }

            // The factor is used to scale the bars in the resultant chart
            var factor = totalInvoiced;
            if (totalInvoiced > 0) {

                // Draw the first bar
                $('#totalInvoicedBar').width((totalInvoiced / factor) * 560);
                var invoicedLabel = document.createElement("DIV");
                invoicedLabel.className = "chartBarLabel";
                invoicedLabel.appendChild(document.createTextNode(totalInvoiced.toLocaleString()));
                $('#totalInvoicedBar').append(invoicedLabel);
            }
            if (totalReceived > 0) {

                // Draw the second bar in the correct scale, relative to the first bar
                $('#totalReceivedBar').width((totalReceived / factor) * 560);
                var receivededLabel = document.createElement("DIV");
                receivededLabel.className = "chartBarLabel";
                receivededLabel.appendChild(document.createTextNode(totalReceived.toLocaleString()));
                $('#totalReceivedBar').append(receivededLabel);
            }
            if (totalBecomingDue > 0) {

                // Draw the third bar in the correct scale, relative to the first bar
                $('#totalDueBar').width((totalBecomingDue / factor) * 560);
                var dueLabel = document.createElement("DIV");
                dueLabel.className = "chartBarLabel";
                dueLabel.appendChild(document.createTextNode(totalBecomingDue.toLocaleString()));
                $('#totalDueBar').append(dueLabel);
            }
            if (totalOverdue > 0) {

                // Draw the fourth bar in the correct scale, relative to the first bar
                $('#totalOverdueBar').width((totalOverdue / factor) * 560);
                var overdueLabel = document.createElement("DIV");
                overdueLabel.className = "chartBarLabel";
                overdueLabel.appendChild(document.createTextNode(totalOverdue.toLocaleString()));
                $('#totalOverdueBar').append(overdueLabel);
            }
            $('#AllReports').fadeIn(500, null);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get invoices. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#AllReports').fadeIn(500, null);
        });
}

// This function shows the form for adding a new customer
function addNewCustomer() {
    $('#CustomerDetails').hide();
    $('#AddCustomer').fadeIn(500, null);
}

// This function saves the newly-entered customer
function saveNewCustomer() {
    if ($('#newCustomer').val() == "") {
        var errArea = document.getElementById("errAllCustomers");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Customer' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var listItem = list.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#newCustomer').val());
        //The following column's internal name is WorkAddress
        listItem.set_item("WorkAddress", $('#newAddress').text());
        listItem.set_item("WorkPhone", $('#newPhone').val());
        listItem.set_item("EMail", $('#newEmail').val());
        listItem.set_item("WebPage", $('#newWeb').val());
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            clearNewCustomerForm();
            showCustomers();
        },
            function (sender, args) {
                var errArea = document.getElementById("errAllCustomers");
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

// This function cancels the creation of a customer
function cancelNewCustomer() {
    clearNewCustomerForm();
}

// This function clears the inputs on the new customer form
function clearNewCustomerForm() {
    var errArea = document.getElementById("errAllCustomers");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
 $('#AddCustomer').fadeOut(500, function () {
        $('#AddCustomer').hide();
        $('#newCustomer').val("");
        $('#newAddress').val("");
        $('#newPhone').val("");
        $('#newEmail').val("");
        $('#newWeb').val("");
    });
}

// This function shows the details for a specific customer
function showCustomerDetails(itemID) {
    var errArea = document.getElementById("errAllCustomers");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddCustomer').hide();
    $('#CustomerDetails').hide();
    $('#AddPO').hide();
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            $('#editCustomer').val(currentItem.get_fieldValues()["Title"]);
            $('#editAddress').val(currentItem.get_fieldValues()["WorkAddress"]);
            $('#editPhone').val(currentItem.get_fieldValues()["WorkPhone"]);
            $('#editEmail').val(currentItem.get_fieldValues()["EMail"]);
            if (currentItem.get_fieldValues()["WebPage"] == null) {
                $('#editWeb').val("");
            }
            else {
                $('#editWeb').val(currentItem.get_fieldValues()["WebPage"].get_url());
            }
            //Add an onclick event to the addPOToCustomer div
            //var addPo = document.getElementById("addPOToCustomer");
            $('#addPOToCustomer').click(function (sender) {
                addNewPO(itemID, currentItem.get_fieldValues()["Title"]);
            });
            //addPo.onclick = function (sender) { addNewPO(itemID, currentItem.get_fieldValues()["Title"]); };
            //
            //var viewPo = document.getElementById("viewPOsForCustomer");
            $('#viewPOsForCustomer').click(function (sender) {
                showPOsForCustomer(itemID, currentItem.get_fieldValues()["Title"]);
            });
            //viewPo.onclick = function (sender) { showPOsForCustomer(itemID, currentItem.get_fieldValues()["Title"]); };
            //
            //var viewInv = document.getElementById("viewInvoicesForCustomer");
            $('#viewInvoicesForCustomer').click(function (sender) {
                showInvoicesForCustomer(itemID, currentItem.get_fieldValues()["Title"]);
            });
            //viewInv.onclick = function (sender) { showInvoicesForCustomer(itemID, currentItem.get_fieldValues()["Title"]); };
            //
            $('#CustomerDetails').fadeIn(500, null);
        },
        function (sender, args) {
            var errArea = document.getElementById("errAllCustomers");
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

// This function updates an existing customer's details
function saveEditCustomer() {
    if ($('#editCustomer').val() == "") {
        var errArea = document.getElementById("errAllCustomers");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Customer' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        currentItem.set_item("Title", $('#editCustomer').val());
        currentItem.set_item("WorkAddress", $('#editAddress').text());
        currentItem.set_item("WorkPhone", $('#editPhone').val());
        currentItem.set_item("EMail", $('#editEmail').val());
        currentItem.set_item("WebPage", $('#editWeb').val());
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            clearEditCustomerForm();
            showCustomers();
        },
            function (sender, args) {
                var errArea = document.getElementById("errAllCustomers");
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

// This function cancels the editing of an existing customer's details
function cancelEditCustomer() {
    clearEditCustomerForm();
}

// This function clears the inputs on the edit form for a customer
function clearEditCustomerForm() {
    var errArea = document.getElementById("errAllCustomers");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in future operations
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#CustomerDetails').fadeOut(500, function () {
        $('#CustomerDetails').hide();
        $('#editCustomer').val("");
        $('#editAddress').val("");
        $('#editPhone').val("");
        $('#editEmail').val("");
        $('#editWeb').val("");
    });
}

// This function deletes the selected customer
function deleteEditCustomer() {
    var errArea = document.getElementById("errAllCustomers");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var POList = web.get_lists().getByTitle('Purchase Order');
    var poQuery = new SP.CamlQuery();
    poQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='POCustomer' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var POListItems = POList.getItems(poQuery);
    context.load(POListItems);
    context.executeQueryAsync(
        function () {
            if (POListItems.get_count() >= 1) {
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode("Customer has purchase orders and cannot be deleted."));
                errArea.appendChild(divMessage);
            }
            else {
                currentItem.deleteObject();
                context.executeQueryAsync(
                    function () {
                        clearEditCustomerForm();
                        showCustomers();
                    },
                    function (sender, args) {
                        var divMessage = document.createElement("DIV");
                        divMessage.setAttribute("style", "padding:5px;");
                        divMessage.appendChild(document.createTextNode(args.get_message()));
                        errArea.appendChild(divMessage);
                    }
                    );
            }
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to check purchase orders. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        }
        );
}

// This function shows the form for adding a new purchase order
function addNewPO(itemID, customer) {
    $('#PODetails').hide();
    $('#CustomerDetails').hide();
    clearEditCustomerForm();
    $('#newPOCustomer').val(itemID + ";#" + customer);
    $('#AddPO').fadeIn(500, null);
}

// This function saves the newly-entered purchase order
function saveNewPO() {
    if ($('#newPONumber').val() == "") {
        var errArea = document.getElementById("errAllCustomers");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'PO Number' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var poList = web.get_lists().getByTitle('Purchase Order');
        var poListItem = poList.addItem(itemCreateInfo);
        poListItem.set_item("POCustomer", $('#newPOCustomer').val());
        poListItem.set_item("PONumber", $('#newPONumber').val());
        poListItem.set_item("PODate", $('#newPODate').val());
        poListItem.set_item("PODueDate", $('#newPODueDate').val());
        poListItem.set_item("POAmount", $('#newPOAmount').val());
        poListItem.update();
        context.load(poListItem);
        context.executeQueryAsync(function () {
            clearNewPOForm();
            showPOs();
        },
            function (sender, args) {
                var errArea = document.getElementById("errAllCustomers");
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

// This function cancels the creation of a purchase order
function cancelNewPO() {
    clearNewPOForm();
}

// This function clears the inputs on the new purchase order form
function clearNewPOForm() {
    var errArea = document.getElementById("errAllPOs");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddPO').fadeOut(500, function () {
        $('#AddPO').hide();
        $('#newPOCustomer').val("");
        $('#newPONumber').val("");
        $('#newPODate').val("");
        $('#newPODueDate').val("");
        $('#newPOAmount').val("");
    });
}

// This function shows the details for a specific purchase order
function showPODetails(itemID) {
    var errArea = document.getElementById("errAllPOs");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddPO').hide();
    $('#PODetails').hide();
    $('#AddInvoice').hide();
    $('#InvoiceDetails').hide();
    clearNewInvoiceForm();
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            $('#editPOCustomer').val(currentItem.get_fieldValues()["POCustomer"].get_lookupId() + ";#" + currentItem.get_fieldValues()["POCustomer"].get_lookupValue());
            $('#editPONumber').val(currentItem.get_fieldValues()["PONumber"]);
            $('#editPOAmount').val(currentItem.get_fieldValues()["POAmount"]);
            $('#editPODate').val(new Date(currentItem.get_fieldValues()["PODate"]).format("MMMM dd, yyyy"));
            $('#editPODueDate').val(new Date(currentItem.get_fieldValues()["PODueDate"]).format("MMMM dd, yyyy"));
            // Add an onclick event to the addPOToCustomer div
            //var addInv = document.getElementById("addInvoiceToPO");
            $('#addInvoiceToPO').click(function (sender) {
                addNewInvoice(currentItem.get_fieldValues()["POCustomer"].get_lookupId() + ";#" + currentItem.get_fieldValues()["POCustomer"].get_lookupValue(),
                    itemID,
                    currentItem.get_fieldValues()["PONumber"]);
            });
            //addInv.onclick = function (sender) {
            //    addNewInvoice(currentItem.get_fieldValues()["POCustomer"].get_lookupId() + ";#" + currentItem.get_fieldValues()["POCustomer"].get_lookupValue(),
            //        itemID,
            //        currentItem.get_fieldValues()["PONumber"]);
            //};
            //var viewInv = document.getElementById("viewInvoicesForPO");
            $('#viewInvoicesForPO').click(function (sender) {
                showInvoicesForPO(itemID, currentItem.get_fieldValues()["PONumber"]);
            });
            //viewInv.onclick = function (sender) { showInvoicesForPO(itemID, currentItem.get_fieldValues()["PONumber"]); };

            var poList = document.getElementById("POAttachments");
            while (poList.hasChildNodes()) {
                poList.removeChild(poList.lastChild);
            }
            if (currentItem.get_fieldValues()["Attachments"] == true) {
                var attachmentFolder = web.getFolderByServerRelativeUrl("Lists/Purchase Order/Attachments/" + itemID);
                var attachments = attachmentFolder.get_files();
                context.load(attachments);
                context.executeQueryAsync(function () {
                    // Enumerate and list the PO Attachments if they exist
                    var attachementEnumerator = attachments.getEnumerator();
                    while (attachementEnumerator.moveNext()) {
                        var attachment = attachementEnumerator.get_current();

                        var poDelete = document.createElement("span");
                        poDelete.appendChild(document.createTextNode("Delete"));
                        poDelete.className = "deleteButton";
                        poDelete.id = attachment.get_serverRelativeUrl();

                        //poDelete.onclick = function (sender) { deletePOAttachment(sender.target.id, itemID); };
                        $(poDelete).click(function (sender) {
                            deletePOAttachment(sender.target.id, itemID);
                        });
                        poList.appendChild(poDelete);
                        var poLink = document.createElement("a");
                        poLink.setAttribute("target", "_blank");
                        poLink.setAttribute("href", attachment.get_serverRelativeUrl());
                        poLink.appendChild(document.createTextNode(attachment.get_name()));
                        poList.appendChild(poLink);
                        poList.appendChild(document.createElement("br"));
                        poList.appendChild(document.createElement("br"));
                    }
                },
                function () {

                });
            }
            $('#PODetails').fadeIn(500, null);

        },
        function (sender, args) {
            var errArea = document.getElementById("errAllPOs");
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

// This function updates an existing purchase order's details
function saveEditPO() {
    currentItem.set_item("POCustomer", $('#editPOCustomer').val());
    currentItem.set_item("PONumber", $('#editPONumber').val());
    currentItem.set_item("PODate", $('#editPODate').val());
    currentItem.set_item("PODueDate", $('#editPODueDate').val());
    currentItem.set_item("POAmount", $('#editPOAmount').val());
    currentItem.update();
    context.load(currentItem);
    context.executeQueryAsync(function () {
        clearEditPOForm();
        showPOs();
    },
        function (sender, args) {
            var errArea = document.getElementById("errAllPOs");
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

// This function cancels the editing of an existing purchase order's details
function cancelEditPO() {
    clearEditPOForm();
}

// This function deletes the selected purchase order
function deleteEditPO() {
    var errArea = document.getElementById("errAllPOs");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var InvoiceList = web.get_lists().getByTitle('Invoice');
    var invoiceQuery = new SP.CamlQuery();
    invoiceQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='PONumber' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var InvoiceListItems = InvoiceList.getItems(invoiceQuery);
    context.load(InvoiceListItems);
    context.executeQueryAsync(
        function () {
            if (InvoiceListItems.get_count() >= 1) {
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode("Purchase order has invoices and cannot be deleted."));
                errArea.appendChild(divMessage);
            }
            else {

                currentItem.deleteObject();
                context.executeQueryAsync(
                    function () {
                        clearEditPOForm();
                        showPOs();
                    },
                    function (sender, args) {
                        var divMessage = document.createElement("DIV");
                        divMessage.setAttribute("style", "padding:5px;");
                        divMessage.appendChild(document.createTextNode(args.get_message()));
                        errArea.appendChild(divMessage);
                    }
                    );


            }
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to check invoices. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        }
        );
}

// This function clears the inputs on the edit purchase order form
function clearEditPOForm() {
    var errArea = document.getElementById("errAllPOs");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#PODetails').fadeOut(500, function () {
        $('#PODetails').hide();
        $('#editPOCustomer').val("");
        $('#editPONumber').val("");
        $('#editPODate').val("");
        $('#editPODueDate').val("");
        $('#editPOAmount').val("");
    });
}

// This function shows the form for adding a new invoice
function addNewInvoice(customer, itemID, po) {
    $('#PODetails').hide();
    $('#CustomerDetails').hide();
    clearEditPOForm();
    $('#newInvoiceCustomer').val(customer);
    $('#newInvoicePO').val(itemID + ";#" + po);
    $('#AddInvoice').fadeIn(500, null);
    
}

// This function saves the newly-entered  invoice
function saveNewInvoice() {
    if ($('#newInvoiceNumber').val() == "") {
        var errArea = document.getElementById("errAllPOs");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Invoice Number' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var invoiceList = web.get_lists().getByTitle('Invoice');
        var invoiceListItem = invoiceList.addItem(itemCreateInfo);
        invoiceListItem.set_item("Customer", $('#newInvoiceCustomer').val());
        invoiceListItem.set_item("PONumber", $('#newInvoicePO').val());
        invoiceListItem.set_item("InvoiceNumber", $('#newInvoiceNumber').val());
        invoiceListItem.set_item("InvoiceDate", $('#newInvoiceDate').val());
        invoiceListItem.set_item("PaymentTerms", $('#newInvoiceTerms').val());
        invoiceListItem.set_item("InvoiceAmount", $('#newInvoiceAmount').val());
        invoiceListItem.set_item("PaymentStatus", $('#newInvoiceStatus').val());
        invoiceListItem.update();
        context.load(invoiceListItem);
        context.executeQueryAsync(function () {
            clearNewInvoiceForm();
            showInvoices();
        },
            function (sender, args) {
                var errArea = document.getElementById("errAllPOs");
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

// This function cancels the creation of an invoice
function cancelNewInvoice() {
    clearNewInvoiceForm();
}

// This function clears the inputs on the new invoice form
function clearNewInvoiceForm() {
    var errArea = document.getElementById("errAllPOs");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddInvoice').fadeOut(500, function () {
        $('#AddInvoice').hide();
        $('#newInvoiceCustomer').val("");
        $('#newInvoicePO').val("");
        $('#newInvoiceNumber').val("");
        $('#newInvoiceDate').val("");
        $('#newInvoiceStatus').val("Open");
        $('#newInvoiceAmount').val("");
        $('#newInvoiceTerms').val("30");
    });
}

// This function shows the details for a specific invoice
function showInvoiceDetails(itemID) {
    var errArea = document.getElementById("errAllInvoices");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddInvoice').hide();
    $('#InvoiceDetails').hide();
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            $('#editInvoiceCustomer').val(currentItem.get_fieldValues()["Customer"].get_lookupId() + ";#" + currentItem.get_fieldValues()["Customer"].get_lookupValue());
            $('#editInvoicePO').val(currentItem.get_fieldValues()["PONumber"].get_lookupId() + ";#" + currentItem.get_fieldValues()["PONumber"].get_lookupValue());
            $('#editInvoiceNumber').val(currentItem.get_fieldValues()["InvoiceNumber"]);
            $('#editInvoiceAmount').val(currentItem.get_fieldValues()["InvoiceAmount"]);
            $('#editInvoiceTerms').val(currentItem.get_fieldValues()["PaymentTerms"]);
            $('#editInvoiceStatus').val(currentItem.get_fieldValues()["PaymentStatus"]);
            $('#editInvoiceDate').val(new Date(currentItem.get_fieldValues()["InvoiceDate"]).format("MMMM dd, yyyy"));
            $('#InvoiceDetails').fadeIn(500, null);

            var invList = document.getElementById("InvoiceAttachments");
            while (invList.hasChildNodes()) {
                invList.removeChild(invList.lastChild);
            }
            if (currentItem.get_fieldValues()["Attachments"] == true) {
                var attachmentFolder = web.getFolderByServerRelativeUrl("Lists/Invoice/Attachments/" + currentItem.get_id());
                var attachments = attachmentFolder.get_files();
                context.load(attachments);
                context.executeQueryAsync(function () {
                    // Enumerate and list the Invoice Attachments if they exist
                    var attachementEnumerator = attachments.getEnumerator();
                    while (attachementEnumerator.moveNext()) {
                        var attachment = attachementEnumerator.get_current();

                        var invDelete = document.createElement("span");
                        invDelete.appendChild(document.createTextNode("Delete"));
                        invDelete.className = "deleteButton";
                        invDelete.id = attachment.get_serverRelativeUrl();
                        //invDelete.onclick = function (sender) { deleteInvoiceAttachment(sender.target.id, itemID); };
                        $(invDelete).click(function (sender) {
                            deleteInvoiceAttachment(sender.target.id, itemID);
                        });
                        invList.appendChild(invDelete);
                        var invLink = document.createElement("a");
                        invLink.setAttribute("target", "_blank");
                        invLink.setAttribute("href", attachment.get_serverRelativeUrl());
                        invLink.appendChild(document.createTextNode(attachment.get_name()));
                        invList.appendChild(invLink);
                        invList.appendChild(document.createElement("br"));
                        invList.appendChild(document.createElement("br"));
                    }
                },
                function () {

                });
            }
        },
        function (sender, args) {
            var errArea = document.getElementById("errAllInvoices");
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

// This function updates an existing invoice's details
function saveEditInvoice() {
    currentItem.set_item("Customer", $('#editInvoiceCustomer').val());
    currentItem.set_item("PONumber", $('#editInvoicePO').val());
    currentItem.set_item("InvoiceNumber", $('#editInvoiceNumber').val());
    currentItem.set_item("InvoiceAmount", $('#editInvoiceAmount').val());
    currentItem.set_item("PaymentTerms", $('#editInvoiceTerms').val());
    currentItem.set_item("PaymentStatus", $('#editInvoiceStatus').val());
    currentItem.set_item("InvoiceDate", $('#editInvoiceDate').val());
    currentItem.update();
    context.load(currentItem);
    context.executeQueryAsync(function () {
        clearEditInvoiceForm();
        showInvoices();
    },
        function (sender, args) {
            var errArea = document.getElementById("errAllInvoices");
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

// This function cancels the editing of an existing invoice's details
function cancelEditInvoice() {
    clearEditInvoiceForm();
}

// This function deletes the selected invoice
function deleteEditInvoice() {
    var errArea = document.getElementById("errAllInvoices");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    currentItem.deleteObject();
    context.executeQueryAsync(
    function () {
        clearEditInvoiceForm();
        showInvoices();
    },
    function (sender, args) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode(args.get_message()));
        errArea.appendChild(divMessage);
    });
}

// This function clears the inputs on the edit invoice form
function clearEditInvoiceForm() {
    var errArea = document.getElementById("errAllInvoices");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#InvoiceDetails').fadeOut(500, function () {
        $('#InvoiceDetails').hide();
        $('#editInvoiceCustomer').val("");
        $('#editInvoicePO').val("");
        $('#editInvoiceNumber').val("");
        $('#editInvoiceAmount').val("");
        $('#editInvoiceTerms').val("");
        $('#editInvoiceStatus').val("");
        $('#editInvoiceDate').val("");
    });
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

// This function runs when the file input is used to uplaod a supporting document for a purchase order
function poAttach(event) {
    var errArea = document.getElementById("errAllPOs");
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
        return(false);
    }
    if (files.length > 0) {

        // Get the first file. In this case, only one file can be selected but because the file input could support
        // multi-file selection in some browsers we still need to access the file as the 0th member of the files collection
        file = files[0];

        // Wire up the HTML5 FileReader to read the selected file
        var reader = new FileReader();
        reader.onload = poFileOnload;
        reader.onerror = function (event) {
            var errArea = document.getElementById("errAllPOs");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Error reading file: " + event.target.error.code));
            errArea.appendChild(divMessage);
        };

        // Reading the file triggers the poFileOnLoad function that was wired up above
        reader.readAsArrayBuffer(file);
    }
    return false;
}

// This function runs when a file is successfully loaded and read by the PO file input.
// It references SP.RequestExecutor.js which will upload the file as an attachment by using the REST API.
// NOTE: This is safer and more capabale (in terms of file size) than using JSOM file creation for uploading files as attachments.
function poFileOnload(event) {
    contents = event.target.result;
    // The storePOAsAttachment function is called to do the actual work after we have a reference to SP.RequestExecutor.js
    $.getScript(web.get_url() + "/_layouts/15/SP.RequestExecutor.js", storePOAsAttachment);
}

// This function runs after we are sure we have a reference to SP.RequestExecutor.js.
// It uses the REST API to upload the file as an attachment 
function storePOAsAttachment() {
    var fileContents = fixBuffer(contents);
    var createitem = new SP.RequestExecutor(web.get_url());
    createitem.executeAsync({
        url: web.get_url() + "/_api/web/lists/GetByTitle('Purchase Order')/items(" + currentItem.get_id() + ")/AttachmentFiles/add(FileName='" + file.name + "')",
        method: "POST",
        binaryStringRequestBody: true,
        body: fileContents,
        success: storePOSuccess,
        error: storePOFailure,
        state: "Update"
    });
    function storePOSuccess(data) {

        // Success callback
        var errArea = document.getElementById("errAllPOs");

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
        $('#poUpload').replaceWith($('#poUpload').val('').clone(true));
        var poUpload = document.getElementById("poUpload");
        poUpload.addEventListener("change", poAttach, false);
        showPODetails(currentItem.get_id());
    }
    function storePOFailure(data) {

        // Failure callback
        var errArea = document.getElementById("errAllPOs");

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

// This function runs when the file input is used to uplaod a supporting document for an invoice
function invAttach(event) {
    var errArea = document.getElementById("errAllInvoices");
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
        reader.onload = invFileOnload;
        reader.onerror = function (event) {
            var errArea = document.getElementById("errAllInvoices");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Error reading file: " + event.target.error.code));
            errArea.appendChild(divMessage);
        };

        // Reading the file triggers the poFileOnLoad function that was wired up above
        reader.readAsArrayBuffer(file);
    }
    return false;
}

// This function runs when a file is successfully loaded and read by the PO file input.
// It references SP.RequestExecutor.js which will upload the file as an attachment by using the REST API.
// NOTE: This is safer and more capabale (in terms of file size) than using JSOM file creation for uploading files as attachments.
function invFileOnload(event) {
    contents = event.target.result;

    // The storeInvoiceAsAttachment function is called to do the actual work after we have a reference to SP.RequestExecutor.js
    $.getScript(web.get_url() + "/_layouts/15/SP.RequestExecutor.js", storeInvoiceAsAttachment);
}

// This function runs after we are sure we have a reference to SP.RequestExecutor.js.
// It uses the REST API to upload the file as an attachment
function storeInvoiceAsAttachment() {
    var fileContents = fixBuffer(contents);
    var createitem = new SP.RequestExecutor(web.get_url());
    createitem.executeAsync({
        url: web.get_url() + "/_api/web/lists/GetByTitle('Invoice')/items(" + currentItem.get_id() + ")/AttachmentFiles/add(FileName='" + file.name + "')",
        method: "POST",
        binaryStringRequestBody: true,
        body: fileContents,
        success: storeInvSuccess,
        error: storeInvFailure,
        state: "Update"
    });
    function storeInvSuccess(data) {

        // Success callback
        var errArea = document.getElementById("errAllInvoices");
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
        $('#invUpload').replaceWith($('#invUpload').val('').clone(true));
        var invUpload = document.getElementById("invUpload");
        invUpload.addEventListener("change", invAttach, false);
        showInvoiceDetails(currentItem.get_id());
    }
    function storeInvFailure(data) {


        // Failure callback
        var errArea = document.getElementById("errAllInvoices");
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

// This function deletes an attachment from a PO list item and then refreshes the PO form
function deletePOAttachment(url, itemID) {
    var attachment = web.getFileByServerRelativeUrl(url);
    attachment.deleteObject();
    showPODetails(itemID);
}

// This function deletes an attachment from an invoice list item  and then refreshes the invoice form
function deleteInvoiceAttachment(url, itemID) {
    var attachment = web.getFileByServerRelativeUrl(url);
    attachment.deleteObject();
    showInvoiceDetails(itemID);
}