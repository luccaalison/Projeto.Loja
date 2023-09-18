using TesteThomas2.Entities;
using TesteThomas2.Models;

namespace TesteThomas2.Services.EstoqueService.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> ListarProdutos();
        Task<Produto> BuscarPorId(int id);
        Task<Produto> CadastrarNovoProduto(ProdutoCreateModel produtoCadastro);
        Task<Produto> EditarProduto(ProdutoEditModel produtoEditar);
        Task RetirarProduto(Produto produtoEditar, int qtde);
        Task AlterarQtdeEstoque(int produtoId, int qtde);
    }
}
