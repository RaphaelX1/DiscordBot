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
    public class ReligiaoRepositorio: BaseRepositorio
    {
        public ReligiaoRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper) : base(aplicacaoContexto, mapper)
        {

        }

        public async Task<ReligiaoDTO> Obter(Guid Id)
        {
            var religiao = await _contexto.Religioes
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Religiao, ReligiaoDTO>(religiao);
        }

        public async Task<ReligiaoDTO> Obter(string nome)
        {
            var religiao = await _contexto.Religioes
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Religiao, ReligiaoDTO>(religiao);
        }


        public async Task<List<ReligiaoDTO>> ObterTodos()
        {
            var religioes = await _contexto.Religioes
                .ToListAsync();

            return Converter<List<Religiao>, List<ReligiaoDTO>>(religioes);
        }

        public async Task<List<Personagem>> ObterFies(Guid religiaoId)
        {
            var personagens = await _contexto.Personagens
                .Where(o => o.ReligiaoId == religiaoId)
                .ToListAsync();

            return Converter<List<Personagem>, List<Personagem>>(personagens);
        }

        public async Task Cadastrar(ReligiaoDTO religiaoDTO)
        {
            var religiao = Converter<ReligiaoDTO, Religiao>(religiaoDTO);

            _contexto.Religioes.Add(religiao);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(ReligiaoDTO religiaoDTO)
        {
            var religiao = await _contexto.Religioes
                .FirstOrDefaultAsync(o => o.Id == religiaoDTO.Id);

            Converter(religiaoDTO, religiao);

            _contexto.Entry(religiao).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome)
        {
            var religiao = await _contexto.Religioes
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            if (religiao != null)
                _contexto.Entry(religiao).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistente(string nome)
        {
            return await _contexto.Religioes
                .AnyAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));
        }
    }
}
