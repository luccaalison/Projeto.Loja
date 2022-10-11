using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Projeto.Loja.Entities;
using Projeto.Loja.Models;
using Projeto.Loja.Service.EstoqueService;
using Projeto.Loja.Services.EstoqueService.Interfaces;
using Projeto.Loja.Services.VendasService.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Projeto.Loja.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly IVendasService _vendasService;
        private readonly ILogger<VendaController> _logger;

        public VendaController(ILogger<VendaController> logger, IVendasService vendaService) {
            _logger = logger;
            _vendasService = vendaService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarNovaVenda([FromBody] VendasCreateModel vendasCreate) {
            try {
                var resp = await _vendasService.CriarVenda(vendasCreate);
                return Ok(resp);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarVendas() {
            try {
                var resp = await _vendasService.ListarVendas();
                return Ok(resp);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("detalhes/{id}")]
        public async Task<IActionResult> ListarVendas([FromRoute] int Id) {
            try {
                var resp = await _vendasService.BuscarPorId(Id);
                return Ok(resp);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
