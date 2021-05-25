using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class PersonagemEfeito
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Valor { get; set; }

        public Guid PersonagemId { get; set; }

        public Personagem Personagem { get; set; }

        public Guid EfeitoId { get; set; }

        public Efeito Efeito { get; set; }
    }
}
