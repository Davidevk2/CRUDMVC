using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Prueba.Data;
using Prueba.Models;
using System.IO;
using Prueba.Helpers;
using Prueba.Providers;

namespace Prueba.Controllers
{

    public class CompaniesController : Controller
    {
        public readonly SectorsContext _context;
        private HelperUploadFiles helperUpload;

        public CompaniesController(SectorsContext context, HelperUploadFiles  helperUpload){
            _context = context;
            this.helperUpload = helperUpload;
        }
      

        public IActionResult Index()
        {
            return View( _context.Companies.ToList());
        }

        public async Task<IActionResult> Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Company company, IFormFile archivo, int ubicacion){

        string nombreArchivo = archivo.FileName;
        string path = "";
          switch (ubicacion)
            {
                case 0:
                    path = await this.helperUpload.UploadFilesAsync(archivo, nombreArchivo, Folders.Uploads);
                    break;
                case 1:
                    path = await this.helperUpload.UploadFilesAsync(archivo, nombreArchivo, Folders.Images);
                    break;
                case 2:
                    path = await this.helperUpload.UploadFilesAsync(archivo, nombreArchivo, Folders.Documents);
                    break;
                case 3:
                    path = await this.helperUpload.UploadFilesAsync(archivo, nombreArchivo, Folders.Temp);
                    break;
            }

            company.Logo = nombreArchivo;
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


    }
}