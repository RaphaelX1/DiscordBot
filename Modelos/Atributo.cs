using System;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Atributo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}
