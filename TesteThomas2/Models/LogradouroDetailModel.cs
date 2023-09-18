using TesteThomas2.Entities;

namespace TesteThomas2.Models
{
    public class LogradouroDetailModel
    {
        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string CEP { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }
    }
}
