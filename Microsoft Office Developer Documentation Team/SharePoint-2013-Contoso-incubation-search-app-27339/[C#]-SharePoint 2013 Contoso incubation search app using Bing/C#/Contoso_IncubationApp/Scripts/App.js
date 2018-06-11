'use strict';

/*
* Developed by:    Martin Harwar, www.Point8020.com
* Developed for:   MSDN and SharePoint Product group
* First released:  14th February, 2014   
*/

var accountKey = ''; // ENTER YOUR BING API ACCOUNT KEY BETWEEN THE QUOTES
var accountKeyEncoded;
var context;
var web;
var user;
var hostUrl;
var hostSite;
var hostWeb;
var appUrl;
var listCollection;
var foundIdeas = false;
var foundTags = false;
var operation;
var lists;
var list;
var listItem;
var listItems;
var listItemUrl;
var itemID;
var numResultsToTag;
var currentTag = 0;
var ideasListURL;
var masterTagList = new Array();
var tagList;
var tagListItems;

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {
    accountKeyEncoded = base64_encode(":" + accountKey);
    jQuery.support.cors = true;
    checkConfiguration("");
});

// This function shows the Search UI
function showSearchPanel() {
    showSourceDetails();
    var hLink = document.createElement("A");
    $(hLink).attr("id", "ideasLink");
    $(hLink).attr("href", ideasListURL);
    $(hLink).append("<img src='../Images/ListIcon.png' alt='Ideas List' style='vertical-align:center;'/>&nbsp;Ideas List");
    $('#ctl00_PlaceHolderSiteName_onetidProjectPropertyTitle').empty();
    $('#ctl00_PlaceHolderSiteName_onetidProjectPropertyTitle').append(hLink);
    $('#configPanel').hide();
    $('#searchPanel').show();
    $('#tagsPanel').hide();
}

// This function shows the Tags UI for currently-tagged results
function showTagsPanel() {
    showSourceDetails();
    var hLink = document.createElement("A");
    $(hLink).attr("id", "ideasLink");
    $(hLink).attr("href", ideasListURL);
    $(hLink).append("<img src='../Images/ListIcon.png' alt='Ideas List' style='vertical-align:center;'/>&nbsp;Ideas List");
    $('#ctl00_PlaceHolderSiteName_onetidProjectPropertyTitle').empty();
    $('#ctl00_PlaceHolderSiteName_onetidProjectPropertyTitle').append(hLink);
    $('#configPanel').hide();
    $('#searchPanel').hide();
    $('#tagsPanel').fadeIn(500, null);
    itemID = decodeURIComponent(getQueryStringParameter("SPListItemId"));
    tagList = hostWeb.get_lists().getByTitle('Tags');
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Idea'/><Value Type='Number'>" + itemID + "</Value></Eq></Where></Query></View>");
    tagListItems = tagList.getItems(camlQuery);
    context.load(tagList);
    context.load(tagListItems);
    context.executeQueryAsync(
        function () {
            var listItemEnumerator = tagListItems.getEnumerator();
            var taggedResultsHtml = "";
            var tagCount = 0;
            while (listItemEnumerator.moveNext()) {
                tagCount++;
                var taggedResult = listItemEnumerator.get_current();
                var displayText = taggedResult.get_item('Title');
                var urlValue = taggedResult.get_item('Research');
                
                taggedResultsHtml += "<div style='float:none;'><img src='../Images/Delete.png' onclick='deleteTag(" + taggedResult.get_id() + ");' style='cursor:pointer;' title='Remove Tag'><a title='View' target='_blank' href='" + decodeURIComponent(urlValue) + "' style='margin-left:0px;'>" + decodeURIComponent(displayText) + "</a></div>";
                
            }
            if (tagCount == 0)
            {
                taggedResultsHtml = "This item has no tagged results.";
            }
            $('#taggedResults').html(taggedResultsHtml);

        },
        function (sender, args) {
            renderError(args.get_message());
        }
        );



}

