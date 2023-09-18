using TesteThomas2.Entities;
using TesteThomas2.Models;

namespace TesteThomas2.Services.ClienteService.Interfaces
{
    public interface IClientesService
    {
        Task<List<Cliente>> ListarClientes();
        Task<Cliente> BuscarPorId(int id);
        Task<Cliente> CadastrarNovoCliente(ClienteCreateModel clienteCadastro);
        Task<Cliente> EditarCliente(ClienteEditModel clienteEditar);
        Task Excluir(Cliente ClienteEditar, int Id);
    }
}
