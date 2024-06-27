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
                if (callbackError) {
                    callbackError(response, settings);
                }
                else {
                    alert(response.responseText);
                }
            }
        });
    }
},
jcode.mensaje = {
    success: function (mensaje, callbackExito) {
        Swal.fire({
            title: "Guardado!",
            text: mensaje,
            icon: "success",
            timer: 2000,
            showConfirmButton: false,
        }).then(callbackExito);
    },
    error: function (titulo, mensaje) {
        Swal.fire({
            icon: "error",
            title: "Error...",
            html: "<div style='text-align: left !important;'>" + mensaje.join("<br/>") + "</div>",
            confirmButtonText: `<i class="fa fa-thumbs-up"></i> Aceptar`,
        })
    }
},
jcode.validacion = {
    decimal: function (e) {
        var tecla = e.which || e.keyCode;
        if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
            (
                (tecla >= 48 && tecla <= 57) || //números principal
                (tecla >= 96 && tecla <= 105) || //números derecha
                tecla === 190 || //punto centro
                tecla === 110 || //punto derecha
                tecla === 8 || //borrar atrás
                tecla === 9 || //tab
                tecla === 13 || // enter
                tecla === 35 || //fin
                tecla === 36 || //inicio
                tecla === 37 || //izquierda
                tecla === 39 || //derecha
                tecla === 46 || //suprimir
                tecla === 45  //insertar
            )
        ) {
            return true;
        }
        return false;
    },
    entero: function () {
        var tecla = e.which || e.keyCode;
        if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
            (
                (tecla >= 48 && tecla <= 57) || //números principal
                (tecla >= 96 && tecla <= 105) || //números derecha
                tecla === 8 || //borrar atrás
                tecla === 9 ||  //tab
                tecla === 13 || // enter
                tecla === 35 || //fin
                tecla === 36 || //inicio
                tecla === 37 || //izquierda
                tecla === 39 || //derecha
                tecla === 46 || //suprimir
                tecla === 45 //insertar
            )
        ) {
            return true;
        }
        return false;
    }
}