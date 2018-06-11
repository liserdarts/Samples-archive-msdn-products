"use strict";

var Wingtip = window.Wingtip || {};

Wingtip.Social = function () {

    var getFollowing = function () {

        var deferred = $.Deferred();

        var ctx = new SP.ClientContext.get_current();
        var sfm = new SP.Social.SocialFollowingManager(ctx)
        this.following = sfm.getFollowed(1);
        ctx.executeQueryAsync(
            Function.createDelegate(this, function () {
                deferred.resolve(this.following);
            }),
            Function.createDelegate(this, function (sender, args) {
                deferred.reject(sender, args);
            })
        );

        return deferred.promise();

    },

    getTimeline = function () {

        var deferred = $.Deferred();

        var ctx = new SP.ClientContext.get_current();
        var sfm = new SP.Social.SocialFeedManager(ctx)
        var options = new SP.Social.SocialFeedOptions();
        ctx.load(sfm);
        options.set_maxThreadCount(10);
        options.set_sortOrder(SP.Social.SocialFeedSortOrder.byCreatedTime);
        this.feed = sfm.getFeed(SP.Social.SocialFeedType.timeline, options);
        ctx.executeQueryAsync(
            Function.createDelegate(this, function () {
                deferred.resolve(this.feed);
            }),
            Function.createDelegate(this, function (sender, args) {
                deferred.reject(sender, args);
            })
        );

        return deferred.promise();

    },

    getMentions = function () {

        var deferred = $.Deferred();

        var ctx = new SP.ClientContext.get_current();
        var sfm = new SP.Social.SocialFeedManager(ctx)
        var options = new SP.Social.SocialFeedOptions();
        ctx.load(sfm);
        options.set_maxThreadCount(10);
        options.set_sortOrder(SP.Social.SocialFeedSortOrder.byCreatedTime);
        this.feed = sfm.getMentions(false, options);
        ctx.executeQueryAsync(
            Function.createDelegate(this, function () {
                deferred.resolve(this.feed);
            }),
            Function.createDelegate(this, function (sender, args) {
                deferred.reject(sender, args);
            })
        );

        return deferred.promise();

    },

    postMessage = function (body) {

        var deferred = $.Deferred();

        var ctx = new SP.ClientContext.get_current();
        var sfm = new SP.Social.SocialFeedManager(ctx)

        var postData = new SP.Social.SocialPostCreationData();
        postData.set_contentText(body);

        sfm.createPost(null, postData);

        ctx.executeQueryAsync(
            Function.createDelegate(this, function () {
                deferred.resolve();
            }),
            Function.createDelegate(this, function (sender, args) {
                deferred.reject(sender, args);
            })
        );

        return deferred.promise();

    };

    return {
        get_following: getFollowing,
        get_timeline: getTimeline,
        get_mentions: getMentions,
        post_message: postMessage
    };

};