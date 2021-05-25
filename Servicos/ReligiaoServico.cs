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
    public class ReligiaoServico: BaseServico
    {
        public ReligiaoRepositorio _religiaoRepositorio;
        public ReligiaoServico(ReligiaoRepositorio religiaoRepositorio)
        {
            _religiaoRepositorio = religiaoRepositorio;
        }

        public async Task<DiscordEmbedBuilder> ObterInfo(string nome)
        {
            var religiao = await _religiaoRepositorio.Obter(nome);

            if (religiao == null)
                return null;

            return LayoutHandler
              .MontarInfo(religiao.Nome, religiao.Descricao, "Crença do Herói", TipoInfo.Religiao, DiscordColor.Aquamarine);
        }

        public async Task<DiscordEmbedBuilder> ObterTodosInfo()
        {
            var religioes = await _religiaoRepositorio.ObterTodos();

            if (!religioes.Any())
                return null;

            return LayoutHandler
              .MontarInfo("Religiões", "Todas as Religiões", "Crença do Herói", religioes.Select(o => new Tuple<string, string>(o.Nome, o.Descricao)), TipoInfo.Religiao, DiscordColor.Aquamarine);
        }

        public async Task<DiscordEmbedBuilder> ObterFieisInfo(string nome)
        {
            var religiao = await _religiaoRepositorio.Obter(nome);

            if (religiao == null)
                return null;

            var fieis = await _religiaoRepositorio.ObterFies(religiao.Id);

            if (!fieis.Any())
                return null;

            return LayoutHandler
               .MontarInfo(religiao.Nome, "Fieis", "Crença do Herói", fieis.Select(o => new Tuple<string, string>(o.Nome, $"Fortuna: {o.Fortuna}")), TipoInfo.Religiao, DiscordColor.Aquamarine);
        }

        public async Task<ReligiaoDTO> Obter(string nome)
        {
            return await _religiaoRepositorio.Obter(nome);
        }

        public async Task Cadastrar(ReligiaoDTO religiaoDTO)
        {
            await VerificarExistente(religiaoDTO.Nome);

            await _religiaoRepositorio.Cadastrar(religiaoDTO);
        }

        public async Task Editar(ReligiaoDTO religiaoDTO)
        {
            await _religiaoRepositorio.Editar(religiaoDTO);
        }

        public async Task Excluir(string nome)
        {
            await _religiaoRepositorio.Excluir(nome);
        }

        public async Task VerificarExistente(string nome)
        {
            if (await _religiaoRepositorio.VerificarExistente(nome))
            {
                throw new RegraException(Mensagens.MensagensCrud.JaExistente);
            };
        }
    }
}
