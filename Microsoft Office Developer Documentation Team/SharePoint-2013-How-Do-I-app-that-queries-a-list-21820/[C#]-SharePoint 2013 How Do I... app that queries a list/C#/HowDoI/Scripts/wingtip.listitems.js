"use strict";

window.WingtipToys = window.WingtipToys || {};

WingtipToys.ListItems = function () {

    var create = function (listName, itemTitle, itemQuestion, itemAnswer) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var list = ctx.get_web().get_lists().getByTitle(listName);
        ctx.load(list);
        var listItemCreationInfo = new SP.ListItemCreationInformation();
        this.newItem = list.addItem(listItemCreationInfo);
        this.newItem.set_item("Title", itemTitle);
        this.newItem.set_item("Question", itemQuestion);
        this.newItem.set_item("Answer", itemAnswer);
        this.newItem.update();
        ctx.load(this.newItem);
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () {
                    deferred.resolve(this.newItem);
                }),
            Function.createDelegate(this,
                function (sender, args) {
                    deferred.reject(sender, args);
                }));

        return deferred.promise();

    },

    read = function (listName, id) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var list = ctx.get_web().get_lists().getByTitle(listName);
        ctx.load(list);
        this.item = list.getItemById(id);
        ctx.load(this.item, 'Include(ID,Title,Question,Answer)');
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.item); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();
    },

    readAll = function (listName) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var query = "<View><Query><OrderBy><FieldRef Name='ID'/></OrderBy></Query><ViewFields><FieldRef Name='ID'/><FieldRef Name='Title'/><FieldRef Name='Question'/><FieldRef Name='Answer'/></ViewFields></View>";
        var camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml(query);
        var list = ctx.get_web().get_lists().getByTitle(listName);
        ctx.load(list);
        this.items = list.getItems(camlQuery);
        ctx.load(this.items, 'Include(ID,Title,Question,Answer)');
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.items); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();
    },

    update = function (listName, id, itemTitle, itemQuestion, itemAnswer) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var list = ctx.get_web().get_lists().getByTitle(listName);
        ctx.load(list);
        this.listItem = list.getItemById(id);
        this.listItem.set_item("Title", itemTitle);
        this.listItem.set_item("Question", itemTitle);
        this.listItem.set_item("Answer", itemTitle);
        this.listItem.update();
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.listItem); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferreds[deferreds.length - 1].promise();
    },

    remove = function (listName, id) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var list = ctx.get_web().get_lists().getByTitle(listName);
        ctx.load(list);
        this.listItem = list.getItemById(id);
        this.listItem.deleteObject();
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();
    },

    query = function (listName, freetext) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var query = "<View><Query><Where><Contains><FieldRef Name='Question'/><Value Type='Text'>" +
                    freetext +
                    "</Value></Contains></Where><OrderBy><FieldRef Name='ID'/></OrderBy></Query><ViewFields><FieldRef Name='ID'/><FieldRef Name='Title'/><FieldRef Name='Question'/><FieldRef Name='Answer'/></ViewFields></View>";
        var camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml(query);
        var list = ctx.get_web().get_lists().getByTitle(listName);
        ctx.load(list);
        this.items = list.getItems(camlQuery);
        ctx.load(this.items, 'Include(ID,Title,Question,Answer)');
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.items); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    };


    //public interface
    return {
        create: create,
        update: update,
        remove: remove,
        read: read,
        readAll: readAll,
        query: query
    }

}();
