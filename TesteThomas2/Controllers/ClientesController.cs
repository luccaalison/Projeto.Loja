﻿using System;
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
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Categoria,Preco,QtdeEstoque")] ClienteCreateModel cliente)
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,Categoria,Preco,QtdeEstoque,Id")] ClienteEditModel clienteEditar)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return View();
        }

        private bool Existe(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
