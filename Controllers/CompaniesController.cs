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


        public CompaniesController(SectorsContext context){
            _context = context;
            
        }
      

        public IActionResult Index()
        {
            return View( _context.Companies.ToList());
        }

        public async Task<IActionResult> Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Company company){

    

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


    }
}
