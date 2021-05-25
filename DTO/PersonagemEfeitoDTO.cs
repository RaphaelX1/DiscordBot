using System;


namespace DTO
{
    public class PersonagemEfeitoDTO
    {
        public Guid Id { get; set; }

        public int Valor { get; set; }

        public Guid PersonagemId { get; set; }

        public PersonagemDTO Personagem { get; set; }

        public Guid EfeitoId { get; set; }

        public EfeitoDTO Efeito { get; set; }
    }
}
