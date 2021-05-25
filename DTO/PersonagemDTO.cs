using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PersonagemDTO
    {
        public PersonagemDTO()
        {
            Arcanos = new List<PersonagemArcanoDTO>();
            Atributos = new List<PersonagemAtributoDTO>();
            Periciais = new List<PersonagemPericiaDTO>();
            Vantagens = new List<PersonagemVantagemDTO>();
            Formacoes = new List<PersonagemFormacaoDTO>();
            Efeitos = new List<PersonagemEfeitoDTO>();
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int Fortuna { get; set; }

        public int Ferimentos { get; set; }

        public int FerimentosDramaticos { get; set; }

        public string Bio { get; set; }


        public ICollection<PersonagemArcanoDTO> Arcanos { get; set; }

        public ICollection<PersonagemAtributoDTO> Atributos { get; set; }

        public ICollection<PersonagemPericiaDTO> Periciais { get; set; }

        public ICollection<PersonagemVantagemDTO> Vantagens { get; set; }

        public ICollection<PersonagemFormacaoDTO> Formacoes { get; set; }

        public ICollection<PersonagemEfeitoDTO> Efeitos { get; set; }

        #region foreign key

        public Guid ReligiaoId { get; set; }

        public ReligiaoDTO Religiao { get; set; }

        public Guid NacaoId { get; set; }

        public NacaoDTO Nacao { get; set; }

        #endregion
    }
}
