using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteThomas2.Data;
using TesteThomas2.Entities;
using TesteThomas2.Models;
using TesteThomas2.Service.EstoqueService;
using TesteThomas2.Services.EstoqueService.Interfaces;
using TesteThomas2.Services.VendasService;
using TesteThomas2.Services.VendasService.Interfaces;

namespace TesteThomas2.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly LojaDbContext _context;
        private readonly IVendasService _vendasService;
        private readonly IProdutoService _produtoService;

        public ProdutosController(LojaDbContext context, IVendasService vendasService, IProdutoService produtoService)
        {
            _vendasService = vendasService;
            _produtoService = produtoService;
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Produtos.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        /// <summary>
        /// Create Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Categoria,Preco,QtdeEstoque")] ProdutoCreateModel produto)
        {
            if (ModelState.IsValid)
            {
                // Validações

                var novoProduto = new Produto {
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Categoria = produto.Categoria,
                    Preco = produto.Preco,
                    QtdeEstoque = produto.QtdeEstoque ?? 0
                };

                _context.Add(novoProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,Categoria,Preco,QtdeEstoque,Id")] ProdutoEditModel produtoEditar)
        {
            // Validar campos
            if (string.IsNullOrEmpty(produtoEditar.Nome) || string.IsNullOrEmpty(produtoEditar.Categoria))
                throw new Exception("Por favor, preencha todos os campos.");
            if (produtoEditar.Preco <= 0)
                throw new Exception("Por favor, informe o preço do produto.");

            // buscar produto
            var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == produtoEditar.Id);
            if (produto == null)
                throw new Exception("O produto informado não foi encontrado.");

            // atualizar
            produto.Nome = produtoEditar.Nome == produto.Nome ? produto.Nome : produtoEditar.Nome;
            produto.Preco = produtoEditar.Preco == produto.Preco ? produto.Preco : produtoEditar.Preco;
            produto.QtdeEstoque = produtoEditar.QtdeEstoque == produto.QtdeEstoque ? produto.QtdeEstoque : produtoEditar.QtdeEstoque;
            produto.Descricao = produtoEditar.Descricao == produto.Descricao ? produto.Descricao : produtoEditar.Descricao;
            produto.Categoria = produtoEditar.Categoria == produto.Categoria ? produto.Nome : produtoEditar.Categoria;

            // save changes
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return View(produto);
        }
        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'LojaDbContext.Produtos'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
          return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
