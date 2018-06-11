/// <reference name="MicrosoftAjax.js" />
/// <reference path="~/_layouts/15/init.js" />
/// <reference path="~/_layouts/15/SP.Core.js" />
/// <reference path="~/_layouts/15/SP.Runtime.js" />
/// <reference path="~/_layouts/15/SP.UI.Dialog.js" />
/// <reference path="~/_layouts/15/SP.js" />
/// <reference path="jquery-1.7.1.js"/>
(function () {
    var originalExecuteQueryAsync = SP.ClientRuntimeContext.prototype.executeQueryAsync;
    SP.ClientRuntimeContext.prototype.executeQueryAsync = function (succeededCallback, failedCallback) {
        succeededCallback();
        failedCallback();
    };
    intellisense.redirectDefinition(SP.ClientRuntimeContext.prototype.executeQueryAsync, originalExecuteQueryAsync);
})();
