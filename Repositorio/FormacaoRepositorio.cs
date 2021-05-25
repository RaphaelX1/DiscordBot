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
    public class FormacaoRepositorio: BaseRepositorio
    {
        public FormacaoRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper): base(aplicacaoContexto, mapper)
        {

        }

        public async Task<FormacaoDTO> Obter(Guid Id)
        {
            var formacao = await _contexto.Formacoes
                .FirstOrDefaultAsync(o => o.Id == Id);

            return Converter<Formacao, FormacaoDTO>(formacao);
        }

        public async Task<FormacaoDTO> Obter(string nome)
        {
            var formacao = await _contexto.Formacoes
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            return Converter<Formacao, FormacaoDTO>(formacao);
        }


        public async Task<List<FormacaoDTO>> ObterTodos()
        {
            var formacoes = await _contexto.Formacoes
                .ToListAsync();

            return Converter<List<Formacao>, List<FormacaoDTO>>(formacoes);
        }

        public async Task Cadastrar(FormacaoDTO formacaoDTO)
        {
            var formacao = Converter<FormacaoDTO, Formacao>(formacaoDTO);

            _contexto.Formacoes.Add(formacao);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(FormacaoDTO formacaoDTO)
        {
            var formacao = await _contexto.Formacoes
                .FirstOrDefaultAsync(o => o.Id == formacaoDTO.Id);

            Converter(formacaoDTO, formacao);

            _contexto.Entry(formacao).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(string nome)
        {
            var formacao = await _contexto.Formacoes
                .FirstOrDefaultAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

            if (formacao != null)
                _contexto.Entry(formacao).State = EntityState.Deleted;

            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistente(string nome)
        {
            return await _contexto.Formacoes
                .AnyAsync(o => o.Nome.ToLower().Equals(nome.ToLower()));

        }
    }
}
