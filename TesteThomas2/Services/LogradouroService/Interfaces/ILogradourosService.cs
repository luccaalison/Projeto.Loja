
using TesteThomas2.Entities;
using TesteThomas2.Models;

namespace TesteThomas2.Services.LogradouroService.Interfaces
{
    public interface ILogradourosService
    {
        Task<List<Logradouro>> ListarEnderecos();
        Task<Logradouro> BuscarPorId(int id);
        Task<Logradouro> CadastrarNovoEndereco(LogradouroDetailModel enderecoCadastro);
        Task<Logradouro> EditarEndereco(LogradouroEditModel enderecoEditar);
        Task Excluir(Logradouro enderecoEditar, int ClienteId);
    }
}
