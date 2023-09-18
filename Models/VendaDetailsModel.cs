using Projeto.Loja.Entities;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Loja.Models
{
    public class VendaDetailsModel 
    {
        public decimal ValorTotal { get; set; }
        public int TotalItems { get; set; }
        public List<VendaProdutoDetailsModel> Items { get; set; }
    }
}
