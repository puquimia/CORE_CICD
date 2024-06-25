using CICD.LIB.Logica;
using Microsoft.AspNetCore.Mvc;

namespace CICD_WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            GVarios varios = new GVarios();
            var estadocivil = varios.TraerVariosxGrupo("GEN.Empleado.EstadoCivil");
            return View();
        }
    }
}
