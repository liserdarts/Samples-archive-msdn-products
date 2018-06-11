'use strict';

// The following variable hold SharePoint objects and data
var context;
var appWeb;
var playlistList;
var playlistItemsList;
var currentPlaylist;
var videoStorageList;
var playlistItem;
var errArea;
var currentPlayPosition;
var playOrder;


// The following function applys some jQuery to allow drag and drop in the named lists
$(function () {
    $("#videosInPlaylist, #availableVideos").sortable({
        connectWith: ".connectedSortable"
    }).disableSelection();
});

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {
    showPlaylists();
});

// This function renders all current playlists, with a link to edit them and a link to play them
function showPlaylists() {
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    errArea = document.getElementById("ErrorArea");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Reference and load the SharePoint Objects that we'll need throughout the app
    context = SP.ClientContext.get_current();
    appWeb = context.get_web();
    playlistList = appWeb.get_lists().getByTitle('Playlist');
    playlistItemsList = appWeb.get_lists().getByTitle('PlaylistItems');
    videoStorageList = appWeb.get_lists().getByTitle('Videos');
    context.load(appWeb);
    context.load(playlistList);
    context.load(playlistItemsList);
    context.load(videoStorageList);
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var playlists = playlistList.getItems(camlQuery);
    context.load(playlists);
    context.executeQueryAsync(

        // Success callback after getting the SharePoint objects.
        function () {
            // Success returned from executeQueryAsync
            // We now want to list all saved playlists.
            // Remove all nodes from the playlists <DIV> so we have a clean space to write to
            var playlistTable = document.getElementById("Playlists");
            while (playlistTable.hasChildNodes()) {
                playlistTable.removeChild(playlistTable.lastChild);
            }

            // Iterate through the Playlists list
            var hasPlaylists = false;
            var listItemEnumerator = playlists.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var playlistEdit = document.createElement("div");
                var playlistPlay = document.createElement("div");
                var playlist = document.createElement("div");
                playlistEdit.className = "edit";
                playlistPlay.className = "play";
                playlistEdit.appendChild(document.createTextNode('\u00A0'));
                playlistPlay.appendChild(document.createTextNode('\u00A0'));
                playlistEdit.title = "Edit";
                playlistPlay.title = "Play";
                var playlistLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                playlist.appendChild(playlistLabel);
                playlistEdit.id = "Edit" + listItem.get_id();
                $(playlistEdit).click(function (sender) {
                    showPlaylistDetails(sender.target.id);
                });
                playlistPlay.id = "Play" + listItem.get_id();
                $(playlistPlay).click(function (sender) {
                    playNow(sender.target.id);
                });

                // Add the playlist div and its Play & Edit buttons to the UI
                playlistTable.appendChild(playlistPlay);
                playlistTable.appendChild(playlistEdit);
                playlistTable.appendChild(playlist);
                hasPlaylists = true;
            }
            if (!hasPlaylists) {
                var noPlaylists = document.createElement("div");
                noPlaylists.appendChild(document.createTextNode("There are currently no playlists."));
                playlistTable.appendChild(noPlaylists);
            }
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get playlists. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function hides all main DIV elements. The caller is then responsible 
// for re-showing the one that needs to be displayed.
function hideAllPanels() {
    $('#AddPlaylist').hide();
    $('#PlaylistDetails').hide();
    $('#PlaylistPlayer').hide();
    $('#PlaylistPlayer').empty();
   
}

// This function shows the form for adding a new playlist
function addNewPlaylist() {
    hideAllPanels();
    $('#newPlaylistName').val('');
    $('#AddPlaylist').fadeIn(500, null);
}

// This function shows the available videos and the assigned videos for the selected playlist.
// Note that the listVideos() function does the actual population
function showPlaylistDetails(itemID) {
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    hideAllPanels();
    itemID = itemID.replace("Edit", "");
    currentPlaylist = playlistList.getItemById(itemID);
    context.load(currentPlaylist);
    context.executeQueryAsync(
        function () {
            $('#editPlaylistName').val(currentPlaylist.get_fieldValues()["Title"]);
            $('#PlaylistDetails').fadeIn(500, function () {
                listVideos();
            });
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });

}

// This function starts the play process for the selected playlist
function playNow(itemID) {
    itemID = itemID.replace("Play", "");
    hideAllPanels();
    $('#PlaylistPlayer').fadeIn(1000, null);
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='PlaylistID' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where><OrderBy><FieldRef Name='PlayOrder' /></OrderBy></Query></View>");
    var videos = playlistItemsList.getItems(camlQuery);
    context.load(videos);
    context.executeQueryAsync(function () {

        // Iterate through the video list
        var listItemEnumerator = videos.getEnumerator();
        currentPlayPosition = 0;
        playOrder = [];
        while (listItemEnumerator.moveNext()) {
            var videoItem = listItemEnumerator.get_current();
            playOrder.push(videoItem.get_fieldValues()["RelativeURL"]);
        }
        playVid(currentPlayPosition);
    },
    function (sender, args) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode(args.get_message()));
        errArea.appendChild(divMessage);
    });
}

