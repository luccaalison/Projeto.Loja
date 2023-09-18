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
    public class ClientesController : Controller
    {
        private readonly LojaDbContext _context;
        private readonly IVendasService _vendasService;

        public ClientesController(LojaDbContext context, IVendasService vendasService, IProdutoService produtoService)
        {
            _vendasService = vendasService;
            _context = context;
        }

    }
}
