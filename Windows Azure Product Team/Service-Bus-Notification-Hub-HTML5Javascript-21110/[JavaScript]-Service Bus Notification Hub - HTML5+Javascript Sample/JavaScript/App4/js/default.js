// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var messaging = Microsoft.WindowsAzure.Messaging;

    var hub = new messaging.NotificationHub("https://<your namespace>.servicebus.windows.net/", "<your listen SAS key>", "<your hub path>");

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                var toastTemplate = '<toast><visual><binding template="ToastText01"><text id="1">$(message)</text></binding></visual></toast>';

                var pushNotifications = Windows.Networking.PushNotifications;
                var channelOperation = pushNotifications.PushNotificationChannelManager.createPushNotificationChannelForApplicationAsync();

                channelOperation.then(function(newChannel) {
                    return newChannel.uri;
                }).then(function (channelUri) {
                    return hub.registerApplicationAsync(channelUri);

                    // to unregister
                    //return hub.unregisterApplicationAsync();

                    // to register tags comment the previous line, and uncomment this one
                    //return hub.registerApplicationAsync(channelUri, ["myTag", "myOtherTag"]);

                    // to use templates
                    //return hub.registerTemplateForApplicationAsync(channelUri, 'toast1', ["myTag", "myOtherTag"], { 'X-WNS-Type': 'wns/toast' }, toastTemplate);

                    // to unregister
                    //return hub.unregisterTemplateForApplicationAsync('toast1');
                }).done();

            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        // args.setPromise().
    };

    app.start();
})();
