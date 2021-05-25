using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class PersonagemAtributo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Valor { get; set; }

        public Guid PersonagemId { get; set; }

        public Personagem Personagem { get; set; }

        public Guid AtributoId { get; set; }

        public Atributo Atributo { get; set; }
    }
}
