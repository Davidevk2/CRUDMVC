using Microsoft.AspNetCore.Mvc;
using Prueba.Data;

namespace Prueba.Controllers
{
   
   public class SectorsController : Controller{

        public readonly SectorsContext _context;
        
        //Constructor que inicializa los modelos
        public SectorsController(SectorsContext context){
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

   }
}