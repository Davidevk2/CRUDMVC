using Microsoft.AspNetCore.Mvc;

namespace Prueba.Controllers
{
   
   public class SectorsController : Controller{
        public IActionResult Index()
        {
            return View();
        }

   }
}