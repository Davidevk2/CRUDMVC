using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Prueba.Data;
using Prueba.Models;
using System.IO;


namespace Prueba.Controllers
{

    public class CompaniesController : Controller
    {
        public readonly SectorsContext _context;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public CompaniesController(SectorsContext context, IWebHostEnvironment hostingEnvironment){
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }
      

        public IActionResult Index()
        {
            return View( _context.Companies.ToList());
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Company company, IFormFile archivo){


            var rutaDestino = Path.Combine(_hostingEnviroment.WebRootPath, "images", archivo.FileName);
            using (var stream = new FileStream(rutaDestino, FileMode.Create)){
                await archivo.CopyToAsync(stream);
            }   
      
            company.Logo = "/images/"+ archivo.FileName;
            
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


    }
}
