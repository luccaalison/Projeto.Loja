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
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Endereco,Cidade,Estado,CEP,ClienteId,cliente")] LogradouroEditModel enderecoEditar)
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
            return _context.Logradouros.Any(e => e.Id == id);
        }
    }
}
