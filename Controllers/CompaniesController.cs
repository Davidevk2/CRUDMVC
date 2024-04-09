using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Prueba.Data;
using Prueba.Models;
using System.IO;
using Prueba.Helpers;
using Prueba.Providers;
using Microsoft.EntityFrameworkCore;


namespace Prueba.Controllers
{

    public class CompaniesController : Controller
    {   
        //Inyecciones de dependencias la conexion a la db y subir archivos
        public readonly SectorsContext _context;
        private readonly HelperUploadFiles  helperUploadFiles;

        //Constructor que inicializar las variables de las dependencias 
        public CompaniesController(SectorsContext context, HelperUploadFiles helperUpload ){
            _context = context;
            this.helperUploadFiles = helperUpload;
            
        }
      

        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Company company, IFormFile archivo ,int ubicacion){

            string nombreArchivo = archivo.FileName;
            string path = "";

            switch(ubicacion){

                case 0:
                    path = await this.helperUploadFiles.UploadFilesAsync(archivo, nombreArchivo, Folders.Uploads);
                break;
                case 1:
                    path = await this.helperUploadFiles.UploadFilesAsync(archivo, nombreArchivo, Folders.Images);
                break;
                case 2:
                    path = await this.helperUploadFiles.UploadFilesAsync(archivo, nombreArchivo, Folders.Documents);
                break;
                case 3:
                    path = await this.helperUploadFiles.UploadFilesAsync(archivo, nombreArchivo, Folders.Temp);
                break;
               

            }

            company.Logo = nombreArchivo;
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


    }
}
