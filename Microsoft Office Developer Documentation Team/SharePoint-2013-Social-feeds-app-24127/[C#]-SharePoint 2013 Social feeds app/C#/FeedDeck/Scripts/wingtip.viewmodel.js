"use strict"

var Wingtip = window.Wingtip || {};

Wingtip.Poster = function (account, name) {

    var displayName = name,
        imageUri = "/_layouts/15/images/PersonPlaceholder.42x42x32.png",
        accountName = account,
        get_account = function () { return accountName; },
        set_account = function (v) { accountName = v; },
        get_name = function () { return displayName; },
        set_name = function (v) { displayName = v; },
        get_picture = function () { return imageUri; },
        set_picture = function (v) {
            imageUri = v;
            if (v === null || v.length === 0) {
                imageUri = "/_layouts/15/images/PersonPlaceholder.42x42x32.png";
            }
        };

    return {
        get_account: get_account,
        set_account: set_account,
        get_name: get_name,
        set_name: set_name,
        get_picture: get_picture,
        set_picture: set_picture
    };

};

Wingtip.Post = function (author, content, created) {

    var poster = author,
        date = created,
        body = content,
        picture = "/_layouts/15/images/PersonPlaceholder.42x42x32.png",
        get_poster = function () { return poster; },
        set_poster = function (v) { poster = v; },
        get_date = function () {
            var dateParts = date.toString().split(' ');
            return dateParts[1] + " " + dateParts[2] + " " + dateParts[5];
        },
        set_date = function (v) { date = v; },
        get_body = function () { return body; },
        set_body = function (v) { body = v; },
        get_picture = function () { return picture; },
        set_picture = function (v) {
            picture = v;
            if (v === null || v.length === 0) {
                picture = "/_layouts/15/images/PersonPlaceholder.42x42x32.png";
            }
        };

    return {
        get_poster: get_poster,
        set_poster: set_poster,
        get_date: get_date,
        set_date: set_date,
        get_body: get_body,
        set_body: set_body,
        get_picture: get_picture,
        set_picture: set_picture
    };

};

Wingtip.ViewModel = function () {

    var followed = ko.observableArray(),
        get_followed = function () { return followed; },
        posts = ko.observableArray(),
        get_posts = function () { return posts; },
        mentions = ko.observableArray(),
        get_mentions = function () { return mentions; },

        load = function () {

            //Following
            new Wingtip.Social().get_following().then(
                function (following) {
                    followed.removeAll();
                    for (var f = 0; f < following.length; f++) {
                        var follow = new Wingtip.Poster(
                            following[f].get_accountName,
                            following[f].get_name());
                        follow.set_picture(following[f].get_imageUri);
                        followed.push(follow);
                    }
                },
                function (sender, args) {
                    alert(args.get_message());
                }
            );

            //Posts
            new Wingtip.Social().get_timeline().then(
                function (feed) {
                    posts.removeAll();
                    var threads = feed.get_threads();
                    for (var t = 0; t < threads.length; t++) {
                        var thread = threads[t];
                        var actors = thread.get_actors();
                        if (thread.get_threadType() === 0) {
                            var rootPost = thread.get_rootPost();
                            var content = rootPost.get_text();
                            var author = actors[rootPost.get_authorIndex()].get_name();
                            var pictureUrl = actors[rootPost.get_authorIndex()].get_imageUri();
                            var createdTime = rootPost.get_createdTime();
                            var post = new Wingtip.Post(author, content, createdTime);
                            post.set_picture(pictureUrl);
                            posts.push(post);
                        }
                    }
                },
                function (sender, args) {
                    alert(args.get_message());
                }
            );

            //Mentions
            new Wingtip.Social().get_mentions().then(
                function (feed) {
                    mentions.removeAll()
                    var threads = feed.get_threads();
                    for (var t = 0; t < threads.length; t++) {
                        var thread = threads[t];
                        var actors = thread.get_actors();
                        var rootPost = thread.get_rootPost();
                        var content = rootPost.get_text();
                        var author = actors[rootPost.get_authorIndex()].get_name();
                        var pictureUrl = actors[rootPost.get_authorIndex()].get_imageUri();
                        var createdTime = rootPost.get_createdTime();
                        var post = new Wingtip.Post(author, content, createdTime);
                        post.set_picture(pictureUrl);
                        mentions.push(post);
                    }
                },
                function (sender, args) {
                    alert(args.get_message());
                }
            );
        };

    return {
        load: load,
        get_followed: get_followed,
        get_posts: get_posts,
        get_mentions: get_mentions
    };

}();