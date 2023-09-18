using Microsoft.EntityFrameworkCore;
using TesteThomas2.Data;
using TesteThomas2.Entities;
using TesteThomas2.Migrations;
using TesteThomas2.Models;
using TesteThomas2.Service.EstoqueService;
using TesteThomas2.Services.VendasService.Interfaces;
using System.Linq.Expressions;

namespace TesteThomas2.Services.VendasService
{
    public class VendasService : IVendasService
    {
        private readonly LojaDbContext _db;

        public VendasService(LojaDbContext db) {
            _db = db;
        }
        public async Task<Produto> BuscarPorId(int id) {
            return await _db.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task<List<VendaDetailsModel>> ListarVendas() {
            throw new NotImplementedException();
        }
        public async Task AlterarQtdeEstoque(int produtoId, int qtde) {
            // buscar produto
            var produto = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == produtoId);
            // subtrai quantidade vendido pela quantidade em estoque
            var produtoVendido = produto.QtdeEstoque - qtde;
            // atribui a quantidade de estoque
            produto.QtdeEstoque = produtoVendido;

            // save changes
            _db.Produtos.Update(produto);
            await _db.SaveChangesAsync();
        }

        public async Task<decimal> ObtemValorVenda(VendasCreateModel vendasCreateModel) 
        {
            List<Produto> produtos = new();
            foreach (var item in vendasCreateModel.items) {
                produtos.Add(await _db.Produtos.FirstOrDefaultAsync(x => x.Id == item));
            }
            return produtos.Sum(x => x.Preco);
        }

        public async Task VerificaQtdeEstoque(int produtoId, int qtde) {
            // buscar produto
            var produto = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == produtoId);
            if (produto == null)
                throw new Exception("O produto informado não foi encontrado.");
            if (qtde > produto.QtdeEstoque)
                throw new Exception("A quantidade vendida é maior que a quantidadade em estoque.");
        }
        public async Task<VendaDetailsModel> CriarVenda(VendasCreateModel vendasCreateModel) {
            // buscar produtos e criar venda produtos
            decimal valorTotal = 0;
            List<VendaProduto> vendaProdutos = new();
            List<int> listaVendaItems = new();
            foreach (var item in vendasCreateModel.items) {
                if (!listaVendaItems.Contains(item)) {
                    int qtde = vendasCreateModel.items.Where(a => a == item).Count();
                    var produto = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == item);
                    // validar se o produto tem em estoque
                    //Verifica a quantidade de produtos em estoque
                    await VerificaQtdeEstoque(produto.Id, qtde);

                    vendaProdutos.Add(new VendaProduto {
                        ProdutoId = produto.Id,
                        Quantidade = qtde,
                        Preco = produto.Preco * qtde
                    });

                    valorTotal += produto.Preco * qtde;
                    //subtrai quantidade vendido pela quantidade em estoque
                    await AlterarQtdeEstoque(produto.Id, qtde);
                    listaVendaItems.Add(item);
                }
            }


            // criar venda
            var venda = new Venda {
                VendaTotal = valorTotal,
                QtdProduto = vendaProdutos.Count
            };

            // salvar venda
            var respVenda = await _db.Vendas.AddAsync(venda);
            var idVenda = respVenda.Entity.Id;

            // salvar vendaProduto
            vendaProdutos.ForEach(a => a.VendaId = idVenda);
            await _db.VendaProdutos.AddRangeAsync(vendaProdutos);
            await _db.SaveChangesAsync();
            // retornar venda criada
            var itemsProduto = new List<VendaProdutoDetailsModel>();
            vendaProdutos.ForEach(a => itemsProduto.Add(new VendaProdutoDetailsModel { NomeProduto = "", Preco = a.Preco, ProdutoId = a.ProdutoId }));
            var resp = new VendaDetailsModel {
                Items = itemsProduto,
                TotalItems = itemsProduto.Count,
                ValorTotal = valorTotal
            };
            return resp;
        }
    }
}
