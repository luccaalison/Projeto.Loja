namespace TesteThomas2.Models
{
    public class ProdutoEditModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QtdeEstoque { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
    }
}
