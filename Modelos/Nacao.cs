using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class Nacao
    {
        public Nacao()
        {
            Habitantes = new List<Personagem>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        public ICollection<Personagem> Habitantes { get; set; }
    }
}
