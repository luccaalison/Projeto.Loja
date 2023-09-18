using TesteThomas2.Entities;
using TesteThomas2.Models;

namespace TesteThomas2.Services.VendasService.Interfaces
{
    public interface IVendasService {

        Task<decimal> ObtemValorVenda(VendasCreateModel vendasCreateModel);
        Task<VendaDetailsModel> CriarVenda(VendasCreateModel vendasCreateModel);
        Task<List<VendaDetailsModel>> ListarVendas();
        Task<Produto> BuscarPorId(int Id);


    }
}
