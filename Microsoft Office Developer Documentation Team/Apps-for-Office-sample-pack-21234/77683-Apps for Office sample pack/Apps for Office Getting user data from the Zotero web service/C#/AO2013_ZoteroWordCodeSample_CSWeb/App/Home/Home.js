(function () {
    "use strict";

     //The initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
    };

    $(document).ready(function () {
        var cookieValues = Utilities.getCookieValues("zoteroAccessCookie");
        var userName = cookieValues.userName;
        $("#userNameSpan").text(userName);
        $("#AddBooksButton").click(addBooksButton_click);
        $("#RefreshButton").click(refreshButton_click);
        $("#StyleDropDown").change(refreshButton_click);
        $("#logoutLink").click(logoutLink_click);
        app.initialize();
        getAllItems(cookieValues.userId, cookieValues.accessToken);
    });
})();

//Global variable to hold the returned result of all books
var currentResults;
var userName;

//Presentation

function fillInBookList(books) {
    var bookList = $("#BooksList");
    for (var key in books) {
        var book = books[key]
        var listItem = $("<li>").attr("key", key);
        listItem.addClass("entry");

        var titleSpan = $("<span>").addClass("entryTitle").text(book.title);
        listItem.append(titleSpan);

        var checkBox = $("<input>").attr("type", "checkbox");
        checkBox.addClass("entryCheckBox");
        listItem.append(checkBox);

        listItem.append("<br/>");

        var authorAndYearSpan = $("<span>").addClass("entryAuthorAndYear")
            .text(book.author + ", " + book.year);
        listItem.append(authorAndYearSpan);

        var insertButton = $("<button>").attr("type", "button");
        insertButton.addClass("entryInsertButton");
        insertButton.click(insertSingleButton_click);
        listItem.append(insertButton);

        bookList.append(listItem);
    }
    $("#LoadingGif").hide();
}

//Word interaction

function insertCitation(citationHtml) {
    citationHtml = Utilities.replaceDivsWithParagraphs(citationHtml);
    Office.context.document.setSelectedDataAsync(citationHtml, { coercionType: "html" });
}

function insertSelectedCitations() {
    var citations = [];
    var booksList = $("#BooksList");
    var booksListItems = booksList.children("li");

    for (var i = 0; i < booksListItems.length; i++) {
        var listItem = $(booksListItems[i]);
        if (listItem.find("input:checkbox").is(":checked")){
            var itemKey = listItem.attr("key");
            var citation = currentResults[itemKey].citation;

            //Word has trouble handling multiple div tags, so we're stripping down 
            //to just the html we need. This will allow Word to insert using the
			//default 'Normal' style.
            var div = $("<div>").html(citation);
            var text = div[0].children[0].children[0].innerHTML;
            citations.push(text);
        }
    }

    var selectedCitations = "<p>";

    for (var j = 0; j < citations.length; j++) {
        selectedCitations += citations[j];
        if (j < citations.length - 1) selectedCitations += "<br /><br />";
    }
    selectedCitations += "</p>";

    Office.context.document.setSelectedDataAsync(selectedCitations, { coercionType: "html" });
}

//Event handlers

function logoutLink_click(e) {
    var cookie = $.removeCookie("zoteroAccessCookie", { path: '/' });
    document.location.reload(true);
}

function insertSingleButton_click(e) {
    var key = $(e.target).parent("li").attr("key");
    insertCitation(currentResults[key].citation);
}

function refreshButton_click(e) {
    var bookList = $("#BooksList");
    bookList.empty();
    $("#LoadingGif").show();
    var cookieValues = Utilities.getCookieValues("zoteroAccessCookie");
    var style = $("#StyleDropDown").val();
    getAllItems(cookieValues.userId, cookieValues.accessToken,0,style);
}

function addBooksButton_click(e) {
    if (currentResults !== undefined) {
        insertSelectedCitations();
    }
}

//Api Calls

function getAllItems(userId, accessToken, limit, style) {
    var fullUrl = "../../Service/ZoteroApiCaller.svc/GetAllItems?userId=" + userId + "&accessToken=" + accessToken;
    if (limit !== undefined && limit > 0) {
        fullUrl += "&limit=" + limit.toString();
    }
    if (style !== undefined && style !== "") {
        fullUrl += "&style=" + style;
    }

    $.ajax({
        type: "get",
        url: fullUrl,
        success: getAllData_success,
        error: handleAjaxError
    });
}

//Data layer

function getAllData_success(data) {
    var entries = $(data).find("entry");
    var apiResults = {};
    for (var i = 0; i < entries.length; i++) {
        var entry = $(entries[i]);
        var zoteroItem = {};
        zoteroItem.title = entry.find("title").text();
        var author = entry.find("zapi\\:creatorSummary");

        //Some entries don't have the creatorSummary element, if this is the case
        //"author" becomes the title for sorting purposes.
        if (author.text() === "") {
            zoteroItem.author = "N/A";
        }
        else {
            zoteroItem.author = author.text();
        }
        
        zoteroItem.key = entry.find("zapi\\:key").text();
        zoteroItem.published = entry.find("published").text();
        zoteroItem.citation = entry.find("content").html();
        zoteroItem.year = entry.find("zapi\\:year").text();
        apiResults[zoteroItem.key] = zoteroItem;
    }
    currentResults = apiResults;

    //Finally call to the presentation method to display the books
    fillInBookList(currentResults);
};

//Error handling

function handleAjaxError(a, b, c) {
    app.showNotification(b, c);
}

//Utility functions
var Utilities = {
    getCookieValues: function (cookieName) {
        // get and verify cookie
        var cookie = $.cookie(cookieName);
        if (cookie === undefined || cookie.length < 3) {
            return null;
        }
        // cookie stored key-value pairs in querystring format
        return Utilities.parseParams(cookie);
    },
    parseParams: function (paramString) {
        var values = {};
        var keyValPairs = paramString.split("&");
        keyValPairs.forEach(function (val, idx, ar) {
            var keyval = val.split("=");
            values[keyval[0]] = keyval[1];
        });
        return values;
    },
    replaceDivsWithParagraphs: function (htmlString) {
        htmlString = htmlString.replace(/<div/g, "<p");
        htmlString = htmlString.replace(/<\/div/g, "</p");
        return htmlString;
    }
};

