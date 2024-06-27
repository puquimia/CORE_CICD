using CICD.LIB.Entidad.Personalizado;
using CICD.LIB.Entidad.Tabla;
using CICD.LIB.Logica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CICD_WEB.Controllers
{
    public class EmpleadosController : Controller
    {
		#region Inicio
        public readonly GEmpleados empleados;
		public readonly GVarios varios;
		public readonly GCargos cargos;
		public EmpleadosController()
        {
            empleados = new GEmpleados();
            varios = new GVarios();
            cargos = new GCargos();
        }
		public IActionResult Index()
        {
			return View();
        }
        public IActionResult Nuevo()
        {
			ViewData["EstadoCivil"] = TraerVariosxGrupo("GEN.Empleado.EstadoCivil", string.Empty);
			ViewData["Sexo"] = TraerVariosxGrupo("GEN.Empleado.Sexo", string.Empty);
			ViewData["TipoDocumentoIdentidad"] = TraerVariosxGrupo("GEN.Empleado.TipoDocumentoIdentidad", string.Empty);
			ViewData["Cargo"] = cargos.TraerCargos().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nombre }).ToList();
			return View();
        }
        public IActionResult Editar(int id)
        {
            Empleado eEmpleado = empleados.TraerEmpleadoEntidad(id);
            ViewData["Empleado"] = eEmpleado;
			ViewData["EstadoCivil"] = TraerVariosxGrupo("GEN.Empleado.EstadoCivil", eEmpleado.EstadoCivil.ToString());
			ViewData["Sexo"] = TraerVariosxGrupo("GEN.Empleado.Sexo", eEmpleado.Sexo);
			ViewData["TipoDocumentoIdentidad"] = TraerVariosxGrupo("GEN.Empleado.TipoDocumentoIdentidad", eEmpleado.TipoDocumentoIdentidad.ToString());
			ViewData["Cargo"] = cargos.TraerCargos().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nombre, Selected = x.Id == eEmpleado.IdCargo }).ToList();
			return View();
        }

        private List<SelectListItem>TraerVariosxGrupo(string grupo, string valorSeleccionado)
        {
            return varios.TraerVariosxGrupo(grupo).AsEnumerable().Select(x => new SelectListItem() { Value = x.Codigo, Text = x.Descripcion, Selected  =  valorSeleccionado == x.Codigo}).ToList();
		}
        #endregion

        #region Ajax

        [HttpPost]
        public IActionResult Buscar(string nombreEmpleado)
        {
            try
            {
                List<Empleados> lEmpleados = empleados.Buscar(nombreEmpleado);
                return Json(from empleado in lEmpleados
                            select new
                            {
                                Id = empleado.Id,
                                NombreCompleto = $"{empleado.Nombre} {empleado.ApellidoPaterno} {empleado.ApellidoMaterno}",
                                FechaNacimiento = empleado.FechaNacimiento.Value.ToShortDateString(),
                                Sexo = empleado.DescripcionSexo,
                                Cargo = empleado.NombreCargo,
                                HaberBasico = empleado.HaberBasico.Value.ToString("N2")
                            });
            }
            catch(Exception ex) 
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Guardar(Empleado eEmpleado) 
        {
            try
            {
                if(eEmpleado.Id > 0)
                {
					empleados.Modificar(eEmpleado);
				}
                else
                {
					empleados.Registrar(eEmpleado);
				}
                return Json(new
                {
                    Exito = true,
                    Id = eEmpleado.Id,
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

		[HttpPost]
		public IActionResult Eliminar(int idEmpleado)
		{
			try
			{
                empleados.Eliminar(idEmpleado);
				return Json(new
				{
					Exito = true
				});
			}
			catch (Exception ex)
			{
				throw new Exception("Error: " + ex.Message);
			}
		}
		#endregion
	}
}
