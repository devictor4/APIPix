using APIPix.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace APIPix.Repository.Context
{
    public class APIPixContext : DbContext
    {
        public APIPixContext(DbContextOptions<APIPixContext> options)
            : base(options)
        {
            
        }

        public DbSet<Usuario> usuario { get; set; }
        public DbSet<ChavePix> chavePix { get; set; }
        public DbSet<UsuarioChavePix> usuarioChavePix { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
