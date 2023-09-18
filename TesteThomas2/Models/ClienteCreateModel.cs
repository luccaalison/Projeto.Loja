using TesteThomas2.Entities;
using System.ComponentModel.DataAnnotations;

namespace TesteThomas2.Models
{
    public class ClienteCreateModel
    {
        [Required(ErrorMessage = "Por favor, informe o campo Nome.")]
        public string Nome { get; set; }
        public byte[]? Logotipo { get; set; }

        [Required(ErrorMessage = "Por favor, informe o E-mail.")]
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        public string Email { get; set; }

        public List<LogradouroDetailModel>? Logradouros { get; set; }
    }
}
