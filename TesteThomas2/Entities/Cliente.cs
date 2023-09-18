using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TesteThomas2.Entities
{
    public class Cliente : BaseAudityEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public byte[]? Logotipo { get; set; }

        // Relacionamento com Logradouros (um cliente pode ter vários logradouros)
        public ICollection<Logradouro> Logradouros { get; set; }
    }
}
