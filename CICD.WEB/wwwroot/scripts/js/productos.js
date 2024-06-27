
//#region VARIABLES
var jtxbNombreProducto = $("");
var jdivResultado = $("");
var jtmpResultado = null;


var jtxbCodigo = $("");
var jtxbNombre = $("");
var jtxbPrecioVenta = $("");
var jtxbStockMinimo = $("");
var jddlUnidadMedida = $("");
var jddlMarca = $("");
var jddlCategoria = $("");
var jddlEstado = $("");

var idProducto = -1;
//#endregion

//#region CARGA INICIAL
$(function () {
    jtxbNombreProducto = $("#txbNombreProducto");
    jdivResultado = $("#divResultado");
    jtmpResultado = $.templates("#tmpResultado");
    $("#divResultado").delegate("a.btn", "click", divResultado_click);
    Buscar();
    jtxbNombreProducto.focus();
    $("#btnBuscar").click(Buscar);
    $("#btnNuevoProducto").click(btnNuevoProducto_click);

    jtxbCodigo = $("#txbCodigo");
    jtxbNombre = $("#txbNombre");
    jtxbPrecioVenta = $("#txbPrecioVenta");
    jtxbPrecioVenta.keydown(jcode.validacion.decimal);
    jtxbStockMinimo = $("#txbStockMinimo");
    jtxbStockMinimo.keydown(jcode.validacion.entero);
    jddlUnidadMedida = $("#ddlUnidadMedida");
    jddlMarca = $("#ddlMarca");
    jddlCategoria = $("#ddlCategoria");
    jddlEstado = $("#ddlEstado");

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
        $('#divProducto').find('.modal-title').text('Editar producto');
        TraerProducto(jcmd);
    }
}
function eliminarProducto_click(evt) {
    Eliminar($(evt.currentTarget));
}

function btnNuevoProducto_click(evt) {
    idProducto = -1;
    $('#divProducto').find('.modal-title').text('Nuevo producto');
    $('#divProducto').find("input[type=text], select").val("");
    $('#divProducto').modal('show');

    setTimeout(function () {
        jtxbCodigo.focus();
    }, 480);
}

function btnGuardar_click(evt) {
    Guardar();
}
//#endregion

//#region MÉTODOS

function Buscar() {
    var dto = {
        nombreProducto: $.trim(jtxbNombreProducto.val())
    };
    jcode.ajax.metodo("Productos", "Buscar", dto, function (data, status) {
        jdivResultado.html(jtmpResultado.render({ Productos: data }));
    });
}

function Eliminar(jcmd) {
    var dto = {
        idProducto: jcmd.closest("tr").attr("id")
    };
    var nombreProducto = jcmd.closest("tr").find("td.nombre").text();
    Swal.fire({
        title: "¿Está seguro de eliminar?",
        text: "Producto: " + nombreProducto,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            jcode.ajax.metodo("Productos", "Eliminar", dto, function (data, status) {
                jcode.mensaje.success("Eliminado","Se eliminó correctamente.", function () {
                    Buscar();
                });
            });
        }
    });
}

function TraerProducto(jcmd) {
    var dto = {
        idProducto: jcmd.closest("tr").attr("id")
    };
    jcode.ajax.metodo("Productos", "TraerProducto", dto, function (data, status) {
        idProducto = data.Id;
        jtxbCodigo.val(data.Codigo);
        jtxbNombre.val(data.Nombre);
        jtxbPrecioVenta.val(data.PrecioVenta);
        jtxbStockMinimo.val(data.PrecioVenta);
        jtxbStockMinimo.val(data.StockMinimo);
        jddlUnidadMedida.val(data.IdUnidadMedida);
        jddlMarca.val(data.IdMarca);
        jddlCategoria.val(data.IdCategoria);
        jddlEstado.val(data.Estado);
        $('#divProducto').modal('show');
        setTimeout(function () {
            jtxbCodigo.focus();
        }, 480);
    });
}

function Guardar() {
    var lMensajes = [];
    var codigo = $.trim(jtxbCodigo.val());
    if (!codigo) {
        lMensajes.push(" - Debe especificar un código.");
    }
    var nombre = $.trim(jtxbNombre.val());
    if (!nombre) {
        lMensajes.push(" - Debe especificar un nombre.");
    }
    if (!$.trim(jtxbPrecioVenta.val())) {
        lMensajes.push(" - Debe especificar un precio de venta.");
    }
    if (!$.trim(jtxbStockMinimo.val())) {
        lMensajes.push(" - Debe especificar un stock mínimo.");
    }
    if (!jddlUnidadMedida.val()) {
        lMensajes.push(" - Debe seleccionar una unidad de medida.");
    }
    if (!jddlMarca.val()) {
        lMensajes.push(" - Debe seleccionar una marca.");
    }
    if (!jddlCategoria.val()) {
        lMensajes.push(" - Debe seleccionar una categoria.");
    }
    if (!jddlEstado.val()) {
        lMensajes.push(" - Debe seleccionar un estado.");
    }
    if (lMensajes.length > 0) {
        jcode.mensaje.error("Error", lMensajes);
        return;
    }
    var dto = {
        eProducto: {
            Id: idProducto,
            Codigo: codigo,
            Nombre: nombre,
            PrecioVenta: jtxbPrecioVenta.val(),
            StockMinimo: jtxbStockMinimo.val(),
            IdUnidadMedida: jddlUnidadMedida.val(),
            IdMarca: jddlMarca.val(),
            IdCategoria: jddlCategoria.val(),
            Estado: jddlEstado.val()
        }
    };
    jcode.ajax.metodo("Productos", "Guardar", dto, function (data, status) {
        if (data.Exito) {
            $('#divProducto').modal('hide');
            jcode.mensaje.success("", idProducto > 0 ? "Se modificó correctamente.": "Se guardó correctamente", function () {
                Buscar();
            });
        }
        else {
            jcode.mensaje.error("Error", data.Mensaje);
        }
    });
}
//#endregion
