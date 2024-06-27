using CICD.LIB.ENT;
using CICD.LIB.Entidad.Personalizado;
using CICD.LIB.Entidad.Tabla;
using CICD.LIB.Logica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CICD_WEB.Controllers
{
    public class ProductosController : Controller
    {
        GProductos productos = null;
        GUnidadesMedida unidadesMedida = null;
        GMarcas marcas = null;
        GCategorias categorias = null;
        GVarios varios = null;
        public ProductosController()
        {
            productos = new GProductos();
            unidadesMedida = new GUnidadesMedida();
            marcas = new GMarcas();
            categorias = new GCategorias();
            varios = new GVarios();
        }
        public IActionResult Index()
        {
            ViewData["UnidadMedida"] = unidadesMedida.TraerUnidadesMedida().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nombre }).ToList();
            ViewData["Marca"] = marcas.TraerMarcas().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nombre }).ToList();
            ViewData["Categoria"] = categorias.TraerCategorias().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nombre }).ToList();
            ViewData["Estado"] = varios.TraerVariosxGrupo("GEN.Producto.Estado").Select(x => new SelectListItem() { Value = x.Codigo, Text = x.Descripcion }).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Buscar(string nombreProducto)
        {
            try
            {
                List<Productos>lProductos = productos.Buscar(nombreProducto);
                return Json(lProductos);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult TraerProducto(int idProducto)
        {
            try
            {
                return Json(productos.TraerProductoEntidad(idProducto));
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Guardar(Producto eProducto)
        {
            try
            {
                List<string> lMensajes = new List<string>();
                if(productos.ExisteCodigoProducto(eProducto.Codigo, eProducto.Id))
                {
                    lMensajes.Add(" - Ya existe algún producto con el código especificado.");
                }
                if (productos.ExisteNombreProducto(eProducto.Nombre, eProducto.Id))
                {
                    lMensajes.Add(" - Ya existe algún producto con el nombre especificado.");
                }
                if(lMensajes.Count > 0)
                {
                    return Json(new
                    {
                        Exito = false,
                        Mensaje = lMensajes
                    });
                }
                if (eProducto.Id > 0)
                {
                    productos.Modificar(eProducto);
                }
                else
                {
                    productos.Registrar(eProducto);
                }
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

        [HttpPost]
        public IActionResult Eliminar(int idProducto)
        {
            try
            {
                productos.Eliminar(idProducto);
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
