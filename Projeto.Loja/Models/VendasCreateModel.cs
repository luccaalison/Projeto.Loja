using Projeto.Loja.Entities;

namespace Projeto.Loja.Models
{
    public class VendasCreateModel : Produto
    {
        public List<int> items { get; set; }
    }
}
