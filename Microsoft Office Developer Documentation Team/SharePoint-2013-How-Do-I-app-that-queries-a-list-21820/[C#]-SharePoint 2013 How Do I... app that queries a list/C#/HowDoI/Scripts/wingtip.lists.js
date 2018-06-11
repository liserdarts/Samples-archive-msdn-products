"use strict";

var WingtipToys = window.WingtipToys || {};

WingtipToys.Lists = function () {

    var create = function (title, description, template) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var createInfo = new SP.ListCreationInformation();
        createInfo.set_title(title);
        createInfo.set_description(description);
        createInfo.set_templateType(template);
        this.newList = ctx.get_web().get_lists().add(createInfo);
        ctx.load(this.newList);
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.newList); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    },

    remove = function (title) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var list = ctx.get_web().get_lists().getByTitle(title)
        ctx.load(list);
        this.oldTitle = title;
        list.deleteObject();
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.oldTitle); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    },

    read = function (title) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        this.list = ctx.get_web().get_lists().getByTitle(title)
        ctx.load(this.list);
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.list); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    },

    getContentType = function (contentTypeId) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        this.ctype = ctx.get_web().get_contentTypes().getById(contentTypeId);
        ctx.load(this.ctype);
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.ctype); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    },

    createContentType = function (contentTypeName, description, parentId) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var parent = ctx.get_web().get_contentTypes().getById(parentId);
        ctx.load(parent);
        var createInfo = new SP.ContentTypeCreationInformation();
        createInfo.set_name(contentTypeName);
        createInfo.set_description(description);
        createInfo.set_parentContentType(parent);
        this.newCtype = ctx.get_web().get_contentTypes().add(createInfo);
        this.newCtype.update();
        ctx.load(this.newCtype);
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.newCtype); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    },

    createSiteColumn = function (fieldDisplayName, fieldType, groupName) {
        var deferred = $.Deferred();
        var xmlDef = "<Field DisplayName='" + fieldDisplayName +
                        "' Type='" + fieldType + "'/>";
        var ctx = new SP.ClientContext.get_current();
        this.field = ctx.get_web().get_fields().addFieldAsXml(xmlDef, false, SP.AddFieldOptions.addToNoContentType);
        ctx.load(this.field);
        this.field.set_group(groupName);
        this.field.updateAndPushChanges(false);
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.field); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    },

    addFieldLink = function (contentTypeId, internalFieldName) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var ctype = ctx.get_web().get_contentTypes().getById(contentTypeId);
        var field = ctx.get_web().get_fields().getByInternalNameOrTitle(internalFieldName);
        ctx.load(ctype);
        ctx.load(field);
        var createInfo = new SP.FieldLinkCreationInformation();
        createInfo.set_field(field);
        this.fieldLink = ctype.get_fieldLinks().add(createInfo);
        ctype.update(false);
        ctx.load(this.fieldLink);
        ctx.executeQueryAsync(
        Function.createDelegate(this,
            function () { deferred.resolve(this.fieldLink); }),
        Function.createDelegate(this,
            function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    },

    bindContentType = function (listTitle, contentTypeId) {
        var deferred = $.Deferred();
        var ctx = new SP.ClientContext.get_current();
        var list = ctx.get_web().get_lists().getByTitle(listTitle);
        ctx.load(list);
        this.ctype = ctx.get_web().get_contentTypes().getById(contentTypeId);
        ctx.load(this.ctype);
        list.get_contentTypes().addExistingContentType(this.ctype);
        list.get_contentTypes().itemAt(0).deleteObject();
        ctx.executeQueryAsync(
            Function.createDelegate(this,
                function () { deferred.resolve(this.ctype); }),
            Function.createDelegate(this,
                function (sender, args) { deferred.reject(sender, args); }));

        return deferred.promise();

    };

    return {
        create: create,
        remove: remove,
        read: read,
        get_contentType: getContentType,
        create_contentType: createContentType,
        create_siteColumn: createSiteColumn,
        add_fieldLink: addFieldLink,
        bind_ContentType: bindContentType
    };

}();