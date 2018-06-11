var context;
var web;
var site;

// This code runs when the DOM is ready. It ensures the SharePoint
// script file sp.js is loaded and then executes sharePointReady()
$(document).ready(function () {
    SP.SOD.executeFunc('sp.js', 'SP.ClientContext', sharePointReady);
});


// This function creates a context object which is needed to use the SharePoint object model
function sharePointReady() {
    __defineRunQuery();
    context = new SP.ClientContext.get_current();
    web = context.get_web();
    site = context.get_site();

    context.load(web);

    var webLoaded = context.runQuery();

    $.ajaxSetup({
        type: "POST",
        contentType: "application/json;odata=verbose",
        dataType: "json",
        accepts: { json: "application/json;odata=verbose" }
    });

    webLoaded.pipe(function () {
        $("#appWebUrl").text(web.get_url());

        getRemoteAppUrlREST()
            .pipe(function (remoteAppUrl) {
                return accessExternalResourceREST(remoteAppUrl + "My/", "POST")
            })
            .pipe(function (result) {
                $("#invocationResult").text(eval(result.d.Invoke.Body));
                $("#message").text("Done!");
            });
    });

    webLoaded.pipe(function () {
        $("#appWebUrlCsom").text(web.get_url());

        getRemoteAppUrlCSOM()
            .pipe(function (remoteAppUrl) {
                return accessExternalResourceCSOM(remoteAppUrl + "My/", "POST")
            }, onQueryFailed)
            .pipe(function (result) {
                $("#invocationResultCsom").text(eval(result.get_body()));
                $("#message").text("Done!");
            }, onQueryFailed);
    });
}

function getRemoteAppUrlREST() {
    return $.when(
            $.ajax("../_vti_bin/client.svc/web/appInstanceId", { type: "GET" }),
            $.ajax("../_vti_bin/client.svc/web/parentWeb/serverRelativeUrl", { type: "GET" }))
        .pipe(function (appInstanceIdXhr, serverRelativeUrlXhr) {
            var serverRelativeUrl = serverRelativeUrlXhr[0].d.ServerRelativeUrl;
            var appInstanceId = appInstanceIdXhr[0].d.AppInstanceId;

            $("#appInstanceId").text(appInstanceId);
            $("#serverRelativeUrl").text(serverRelativeUrl);
            var remoteAppUrlQuery = "../_vti_bin/client.svc/site/OpenWeb('" + encodeURIComponent(serverRelativeUrl) + "')/getAppInstanceById('" + encodeURIComponent(appInstanceId) + "')/remoteAppUrl";
            return $.ajax(remoteAppUrlQuery, 
                {
                type: "POST",
                headers: {
                    "X-RequestDigest": $("#__REQUESTDIGEST").val()
                }
            })
        })
        .pipe(function (remoteAppUrl) {
            $("#remoteAppUrl").text(remoteAppUrl.d.RemoteAppUrl);
            return remoteAppUrl.d.RemoteAppUrl;
        }, function (jqXHR, textStatus, errorThrown) {
            //just do something...
            alert(jqXHR);
        });
}

function accessExternalResourceREST(url, method) {
    return $.ajax({
        url: "../_api/SP.WebProxy.invoke",
        type: "POST",
        data: JSON.stringify(
            {
                'requestInfo': {
                    '__metadata': { 'type': 'SP.WebRequestInfo' },
                    'Url': url,
                    'Method': method,
                    'Headers': {
                        'results': [{
                            '__metadata': { 'type': 'SP.KeyValue' },
                            'Key': 'SPAppWebUrl',
                            'Value': web.get_url(),
                            'ValueType': 'Edm.String'
                        }]
                    }
                }
            }),

        headers: {
            "X-RequestDigest": $("#__REQUESTDIGEST").val(),
            "Content-Type": "application/json;odata=verbose",
            "Accept": "application/json;odata=verbose"
        }
    });
}

function getRemoteAppUrlCSOM() {
    context.load(web);

    return context.runQuery()
        .pipe(function () {
            var appInstanceId = web.get_appInstanceId();
            var parentWebInfo = web.get_parentWeb();
            context.load(parentWebInfo);
            $("#appInstanceIdCsom").text(appInstanceId.toString());
            return context.runQuery({ appInstanceId: appInstanceId, parentWebInfo: parentWebInfo });
        }, onQueryFailed)
        .pipe(function (data) {
            var parentWebUrl = data.parentWebInfo.get_serverRelativeUrl();
            var parentWeb = site.openWeb(parentWebUrl);
            $("#serverRelativeUrlCsom").text(parentWebUrl);

            var appInstance = parentWeb.getAppInstanceById(data.appInstanceId);

            context.load(appInstance, "RemoteAppUrl");
            return context.runQuery(appInstance);

        }, onQueryFailed)
        .pipe(function (appInstance) {
            var remoteAppUrl = appInstance.get_remoteAppUrl();
            $("#remoteAppUrlCsom").text(remoteAppUrl);

            return remoteAppUrl;
        }, onQueryFailed);
}

function accessExternalResourceCSOM(url, method) {
    var requestInfo = new SP.WebRequestInfo();

    requestInfo.set_url(url);
    requestInfo.set_method(method);
    requestInfo.set_headers({ "SPAppWebUrl": web.get_url() });

    var response = SP.WebProxy.invoke(context, requestInfo);

    return context.runQuery(response);
}

function onQueryFailed(args) {
    /// <summary>
    ///     Callback for failed queries 
    /// </summary>
    /// <param name="args" type="SP.ClientRequestFailedEventArgs">
    ///     Explanation of the query failure.
    /// </param>
    alert("Error" + args.get_message());
};

function __defineRunQuery() {
    SP.ClientRuntimeContext.prototype.runQuery = function (data) {
        var def = $.Deferred();

        // SP.ClientRequestSucceededEventArgs
        // Args does not seem super useful
        var success = function (sender, args) {
            def.resolve(data, args);
        };

        // SP.ClientRequestFailedEventArgs
        var failure = function (sender, args) {
            def.reject(args);
        };

        this.executeQueryAsync(success, failure);
        return def.promise();
    }
}
