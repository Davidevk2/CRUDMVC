using Microsoft.EntityFrameworkCore;
using Prueba.Models;

namespace Prueba.Data{

    public class SectorsContext : DbContext{
        //constructor que inicializa la conexion
        public SectorsContext(DbContextOptions<SectorsContext> options) : base(options){}

        //registrar modelos que se usan en el program 
        public DbSet<Sector> Sectors {get; set;}

    }
}