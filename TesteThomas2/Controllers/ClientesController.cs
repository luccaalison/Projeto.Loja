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
using TesteThomas2.Services.ClienteService;
using TesteThomas2.Services.ClienteService.Interfaces;


namespace TesteThomas2.Controllers
{
    public class ClientesController : Controller
    {
        private readonly LojaDbContext _context;
        private readonly IClientesService _clienteService;

        public ClientesController(LojaDbContext context, IClientesService clientesService)
        {
            _clienteService = clientesService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email,Logotipo,Logradouros")] ClienteCreateModel cliente)
        {
            if (ModelState.IsValid)
            {
                var novoCliente = new Cliente
                {
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    Logotipo = cliente.Logotipo ?? null
                };
                _context.Add(novoCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Email,Logotipo,Logradouros")] ClienteEditModel clienteEditar)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == clienteEditar.Id);
            if (cliente == null)
                throw new Exception("O cliente informado não foi encontrado.");


            // atualizar
            cliente.Nome = clienteEditar.Nome == cliente.Nome ? cliente.Nome : clienteEditar.Nome;
            cliente.Email = clienteEditar.Email == cliente.Email ? cliente.Email : clienteEditar.Email;
            cliente.Logotipo = clienteEditar.Logotipo == cliente.Logotipo ? cliente.Logotipo : clienteEditar.Logotipo;
            cliente.Logradouros = clienteEditar.Logradouros == cliente.Logradouros ? cliente.Logradouros : clienteEditar.Logradouros;

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return View(cliente);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if(cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.Clientes == null)
            {
                return Problem("Entidade 'LojaDbContext.Clientes' é nulo");
            }
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes != null)
            {
                _context.Clientes.Remove(clientes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Existe(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
