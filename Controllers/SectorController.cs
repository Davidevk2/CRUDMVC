using Microsoft.AspNetCore.Mvc;
using Prueba.Data;
using Prueba.Models;

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

        //Vista Crear
        public IActionResult Create()
        {
            return View();
        }

        //Accion para crear
        [HttpPost]
        public IActionResult Create(Sector sector){

            _context.Sectors.Add(sector);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        //Accion de editar
        [HttpPost]
        public IActionResult Update(Sector sector){

            _context.Sectors.Update(sector);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Vista para ver detalles 
        public IActionResult Details(int? id){
            return View( _context.Sectors.Find(id));
        }


   }
}