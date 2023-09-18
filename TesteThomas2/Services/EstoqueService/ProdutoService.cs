using Microsoft.EntityFrameworkCore;
using TesteThomas2.Data;
using TesteThomas2.Entities;
using TesteThomas2.Models;
using TesteThomas2.Services.EstoqueService.Interfaces;

namespace TesteThomas2.Service.EstoqueService
{
    public class ProdutoService : IProdutoService
    {
        private readonly LojaDbContext _db;

        public ProdutoService(LojaDbContext db)
        {
            _db = db;
        }

        public async Task AlterarQtdeEstoque(int produtoId, int qtde)
        {
            // buscar produto
            var produto = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == produtoId);
            if (produto == null)
                throw new Exception("O produto informado não foi encontrado.");

            // att
            produto.QtdeEstoque = qtde;

            // save changes
            _db.Produtos.Update(produto);
            await _db.SaveChangesAsync();
        }

        public async Task<Produto> BuscarPorId(int id)
        {
            return await _db.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Produto> CadastrarNovoProduto(ProdutoCreateModel produtoCadastro)
        {
            // Validar campos
            if (string.IsNullOrEmpty(produtoCadastro.Nome) || string.IsNullOrEmpty(produtoCadastro.Categoria))
                throw new Exception("Por favor, preencha todos os campos.");
            if (produtoCadastro.Preco <= 0)
                throw new Exception("Por favor, informe o preço do produto.");

            // Cadastrar na base de dados
            var novoProduto = new Produto
            {
                Nome = produtoCadastro.Nome,
                Categoria = produtoCadastro.Categoria ?? "Sem categoria",
                Descricao = produtoCadastro.Descricao ?? "Sem descrição informada",
                Preco = produtoCadastro.Preco,
                QtdeEstoque = produtoCadastro.QtdeEstoque ?? 0
            };
            await _db.Produtos.AddAsync(novoProduto);
            await _db.SaveChangesAsync();

            return novoProduto;
        }

        public async Task<Produto> EditarProduto(ProdutoEditModel produtoEditar)
        {
            // Validar campos
            if (string.IsNullOrEmpty(produtoEditar.Nome) || string.IsNullOrEmpty(produtoEditar.Categoria))
                throw new Exception("Por favor, preencha todos os campos.");
            if (produtoEditar.Preco <= 0)
                throw new Exception("Por favor, informe o preço do produto.");

            // buscar produto
            var produto = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == produtoEditar.Id);
            if (produto == null)
                throw new Exception("O produto informado não foi encontrado.");

            // atualizar
            produto.Nome = produtoEditar.Nome == produto.Nome ? produto.Nome : produtoEditar.Nome;
            produto.Preco = produtoEditar.Preco == produto.Preco ? produto.Preco : produtoEditar.Preco;
            produto.QtdeEstoque = produtoEditar.QtdeEstoque == produto.QtdeEstoque ? produto.QtdeEstoque : produtoEditar.QtdeEstoque;
            produto.Descricao = produtoEditar.Descricao == produto.Descricao ? produto.Descricao : produtoEditar.Descricao;
            produto.Categoria = produtoEditar.Categoria == produto.Categoria ? produto.Nome : produtoEditar.Categoria;

            // save changes
            _db.Produtos.Update(produto);
            await _db.SaveChangesAsync();
            return produto;
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            return await _db.Produtos.ToListAsync();
        }

        public async Task RetirarProduto(Produto produtoEditar, int qtde)
        {
            // buscar produto
            var produto = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == produtoEditar.Id);
            if (produto == null)
                throw new Exception("O produto informado não foi encontrado.");

            // att
            produto.Ativo = false;

            // save changes
            _db.Produtos.Update(produto);
            await _db.SaveChangesAsync();
        }
    }
}
