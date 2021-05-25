using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class PersonagemVantagem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Valor { get; set; }

        public Guid PersonagemId { get; set; }

        public Personagem Personagem { get; set; }

        public Guid VantagemId { get; set; }

        public Vantagem Vantagem { get; set; }
    }
}
