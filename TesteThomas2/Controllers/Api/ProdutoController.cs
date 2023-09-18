using Microsoft.AspNetCore.Mvc;
using TesteThomas2.Models;
using TesteThomas2.Services.EstoqueService.Interfaces;

namespace TesteThomas2.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoService produtoService) {
            _logger = logger;
            _produtoService = produtoService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarNovoProduto([FromBody] ProdutoCreateModel produtoCadastroDto) {
            
            try {
                var resp = await _produtoService.CadastrarNovoProduto(produtoCadastroDto);
                return Ok(resp);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("editar")]
        public async Task<IActionResult> EditarProduto([FromBody] ProdutoEditModel produtoEditarDto) {
            try {
                var resp = await _produtoService.EditarProduto(produtoEditarDto);
                return Ok(resp);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarProdutos() {
            try {
                var resp = await _produtoService.ListarProdutos();
                return Ok(resp);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("detalhes/{id}")]
        public async Task<IActionResult> ListarProdutos([FromRoute] int Id) {
            try {
                var resp = await _produtoService.BuscarPorId(Id);
                return Ok(resp);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
