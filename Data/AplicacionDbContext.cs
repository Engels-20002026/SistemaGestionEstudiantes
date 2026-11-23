
using Microsoft.EntityFrameworkCore;
using SistemaGestionEstudiantes.Models;

namespace SistemaGestionEstudiantes.Data
{
    public class AplicacionDbContext : DbContext
    {
        public AplicacionDbContext(
            DbContextOptions<AplicacionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
    }
}