// This function implements the main initiation logic for the app
// It uses the client-side object model to determine whether required lists exist in the host site
function checkConfiguration(err) {
    if (err == "") {
        hostUrl = decodeURIComponent(getQueryStringParameter("SPHostUrl"));
        appUrl = decodeURIComponent(getQueryStringParameter("SPAppWebUrl"));
        operation = getQueryStringParameter("Operation");
        context = SP.ClientContext.get_current();
        web = context.get_web()
        user = web.get_currentUser();
        hostSite = new SP.AppContextSite(context, hostUrl);
        hostWeb = hostSite.get_web();
        listCollection = hostWeb.get_lists();
        context.load(hostSite);
        context.load(hostWeb);
        context.load(listCollection)
        context.load(web);
        context.load(user);
        context.executeQueryAsync(
            function () {
                var listEnumerator = listCollection.getEnumerator();
                while (listEnumerator.moveNext()) {
                    var currentList = listEnumerator.get_current();
                    if (currentList.get_title() == "Ideas") {
                        foundIdeas = true;
                        ideasListURL = hostUrl + "/Lists/Ideas";
                    }
                    if (currentList.get_title() == "Tags") {
                        foundTags = true;
                    }
                }
                if (foundTags) {
                    masterTagList = new Array();
                    itemID = decodeURIComponent(getQueryStringParameter("SPListItemId"));
                    var masterTags = listCollection.getByTitle('Tags');
                    var camlQuery = new SP.CamlQuery();
                    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Idea'/><Value Type='Number'>" + itemID + "</Value></Eq></Where></Query></View>");
                    var masterTaglistItems = masterTags.getItems(camlQuery);
                    context.load(masterTags);
                    context.load(masterTaglistItems);
                    context.executeQueryAsync(
                        function () {
                            var tagEnumerator = masterTaglistItems.getEnumerator();
                            while (tagEnumerator.moveNext()) {
                                var taggedResult = tagEnumerator.get_current();
                                var urlValue = taggedResult.get_item('Research');
                                masterTagList.push(urlValue);
                            }
                        },
                        function (sender, args) {
                            renderError(args.get_message());
                        }
                        );
                }
                renderUI(foundIdeas, foundTags);
            },
            function () {
                renderError("Failed to get host site objects");
            }
            );
    }
    else
    {
        renderError(err);
    }
}

//This is a helper function used to determine some key values from the page URL
function getQueryStringParameter(paramToRetrieve) {
    var params = document.URL.split("?")[1].split("&");
    var strParams = "";
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split("=");
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1];
    }
}

// This function is a controller that determines which part of the page to render
function renderUI(ideasListExists, tagsListExists) {
    if (operation == "Search") {
        showSearchPanel();
    }
    else if (operation == "Tags") {
        showTagsPanel();
    }
    else {
        $('#configPanel').show();
        $('#searchPanel').hide();
        $('#tagsPanel').hide();
        if (ideasListExists && tagsListExists) {
            var hLink = document.createElement("A");
            $(hLink).attr("id", "ideasLink");
            $(hLink).attr("href", ideasListURL);
            $(hLink).append("<img src='../Images/ListIcon.png' alt='Ideas List' style='vertical-align:center;'/>&nbsp;Ideas List");
            $('#listsCheck').append(hLink);
        }
        else {
            createMissingLists();
        }
    }
}

// This is a helper function that manages the logic for creating missing lists if necessary.
// Note: the createLists() function does the actual work of creating lists
function createMissingLists() {
    if((!foundIdeas)&&(!foundTags))
    {
        createLists(true, true);
    }
    else if (!foundTags) {
        createLists(false, true);
    }
    else {
        createLists(true, false);
    }
}