// This function plays a video by its index number in a playlist.
// It also prepares the player to play the next one when the current video has ended.
function playVid(position) {
    if (position < playOrder.length) {
        $('#PlaylistPlayer').empty();
        var url = playOrder[position];
        var playerControl = document.getElementById("PlaylistPlayer");
        var player = document.createElement("video");
        player.setAttribute("src", url);
        player.setAttribute("width", "800");
        player.setAttribute("height", "600");
        player.setAttribute("controls", "controls");
        if (!player.addEventListener) {
            player.attachEvent("ended", function () {
                playVid(position + 1);
            });
        }
        else {
            player.addEventListener("ended", function () {
                playVid(position + 1);
            }, false);
        }
        playerControl.appendChild(player);
        player.play();
    }
}

// This function saves a new (empty) playlist.
function saveNewPlaylist() {
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if ($('#newPlaylistName').val() == "") {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Playlist Name is required."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var listItem = playlistList.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#newPlaylistName').val());
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            showPlaylists();
            $('#AddPlaylist').fadeOut(500, null);
        },
            function (sender, args) {
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode(args.get_message()));
                errArea.appendChild(divMessage);
            });
    }
}

// This function cancels the creation of a new playlist
function cancelNewPlaylist() {
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors in subsequent operations
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddPlaylist').fadeOut(500, null);
}

// This function updates the name of the selected playlist and assigns the chosen videos to it
function saveEditPlaylist() {
    if ($('#editPlaylistName').val() == "") {
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Playlist Name is required."));
        errArea.appendChild(divMessage);
    }
    else {
        currentPlaylist.set_item("Title", $('#editPlaylistName').val());
        currentPlaylist.update();
        context.load(currentPlaylist);
        var playlistID = currentPlaylist.get_id();

        //Clear playlist items first of all
        var playlistItemsQuery = new SP.CamlQuery();
        playlistItemsQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='PlaylistID' LookupId='TRUE' /><Value Type='Lookup'>"
            + playlistID
            + "</Value></Eq></Where></Query></View>");
        var playlistContents = playlistItemsList.getItems(playlistItemsQuery);
        context.load(playlistContents);
        context.executeQueryAsync(
            function () {
                var listItemEnumerator = playlistContents.getEnumerator();
                while (listItemEnumerator.moveNext()) {
                    var listItem = listItemEnumerator.get_current();
                    var listItemToDelete = playlistItemsList.getItemById(listItem.get_id());
                    listItemToDelete.deleteObject();
                }
                context.executeQueryAsync(
                    function () {

                        // Build playlist data from the UI elements
                        var assignedVideos = document.getElementById("videosInPlaylist");
                        for (var assignedVid = 0; assignedVid < assignedVideos.childNodes.length; assignedVid++) {
                            var path = "../Lists/Videos/" + assignedVideos.childNodes[assignedVid].innerHTML;
                            var itemCreateInfo = new SP.ListItemCreationInformation();
                            var newplaylistItemsList = appWeb.get_lists().getByTitle('PlaylistItems');
                            var newItem = newplaylistItemsList.addItem(itemCreateInfo);
                            newItem.set_item("RelativeURL", path);
                            newItem.set_item("PlayOrder", assignedVid + 1);
                            newItem.set_item("PlaylistID", playlistID);
                            newItem.update();
                            context.load(newItem);
                        }
                        context.executeQueryAsync(
                            function () {
                                $('#PlaylistDetails').fadeOut(500, function () {
                                    $('#editPlaylistName').val('');
                                    $('#videosInPlaylist').empty();
                                    $('#availableVideos').empty();
                                    $('#videosInPlaylist').append('Loading...');
                                    $('#availableVideos').append('Loading...');
                                    showPlaylists();
                                });
                            },
                            function (sender, args) {
                                while (errArea.hasChildNodes()) {
                                    errArea.removeChild(errArea.lastChild);
                                }
                                var divMessage = document.createElement("DIV");
                                divMessage.setAttribute("style", "padding:5px;");
                                divMessage.appendChild(document.createTextNode(args.get_message()));
                                errArea.appendChild(divMessage);
                            });
                    },
                   function (sender, args) {
                       while (errArea.hasChildNodes()) {
                           errArea.removeChild(errArea.lastChild);
                       }
                       var divMessage = document.createElement("DIV");
                       divMessage.setAttribute("style", "padding:5px;");
                       divMessage.appendChild(document.createTextNode(args.get_message()));
                       errArea.appendChild(divMessage);
                   });
            },
            function (sender, args) {
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

// This function cancels the editing of the selected playlist
function cancelEditPlaylist() {
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors in subsequent operations
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#PlaylistDetails').fadeOut(500, function () {
        $('#editPlaylistName').val('');
        $('#videosInPlaylist').empty();
        $('#availableVideos').empty();
        $('#videosInPlaylist').append('Loading...');
        $('#availableVideos').append('Loading...');
    });
}

// This function deletes the playlist items associated with the selected playlist, and then deletes the playlist itself
function deleteEditPlaylist() {

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var playlistItemsQuery = new SP.CamlQuery();
    playlistItemsQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='PlaylistID' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentPlaylist.get_id()
        + "</Value></Eq></Where></Query></View>");
    var playlistContents = playlistItemsList.getItems(playlistItemsQuery);
    context.load(playlistContents);
    context.executeQueryAsync(
        function () {
            var listItemEnumerator = playlistContents.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var listItemToDelete = playlistItemsList.getItemById(listItem.get_id());
                listItemToDelete.deleteObject();
            }
            currentPlaylist.deleteObject();
            context.executeQueryAsync(
                function () {
                    $('#PlaylistDetails').fadeOut(500, function () {
                        $('#editPlaylistName').val('');
                        $('#videosInPlaylist').empty();
                        $('#availableVideos').empty();
                        $('#videosInPlaylist').append('Loading...');
                        $('#availableVideos').append('Loading...');
                        showPlaylists();
                    });
                },
                function (sender, args) {
                    var divMessage = document.createElement("DIV");
                    divMessage.setAttribute("style", "padding:5px;");
                    divMessage.appendChild(document.createTextNode(args.get_message()));
                    errArea.appendChild(divMessage);
                });
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to check for playlist items associated with this playlist. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function populates the assigned video list for a selected playlist. It also calls the listAvailableVideos function
// and passes in an array of the assigned videos, so those videos won't be rendered in the available videos list
function listVideos() {
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='PlaylistID' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentPlaylist.get_id()
        + "</Value></Eq></Where><OrderBy><FieldRef Name='PlayOrder' /></OrderBy></Query></View>");
    var videos = playlistItemsList.getItems(camlQuery);
    context.load(videos);
    context.executeQueryAsync(function () {
        var videoTable = document.getElementById("videosInPlaylist");
        while (videoTable.hasChildNodes()) {
            videoTable.removeChild(videoTable.lastChild);
        }

        // Iterate through the video list
        var excludeList = [];
        var listItemEnumerator = videos.getEnumerator();
        while (listItemEnumerator.moveNext()) {
            var videoItem = listItemEnumerator.get_current();
            var video = document.createElement("div");
            video.className = "ui-state-highlight";
            var fileName = videoItem.get_fieldValues()["RelativeURL"].toString();
            excludeList.push(fileName);
            var iPosSlash = fileName.lastIndexOf("/");
            var fileName = fileName.substring(iPosSlash + 1);
            var videoLabel = document.createTextNode(fileName);
            video.appendChild(videoLabel);
            videoTable.appendChild(video);
            
        }

        // Call the listAvailableVideos function and pass in an array of the assigned videos, 
        // so those videos won't be rendered in the available videos list
        listAvailableVideos(excludeList);
    },
    function (sender, args) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode(args.get_message()));
        errArea.appendChild(divMessage);
    });
}

