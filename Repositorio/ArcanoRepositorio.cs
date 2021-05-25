using AutoMapper;
using DTO;
using Enumeradores;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ArcanoRepositorio: BaseRepositorio
    {
        public ArcanoRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper): base(aplicacaoContexto, mapper)
        {

        }

        public async Task<List<ArcanoDTO>> ObterTodos()
        {
            var arcanos = await _contexto.Arcanos
                .ToListAsync();

            return Converter<List<Arcano>, List<ArcanoDTO>>(arcanos);
        }

        public async Task<ArcanoDTO> Obter(Guid Id)
        {
            var arcano =  await _contexto.Arcanos
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Arcano, ArcanoDTO>(arcano);
        }

        public async Task<ArcanoDTO> Obter(string nome) 
        {
            var arcano =  await _contexto.Arcanos
                .FirstOrDefaultAsync(o=> o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Arcano, ArcanoDTO>(arcano);
        }

        public async Task<ArcanoDTO> Obter(string nome, TipoArcano tipoArcano)
        {
            var arcano = await _contexto.Arcanos
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()) &&
                o.TipoArcano == tipoArcano);

            return Converter<Arcano, ArcanoDTO>(arcano);

        }

        public async Task Cadastrar(ArcanoDTO arcanoDTO)
        {
            var arcano = Converter<ArcanoDTO, Arcano>(arcanoDTO);

            _contexto.Arcanos.Add(arcano);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(ArcanoDTO arcanoDTO)
        {
            var arcano = await _contexto.Arcanos
                .FirstOrDefaultAsync(o => o.Id == arcanoDTO.Id);

            Converter(arcanoDTO, arcano);

            _contexto.Entry(arcano).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome, TipoArcano tipoArcano)
        {
            var arcano = await _contexto.Arcanos
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()) && o.TipoArcano == tipoArcano);

            if (arcano != null)
                _contexto.Entry(arcano).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistente(string nome, TipoArcano tipoArcano)
        {
            return await _contexto.Arcanos
                .AnyAsync(o => o.Nome.ToLower().Equals(nome.ToLower()) && o.TipoArcano == tipoArcano);
        }

    }
}