// This function  does the actual work of creating lists
// Although it is relatively complex and verbose, this is to deal with the permutations of creating one, other or both of the required list.
// The complexity stems from the fact that the two lists are related by a SharePoint lookup field, which means that it's not just a case of
// simply creating a list if it's missing. Anyway, after this function has run, the two lists configured correctly will be present :-)
function createLists(ideas, tags) {
    if (ideas && tags) {
        var ideaListCreationInfo = new SP.ListCreationInformation();
        ideaListCreationInfo.set_title('Ideas');
        ideaListCreationInfo.set_templateType(SP.ListTemplateType.genericList);
        var ideasList = hostWeb.get_lists().add(ideaListCreationInfo);
        var ideasFields = ideasList.get_fields();
        var descriptionField = context.castTo(
                        ideasFields.addFieldAsXml('<Field Type="Text" DisplayName="Description" Name="Description" />',
                        true,
                        SP.AddFieldOptions.addToDefaultContentType),
                        SP.FieldText);
        var tagCountField = context.castTo(
                        ideasFields.addFieldAsXml('<Field Type="Number" DisplayName="TagCount" Name="TagCount" />',
                        true,
                        SP.AddFieldOptions.addToDefaultContentType),
                        SP.FieldNumber);
        var ideaItemInfo = new SP.ListItemCreationInformation();
        var ideaListItem = ideasList.addItem(ideaItemInfo);
        ideaListItem.set_item("Title", "Moon Dial");
        ideaListItem.set_item("Description", "A clock based on the principles of a sun dial, but one which works at night.");
        ideaListItem.set_item("TagCount", 0);
        ideaListItem.update();
        ideaListItem = ideasList.addItem(ideaItemInfo);
        ideaListItem.set_item("Title", "Perpetual Motion Engine");
        ideaListItem.set_item("Description", "A new breed of engine that requires no fuel. Note: A bit of magic might be required to start it up.");
        ideaListItem.set_item("TagCount", 0);
        ideaListItem.update();
        context.load(ideasList);
        context.executeQueryAsync(
            function () {
                var tagListCreationInfo = new SP.ListCreationInformation();
                tagListCreationInfo.set_title('Tags');
                tagListCreationInfo.set_templateType(SP.ListTemplateType.genericList);
                var tagsList = hostWeb.get_lists().add(tagListCreationInfo);
                tagsList.set_hidden(true);
                tagsList.update();
                var tagsFields = tagsList.get_fields();
                var ideaLookupField = context.castTo(
                       tagsFields.addFieldAsXml('<Field Type="Lookup" DisplayName="Idea" Name="Idea" />',
                       true,
                       SP.AddFieldOptions.addToDefaultContentType),
                       SP.FieldLookup);
                ideaLookupField.set_lookupList(ideasList.get_id());
                ideaLookupField.set_lookupField("ID");
                ideaLookupField.update();
                var researchField = context.castTo(
                                tagsFields.addFieldAsXml('<Field Type="Text" DisplayName="Research" Name="Research" />',
                                true,
                                SP.AddFieldOptions.addToDefaultContentType),
                                SP.FieldText);
                context.load(tagsList);
                context.executeQueryAsync(
                   function () {
                       checkConfiguration("");
                   },
                   function (sender, args) {
                       checkConfiguration(args.get_message());
                   }
                   );
            },
            function (sender, args) {
                checkConfiguration(args.get_message());
            }
        );
    }
    else if (tags) {
        var tagListCreationInfo = new SP.ListCreationInformation();
        tagListCreationInfo.set_title('Tags');
        tagListCreationInfo.set_templateType(SP.ListTemplateType.genericList);
        var tagsList = hostWeb.get_lists().add(tagListCreationInfo);
        tagsList.set_hidden(true);
        tagsList.update();
        var tagsFields = tagsList.get_fields();
        var ideaLookupField = context.castTo(
               tagsFields.addFieldAsXml('<Field Type="Lookup" DisplayName="Idea" Name="Idea" />',
               true,
               SP.AddFieldOptions.addToDefaultContentType),
               SP.FieldLookup);
        var ideasList = hostWeb.get_lists().getByTitle("Ideas");
        context.load(ideasList);
        context.executeQueryAsync(
            function () {
                ideaLookupField.set_lookupList(ideasList.get_id());
                ideaLookupField.set_lookupField("ID");
                ideaLookupField.update();
                var researchField = context.castTo(
                                tagsFields.addFieldAsXml('<Field Type="Text" DisplayName="Research" Name="Research" />',
                                true,
                                SP.AddFieldOptions.addToDefaultContentType),
                                SP.FieldText);
                context.load(tagsList);
                context.executeQueryAsync(
                   function () {
                       checkConfiguration("");
                   },
                   function (sender, args) {
                       checkConfiguration(args.get_message());
                   }
                   );
            },
            function (sender, args) {
                checkConfiguration(args.get_message());
            })
    }
    else {
        var ideaListCreationInfo = new SP.ListCreationInformation();
        ideaListCreationInfo.set_title('Ideas');
        ideaListCreationInfo.set_templateType(SP.ListTemplateType.genericList);
        var ideasList = hostWeb.get_lists().add(ideaListCreationInfo);
        var ideasFields = ideasList.get_fields();
        var descriptionField = context.castTo(
                        ideasFields.addFieldAsXml('<Field Type="Text" DisplayName="Description" Name="Description" />',
                        true,
                        SP.AddFieldOptions.addToDefaultContentType),
                        SP.FieldText);
        var tagCountField = context.castTo(
                        ideasFields.addFieldAsXml('<Field Type="Number" DisplayName="TagCount" Name="TagCount" />',
                        true,
                        SP.AddFieldOptions.addToDefaultContentType),
                        SP.FieldNumber);
        var ideaItemInfo = new SP.ListItemCreationInformation();
        var ideaListItem = ideasList.addItem(ideaItemInfo);
        ideaListItem.set_item("Title", "Moon Dial");
        ideaListItem.set_item("Description", "Works like a sun dial, but at night.");
        ideaListItem.set_item("TagCount", 0);
        ideaListItem.update();
        ideaListItem = ideasList.addItem(ideaItemInfo);
        ideaListItem.set_item("Title", "Perpetual Motion Engine");
        ideaListItem.set_item("Description", "A new breed of engine that requires no fuel. Note: A bit of magic might be required to start it up.");
        ideaListItem.set_item("TagCount", 0);
        ideaListItem.update();
        context.load(ideasList);
        context.executeQueryAsync(
            function () {
                checkConfiguration("");
            },
            function (sender, args) {
                checkConfiguration(args.get_message());
            }
        );

    }
}

