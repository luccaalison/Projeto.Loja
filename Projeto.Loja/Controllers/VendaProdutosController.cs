using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto.Loja.Data;
using Projeto.Loja.Entities;

namespace Projeto.Loja.Controllers
{
    public class VendaProdutosController : Controller
    {
        private readonly LojaDbContext _context;

        public VendaProdutosController(LojaDbContext context)
        {
            _context = context;
        }

        // GET: VendaProdutos
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendaProdutos.ToListAsync());
        }

        // GET: VendaProdutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VendaProdutos == null)
            {
                return NotFound();
            }

            var vendaProduto = await _context.VendaProdutos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendaProduto == null)
            {
                return NotFound();
            }

            return View(vendaProduto);
        }

        // GET: VendaProdutos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendaProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,VendaId,Quantidade,Preco,Id,Ativo,DataCriacao,DataAtualizacao")] VendaProduto vendaProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendaProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendaProduto);
        }

        // GET: VendaProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VendaProdutos == null)
            {
                return NotFound();
            }

            var vendaProduto = await _context.VendaProdutos.FindAsync(id);
            if (vendaProduto == null)
            {
                return NotFound();
            }
            return View(vendaProduto);
        }

        // POST: VendaProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,VendaId,Quantidade,Preco,Id,Ativo,DataCriacao,DataAtualizacao")] VendaProduto vendaProduto)
        {
            if (id != vendaProduto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendaProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaProdutoExists(vendaProduto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendaProduto);
        }

        // GET: VendaProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VendaProdutos == null)
            {
                return NotFound();
            }

            var vendaProduto = await _context.VendaProdutos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendaProduto == null)
            {
                return NotFound();
            }

            return View(vendaProduto);
        }

        // POST: VendaProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VendaProdutos == null)
            {
                return Problem("Entity set 'LojaDbContext.VendaProdutos'  is null.");
            }
            var vendaProduto = await _context.VendaProdutos.FindAsync(id);
            if (vendaProduto != null)
            {
                _context.VendaProdutos.Remove(vendaProduto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaProdutoExists(int id)
        {
            return _context.VendaProdutos.Any(e => e.Id == id);
        }
    }
}
