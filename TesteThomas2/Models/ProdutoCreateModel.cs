using TesteThomas2.Entities;
using System.ComponentModel.DataAnnotations;

namespace TesteThomas2.Models
{
    public class ProdutoCreateModel
    {
        [Required(ErrorMessage = "Por favor, informe o campo Nome.")]
        public string Nome { get; set; }
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe o campo Categoria.")]
        public string? Categoria { get; set; }
        [Required(ErrorMessage = "Por favor, informe o campo preco.")]
        public decimal Preco { get; set; }
        public int? QtdeEstoque { get; set; }
    }
}
