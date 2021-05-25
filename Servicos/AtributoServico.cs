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
    public class AtributoServico : BaseServico
    {
        public AtributoRepositorio _atributoRepositorio;

        public AtributoServico(AtributoRepositorio atributoRepositorio)
        {
            _atributoRepositorio = atributoRepositorio;
        }

        public async Task<DiscordEmbedBuilder> ObterInfo(string nome)
        {
            var atributo = await _atributoRepositorio.Obter(nome);

            if (atributo == null)
                return null;

            return LayoutHandler
              .MontarInfo(atributo.Nome, atributo.Descricao, "Pontos fortes do Herói", TipoInfo.Atributo ,DiscordColor.Gray);
        }

        public async Task<DiscordEmbedBuilder> ObterTodosInfo()
        {
            var atributos = await _atributoRepositorio.ObterTodos();

            if (!atributos.Any())
                return null;

            return LayoutHandler
              .MontarInfo("Atributos", "Todos os atributos", "Pontos fortes do Herói", atributos.Select(o=> new Tuple<string, string>(o.Nome, o.Descricao)), TipoInfo.Atributo, DiscordColor.Gray);
        }

        public async Task<AtributoDTO> Obter(string nome)
        {
            return await _atributoRepositorio.Obter(nome);
        }

        public async Task Cadastrar(AtributoDTO atributoDTO)
        {
            await VerificarExistente(atributoDTO.Nome);

            await _atributoRepositorio.Cadastrar(atributoDTO);
        }

        public async Task Editar(AtributoDTO atributoDTO)
        {
            await _atributoRepositorio.Editar(atributoDTO);
        }

        public async Task Excluir(string nome)
        {
            await _atributoRepositorio.Excluir(nome);

        }

        public async Task VerificarExistente(string nome)
        {
            if (await _atributoRepositorio.VerificarExistente(nome))
            {
                throw new RegraException(Mensagens.MensagensCrud.JaExistente);
            };
        }
    }
}
