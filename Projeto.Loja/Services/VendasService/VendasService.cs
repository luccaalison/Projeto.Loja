using Microsoft.EntityFrameworkCore;
using Projeto.Loja.Data;
using Projeto.Loja.Entities;
using Projeto.Loja.Migrations;
using Projeto.Loja.Models;
using Projeto.Loja.Service.EstoqueService;
using Projeto.Loja.Services.VendasService.Interfaces;
using System.Linq.Expressions;

namespace Projeto.Loja.Services.VendasService
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

            var validaEstoque = listaVendaItems;
            foreach (var item in validaEstoque) {
                int qtde = vendasCreateModel.items.Where(a => a == item).Count();
                var produto = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == item);
                if (listaVendaItems.Count > produto.QtdeEstoque)
                    throw new Exception("A quantidade vendida é maior que a quantidadade em estoque.");

                // remover a qtde vendida do estoque
                var produtoVendido = produto.QtdeEstoque - qtde;

                // att
                produto.QtdeEstoque = produtoVendido;

                // save changes
                _db.Produtos.Update(produto);
                await _db.SaveChangesAsync();
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
