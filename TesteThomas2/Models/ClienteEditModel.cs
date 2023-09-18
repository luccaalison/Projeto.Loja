using TesteThomas2.Entities;
using System.ComponentModel.DataAnnotations;

namespace TesteThomas2.Models
{
    public class ClienteEditModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[]? Logotipo { get; set; }
        public string Email { get; set; }
        public List<LogradouroDetailModel> Logradouro { get; set; }
        public ICollection<Logradouro> Logradouros { get; internal set; }
    }
}
