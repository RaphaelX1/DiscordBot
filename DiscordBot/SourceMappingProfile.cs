using AutoMapper;
using DTO;
using Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servicos
{
    public class SourceMappingProfile: Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<Nacao, NacaoDTO>();
            CreateMap<Arcano, ArcanoDTO>();
            CreateMap<Atributo, AtributoDTO>();
            CreateMap<Efeito, EfeitoDTO>();
            CreateMap<Formacao, FormacaoDTO>();
            CreateMap<Nacao, NacaoDTO>();
            CreateMap<Pericia, PericiaDTO>();
            CreateMap<PersonagemArcano, PersonagemArcanoDTO>();
            CreateMap<PersonagemAtributo, PersonagemAtributoDTO>();
            CreateMap<Personagem, PersonagemDTO>();
            CreateMap<PersonagemEfeito, PersonagemEfeitoDTO>();
            CreateMap<PersonagemFormacao, PersonagemFormacaoDTO>();
            CreateMap<PersonagemPericia, PersonagemPericiaDTO>();
            CreateMap<PersonagemVantagem, PersonagemVantagemDTO>();
            CreateMap<Religiao, ReligiaoDTO>();
            CreateMap<Vantagem, VantagemDTO>();

            CreateMap<NacaoDTO, Nacao>();
            CreateMap<ArcanoDTO, Arcano>();
            CreateMap<AtributoDTO, Atributo>();
            CreateMap<EfeitoDTO, Efeito>();
            CreateMap<FormacaoDTO, Formacao>();
            CreateMap<NacaoDTO, Nacao>();
            CreateMap<PericiaDTO, Pericia>();
            CreateMap<PersonagemArcanoDTO, PersonagemArcano>();
            CreateMap<PersonagemAtributoDTO, PersonagemAtributo>();
            CreateMap<PersonagemDTO, Personagem>();
            CreateMap<PersonagemEfeitoDTO, PersonagemEfeito>();
            CreateMap<PersonagemFormacaoDTO, PersonagemFormacao>();
            CreateMap<PersonagemPericiaDTO, PersonagemPericia>();
            CreateMap<PersonagemVantagemDTO, PersonagemVantagem>();
            CreateMap<ReligiaoDTO, Religiao>();
            CreateMap<VantagemDTO, Vantagem>();

            CreateMap<Nacao, Nacao>();
            CreateMap<Arcano, Arcano>();
            CreateMap<Atributo, Atributo>();
            CreateMap<Efeito, Efeito>();
            CreateMap<Formacao, Formacao>();
            CreateMap<Nacao, Nacao>();
            CreateMap<Pericia, Pericia>();
            CreateMap<PersonagemArcano, PersonagemArcano>();
            CreateMap<PersonagemAtributo, PersonagemAtributo>();
            CreateMap<Personagem, Personagem>();
            CreateMap<PersonagemEfeito, PersonagemEfeito>();
            CreateMap<PersonagemFormacao, PersonagemFormacao>();
            CreateMap<PersonagemPericia, PersonagemPericia>();
            CreateMap<PersonagemVantagem, PersonagemVantagem>();
            CreateMap<Religiao, Religiao>();
            CreateMap<Vantagem, Vantagem>();
        }
               
    }
}
