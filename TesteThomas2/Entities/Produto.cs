namespace TesteThomas2.Entities
{
    public class Produto : BaseAudityEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public int QtdeEstoque { get; set; }
        public List<VendaProduto> VendaProduto { get; set; }
    }
}
