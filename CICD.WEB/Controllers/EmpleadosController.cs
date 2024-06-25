using Microsoft.AspNetCore.Mvc;

namespace CICD_WEB.Controllers
{
    public class EmpleadosController : Controller
    {
        public IActionResult Index()
        {
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
    }
}
