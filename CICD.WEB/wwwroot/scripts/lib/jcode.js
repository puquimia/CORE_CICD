var jcode = jcode || {};

jcode.ajax = {
    metodo: function (controllerName, actionName, dto, callbackExito, callbackError) {
        $.ajax({
            url: "/" + controllerName + "/" + actionName,
            type: "POST",
            data: dto,
            dataType: "json",
            success: function (data, status) {
                if (callbackExito) {
                    callbackExito(data, status, this);
                }
            },
            error: function (response, settings) {
                sonic.utils.ocultarEsperaAjax();
                if (callbackError) {
                    callbackError(response, settings);
                }
                else {
                    alert(response.responseText);
                }
            }
        });
    }
}