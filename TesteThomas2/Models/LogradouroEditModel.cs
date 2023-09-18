using TesteThomas2.Entities;

namespace TesteThomas2.Models
{
    public class LogradouroEditModel
    {
        public int Id { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string CEP { get; set; }

        public int ClienteId { get; set; }

        public Cliente cliente { get; set; }
    }
}

