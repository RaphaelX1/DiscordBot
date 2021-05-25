using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PersonagemVantagemDTO
    {
        public Guid Id { get; set; }

        public int Valor { get; set; }

        public Guid PersonagemId { get; set; }

        public PersonagemDTO Personagem { get; set; }

        public Guid VantagemId { get; set; }

        public VantagemDTO Vantagem { get; set; }
    }
}
