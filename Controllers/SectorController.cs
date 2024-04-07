using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Prueba.Data;
using Prueba.Models;

namespace Prueba.Controllers
{

    public class SectorsController : Controller
    {
        //Inyeccion de dependencias 
        public readonly SectorsContext _context;

        //Constructor que inicializa los modelos
        public SectorsController(SectorsContext context)
        {
            _context = context;
        }


        public IActionResult Index(string search, int pagina = 1)
        {
            int pageSize = 15; // Tamaño de la página

            // Obtener todos los sectores y aplicar filtros de búsqueda si existe
            var sector = from sectors in _context.Sectors select sectors;
            if (!string.IsNullOrEmpty(search))
            {
                sector = sector.Where(result =>
                    result.Id.ToString().Contains(search) ||
                    result.Name.Contains(search) ||
                    result.Description.Contains(search) ||
                    result.Author.Contains(search)
                );
            }

            // Calcular el número total de sectores sin paginar
            int totalSectores = sector.Count();

            // Paginar los resultados según la página actual y el tamaño de la página
            var sectoresPaginados = sector.OrderBy(s => s.Name)
                                          .Skip((pagina - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToList();

            // Calcular el número total de páginas
            int totalPages = (int)Math.Ceiling((double)totalSectores / pageSize);

            // Pasar los datos paginados y la información de paginación a la vista
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pagina;

            return View(sectoresPaginados);

        }


        //Vista editar
        public IActionResult Edit(int? id)
        {
            return View(_context.Sectors.FirstOrDefault(s => s.Id == id));
        }

        //Vista Crear
        public IActionResult Create()
        {
            return View();
        }

        //Accion para crear
        [HttpPost]
        public IActionResult Create(Sector sector)
        {

            _context.Sectors.Add(sector);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        //Accion de editar
        [HttpPost]
        public IActionResult Update(Sector sector)
        {

            _context.Sectors.Update(sector);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Vista para ver detalles 
        public IActionResult Details(int? id)
        {
            return View(_context.Sectors.Find(id));
        }

        //Accion de eliminar
        public IActionResult Delete(int id)
        {

            var sector = _context.Sectors.Find(id);

            _context.Sectors.Remove(sector);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}