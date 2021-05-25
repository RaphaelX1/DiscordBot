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
    public class PericiaRepositorio: BaseRepositorio
    {
        public PericiaRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper): base(aplicacaoContexto, mapper)
        {

        }

        public async Task<PericiaDTO> Obter(Guid Id)
        {
            var pericia = await _contexto.Pericias
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Pericia, PericiaDTO>(pericia);
        }

        public async Task<PericiaDTO> Obter(string nome)
        {
            var pericia = await _contexto.Pericias
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Pericia, PericiaDTO>(pericia);
        }


        public async Task<List<PericiaDTO>> ObterTodos()
        {
            var periciais = await _contexto.Pericias
                .ToListAsync();

            return Converter<List<Pericia>, List<PericiaDTO>>(periciais);
        }

        public async Task Cadastrar(IEnumerable<PericiaDTO> periciasDTO)
        {
            foreach (var periciaDTO in periciasDTO)
            {
                var pericia = Converter<PericiaDTO, Pericia>(periciaDTO);

                _contexto.Pericias.Add(pericia);
            }
           
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(PericiaDTO periciaDTO)
        {
            var pericia = await _contexto.Pericias
                .FirstOrDefaultAsync(o => o.Id == periciaDTO.Id);

            Converter(periciaDTO, pericia);

            _contexto.Entry(pericia).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome)
        {
            var pericia = await _contexto.Pericias
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            if (pericia != null)
                _contexto.Entry(pericia).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistente(IEnumerable<string> nomes)
        {
            return await _contexto.Pericias
                .AnyAsync(o => nomes.Contains(o.Nome.ToLower()));
        }
    }
}
