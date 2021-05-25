using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PersonagemFormacaoDTO
    {
        public Guid Id { get; set; }

        public Guid PersonagemId { get; set; }

        public PersonagemDTO Personagem { get; set; }

        public Guid FormacaoId { get; set; }

        public FormacaoDTO Formacao { get; set; }
    }
}
