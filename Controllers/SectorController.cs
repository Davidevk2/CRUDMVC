using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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


        public async Task<IActionResult> Index(string search)
        {
        
            var sector = from sectors in  _context.Sectors select sectors;
            if(!string.IsNullOrEmpty(search)){
                sector = sector.Where( result => result.Id.ToString().Contains(search) || result.Name.Contains(search)  || result.Description.Contains(search)  || result.Author.Contains(search) );
            }
            
            return View(await sector.OrderByDescending(s => s.Name).Take(30).ToListAsync());
            
        }


        //Vista editar
        public  async Task<IActionResult> Edit(int? id){
            return View(await _context.Sectors.FirstOrDefaultAsync(s => s.Id == id));
        }

        //Vista Crear
        public IActionResult Create()
        {
            return View();
        }

        //Accion para crear
        [HttpPost]
        public async Task<IActionResult> Create(Sector sector){

            _context.Sectors.Add(sector);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        //Accion de editar
        [HttpPost]
        public async Task<IActionResult> Update(Sector sector){

            _context.Sectors.Update(sector);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Vista para ver detalles 
        public async Task<IActionResult> Details(int? id){
            return View( await _context.Sectors.FindAsync(id));
        }

        //Accion de eliminar
        public async Task<IActionResult> Delete(int id){

            var sector = _context.Sectors.Find(id);

            _context.Sectors.Remove(sector);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



   }
}