// This function constructs search queries based on user actions in the app.
function searchNow() {
    var searchString = "";
    if ($('#useTitle').is(':checked')) {
        searchString += listItem.get_item("Title");
    }
    if ($('#useDescription').is(':checked')) {
        searchString += " " + listItem.get_item("Description");
    }
    if ($('#additionalTerms').val()!="") {
    }
    searchString += " " + $('#additionalTerms').val();
    
    //check for empty or whitespace string
    if (/\S/.test(searchString)) {
        //make string safe. (Not 100% necessary for Bing)
        searchString = ($('<div/>').text(searchString).html());
        searchString = searchString.replace("'", " ");
        if ($('#searchSharePoint').is(':checked')) {
            $('#sharPointSearchResults').text();
            $('#sharePointResultPanel').show();

            searchSharePoint(searchString);
        }
        else {
            $('#sharPointSearchResults').text();
            $('#sharePointResultPanel').hide();
        }
        if ($('#searchBing').is(':checked')) {
            $('#bingSearchResults').text();
            $('#bingResultPanel').show();
            //bingResultPanel
            searchBing(searchString);
        }
        else {
            $('#bingSearchResults').text();
            $('#bingResultPanel').hide();
        }

        if (($('#searchSharePoint').is(':checked')) ||($('#searchBing').is(':checked'))) {
            $('#searchOptionsPanel').slideUp(250, function () {
                $('#searchResultsPanel').slideDown(250, null);
            })
        }
        else{
            renderError("At least one content source is required!");
        }
    }
    else
    {
        renderError("At least one search term is required!");
    }
   
}

