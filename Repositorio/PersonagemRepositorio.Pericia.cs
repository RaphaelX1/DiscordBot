using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Threading.Tasks;

namespace Repositorio
{
    public partial class PersonagemRepositorio
    {

        public async Task CadastrarPericia(PersonagemPericiaDTO personagemPericiaDTO)
        {
            var personagemPericia = Converter<PersonagemPericiaDTO, PersonagemPericia>(personagemPericiaDTO);

            _contexto.PersonagemPericias.Add(personagemPericia);
            await _contexto.SaveChangesAsync();
        }

        public async Task EditarPericia(Guid personagemId, Guid periciaId, int valor)
        {
            var personagemPericia = await _contexto.PersonagemPericias
                 .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                     o.PericiaId.Equals(periciaId));

            personagemPericia.Valor += valor;

            if (personagemPericia != null)
                _contexto.Entry(personagemPericia).State = EntityState.Modified;

            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirPericia(Guid personagemId, Guid periciaId)
        {
            var personagemPericia = await _contexto.PersonagemPericias
                .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                    o.PericiaId.Equals(periciaId));

            if (personagemPericia != null)
                _contexto.Entry(personagemPericia).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<PersonagemDTO> ObterPericia(string nome)
        {
            var personagem = await _contexto.Personagens
                 .AsNoTracking()
                .Include(o => o.Periciais)
                    .ThenInclude(o => o.Pericia)
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Personagem, PersonagemDTO>(personagem);
        }
    }
}
