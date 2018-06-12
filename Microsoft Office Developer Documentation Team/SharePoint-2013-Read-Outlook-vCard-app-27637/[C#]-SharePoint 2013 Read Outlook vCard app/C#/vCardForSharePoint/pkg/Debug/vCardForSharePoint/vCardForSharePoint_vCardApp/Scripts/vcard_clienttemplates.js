
jQuery(document).ready(function () {
    SP.SOD.executeFunc('sp.js', 'SP.ClientContext', function () {
        clientContext = new SP.ClientContext.get_current();
    });
});

// regestring the custom js link templates
(function () {
    var formatFieldCtx = {};
    formatFieldCtx.Templates = {};
    formatFieldCtx.Templates.Fields = {
        "FormatFunction": {
            "DisplayForm": formatFunctionFieldViewTemplate,
            "EditForm": formatFunctionFieldEditTemplate,
            "NewForm": formatFunctionFieldEditTemplate,
            "View": formatFunctionFieldViewTemplate
        },
        "UserProfilePropertyName": {
            "NewForm": formatFunctionFieldNewTemplate,
            "EditForm": formatFunctionFieldNewTemplate
        }
    };

    SPClientTemplates.TemplateManager.RegisterTemplateOverrides(formatFieldCtx);
})();

// custom view template for the FormatFunction field
function formatFunctionFieldViewTemplate(ctx) {
    var value = ctx.CurrentItem.FormatFunction;
    if (value != null && jQuery.trim(value) != '') {
        var div = "<div>function&nbsp;(d){</div>";
        div += "<div style='padding-left:5px'>" + value + "</div>";
        div += "<div style='padding-left:5px;'>return d;<br />}</div>";
        return div;
    }

    return value;
}

// custom edit template for the FormatFunction and UserProfilePropertyName field
function formatFunctionFieldEditTemplate(ctx) {
    var value = ctx.CurrentItem.FormatFunction;
    value = value.replace(/"/gi, "'");
    var formCtx = SPClientTemplates.Utility.GetFormContextForCurrentField(ctx);
    formCtx.registerGetValueCallback(formCtx.fieldName, function () {
        return document.getElementById("formatFunctionInput").value;
    });

    var validators = new SPClientForms.ClientValidation.ValidatorSet();
    if (formCtx.fieldSchema.Required) {
        validators.RegisterValidator(new SPClientForms.ClientValidation.RequiredValidator());
    }

    if (validators._registeredValidators.length > 0) {
        formCtx.registerClientValidator(formCtx.fieldName, validators);
    }

    formCtx.registerValidationErrorCallback(formCtx.fieldName, function (errorResult) {
        SPFormControl_AppendValidationErrorMessage('outerformatFunctionDiv', errorResult);
    });

    var html = '<div id="outerformatFunctionDiv"><div>function&nbsp;(d){</div>';
    html += '<input id="formatFunctionInput" type="text" class="ms-long ms-spellcheck-true" value="' + value + '"></input>';
    html += '<div style="padding-left:5px;">return d;<br />}</div></div>';

    return html;
}

// custom new template for the FormatFunction and UserProfilePropertyName field
function formatFunctionFieldNewTemplate(ctx) {
    var orignalvalue = ctx.CurrentItem.UserProfilePropertyName;
    orignalvalue = jQuery.trim(orignalvalue.replace(/"/gi, "'"));

    var formCtx = SPClientTemplates.Utility.GetFormContextForCurrentField(ctx);
    hostweburl = getvaluefromQueryString('SPHostUrl');
    appweburl = _spPageContextInfo.webAbsoluteUrl;
    var userProperties = null;

    var choicehtml = '<div id="outerformatFunctionSpan">';
    choicehtml += '<select id="profileprops" name="profileprops" multiple="multiple" style="width:250px"></select>';
    choicehtml += '&nbsp;<img id="expandicon" src="/_layouts/15/images/EXPAND.GIF" onclick="openSequenceSection();"/>';
    choicehtml += '&nbsp;<span>Reorder properties</span>';
    choicehtml += '<div style="padding-top:3px"><input class="ms-long ms-spellcheck-true" style="display:none" type="text" id="profilepropsvalue" value="' + orignalvalue + '"></input></div>';
    choicehtml += '</div>';

    formCtx.registerGetValueCallback(formCtx.fieldName, function () {
        return document.getElementById("profilepropsvalue").value;
    });

    var validators = new SPClientForms.ClientValidation.ValidatorSet();
    if (formCtx.fieldSchema.Required) {
        validators.RegisterValidator(new SPClientForms.ClientValidation.RequiredValidator());
    }

    if (validators._registeredValidators.length > 0) {
        formCtx.registerClientValidator(formCtx.fieldName, validators);
    }

    formCtx.registerValidationErrorCallback(formCtx.fieldName, function (errorResult) {
        SPFormControl_AppendValidationErrorMessage('outerformatFunctionSpan', errorResult);
    });

    jQuery.when(getUserProfilePropertyNames())
         .then(function (data) {
             userProperties = data.d.UserProfileProperties.results;
         }).then(function () {
             try {
                 var selecthtml = '';
                 var fieldinput = jQuery("input[id^='profilepropsvalue']");
                 var preselectedvalue = new Array();
                 if (fieldinput.val() != null && jQuery.trim(fieldinput.val()) != '') {
                     preselectedvalue = fieldinput.val().split(";");
                 }

                 for (i = 0; i < userProperties.length; i++) {
                     if (preselectedvalue.length > 0 && jQuery.inArray(userProperties[i].Key, preselectedvalue) != -1) {
                         selecthtml += '<option selected="selected" value="' + userProperties[i].Key + '">' + userProperties[i].Key + '</option>';
                     }
                     else {
                         selecthtml += '<option value="' + userProperties[i].Key + '">' + userProperties[i].Key + '</option>';
                     }
                 }

                 jQuery('#profileprops').append(selecthtml);
                 jQuery("#profileprops").multiselect();
                 jQuery("#profileprops").bind("multiselectclose", function (event, ui) {
                     updatePropValues();
                 });
             }
             catch (e) {
                 logError(e.message, false);
             }
         }).fail(function (error) {
             var errorobj = jQuery.parseJSON(error.responseText);
             logError(errorobj.error.message.value, false);
         });

    return choicehtml;
}