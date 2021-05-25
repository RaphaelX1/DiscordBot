using AutoMapper;
using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorio
{
    public class EfeitoRepositorio: BaseRepositorio
    {
        public EfeitoRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper): base(aplicacaoContexto, mapper)
        {

        }

        public async Task<EfeitoDTO> Obter(Guid Id)
        {
            var efeito = await _contexto.Efeitos
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Efeito, EfeitoDTO>(efeito);
        }

        public async Task<EfeitoDTO> Obter(string nome)
        {
            var efeito = await _contexto.Efeitos
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Efeito, EfeitoDTO>(efeito);
        }


        public async Task<List<EfeitoDTO>> ObterTodos()
        {
            var efeitos = await _contexto.Efeitos
                .ToListAsync();

            return Converter<List<Efeito>, List<EfeitoDTO>>(efeitos);
        }

        public async Task Cadastrar(EfeitoDTO efeitoDTO)
        {
            var efeito = Converter<EfeitoDTO, Efeito>(efeitoDTO);

            _contexto.Efeitos.Add(efeito);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(EfeitoDTO efeitoDTO)
        {
            var efeito = await _contexto.Efeitos
                .FirstOrDefaultAsync(o => o.Id == efeitoDTO.Id);

            Converter(efeitoDTO, efeito);

            _contexto.Entry(efeito).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome)
        {
            var efeito = await _contexto.Efeitos
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            if (efeito != null)
                _contexto.Entry(efeito).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistente(string nome)
        {
            return await _contexto.Efeitos
                .AnyAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

        }
    }
}
