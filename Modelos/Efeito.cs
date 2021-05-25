using Enumeradores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Efeito
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

    }
}
