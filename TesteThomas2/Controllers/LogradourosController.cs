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
using TesteThomas2.Services.LogradouroService;
using TesteThomas2.Services.LogradouroService.Interfaces;


namespace TesteThomas2.Controllers
{
    public class LogradourosController : Controller
    {
        private readonly LojaDbContext _context;
        private readonly ILogradourosService _logradouroService;

        public LogradourosController(LojaDbContext context, ILogradourosService logradourosService)
        {
            _logradouroService = logradourosService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Logradouros.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Logradouros == null)
            {
                return NotFound();  
            }

            var endereco = await _context.Logradouros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Endereco,Cidade,Estado,CEP,ClienteId,cliente")] LogradouroDetailModel endereco)
        {

            if (ModelState.IsValid)
            {
                var novoEndereco = new Logradouro
                {
                    Endereco = endereco.Endereco,
                    cliente = endereco.Cliente,
                    CEP = endereco.CEP,
                    Cidade = endereco.Cidade,
                    ClienteId = endereco.ClienteId,
                    Estado = endereco.Estado
                };
                _context.Add(novoEndereco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Logradouros == null)
            {
                return NotFound();
            }

            var endereco = await _context.Logradouros.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }
            return View(endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Endereco,Cidade,Estado,CEP,ClienteId,cliente")] LogradouroEditModel enderecoEditar)
        {
            var endereco = await _context.Logradouros.FirstOrDefaultAsync(x => x.Id == enderecoEditar.Id);
            if (endereco == null)
                throw new Exception("O endereço informado não foi encontrado.");

            // atualizar
            endereco.Endereco = enderecoEditar.Endereco == endereco.Endereco ? endereco.Endereco : enderecoEditar.Endereco;
            endereco.Cidade = enderecoEditar.Cidade == endereco.Cidade ? endereco.Cidade : enderecoEditar.Cidade;
            endereco.Estado = enderecoEditar.Estado == endereco.Estado ? endereco.Estado : enderecoEditar.Estado;
            endereco.CEP = enderecoEditar.CEP == endereco.CEP ? endereco.CEP : enderecoEditar.CEP;
            endereco.ClienteId = enderecoEditar.ClienteId == endereco.ClienteId ? endereco.ClienteId : enderecoEditar.ClienteId;
            endereco.cliente = enderecoEditar.cliente == endereco.cliente ? endereco.cliente : enderecoEditar.cliente;

            // save changes
            _context.Logradouros.Update(endereco);
            await _context.SaveChangesAsync();

            return View(endereco);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Logradouros == null)
            {
                return NotFound();
            }

            var endereco = await _context.Logradouros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Logradouros == null)
            {
                return Problem("Entity set 'LojaDbContext.Logradouros'  is null.");
            }
            var endereco = await _context.Logradouros.FindAsync(id);
            if (endereco != null)
            {
                _context.Logradouros.Remove(endereco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Existe(int id)
        {
            return _context.Logradouros.Any(e => e.Id == id);
        }
    }
}
