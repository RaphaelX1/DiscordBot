using Enumeradores;
using System;


namespace DTO
{
    public class EfeitoCaracteristicaDTO
    {
        public Guid Id { get; set; }

        public Guid EfeitoId { get; set; }

        public EfeitoDTO Efeito { get; set; }

        public TipoCaracteristica TipoCaracteristica { get; set; }
    }
}
