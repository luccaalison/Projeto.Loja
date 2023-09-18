using Microsoft.AspNetCore.Mvc;
using TesteThomas2.Models;
using TesteThomas2.Services.LogradouroService.Interfaces;

namespace TesteThomas2.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class LogradouroController : ControllerBase
    {
        private readonly ILogradourosService _logradouroService;
        private readonly ILogger<LogradouroController> _logger;

        public LogradouroController(ILogger<LogradouroController> logger, ILogradourosService logradouroService)
        {
            _logger = logger;
            _logradouroService = logradouroService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarNovoEndereco([FromBody] LogradouroDetailModel enderecoCadastroDto)
        {

            try
            {
                var resp = await _logradouroService.CadastrarNovoEndereco(enderecoCadastroDto);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("editar")]
        public async Task<IActionResult> EditarEndereco([FromBody] LogradouroEditModel enderecoEditarDto)
        {
            try
            {
                var resp = await _logradouroService.EditarEndereco(enderecoEditarDto);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Listar")]
        public async Task<IActionResult> ListarEnderecos()
        {
            try
            {
                var resp = await _logradouroService.ListarEnderecos();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("detalhes/{id}")]
        public async Task<IActionResult> ListarEnderecos([FromRoute] int Id)
        {
            try
            {
                var resp = await _logradouroService.BuscarPorId(Id);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
