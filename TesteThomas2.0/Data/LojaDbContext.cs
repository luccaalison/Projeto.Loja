using Microsoft.EntityFrameworkCore;
using Projeto.Loja.Entities;

namespace Projeto.Loja.Data
{
    public class LojaDbContext : DbContext
    {
        public virtual DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaProduto> VendaProdutos { get; set; }


        public LojaDbContext(DbContextOptions<LojaDbContext> options)
             : base(options) { }
    }
}
