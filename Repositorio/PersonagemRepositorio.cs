using AutoMapper;
using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public partial class PersonagemRepositorio : BaseRepositorio
    {
        public PersonagemRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper) : base(aplicacaoContexto, mapper)
        {

        }

        public async Task<PersonagemDTO> Obter(Guid Id)
        {
            var personagem = await _contexto.Personagens
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Personagem, PersonagemDTO>(personagem);
        }

        public async Task<PersonagemDTO> Obter(string nome)
        {
            var personagem = await _contexto.Personagens
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Personagem, PersonagemDTO>(personagem);
        }

        public async Task<Guid> ObterId(string nome)
        {
            var personagemId = await _contexto.Personagens
                .Where(o => o.Nome.ToLower().Equals(nome.ToLower()))
                .Select(o => o.Id).FirstOrDefaultAsync();

            return personagemId;
        }

        public async Task<PersonagemDTO> ObterCompleto(string nome)
        {
            var personagem = await _contexto.Personagens
                .AsNoTracking()
                .Include(o => o.Arcanos)
                    .ThenInclude(o => o.Arcano)
                .Include(o => o.Atributos)
                    .ThenInclude(o => o.Atributo)
                 .Include(o => o.Formacoes)
                    .ThenInclude(o => o.Formacao)
                  .Include(o => o.Efeitos)
                    .ThenInclude(o => o.Efeito)
                  .Include(o => o.Vantagens)
                    .ThenInclude(o => o.Vantagem)
                  .Include(o => o.Periciais)
                    .ThenInclude(o => o.Pericia)
                .Include(o => o.Nacao)
                .Include(o => o.Religiao)
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));


            return Converter<Personagem, PersonagemDTO>(personagem);
        }


        public async Task<List<PersonagemDTO>> ObterTodos()
        {
            var personagens = await _contexto.Personagens
                .ToListAsync();

            return Converter<List<Personagem>, List<PersonagemDTO>>(personagens);
        }

        public async Task Cadastrar(PersonagemDTO personagemDTO)
        {
            var personagem = Converter<PersonagemDTO, Personagem>(personagemDTO);

            _contexto.Personagens.Add(personagem);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(PersonagemDTO personagemDTO)
        {
            var personagem = await _contexto.Personagens
                .FirstOrDefaultAsync(o => o.Id == personagemDTO.Id);

            personagem.Fortuna = personagemDTO.Fortuna;
            personagem.Ferimentos = personagemDTO.Ferimentos;
            personagem.FerimentosDramaticos = personagemDTO.FerimentosDramaticos;
            personagem.NacaoId = personagemDTO.NacaoId;
            personagem.ReligiaoId = personagemDTO.ReligiaoId;

            if (personagem != null)
                _contexto.Entry(personagem).State = EntityState.Modified;

            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome)
        {
            var personagem = await _contexto.Personagens
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            if (personagem != null)
                _contexto.Entry(personagem).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

      
        public async Task<bool> VerificarExistente(string nome)
        {
            return await _contexto.Personagens
                .AnyAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));
        }
    }
}
