using System.ComponentModel.DataAnnotations;

namespace TesteThomas2.Entities
{
    public class Logradouro
    {
        [Key]
        public int Id { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string CEP { get; set; }

        // Chave estrangeira referenciando o cliente a que pertence
        public int ClienteId { get; set; }

        // Navegação para o cliente pai
        public Cliente Cliente { get; set; }
    }
}
