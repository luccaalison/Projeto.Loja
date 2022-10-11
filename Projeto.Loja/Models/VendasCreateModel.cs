using Projeto.Loja.Entities;


namespace Projeto.Loja.Models
{
    public class VendasCreateModel 
    {
        public decimal VendaTotal { get; set; }
        public decimal QtdProduto { get; set; }
        public List<int> items { get; set; }
    }
}
