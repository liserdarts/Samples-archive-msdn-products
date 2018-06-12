var context;
var web;
var user;
var groupName;
var groupId;
var yammerResponse;

function initYammerApp(appid, groupid) {
    yam.config({ appId: appid });
    groupId = groupid;

    yam.getLoginStatus(function (response) {
        if (response.authResponse) {
            getLists();
        } else {
            yam.login(function (response) {
                if (response.authResponse) {
                    getLists();
                }
            });
        }
    });

    $('#btnPost').click(function () {
        var d = { "body": $('#txtQuestion').val() }
        if (groupId && groupId.length > 0) {
            d.group_id = groupId;
        }

        yam.request({
            url: "https://www.yammer.com/api/v1/messages.json",
            method: "POST",
            data: d,
            success: function (response) {
                yammerResponse = response;
                getFeeds();
                $('#txtQuestion').val('');
            },
            error: errorMsg,
        });
    });
}

function getLists() {
    $('#YammerClientForm').show();

    var u = "https://www.yammer.com/api/v1/groups.json";
    yam.request(
        {
            url: u
            , method: "GET"
            , success: function (response) {
                yammerResponse = response;
                if (yammerResponse) {
                    getFeeds();
                }
            }
            , error: errorMsg
        }
    );
}

function getFeeds() {
    yammerResponse = null;

    var u = "https://www.yammer.com/api/v1/messages";
    if (groupId && groupId.length > 0) {
        u += "/in_group/" + groupId;
    }
    u += ".json";

    yam.request({
        url: u,
        method: "GET",
        success: function (response) {
            yammerResponse = response;
            renderFeed();
        },
        error: errorMsg
    }
    );
}

function renderFeed() {
    var messages = yammerResponse.messages;
    var arr = 0;
    var posts = new Array();

    //Process Yammer Feed
    for (var i = 0; i < messages.length; i++) {
        if (messages[i].replied_to_id == null) {
            var post = {};
            post.source = "Y";
            post.messageId = messages[i].id;
            post.rmId = "YA_" + messages[i].id;
            post.clientType = messages[i].client_type;
            post.message = messages[i].body.plain;
            var userProfile = getUserProfileById(messages[i].sender_id, yammerResponse.references);
            post.fullName = userProfile.fullName;
            post.avatarUrl = userProfile.avatarUrl;
            post.userUrl = userProfile.userUrl;
            var local = new Date(messages[i].created_at);
            post.postTime = local.toLocaleString();
            post.likeTxt = getYammerLikeText(messages[i].liked_by, yammerResponse.meta.current_user_id);
            post.replies = getYammerReplies(messages[i].id, yammerResponse);
            posts[arr] = post;
            arr++;
        }
    }

    posts = dedupe(posts);

    posts.sort(postSort);

    $('#YammerClientFeeds').empty();

    $("#threadTemplate").tmpl(posts).prependTo("#YammerClientFeeds");
}

function showReply(id) {
    $("#" + id).show();
}

function postLike(msgid) {
    yam.request({
        url: "https://www.yammer.com/api/v1/messages/liked_by/current.json",
        method: "POST",
        data: { message_id: msgid }
    });
    var likeTxt = $("#likespan_" + msgid).html();
    if (likeTxt.length == 0) {
        likeTxt = "You like this";
    }
    else if (likeTxt == "You like this") {
        return false;
    }
    else {
        likeTxt = "You and " + likeTxt;
    }
    $("#likespan_" + msgid).html(likeTxt);
    $("#likehdr_" + msgid).show();
    return false;
}

function postReply(id) {
    var rType = id.substr(6, 2);
    var msgid = id.substr(9);
    var reply = $("#reply_" + rType + "_" + msgid).val();

    if (rType == "YA") {
        msgPostSuccess = false;
        yam.request({
            url: "https://www.yammer.com/api/v1/messages.json",
            method: "POST",
            data: { "body": reply, replied_to_id: msgid },
            success: function (response) {
                yammerResponse = response;
                getFeeds();
            },
            error: errorMsg
        });
    }

    return false;
}

function dedupe(posts) {
    return posts;
}

function postSort(a, b) {
    var aDate = new Date(a.postTime);
    var bDate = new Date(b.postTime);
    return bDate - aDate;
}

function getUserProfileById(senderId, references) {
    var userProfile = {};
    userProfile.avatarUrl = "";
    userProfile.fullName = "";
    userProfile.userUrl = "";

    for (var i = 0; i < references.length; i++) {
        if (references[i].type == "user") {
            if (references[i].id == senderId) {
                userProfile.fullName = references[i].full_name;
                userProfile.avatarUrl = references[i].mugshot_url;
                userProfile.userUrl = references[i].web_url;
                return userProfile;
            }
        }
    }
    return userProfile;
}

function getYammerReplies(messageId, response) {
    var replies = new Array();
    var messages = response.messages;
    var arr = 0;
    for (var i = 0; i < messages.length; i++) {
        if (messages[i].replied_to_id == messageId) {
            var post = {};
            post.source = "Y";
            post.clientType = messages[i].client_type;
            post.messageId = messages[i].id;
            post.message = messages[i].body.plain;
            var userProfile = getUserProfileById(messages[i].sender_id, response.references);
            post.fullName = userProfile.fullName;
            post.avatarUrl = userProfile.avatarUrl;
            post.userUrl = userProfile.userUrl;
            var local = new Date(messages[i].created_at);
            post.postTime = local.toLocaleString();
            post.likeTxt = getYammerLikeText(messages[i].liked_by, response.meta.current_user_id);
            replies[arr] = post;
            arr++;
        }
    }

    replies.sort(postSort);

    return replies;
}

function getYammerLikeText(likes, userId) {
    var iLike = false;
    var txt = "";
    for (var i = 0; i < likes.count; i++) {
        if (likes.names[i].user_id == userId) {
            iLike = true;
        }
    }

    if (likes.count == 1 && iLike) {
        txt = "You like this";
    } else if (likes.count > 1 && iLike) {
        txt = "You and " + (likes.count - 1) + " others like this";
    } else if (likes.count > 0 && !iLike) {
        txt = (likes.count) + " others like this";
    }

    return txt;
}

function errorMsg(response) {
    console.log(response);
}

function getQueryStringParameter(paramToRetrieve) {
    var params =
        document.URL.split("?")[1].split("&");
    var strParams = "";
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split("=");
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1];
    }
}

// This function prepares, loads, and then executes a SharePoint query to get the current users information
function getUserName() {
    user = web.get_currentUser();
    context.load(user);
    context.executeQueryAsync(onGetUserNameSuccess, onGetUserNameFail);
}

// This function is executed if the above OM call is successful
// It replaces the content of the 'welcome' element with the user name
function onGetUserNameSuccess() {
    $('#message').text('Hello ' + user.get_title());
}

// This function is executed if the above OM call fails
function onGetUserNameFail(sender, args) {
    alert('Failed to get user name. Error:' + args.get_message());
}
