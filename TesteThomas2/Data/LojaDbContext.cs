using Microsoft.EntityFrameworkCore;
using TesteThomas2.Entities;

namespace TesteThomas2.Data
{
    public class LojaDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para garantir emails únicos
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Id)
                .IsUnique();
        }

        public LojaDbContext(DbContextOptions<LojaDbContext> options)
             : base(options) { }
    }
}
