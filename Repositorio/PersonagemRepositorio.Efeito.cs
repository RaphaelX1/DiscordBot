using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Threading.Tasks;

namespace Repositorio
{
    public partial class PersonagemRepositorio
    {

        public async Task CadastrarEfeito(PersonagemEfeitoDTO personagemEfeitoDTO)
        {
            var personagemEfeito = Converter<PersonagemEfeitoDTO, PersonagemEfeito>(personagemEfeitoDTO);

            _contexto.PersonagemEfeitos.Add(personagemEfeito);
            await _contexto.SaveChangesAsync();
        }

        public async Task EditarEfeito(Guid personagemId, Guid efeitoId, int valor)
        {
            var personagemEfeito = await _contexto.PersonagemEfeitos
                 .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                     o.EfeitoId.Equals(efeitoId));

            personagemEfeito.Valor += valor;

            if (personagemEfeito != null)
                _contexto.Entry(personagemEfeito).State = EntityState.Modified;

            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirEfeito(Guid personagemId, Guid efeitoId)
        {
            var personagemEfeito = await _contexto.PersonagemEfeitos
                .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                    o.EfeitoId.Equals(efeitoId));

            if (personagemEfeito != null)
                _contexto.Entry(personagemEfeito).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<PersonagemDTO> ObterEfeitos(string nome)
        {
            var personagem = await _contexto.Personagens
                 .AsNoTracking()
                .Include(o => o.Efeitos)
                    .ThenInclude(o => o.Efeito)
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Personagem, PersonagemDTO>(personagem);
        }
    }
}
