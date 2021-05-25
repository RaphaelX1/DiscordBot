using Comum;
using DSharpPlus.Entities;
using DTO;
using Enumeradores;
using Repositorio;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Servicos
{
    public class FormacaoServico: BaseServico
    {
        public FormacaoRepositorio _formacaoRepositorio;

        public FormacaoServico(FormacaoRepositorio formacaoRepositorio)
        {
            _formacaoRepositorio = formacaoRepositorio;
        }

        public async Task<DiscordEmbedBuilder> ObterInfo(string nome)
        {
            var formacao = await _formacaoRepositorio.Obter(nome);

            if (formacao == null)
                return null;

            return LayoutHandler
              .MontarInfo(formacao.Nome, formacao.Descricao, "Passado do Herói", TipoInfo.Formacao, DiscordColor.Blue);
        }

        public async Task<DiscordEmbedBuilder> ObterTodosInfo()
        {
            var formacoes = await _formacaoRepositorio.ObterTodos();

            if (!formacoes.Any())
                return null;

            return LayoutHandler
              .MontarInfo("Formações", "Todos as formações", "Passado do Herói", formacoes.Select(o => new Tuple<string, string>(o.Nome, o.Descricao)), TipoInfo.Formacao, DiscordColor.Blue);
        }

        public async Task<FormacaoDTO> Obter(string nome)
        {
            return await _formacaoRepositorio.Obter(nome);
        }

        public async Task Cadastrar(FormacaoDTO formacaoDTO)
        {
            await VerificarExistente(formacaoDTO.Nome);

            await _formacaoRepositorio.Cadastrar(formacaoDTO);
        }

        public async Task Editar(FormacaoDTO formacaoDTO)
        {
            await _formacaoRepositorio.Editar(formacaoDTO);
        }

        public async Task Excluir(string nome)
        {
            await _formacaoRepositorio.Excluir(nome);
        }

        public async Task VerificarExistente(string nome)
        {
            if (await _formacaoRepositorio.VerificarExistente(nome))
            {
                throw new RegraException(Mensagens.MensagensCrud.JaExistente);
            };
        }
    }
}
