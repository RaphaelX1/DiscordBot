using AutoMapper;
using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class VantagemRepositorio: BaseRepositorio
    {
        public VantagemRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper): base(aplicacaoContexto, mapper)
        {

        }

        public async Task<VantagemDTO> Obter(Guid Id)
        {
            var vantagem = await _contexto.Vantagens
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Vantagem, VantagemDTO>(vantagem);
        }

        public async Task<VantagemDTO> Obter(string nome)
        {
            var vantagem = await _contexto.Vantagens
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Vantagem, VantagemDTO>(vantagem);
        }


        public async Task<List<VantagemDTO>> ObterTodos()
        {
            var vantagens = await _contexto.Vantagens
                .ToListAsync();

            return Converter<List<Vantagem>, List<VantagemDTO>>(vantagens);
        }

        public async Task Cadastrar(VantagemDTO vantagemDTO)
        {
            var vantagem = Converter<VantagemDTO, Vantagem>(vantagemDTO);

            _contexto.Vantagens.Add(vantagem);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(VantagemDTO vantagemDTO)
        {
            var vantagem = await _contexto.Vantagens
                .FirstOrDefaultAsync(o => o.Id == vantagemDTO.Id);

            Converter(vantagemDTO, vantagem);

            _contexto.Entry(vantagem).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome)
        {
            var vantagem = await _contexto.Vantagens
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            if (vantagem != null)
                _contexto.Entry(vantagem).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistente(string nome)
        {
            return await _contexto.Vantagens
                .AnyAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

        }
    }
}
