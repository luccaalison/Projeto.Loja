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
using TesteThomas2.Services.EstoqueService.Interfaces;
using TesteThomas2.Services.VendasService.Interfaces;

namespace TesteThomas2.Controllers
{
    public class VendasController : Controller
    {
        private readonly LojaDbContext _context;
        private readonly IVendasService _vendasService;
        private readonly IProdutoService _produtoService;

        public VendasController(LojaDbContext context, IVendasService vendasService, IProdutoService produtoService) {
            _context = context;
            _vendasService = vendasService;
            _produtoService = produtoService;
        }

        // GET: Vendas
        public async Task<IActionResult> Index() {
            return View(await _context.Vendas.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Vendas == null) {
                return NotFound();
            }

            var venda = await _context.Vendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null) {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public async Task<IActionResult> Create() {
            VendasCreateModel venda = new VendasCreateModel();
            ViewBag.produtos = await _produtoService.ListarProdutos();
            return View(venda);
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendaTotal,QtdProduto")] VendasCreateModel venda, [FromForm] string itemAdd) {
            try {
                venda.items = itemAdd.Split(",")?.Select(Int32.Parse)?.ToList();
                ViewBag.produtos = await _produtoService.ListarProdutos();
                ViewBag.ItemsAdd = itemAdd;
                if (venda.VendaTotal <= 0) {
                    venda.VendaTotal = await _vendasService.ObtemValorVenda(venda);
                    return View(venda);
                }
                else {
                    var resp = await _vendasService.CriarVenda(venda);
                }
                return Redirect("Index");
            }
            catch (Exception ex) {
                ViewBag.Erro = ex.Message;
                return View(venda);
            }
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Vendas == null) {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null) {
                return NotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendaTotal,QtdProduto,Id,Ativo,DataCriacao,DataAtualizacao")] Venda venda) {
            if (id != venda.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!VendaExists(venda.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Vendas == null) {
                return NotFound();
            }

            var venda = await _context.Vendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null) {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Vendas == null) {
                return Problem("Entity set 'LojaDbContext.Vendas'  is null.");
            }
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null) {
                _context.Vendas.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id) {
            return _context.Vendas.Any(e => e.Id == id);
        }
    }
}
