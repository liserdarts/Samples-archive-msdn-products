"use strict";

var Wingtip = window.Wingtip || {};

Wingtip.PeopleManager = function () {

    var deferreds = new Array(),
    
    getMyProperties = function () {

        var deferred = $.Deferred();

        var ctx = new SP.ClientContext.get_current();
        var pm = new SP.UserProfiles.PeopleManager(ctx)
        this.myProperties = pm.getMyProperties();
        ctx.load(this.myProperties);
        ctx.executeQueryAsync(
            Function.createDelegate(this, function () {
                deferred.resolve(this.myProperties);
            }),
            Function.createDelegate(this, function (sender, args) {
                deferred.reject(sender, args);
            })
        );

        return deferred.promise();
    },

    getUserProperties = function (username) {

        var deferred = $.Deferred();

        var ctx = new SP.ClientContext.get_current();
        var pm = new SP.UserProfiles.PeopleManager(ctx);
        var propNames = ["AccountName", "PreferredName", "PictureURL"];
        var userProfileProps = new SP.UserProfiles.UserProfilePropertiesForUser(ctx, username, propNames);
        this.userProperties = pm.getUserProfilePropertiesFor(userProfileProps);
        this.username = username;
        ctx.load(userProfileProps);

        ctx.executeQueryAsync(
            Function.createDelegate(this, function () {
                if (this.userProperties.length === 0) {
                    deferred.resolve([this.username, this.username, "/_layouts/15/images/PersonPlaceholder.42x42x32.png"]);
                }
                else {
                    deferred.resolve(this.userProperties);
                }
            }),
            Function.createDelegate(this, function (sender, args) {
                deferred.reject(sender, args);
            })
        );

        return deferred.promise();
    },

    getUserTasks = function (displayName) {

        var deferred = $.Deferred();

        var date1 = new Date();
        date1.setDate(date1.getDate()-1);
        var date2 = new Date();
        date2.setDate(date2.getDate()+7);
        var startDate = (date1.getMonth() + 1) + "/" + (date1.getDate()) + "/" + (date1.getFullYear());
        var endDate = (date2.getMonth() + 1) + "/" + (date2.getDate()) + "/" + (date2.getFullYear());

        var ctx = new SP.ClientContext.get_current();
        var keywordQuery = new Microsoft.SharePoint.Client.Search.Query.KeywordQuery(ctx);
        keywordQuery.set_queryText("ContentClass:STS_ListItem_Tasks PercentComplete<100 AssignedTo:\"" + displayName + "\" DueDate=" + startDate + "..." + endDate);
        var searchExecutor = new Microsoft.SharePoint.Client.Search.Query.SearchExecutor(ctx);
        this.results = searchExecutor.executeQuery(keywordQuery);
        ctx.executeQueryAsync(
            Function.createDelegate(this, function () {
                if (this.results.m_value.ResultTables.length > 0) {
                    deferred.resolve(this.results.m_value.ResultTables[0]);
                }
                else {
                    deferred.resolve(null);
                }
            }),
            Function.createDelegate(this, function (sender, args) {
                deferred.reject(sender, args);
            })
        );

        return deferred.promise();
    },

    getAppSetting = function (key) {

        var deferred = $.Deferred();

        var ctx = new SP.ClientContext.get_current();
        var query = "<View><Query><Where><Eq><FieldRef Name='Title'/><Value Type='Text'>" +
                    key + "</Value></Eq></Where></Query>" +
                    "<ViewFields><FieldRef Name='Title'/><FieldRef Name='Value1'/></ViewFields></View>";
        var camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml(query);
        var list = ctx.get_web().get_lists().getByTitle("Settings");
        ctx.load(list);
        this.items = list.getItems(camlQuery);
        ctx.load(this.items, 'Include(Title,Value1)');

        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () {
                    var enumerator = this.items.getEnumerator();
                    var value = null;
                    while (enumerator.moveNext()) {
                        var listItem = enumerator.get_current();
                        value = listItem.get_item("Value1");
                    }

                    deferred.resolve(value);
                }),
            Function.createDelegate(this,
                function (sender, args) {
                    deferred.reject(sender, args);
                }));

        return deferred.promise();
    };

    return {
        get_myProperties: getMyProperties,
        get_userProperties: getUserProperties,
        get_userTasks: getUserTasks,
        get_appSetting: getAppSetting
    };

};