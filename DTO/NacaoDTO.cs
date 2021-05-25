using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class NacaoDTO
    {
        public NacaoDTO()
        {
            Habitantes = new List<PersonagemDTO>();
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public ICollection<PersonagemDTO> Habitantes { get; set; }
    }
}
