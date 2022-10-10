using Projeto.Loja.Entities;
using Projeto.Loja.Models;

namespace Projeto.Loja.Services.VendasService.Interfaces
{
    public interface IVendasService {
        Task<VendaDetailsModel> CriarVenda(VendasCreateModel vendasCreateModel);
        Task<List<VendaDetailsModel>> ListarVendas();
        Task<Produto> BuscarPorId(int Id);


    }
}
