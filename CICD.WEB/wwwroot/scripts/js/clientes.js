
//#region VARIABLES
var jtxbNombreCliente= $("");
var jdivResultado = $("");
var jtmpResultado = null;

var idCliente = -1;

var jddlTipo = $("");
var jtxbNombre = $("");
var jtxbDocumento = $("");
var jtxbTelefono = $("");
var jtxbCorreo = $("");
var jtxbDireccion = $("");
var jtxbNombreContacto = $("");
var jtxbTelefonoContacto = $("");
var jtxbCorreoContacto = $("");
var jddlEstado = $("");
//#endregion

//#region CARGA INICIAL
$(function () {
    jtxbNombreCliente = $("#txbNombreCliente");
    jdivResultado = $("#divResultado");
    jtmpResultado = $.templates("#tmpResultado");
    $("#divResultado").delegate("a.btn", "click", divResultado_click);
    Buscar();
    jtxbNombreCliente.focus();
    $("#btnBuscar").click(Buscar);


    jddlTipo = $("#ddlTipo");
    jddlTipo.change(jddlTipo_change);
    jtxbNombre = $("#txbNombre");
    jtxbDocumento = $("#txbDocumento");
    jtxbTelefono = $("#txbTelefono");
    jtxbCorreo = $("#txbCorreo");
    jtxbDireccion = $("#txbDireccion");
    jtxbNombreContacto = $("#txbNombreContacto");
    jtxbTelefonoContacto = $("#txbTelefonoContacto");
    jtxbCorreoContacto = $("#txbCorreoContacto");
    jddlEstado = $("#ddlEstado");

    $("#btnNuevocliente").click(btnNuevoCliente_click);
    $("#btnGuardar").click(btnGuardar_click);
});
//#endregion

//#region EVENTOS
function divResultado_click(evt) {
    var jcmd = $(evt.currentTarget);
    if (jcmd.hasClass("eliminar")) {
        Eliminar(jcmd);
    }
    else {
        $('#divCliente').find('.modal-title').text('Editar cliente');
        TraerCliente(jcmd);
    }
}

function btnNuevoCliente_click(evt) {
    idCliente = -1;
    $('#divCliente').find("input.contacto").val("").prop("disabled", false);
    $('#divCliente').find('.modal-title').text('Nuevo cliente');
    $('#divCliente').find("input[type=text], select, textarea").val("");
    $('#divCliente').modal('show');

    setTimeout(function () {
        jddlTipo.focus();
    }, 480);
}

function jddlTipo_change(evt) {
    if (jddlTipo.val() && jddlTipo.val() == 2 /*Juridico*/) {
        $('#divCliente').find("input.contacto").val("").prop("disabled", false);
    }
    else {
        $('#divCliente').find("input.contacto").val("").prop("disabled", true);
    }
}

function btnGuardar_click(evt) {
    Guardar();
}
//#endregion

//#region MÉTODOS

function Buscar() {
    var dto = {
        nombreCliente: $.trim(jtxbNombreCliente.val())
    };
    jcode.ajax.metodo("Clientes", "Buscar", dto, function (data, status) {
        jdivResultado.html(jtmpResultado.render({ Clientes: data }));
    });
}

function Eliminar(jcmd) {
    var dto = {
        idCliente: jcmd.closest("tr").attr("id")
    };
    var nombre = jcmd.closest("tr").find("td.nombre").text();
    Swal.fire({
        title: "¿Está seguro de eliminar?",
        text: "Cliente: " + nombre,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            jcode.ajax.metodo("Clientes", "Eliminar", dto, function (data, status) {
                jcode.mensaje.success("Eliminado", "Se eliminó correctamente.", function () {
                    Buscar();
                });
            });
        }
    });
}

function TraerCliente(jcmd) {
    var dto = {
        idCliente: jcmd.closest("tr").attr("id")
    };
    jcode.ajax.metodo("Clientes", "TraerCliente", dto, function (data, status) {
        idCliente = data.Id;
        jddlTipo.val(data.TipoCliente);
        jtxbNombre.val(data.Nombre);
        jtxbDocumento.val(data.Documento);
        jtxbTelefono.val(data.Telefono);
        jtxbCorreo.val(data.Correo);
        jtxbDireccion.val(data.Direccion);
        jtxbNombreContacto.val(data.NombreContacto);
        jtxbTelefonoContacto.val(data.TelefonoContacto);
        jtxbCorreoContacto.val(data.CorreoContacto);
        jddlEstado.val(data.Estado);

        $('#divCliente').modal('show');
        setTimeout(function () {
            jddlTipo.focus();
        }, 480);
    });
}

function Guardar() {
    var lMensajes = [];
    var idTipo = jddlTipo.val();
    if (!jddlTipo.val()) {
        lMensajes.push(" - Debe especificar un tipo de cliente.");
    }
    var nombre = $.trim(jtxbNombre.val());
    if (!nombre) {
        lMensajes.push(" - Debe especificar un nombre.");
    }

    var documento = $.trim(jtxbDocumento.val());
    if (!documento) {
        lMensajes.push(" - Debe especificar un NIT / CI.");
    }
    var nombreContacto = $.trim(jtxbNombreContacto.val());
    if (idTipo && idTipo == 2 /*Juridico*/) {
        if (!nombreContacto) {
            lMensajes.push(" - Debe especificar un nombre de contacto.");
        }
    }
    if (!jddlEstado.val()) {
        lMensajes.push(" - Debe seleccionar un estado.");
    }
    if (lMensajes.length > 0) {
        jcode.mensaje.error("Error", lMensajes);
        return;
    }
    var dto = {
        eCliente: {
            Id: idCliente,
            TipoCliente: idTipo,
            Nombre: nombre,
            Documento: documento,
            Telefono: $.trim(jtxbTelefono.val()) || null,
            Correo: $.trim(jtxbCorreo.val()) || null,
            Direccion: $.trim(jtxbDireccion.val()) || null,
            NombreContacto: nombreContacto,
            TelefonoContacto: $.trim(jtxbTelefonoContacto.val()) || null,
            CorreoContacto: $.trim(jtxbCorreoContacto.val()) || null,
            Estado: jddlEstado.val()
        }
    };
    jcode.ajax.metodo("Clientes", "Guardar", dto, function (data, status) {
        if (data.Exito) {
            $('#divCliente').modal('hide');
            jcode.mensaje.success("Guardado",idCliente > 0 ? "Se modificó correctamente." : "Se guardó correctamente", function () {
                Buscar();
            });
        }
        else {
            jcode.mensaje.error("Error", data.Mensaje);
        }
    });
}
//#endregion
