using Microsoft.AspNetCore.Mvc;
using TesteThomas2.Models;
using TesteThomas2.Services.ClienteService.Interfaces;

namespace TesteThomas2.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClientesService _clienteService;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger, IClientesService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarNovoCliente([FromBody] ClienteCreateModel clienteCadastroDto)
        {

            try
            {
                var resp = await _clienteService.CadastrarNovoCliente(clienteCadastroDto);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("editar")]
        public async Task<IActionResult> Editarcliente([FromBody] ClienteEditModel clienteEditarDto)
        {
            try
            {
                var resp = await _clienteService.EditarCliente(clienteEditarDto);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Listar")]
        public async Task<IActionResult> Listarclientes()
        {
            try
            {
                var resp = await _clienteService.ListarClientes();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("detalhes/{id}")]
        public async Task<IActionResult> Listarclientes([FromRoute] int Id)
        {
            try
            {
                var resp = await _clienteService.BuscarPorId(Id);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
    