using Microsoft.EntityFrameworkCore;
using TesteThomas2.Entities;

namespace TesteThomas2.Data
{
    public class LojaDbContext : DbContext
    {
        public virtual DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaProduto> VendaProdutos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para garantir emails únicos
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }

        public LojaDbContext(DbContextOptions<LojaDbContext> options)
             : base(options) { }
    }
}