// This function searches Bing via the Bing Search API on the Windows Azure Marketplace.
function searchBing(queryText) {
    var bingURL = "https://api.datamarket.azure.com/Data.ashx/Bing/Search/v1/Web?Query=%27" + queryText + "%27&$top=10&$format=json";
    var bingSearchResultsHtml = "";
    $.ajax({
        url: bingURL,
        beforeSend: setHeader,
        context: this,
        type: 'GET',
        success: function (data, status) {
            var results = data.d.results;
            for (var currentResult = 0; currentResult < results.length; currentResult++) {
                if (results[currentResult].Title.length > 53)
                {
                    results[currentResult].Title = results[currentResult].Title.substr(0, 50) + "...";
                }
                bingSearchResultsHtml += "<div class='checkBoxSmarterCheck'><input type='checkbox' display='" + results[currentResult].Title + "' data='" + results[currentResult].Url + "' value='1' id='bingResult" + currentResult + "' name='' /><label for='bingResult" + currentResult + "'></label><label for='bingResult" + currentResult + "' class='checkboxLabel' style='border:1px solid #EEEEEE;background:none;width:500px;max-width:500px;min-width:500px;text-overflow:ellipsis;padding-top:0px;'>" + results[currentResult].Title + "<a href='" + results[currentResult].Url + "' style='float:right;' target='_blank'><img src='../Images/GlobeIcon.png' alt='View'></a></label></div>";
            }
            $('#bingSearchResults').html(bingSearchResultsHtml);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            renderError(textStatus);
        }
    });
}

//This function issues a search to SharePoint via its REST API.
function searchSharePoint(queryText) {
    var searchUrl = appUrl + "/_api/search/query?querytext='" + queryText + "'";
    var executor = new SP.RequestExecutor(appUrl);
    executor.executeAsync(
        {
            url: searchUrl,
            method: "GET",
            headers: { "Accept": "application/json; odata=verbose" },
            success: sharePointSearchSuccess,
            error: sharePointSearchFailure
        }
    );
}

//This is the ”success” callback from the REST API call. It builds the SharePoint results panel.
function sharePointSearchSuccess(data) {
    var jsonObject = JSON.parse(data.body);
    var results = jsonObject.d.query.PrimaryQueryResult.RelevantResults.Table.Rows.results;
    if (results.length == 0) {
        $('#sharPointSearchResults').text('No results were found');
    }
    else {
        var searchResultsHtml = '';
        $.each(results, function (index, result) {
            if (result.Cells.results[3].Value.length > 53) {
                result.Cells.results[3].Value = result.Cells.results[3].Value.substr(0, 50) + "...";
            }
            searchResultsHtml += "<div class='checkBoxSmarterCheck'><input type='checkbox' value='1' display='" + result.Cells.results[3].Value + "' data='" + result.Cells.results[6].Value + "' id='spResult" + index + "' name='' /><label for='spResult" + index + "'></label><label for='spResult" + index + "' class='checkboxLabel' style='border:1px solid #EEEEEE;background:none;width:500px;padding-top:0px;'>" + result.Cells.results[3].Value + "<a href='" + result.Cells.results[6].Value + "' style='float:right;' target='_blank'><img src='../Images/GlobeIcon.png' alt='View'></a></label></div>";
        });
        $('#sharPointSearchResults').html(searchResultsHtml);
    }
}

//This is the “failure” callback from the REST API call. It renders an error message.
function sharePointSearchFailure(data, errorCode, errorMessage) {
    $('#sharPointSearchResults').text('An error occurred while searching SharePoint - ' + errorMessage);
}

// This function renders a modern-style pop-up
function renderError(message) {
    $('#errorMessage').text(message);
    removeStyle();
    $('#errorOK').show();
    $('#overlaydiv').fadeIn(500, null);
    $('#errorUI').addClass('errorDlg');
    $('#errorUI').fadeIn(100, null);

}

// This function switches the UI back to the Search panel
function refineSearch() {
    $('#tagsPanel').fadeOut(250, function () {
        $('#searchPanel').fadeIn(250, null);
    })
    $('#searchResultsPanel').slideUp(250, function () {
        $('#searchOptionsPanel').slideDown(250, null);
    })
}

// This helper function sets an HTTP header for the Bing Search API request
function setHeader(xhr) {
    xhr.setRequestHeader('Authorization', "Basic " + accountKeyEncoded);
}

