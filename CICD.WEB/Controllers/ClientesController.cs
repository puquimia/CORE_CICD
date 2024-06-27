using CICD.LIB.Entidad.Personalizado;
using CICD.LIB.Entidad.Tabla;
using CICD.LIB.Logica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CICD_WEB.Controllers
{
    public class ClientesController : Controller
    {
        GVarios varios = null;
        GClientes clientes = null;
        public ClientesController() 
        { 
            varios = new GVarios();
            clientes = new GClientes();
        }
        public IActionResult Index()
        {
            ViewData["Estado"] = varios.TraerVariosxGrupo("GEN.Cliente.Estado").Select(x => new SelectListItem() { Value = x.Codigo, Text = x.Descripcion }).ToList();
            ViewData["TipoCliente"] = varios.TraerVariosxGrupo("GEN.Cliente.TipoCliente").Select(x => new SelectListItem() { Value = x.Codigo, Text = x.Descripcion }).ToList();
            return View();
        }
        public IActionResult Nuevo()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buscar(string nombreCliente)
        {
            try
            {
                List<Clientes> lClientes = clientes.Buscar(nombreCliente);
                return Json(from cliente in lClientes
                            select new
                            {
                                Id = cliente.Id,
                                TipoCliente = cliente.DescripcionTipo,
                                Nombre = cliente.Nombre,
                                Documento = cliente.Documento ?? "----",
                                Telefono = cliente.Telefono ?? "----",
                                Correo = cliente.Correo ?? "----",
                                NombreContacto = cliente.NombreContacto ?? "----",
                                DescripcionEstado = cliente.DescripcionEstado
                            });
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult TraerCliente(int idCliente)
        {
            try
            {
                return Json(clientes.TraerClienteEntidad(idCliente));
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Guardar(Cliente eCliente)
        {
            try
            {
                List<string> lMensajes = new List<string>();
                if (eCliente.TipoCliente == 2 && clientes.ExisteNombreClienteJuridico(eCliente.Nombre, eCliente.Id))
                {
                    lMensajes.Add("Ya existe algún cliente con el nombre especificado.");
                }
                if (eCliente.TipoCliente == 2 && clientes.ExisteDocumentoClienteJuridico(eCliente.Documento, eCliente.Id))
                {
                    lMensajes.Add("Ya existe algún cliente con el NIT/CI especificado.");
                }
                if(lMensajes.Count > 0)
                {
                    return Json(new
                    {
                        Exito = false,
                        Mensaje = lMensajes
                    });
                }
                if (eCliente.Id > 0)
                {
                    clientes.Modificar(eCliente);
                }
                else
                {
                    clientes.Registrar(eCliente);
                }
                return Json(new
                {
                    Exito = true,
                    Id = eCliente.Id,
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Eliminar(int idCliente)
        {
            try
            {
                clientes.Eliminar(idCliente);
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
    }
}
