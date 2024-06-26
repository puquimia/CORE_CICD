
//#region VARIABLES

var idEmpleado = -1;

var jtxbNombre = $("");
var jtxbApellidoPaterno = $("");
var jtxbApellidoMaterno = $("");
var jtxbFechaNacimiento = $("");
var jddlTipoDocumento = $("");
var jtxbDocumentoIdentidad = $("");
var jddlSexo = $("");
var jtxbTelefono = $("");
var jddlEstadoCivil = $("");
var jtxbHaberBasico = $("");
var jddlCargo = $("");
var jtxbCorreo = $("");
var jtxbDireccion = $("");
//#endregion

//#region CARGA INICIAL
$(function(){
    jtxbNombre = $("#txbNombre");
    jtxbApellidoPaterno = $("#txbApellidoPaterno");
    jtxbApellidoMaterno = $("#txbApellidoMaterno");
    jtxbFechaNacimiento = $("#txbFechaNacimiento");
    jddlTipoDocumento = $("#ddlTipoDocumento");
    jtxbDocumentoIdentidad = $("#txbDocumentoIdentidad");
    jddlSexo = $("#ddlSexo");
    jtxbTelefono = $("#txbTelefono");
    jtxbHaberBasico = $("#txbHaberBasico");
    jtxbHaberBasico.keydown(jcode.validacion.decimal);
    jddlCargo = $("#ddlCargo");
    jtxbCorreo = $("#txbCorreo");
    jtxbDireccion = $("#txbDireccion");
    jddlEstadoCivil = $("#ddlEstadoCivil");
    jtxbNombre.focus();

    $("#btnGuardar").click(btnGuardar_click);
});
//#endregion

//#region EVENTOS
function btnGuardar_click(evt) {
    Guardar();
}
//#endregion

//#region MÉTODOS
function Guardar() {
    var lMensajes = [];
    var nombre = $.trim(jtxbNombre.val());
    if (!nombre) {
        lMensajes.push("- Debe especificar un nombre.");
    }
    var apellidoPaterno = $.trim(jtxbApellidoPaterno.val());
    if (!apellidoPaterno) {
        lMensajes.push("- Debe especificar un apellido paterno.");
    }
    var apellidoMaterno = $.trim(jtxbApellidoMaterno.val());
    if (!apellidoMaterno) {
        lMensajes.push("- Debe especificar un apellido materno.");
    }
    if (!jtxbFechaNacimiento.val()) {
        lMensajes.push("- Debe especificar una fecha de nacimiento.");
    }
    if (!jddlTipoDocumento.val()) {
        lMensajes.push("- Debe seleccionar un tipo de documento.");
    }
    var documentoIdentidad = $.trim(jtxbDocumentoIdentidad.val());
    if (!documentoIdentidad) {
        lMensajes.push("- Debe especificar un documento de identidad.");
    }
    if (!jddlSexo.val()) {
        lMensajes.push("- Debe seleccionar un sexo.");
    }
    if (!jddlEstadoCivil.val()) {
        lMensajes.push("- Debe seleccionar un estado civil.");
    }
    var haberBasico = $.trim(jtxbHaberBasico.val());
    if (!haberBasico) {
        lMensajes.push("- Debe especificar un haber básico.");
    }
    if (!jddlCargo.val()) {
        lMensajes.push("- Debe seleccionar un cargo.");
    }
    if (lMensajes.length > 0) {
        jcode.mensaje.error("", lMensajes);
        return;
    }

    var dto = {
        eEmpleado: {
            Id: idEmpleado,
            Nombre: nombre,
            ApellidoPaterno: apellidoPaterno,
            ApellidoMaterno: apellidoMaterno,
            FechaNacimiento: jtxbFechaNacimiento.val(),
            TipoDocumentoIdentidad: jddlTipoDocumento.val(),
            DocumentoIdentidad: documentoIdentidad,
            Sexo: jddlSexo.val(),
            EstadoCivil: jddlEstadoCivil.val(),
            Telefono: $.trim(jtxbTelefono.val()) || null,
            Correo: $.trim(jtxbCorreo.val()) || null,
            Direccion: $.trim(jtxbDireccion.val()) || null,
            HaberBasico: $.trim(jtxbHaberBasico.val()),
            IdCargo: jddlCargo.val()
        }
    }
    jcode.ajax.metodo("Empleados", "Guardar", dto, function (data, status) {
        if (idEmpleado > 0) {
            jcode.mensaje.success("Se guardó el empleado correctamente");
        }
        else {
            jcode.mensaje.success("Se registró el empleado correctamente", function () {
                location.href = "/Empleados/Editar/" + data.Id;
            });
        }
    });
}
//#endregion
