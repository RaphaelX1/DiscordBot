using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PersonagemAtributoDTO
    {
        public Guid Id { get; set; }

        public int Valor { get; set; }

        public Guid PersonagemId { get; set; }

        public PersonagemDTO Personagem { get; set; }

        public Guid AtributoId { get; set; }

        public AtributoDTO Atributo { get; set; }
    }
}
