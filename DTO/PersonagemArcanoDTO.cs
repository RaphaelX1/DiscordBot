using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PersonagemArcanoDTO
    {
        public Guid Id { get; set; }

        public Guid PersonagemId { get; set; }

        public PersonagemDTO Personagem { get; set; }

        public Guid ArcanoId { get; set; }

        public ArcanoDTO Arcano { get; set; }
    }
}
