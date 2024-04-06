using Microsoft.AspNetCore.Mvc;
using Prueba.Data;

namespace Prueba.Controllers
{
   
   public class SectorsController : Controller{
        //Inyeccion de dependencias 
        public readonly SectorsContext _context;

        //Constructor que inicializa los modelos
        public SectorsController(SectorsContext context){
            _context = context;
        }


        public IActionResult Index()
        {
            return View( _context.Sectors.ToList());
        }


        //Vista editar
        public IActionResult Edit(int? id){
            return View( _context.Sectors.FirstOrDefault(s => s.Id == id));
        }


   }
}