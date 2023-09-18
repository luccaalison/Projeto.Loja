using TesteThomas2.Entities;
using System.ComponentModel.DataAnnotations;

namespace TesteThomas2.Models
{
    public class VendaDetailsModel 
    {
        public decimal ValorTotal { get; set; }
        public int TotalItems { get; set; }
        public List<VendaProdutoDetailsModel> Items { get; set; }
    }
}
