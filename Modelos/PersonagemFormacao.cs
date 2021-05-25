using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class PersonagemFormacao
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PersonagemId { get; set; }

        public Personagem Personagem { get; set; }

        public Guid FormacaoId { get; set; }

        public Formacao Formacao { get; set; }
    }
}
