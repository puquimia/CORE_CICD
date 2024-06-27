
//#region VARIABLES
var jtxbNombreEmpleado = $("");
var jdivResultado = $("");
var jtmpResultado = null;
//#endregion

//#region CARGA INICIAL
$(function () {
    jtxbNombreEmpleado = $("#txbNombreEmpleado");
    jdivResultado = $("#divResultado");
    jtmpResultado = $.templates("#tmpResultado");
    $("#divResultado").delegate("a.eliminar", "click", eliminarEmpleado_click);
    Buscar();
    jtxbNombreEmpleado.focus();
    $("#btnBuscar").click(Buscar);
});
//#endregion

//#region EVENTOS
function eliminarEmpleado_click(evt) {
    Eliminar($(evt.currentTarget));
}
//#endregion

//#region MÉTODOS

function Buscar() {
    var dto = {
        nombreEmpleado: $.trim(jtxbNombreEmpleado.val())
    };
    jcode.ajax.metodo("Empleados", "Buscar", dto, function (data, status) {
        jdivResultado.html(jtmpResultado.render({ Empleados: data }));
    });
}

function Eliminar(jcmd) {
    var dto = {
        idEmpleado: jcmd.closest("tr").attr("id")
    };
    var nombreEmpleado = jcmd.closest("tr").find("td.nombreEmpleado").text();
    Swal.fire({
        title: "¿Está seguro de eliminar?",
        text: "Empleado: " + nombreEmpleado,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            jcode.ajax.metodo("Empleados", "Eliminar", dto, function (data, status) {
                jcode.mensaje.success("Se eliminó correctamente.", function () {
                    Buscar();
                });
            });
        }
    });
}
//#endregion
