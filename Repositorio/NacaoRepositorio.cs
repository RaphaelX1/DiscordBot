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
    public class NacaoRepositorio: BaseRepositorio
    {
        public NacaoRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper): base(aplicacaoContexto, mapper)
        {

        }

        public async Task<NacaoDTO> Obter(Guid Id)
        {
            var nacao = await _contexto.Nacoes
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Nacao, NacaoDTO>(nacao);
        }

        public async Task<NacaoDTO> Obter(string nome)
        {
            var nacao = await _contexto.Nacoes
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Nacao, NacaoDTO>(nacao);
        }


        public async Task<List<NacaoDTO>> ObterTodos()
        {
            var nacoes = await _contexto.Nacoes
                .ToListAsync();

            return Converter<List<Nacao>, List<NacaoDTO>>(nacoes);
        }

        public async Task<List<Personagem>> ObterHabitantes(Guid nacaoId)
        {

            var personagens = await _contexto.Personagens
                .Where(o => o.NacaoId == nacaoId)
                .ToListAsync();
                
            return Converter<List<Personagem>, List<Personagem>>(personagens);
        }

        public async Task Cadastrar(NacaoDTO nacaoDTO)
        {
            var nacao = Converter<NacaoDTO, Nacao>(nacaoDTO);

            _contexto.Nacoes.Add(nacao);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(NacaoDTO nacaoDTO)
        {
            var nacao = await _contexto.Nacoes
                .FirstOrDefaultAsync(o => o.Id == nacaoDTO.Id);

            Converter(nacaoDTO, nacao);

            _contexto.Entry(nacao).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome)
        {
            var nacao = await _contexto.Nacoes
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            if (nacao != null)
                _contexto.Entry(nacao).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistente(string nome)
        {
            return await _contexto.Nacoes
                .AnyAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));
        }
    }
}
