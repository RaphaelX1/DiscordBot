using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class PersonagemArcano
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PersonagemId { get; set; }

        public Personagem Personagem { get; set; }

        public Guid ArcanoId { get; set; }

        public Arcano Arcano { get; set; }
    }
}
