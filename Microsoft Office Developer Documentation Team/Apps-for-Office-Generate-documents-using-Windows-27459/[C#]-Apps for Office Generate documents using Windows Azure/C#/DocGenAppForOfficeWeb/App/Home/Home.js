/// <reference path="../App.js" />

(function () {
    "use strict";
    var baseUrl;

    // The initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();
            baseUrl = getBaseURL();
            $('#open-doc-section-form').click(showDocSectionForm);
            $('#save-doc-section').click(saveDocSection);
            $('#populate-sample-data').click(populateSampleData);
            $('#cancel-save').click(cancelSave);

            populateCategoryCombo();
            generateSectionList();

        });
    };
    function getBaseURL() {
        var url = location.href;  // entire url including querystring - also: window.location.href;
        var baseURL = url.substring(0, url.indexOf('/', 14));


        if (baseURL.indexOf('http://localhost') != -1) {
            // Base Url for localhost
            var url = location.href;  // window.location.href;
            var pathname = location.pathname;  // window.location.pathname;
            var index1 = url.indexOf(pathname);
            var index2 = url.indexOf("/", index1 + 1);
            var baseLocalUrl = url.substr(0, index2);

            return baseLocalUrl + "/";
        }
        else {
            // Root Url for domain name
            return baseURL + "/";
        }

    }

    function showDocSectionForm() {
        $('#new-doc-section-form').show();
        $("#open-doc-section-form").hide();
        $("#doc-section-category").text = "";
        $("#doc-section-name").text = "";
    }

    function hideDocSectionForm() {
        $('#new-doc-section-form').hide();
        $("#open-doc-section-form").show();
        $("#doc-section-category").text = "";
        $("doc-section-name").text = "";
    }

    // Reads data from current document selection and displays a notification
    function saveDocSection() {
        var docSection;
        Office.context.document.getSelectedDataAsync(Office.CoercionType.Ooxml,
            function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                    var partitionKey = document.getElementById('doc-section-category').value;
                    var rowKey = document.getElementById('doc-section-name').value;
                    docSection = {
                        'PartitionKey': partitionKey,
                        'RowKey': rowKey,
                        'ContentOoxml': result.value
                    };
                    app.showNotification('Saving document section.', '');
                    Office.context.document.getSelectedDataAsync(Office.CoercionType.Html,
                        function (result) {
                            if (result.status === Office.AsyncResultStatus.Succeeded) {
                                docSection.ContentHtml = result.value;
                                app.showNotification('Saving document section.', '');
                                insertDocSection(docSection);
                            } else {
                                app.showNotification('Error:', result.error.message);
                            }
                        }
                        );
                } else {
                    app.showNotification('Error:', result.error.message);
                }
            }
        );
    }

    function insertDocSection(docSection) {
        $.ajax({
            url: baseUrl + "api/docsections",
            type: "Post",
            data: JSON.stringify(docSection),
            contentType: "application/json",
            success: function (data) {
                hideDocSectionForm();
                app.showNotification('Document section was saved.', '')
                generateSectionList();

            },
            error: function (msg) {
                app.showNotification('Error creating new doc section.', msg.statusText)
            }
        })
    }

    function retrieveDocSectionOOXML(category, docSectionName, callback) {
        $.ajax({
            url: baseUrl + "api/categories/" + category + "/docsections/" + docSectionName,
            type: "Get",
            success: function (data) {
                app.showNotification('Document section was retrieved.', '')
                callback(data);
            },
            error: function (msg) {
                app.showNotification('Error retrieving doc section.', msg.statusText)
            }
        })
    }

    function cancelSave() {
        hideDocSectionForm();
    }

    function populateCategoryCombo() {
        $.ajax({
            url: baseUrl + "api/categories",
            type: "Get",
            success: function (data) {
                for (var key in data) {
                    var option = '<option>' + data[key].RowKey + '</option>';
                    $(option).appendTo('#categories');
                }
            },
            error: function (msg) {
                app.showNotification('Error retrieving list of categories.', msg.statusText)
            }
        })
    }

    function populateSampleData() {
        $('#sections').empty();
        $('<p >Loading sample data <img src="/Images/loadingcirclests16.gif" style="vertical-align:middle" /></p>').appendTo('#sections');
        $.ajax({
            url: baseUrl + "api/sampledata",
            type: "Post",
            success: function (data) {
                app.showNotification("Sample Data populated", '')
                generateSectionList();
            },
            error: function (msg) {
                app.showNotification('Error creating sample data.', msg.statusText)
            }
        })
    }


    function generateSectionList() {
        $('<p>Loading sections <img src="/Images/loadingcirclests16.gif" style="vertical-align:middle" /></p>').appendTo('#sections');
        $.ajax({
            url: baseUrl + "api/docsections",
            type: "Get",
            success: function (data) {
                if (data.length > 0) {
                    $('#sections').empty();
                    $('<div id="accordion">').appendTo('#sections');
                    var currentCategory = "";
                    var currentCategoryID;
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].PartitionKey !== currentCategory) {
                            // new category
                            currentCategory = data[i].PartitionKey;
                            currentCategoryID = currentCategory.replace(/ /g, "-");
                            // generate accordion group header
                            $('<h3>' + currentCategory + '</h3>').appendTo('#accordion');
                            // generate div for sections of a category
                            $('<div id= \"' + currentCategoryID + '\">').appendTo('#accordion');
                            $('<table class="doc-section-content" id= \"' + currentCategoryID + 'Table\">').appendTo('#'+ currentCategoryID);
                        }
                        // accordion grouping
                        $('<tr><td class=\'doc-section\' data-category=\'' + currentCategory + '\'>' + data[i].RowKey + '</td><td class="delete-icon" data-category=\'' + currentCategory + '\' data-docsection = \'' + data[i].RowKey + '\'><img   src="/Images/delete-16.jpg" /></td></tr>').appendTo("#" + currentCategoryID + "Table");
                    }
                    $("#accordion").accordion();
                    $('.doc-section').click(insertInDocument);
                    $('.delete-icon').click(deleteDocSection);
                }
                else {
                    $('#sections').empty();
                    $('<p>There are no document sections, yet.</p><p>You can create new sections by selecting some content in your document and clicking the button to save it. You can also click the populate sample data button to see some examples.</p>').appendTo('#sections');
                }
            },
            error: function (msg) {
                $("<p><b>" + msg.statusText + "</b></p>").appendTo("#data")
            }
        });
    }

    function insertInDocument(event) {
        var category = event.currentTarget.attributes.getNamedItem('data-category').nodeValue;
        var docSectionName = event.currentTarget.innerText;
        var docSectionOoxml = retrieveDocSectionOOXML(category, docSectionName,
            function (docSection) {
                Office.context.document.setSelectedDataAsync(docSection.ContentOOXML, { coercionType: 'ooxml' },
                    function (result) {
                        if (result.status === Office.AsyncResultStatus.Succeeded) {
                            app.showNotification('Section inserted.', '');

                        } else {
                            app.showNotification('Error:', result.error.message);
                        }
                    }
                );
            });
    }

    function deleteDocSection(event) {
        var category = event.currentTarget.attributes.getNamedItem('data-category').nodeValue;
        var docSectionName = event.currentTarget.attributes.getNamedItem('data-docsection').nodeValue;
        $.ajax({
            url: baseUrl + "api/categories/" + category + "/docsections/" + docSectionName,
            type: "Delete",
            success: function (data) {
                app.showNotification('Document section was deleted.', '')
                generateSectionList();
            },
            error: function (msg) {
                app.showNotification('Error deleting doc section.', msg.statusText)
            }
        })

    }
})();