// This function populates the available video list for a selected playlist. The parameter passed in is an array of currently-assigned
// videos, so those videos won't be rendered in the available videos list
function listAvailableVideos(excludeList) {
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var videos = videoStorageList.getItems(camlQuery);
    context.load(videos);
    context.executeQueryAsync(function () {
        var videoTable = document.getElementById("availableVideos");
        while (videoTable.hasChildNodes()) {
            videoTable.removeChild(videoTable.lastChild);
        }

        // Iterate through the video list
        var listItemEnumerator = videos.getEnumerator();
        while (listItemEnumerator.moveNext()) {
            var videoItem = listItemEnumerator.get_current();
            var video = document.createElement("div");
            video.className = "ui-state-default";
            var fileName =  videoItem.get_fieldValues()["FileRef"].toString()
            var iPosSlash = fileName.lastIndexOf("/");
            var fileName = fileName.substring(iPosSlash+1);
            var iPosExt = fileName.lastIndexOf(".");
            var fileExt = fileName.substring(iPosExt);
            if (fileExt == ".mp4") {
                var includeFile = true;

                //Check for videos already in playlist and do not add them 
                for (var iCheckExclusion = 0; iCheckExclusion < excludeList.length; iCheckExclusion++) {
                    if ("../Lists/Videos/" + fileName == excludeList[iCheckExclusion]) {
                        includeFile = false;
                        break;
                    }
                }
                if (includeFile) {
                    var videoLabel = document.createTextNode(fileName);
                    video.appendChild(videoLabel);
                    videoTable.appendChild(video);
                }
            }
        }
        
    },
    function (sender, args) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode(args.get_message()));
        errArea.appendChild(divMessage);
    });

}

// This function simply redirects to the video library page, so the user can use any method for uploading files
// with which they are already familiar
function uploadFiles() {
    window.location.href = "../Lists/Videos/Forms/AllItems.aspx"
}
