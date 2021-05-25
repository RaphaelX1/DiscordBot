using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Threading.Tasks;

namespace Repositorio
{
    public partial class PersonagemRepositorio
    {

        public async Task CadastrarAtributo(PersonagemAtributoDTO personagemAtributoDTO)
        {
            var personagemAtributo = Converter<PersonagemAtributoDTO, PersonagemAtributo>(personagemAtributoDTO);

            _contexto.PersonagemAtributos.Add(personagemAtributo);
            await _contexto.SaveChangesAsync();
        }

        public async Task EditarAtributo(Guid personagemId, Guid atributoId, int valor)
        {
            var personagemAtributo = await _contexto.PersonagemAtributos
                 .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                     o.AtributoId.Equals(atributoId));

            personagemAtributo.Valor += valor;

            if (personagemAtributo != null)
                _contexto.Entry(personagemAtributo).State = EntityState.Modified;

            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirAtributo(Guid personagemId, Guid atributoId)
        {
            var personagemAtributo = await _contexto.PersonagemAtributos
                .FirstOrDefaultAsync(o => o.PersonagemId.Equals(personagemId) &&
                    o.AtributoId.Equals(atributoId));

            if (personagemAtributo != null)
                _contexto.Entry(personagemAtributo).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<PersonagemDTO> ObterAtributos(string nome)
        {
            var personagem = await _contexto.Personagens
                 .AsNoTracking()
                .Include(o => o.Atributos)
                    .ThenInclude(o => o.Atributo)
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Personagem, PersonagemDTO>(personagem);
        }
    }
}
