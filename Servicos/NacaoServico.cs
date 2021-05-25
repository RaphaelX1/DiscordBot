using Comum;
using DSharpPlus.Entities;
using DTO;
using Enumeradores;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos
{
    public class NacaoServico : BaseServico
    {
        public NacaoRepositorio _nacaoRepositorio;
        public NacaoServico(NacaoRepositorio nacaoRepositorio)
        {
            _nacaoRepositorio = nacaoRepositorio;
        }

        public async Task<DiscordEmbedBuilder> ObterInfo(string nome)
        {
            var nacao = await _nacaoRepositorio.Obter(nome);

            if (nacao == null)
                return null;

            return LayoutHandler
              .MontarInfo(nacao.Nome, nacao.Descricao, "Terra do Herói", TipoInfo.Nacao, DiscordColor.Brown);
        }

        public async Task<DiscordEmbedBuilder> ObterTodosInfo()
        {
            var nacoes = await _nacaoRepositorio.ObterTodos();

            if (!nacoes.Any())
                return null;

            return LayoutHandler
              .MontarInfo("Nações", "Todos as nações", "Terra do Herói", nacoes.Select(o => new Tuple<string, string>(o.Nome, o.Descricao)), TipoInfo.Nacao, DiscordColor.Brown);
        }

        public async Task<DiscordEmbedBuilder> ObterHabitantesInfo(string nome)
        {
            var nacao = await _nacaoRepositorio.Obter(nome);

            if (nacao == null)
                return null;

            var habitantes = await _nacaoRepositorio.ObterHabitantes(nacao.Id);

            if (!habitantes.Any())
                return null;

            return LayoutHandler
               .MontarInfo(nacao.Nome, "Habitantes", "Terra do Herói", habitantes.Select(o => new Tuple<string, string>(o.Nome, $"Fortuna: {o.Fortuna}")), TipoInfo.Nacao, DiscordColor.Brown);
        }

        public async Task<NacaoDTO> Obter(string nome)
        {
            return await _nacaoRepositorio.Obter(nome);
        }

        public async Task Cadastrar(NacaoDTO nacaoDTO)
        {
            await VerificarExistente(nacaoDTO.Nome);

            await _nacaoRepositorio.Cadastrar(nacaoDTO);
        }

        public async Task Editar(NacaoDTO nacaoDTO)
        {
            await _nacaoRepositorio.Editar(nacaoDTO);
        }

        public async Task Excluir(string nome)
        {
            await _nacaoRepositorio.Excluir(nome);
        }

        public async Task VerificarExistente(string nome)
        {
            if (await _nacaoRepositorio.VerificarExistente(nome))
            {
                throw new RegraException(Mensagens.MensagensCrud.JaExistente);
            };
        }
    }
}
