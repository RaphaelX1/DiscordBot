using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }
    }
}
