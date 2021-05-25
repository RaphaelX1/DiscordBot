using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class Personagem
    {
        public Personagem()
        {
            Arcanos = new List<PersonagemArcano>();
            Atributos = new List<PersonagemAtributo>();
            Periciais = new List<PersonagemPericia>();
            Vantagens = new List<PersonagemVantagem>();
            Formacoes = new List<PersonagemFormacao>();
            Efeitos = new List<PersonagemEfeito>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public int Fortuna { get; set; }

        public int Ferimentos { get; set; }

        public int FerimentosDramaticos { get; set; }

        public string Bio { get; set; }

        public ICollection<PersonagemArcano> Arcanos { get; set; }

        public ICollection<PersonagemAtributo> Atributos { get; set; }

        public ICollection<PersonagemPericia> Periciais { get; set; }

        public ICollection<PersonagemVantagem> Vantagens { get; set; }

        public ICollection<PersonagemFormacao> Formacoes { get; set; }

        public ICollection<PersonagemEfeito> Efeitos { get; set; }

        #region foreign key

        public Guid ReligiaoId { get; set; }

        public Religiao Religiao { get; set; }

        public Guid NacaoId { get; set; }

        public Nacao Nacao { get; set; }

        #endregion
    }
}
