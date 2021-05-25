using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Threading.Tasks;

namespace Repositorio
{
    public partial class PersonagemRepositorio
    {

        public async Task CadastrarArcano(PersonagemArcanoDTO personagemArcanoDTO)
        {
            var personagemArcano = Converter<PersonagemArcanoDTO, PersonagemArcano>(personagemArcanoDTO);

            _contexto.PersonagemArcanos.Add(personagemArcano);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirArcano(Guid personagemId, Guid arcanoId)
        {
            var personagemArcano = await _contexto.PersonagemArcanos
                .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                    o.ArcanoId.Equals(arcanoId));

            if (personagemArcano != null)
                _contexto.Entry(personagemArcano).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<PersonagemDTO> ObterArcanos(string nome)
        {
            var personagem = await _contexto.Personagens
                .AsNoTracking()
                .Include(o => o.Arcanos)
                    .ThenInclude(o => o.Arcano)
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Personagem, PersonagemDTO>(personagem);
        }
  
    }
}
