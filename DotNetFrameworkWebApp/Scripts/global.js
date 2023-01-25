var Global = {};
Global.FormHelper = function (formElement, options, onSucccess, onError) {
       "use strict";
    var settings = {};
    settings = $.extend({}, settings, options);
    $.validator.unobtrusive.parse(formElement)
    formElement.validate(settings.validateSettings);
    formElement.submit(function (e) {       
        var submitBtn = formElement.find(':submit');
        if (formElement.validate().valid()) {
            $.ajax(formElement.attr("action"), {
                type: "POST",
                data: formElement.serializeArray(),
                
                beforeSend: function (xhr) {
                    $(':input[type="submit"]').prop('disabled', true);
                    
                },
                success: function (result) {
                    if (onSucccess === null || onSucccess === undefined) {
                        if (result.isSuccess) {
                            window.location.href = result.redirectUrl;
                        } else {
                            if (settings.updateTargetId) {
                                if (result.data == undefined) {
                                    $("#" + settings.updateTargetId).html("<span>" + result + "</span>");
                                }
                                else {
                                    $("#" + settings.updateTargetId).html("<span>" + result.data + "</span>");
                                }
                            }
                        }
                    } else {                        
                        onSucccess(result);
                    }
                },
                error: function (jqXHR, status, error) {
                    if (onError !== null && onError !== undefined) {
                        onError(jqXHR, status, error);
                    }
                    $(':input[type="submit"]').prop('disabled', false);
                }, complete: function () {
                    $(':input[type="submit"]').prop('disabled', false);
                }
            });
        }

        e.preventDefault();
    });

    return formElement;
};

Global.GridHelper = function (gridElement, options) {
    if ($(gridElement).find("thead tr th").length > 1) {
        var settings = {};
        settings = $.extend({}, settings, options);
        $(gridElement).dataTable(settings);
        return $(gridElement);
    }
};