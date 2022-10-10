namespace Projeto.Loja.Entities
{
    public class Venda : BaseAudityEntity
    {
        public decimal VendaTotal { get; set; }
        public decimal QtdProduto { get; set; }
        public List<VendaProduto> Produtos { get; set; }
    }
}
