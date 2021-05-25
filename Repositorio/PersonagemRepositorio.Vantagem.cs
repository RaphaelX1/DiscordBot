using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Threading.Tasks;

namespace Repositorio
{
    public partial class PersonagemRepositorio
    {

        public async Task CadastrarVantagem(PersonagemVantagemDTO personagemVantagemDTO)
        {
            var personagemVantagem = Converter<PersonagemVantagemDTO, PersonagemVantagem>(personagemVantagemDTO);

            _contexto.PersonagemVantagens.Add(personagemVantagem);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirVantagem(Guid personagemId, Guid vantagemId)
        {
            var personagemVantagem = await _contexto.PersonagemVantagens
                .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                    o.VantagemId.Equals(vantagemId));

            if (personagemVantagem != null)
                _contexto.Entry(personagemVantagem).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }


        public async Task<PersonagemDTO> ObterVantagens(string nome)
        {
            var personagem = await _contexto.Personagens
                 .AsNoTracking()
                .Include(o => o.Vantagens)
                    .ThenInclude(o => o.Vantagem)
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Personagem, PersonagemDTO>(personagem);
        }

    }
}
