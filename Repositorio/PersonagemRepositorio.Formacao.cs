using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Threading.Tasks;

namespace Repositorio
{
    public partial class PersonagemRepositorio
    {

        public async Task CadastrarFormacao(PersonagemFormacaoDTO personagemFormacaoDTO)
        {
            var personagemFormacao = Converter<PersonagemFormacaoDTO, PersonagemFormacao>(personagemFormacaoDTO);

            _contexto.PersonagemFormacoes.Add(personagemFormacao);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirFormacao(Guid personagemId, Guid formacaoId)
        {
            var personagemFormacao = await _contexto.PersonagemFormacoes
                .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                    o.FormacaoId.Equals(formacaoId));

            if (personagemFormacao != null)
                _contexto.Entry(personagemFormacao).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }


        public async Task<PersonagemDTO> ObterFormacoes(string nome)
        {
            var personagem = await _contexto.Personagens
                 .AsNoTracking()
                .Include(o => o.Formacoes)
                    .ThenInclude(o => o.Formacao)
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Personagem, PersonagemDTO>(personagem);
        }

    }
}