// Your Bing API key needs to be encoded to Base64. That's what this helper function does!
function base64_encode(data) {
    var b64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var o1, o2, o3, h1, h2, h3, h4, bits, i = 0,
      ac = 0,
      enc = "",
      tmp_arr = [];

    if (!data) {
        return data;
    }

    do { // pack three octets into four hexets
        o1 = data.charCodeAt(i++);
        o2 = data.charCodeAt(i++);
        o3 = data.charCodeAt(i++);

        bits = o1 << 16 | o2 << 8 | o3;

        h1 = bits >> 18 & 0x3f;
        h2 = bits >> 12 & 0x3f;
        h3 = bits >> 6 & 0x3f;
        h4 = bits & 0x3f;

        // use hexets to index into b64, and append result to encoded string
        tmp_arr[ac++] = b64.charAt(h1) + b64.charAt(h2) + b64.charAt(h3) + b64.charAt(h4);
    } while (i < data.length);

    enc = tmp_arr.join('');

    var r = data.length % 3;

    return (r ? enc.slice(0, r - 3) : enc) + '==='.slice(r || 3);

}

// This function interrogates the UI to determine what results the user has selected for tagging
function tagResults() {
    var selectedResults = new Array();
    var selectedTitles = new Array();
    $('#sharPointSearchResults input:checked').each(function () {
        selectedResults.push($(this).attr('data'));
        selectedTitles.push($(this).attr('display'));
    });
    $('#bingSearchResults input:checked').each(function () {
        selectedResults.push($(this).attr('data'));
        selectedTitles.push($(this).attr('display'));
    });
    numResultsToTag = selectedResults.length;
    if (numResultsToTag > 0) {
        currentTag = 0;
        for (var taggedResult = 0; taggedResult < selectedResults.length; taggedResult++) {
            storeTaggedResult(selectedResults[taggedResult], selectedTitles[taggedResult]);
        }
    }
    else {
        renderError("You must select results before attempting to use them as research tags");
    }
}

// This function does the actual work of tagging an item with a result by using the 
// SharePoint client-side object model to add items to the Tags list.
function storeTaggedResult(urlToTag, displayToTag) {
    var tagExists = false;
    for (var iCheck = 0; iCheck < masterTagList.length; iCheck++) {
        if (encodeURIComponent(urlToTag)==masterTagList[iCheck]) {
            tagExists = true;
            break;
        }
    }
    if (!tagExists) {
        masterTagList.push(encodeURIComponent(urlToTag));
        var tagList = hostWeb.get_lists().getByTitle("Tags");
        var tagItemInfo = new SP.ListItemCreationInformation();
        var tagItemInfo = tagList.addItem(tagItemInfo);
        tagItemInfo.set_item("Idea", itemID);
        tagItemInfo.set_item("Research", encodeURIComponent(urlToTag));
        tagItemInfo.set_item("Title", encodeURIComponent(displayToTag));
        tagItemInfo.update();
        context.load(tagList);
        context.executeQueryAsync(
            function () {
                currentTag++;
                if (currentTag == numResultsToTag) {
                    showTagsPanel();
                    updateTagCount();
                }
            },
            function (sender, args) { renderError(args.get_message()); }
            );
    }
    else
    {
        currentTag++;
        if (currentTag == numResultsToTag) {
            showTagsPanel();
            updateTagCount();
        }
    }
}

// This function uses the SharePoint client-side object model to update the TagCount field for an item in the Ideas list
function updateTagCount() {
    var uIitemID = decodeURIComponent(getQueryStringParameter("SPListItemId"));
    var uTagList = hostWeb.get_lists().getByTitle('Tags');
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Idea'/><Value Type='Number'>" + uIitemID + "</Value></Eq></Where></Query></View>");
    var uTagListItems = uTagList.getItems(camlQuery);
    context.load(hostWeb);
    context.load(uTagList);
    context.load(uTagListItems);
    context.executeQueryAsync(
        function () {
            var currentCount = uTagListItems.get_count();
            var sourceItem = hostWeb.get_lists().getByTitle("Ideas").getItemById(uIitemID);
            sourceItem.set_item("TagCount", currentCount);
            sourceItem.update();
            context.executeQueryAsync(
                null,
                function (sender, args) {
                    renderError(args.get_message());
                }
                );

        },
        function (sender, args) {
            renderError(args.get_message());
        }
        );
}

