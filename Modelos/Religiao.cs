using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class Religiao
    {
        public Religiao()
        {
            Fieis = new List<Personagem>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public ICollection<Personagem> Fieis { get; set; }
    }
}
