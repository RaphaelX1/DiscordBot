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
    public class AtributoRepositorio: BaseRepositorio
    {
        public AtributoRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper): base(aplicacaoContexto, mapper)
        {

        }
        public async Task<AtributoDTO> Obter(Guid Id)
        {
            var atributo = await _contexto.Atributos
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Atributo, AtributoDTO>(atributo);
        }

        public async Task<AtributoDTO> Obter(string nome)
        {
            var atributo = await _contexto.Atributos
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Atributo, AtributoDTO>(atributo);
        }

        public async Task<Guid> ObterId(string nome)
        {
            var atributoId = await _contexto.Atributos
                .Where(o => o.Nome.ToLower().Equals(nome.ToLower()))
                .Select(o => o.Id).FirstOrDefaultAsync();

            return atributoId;
        }


        public async Task<List<AtributoDTO>> ObterTodos()
        {
            var atributos = await _contexto.Atributos
                .ToListAsync();

            return Converter<List<Atributo>, List<AtributoDTO>>(atributos);
        }

        public async Task Cadastrar(AtributoDTO atributoDTO)
        {
            var atributo = Converter<AtributoDTO, Atributo>(atributoDTO);

            _contexto.Atributos.Add(atributo);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(AtributoDTO atributoDTO)
        {
            var atributo = await _contexto.Atributos
                .FirstOrDefaultAsync(o => o.Id == atributoDTO.Id);

            Converter(atributoDTO, atributo);

            _contexto.Entry(atributo).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome)
        {
            var atributo = await _contexto.Atributos
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            if (atributo != null)
                _contexto.Entry(atributo).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistente(string nome)
        {
            return await _contexto.Atributos
                .AnyAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

        }
    }
}
