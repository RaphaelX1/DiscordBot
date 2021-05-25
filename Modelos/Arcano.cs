using Enumeradores;
using System;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Arcano
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        public TipoArcano TipoArcano { get; set; }
    }
}
