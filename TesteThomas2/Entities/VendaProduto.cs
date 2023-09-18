namespace TesteThomas2.Entities
{
    public class VendaProduto : BaseAudityEntity
    {
        public int ProdutoId { get; set; }
        public int VendaId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public List<Venda> Vendas { get; set; }
    }
}