// This function uses the SharePoint client-side object model to retrieve the metadata from the Ideas list that can be used as
// inputs into the search (if chosen by the user). The values are also displayed in two of the UI panels so that the user can easily
// remember which Idea they used to start this whole process.
function showSourceDetails()
{
    var listID = decodeURIComponent(getQueryStringParameter("SPListId"));
    itemID = decodeURIComponent(getQueryStringParameter("SPListItemId"));
    lists = hostWeb.get_lists();
    list = lists.getById(listID);
    listItem = list.getItemById(itemID);
    context.load(lists);
    context.load(list);
    context.load(listItem);
    context.executeQueryAsync(
        function () {
            if (list.get_title() == "Ideas") {
                $('#sourceItemTitle').text(listItem.get_item("Title"));
                $('#sourceItemDescription').text(listItem.get_item("Description"));
                $('#taggedItemTitle').text(listItem.get_item("Title"));
                $('#taggedItemDescription').text(listItem.get_item("Description"));
            }
            else {
                $('#configPanel').show();
                $('#searchPanel').hide();
                $('#tagsPanel').hide();
                operation = "";
            }
        },
        function (sender, args) {
            renderError(args.get_message());
            $('#searchPanel').show();
        }
        );
}

// This function manipulates styles for the modern-style popup
function removeStyle() {
    $('#errorUI').removeClass('errorDlg');
}

// This function closes the 'error' popup
function hideError() {
    $('#overlaydiv').fadeOut(200, null);
    $('#errorUI').fadeOut(200, null);


}

// This function closes the 'delete confirmation' popup
function cancelDeleteTag() {
    $('#confirmOverlay').fadeOut(200, null);
    $('#deleteUI').fadeOut(200, null);


}

// This function shows a confirmation dialog for deleting a tagged result. 
function deleteTag(itemID)
{
    var itm = tagList.getItemById(itemID);
    context.load(itm);
    context.executeQueryAsync(
        function () {
            var valToRemove = itm.get_item('Research');
            $('#tagUrl').val(valToRemove);
        },
        null
        );
    $('#deleteMessage').text("Are you sure you want to remove this tag?");
    $('#tagToDelete').val(itemID);
    removeStyle();
    $('#deleteYes').show();
    $('#deleteNo').show();
    $('#confirmOverlay').fadeIn(500, null);
    $('#deleteUI').addClass('errorDlg');
    $('#deleteUI').fadeIn(100, null);
}

// This function uses the SharePoint client-side object model to delete a tagged result 
function yesDeleteTag() {
    var itemID = $('#tagToDelete').val();
    var tagUrl = $('#tagUrl').val();
    var iPos = 0;
    for (iPos = 0; iPos < masterTagList.length; iPos++){
        if(masterTagList[iPos] == tagUrl)
        {
            break;
        }
    }
    masterTagList.splice(iPos, 1);
    var tagList = hostWeb.get_lists().getByTitle("Tags");
    var itemToDelete = tagList.getItemById(itemID);
    itemToDelete.deleteObject();
    updateTagCount();
    context.executeQueryAsync(
       function () {
           showTagsPanel();
           $('#confirmOverlay').fadeOut(200, null);
           $('#deleteUI').fadeOut(200, null);
       },
       function (sender, args) { renderError(args.get_message()); }
       );
}

//This functions enables the user to press [Enter] when they are in the Search UI to actually run a search
function enterKeyPressed(e)
{
    var eve = e || window.event;
    var keycode = eve.keyCode || eve.which || eve.charCode;
    if (keycode == 13) {
        eve.cancelBubble = true;
        eve.returnValue = false;

        if (eve.stopPropagation) {
            eve.stopPropagation();
            eve.preventDefault();
        }
        searchNow();
        return false;
    }
}

