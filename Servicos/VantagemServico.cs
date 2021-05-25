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
    public class VantagemServico: BaseServico
    {
        public VantagemRepositorio _vantagemRepositorio;

        public VantagemServico(VantagemRepositorio vantagemRepositorio)
        {
            _vantagemRepositorio = vantagemRepositorio;
        }

        public async Task<DiscordEmbedBuilder> ObterInfo(string nome)
        {
            var vantagem = await _vantagemRepositorio.Obter(nome);

            if (vantagem == null)
                return null;

            return LayoutHandler
              .MontarInfo(vantagem.Nome, vantagem.Descricao, "Qualificação do Herói", TipoInfo.Vantagem, DiscordColor.Blurple);
        }

        public async Task<DiscordEmbedBuilder> ObterTodosInfo()
        {
            var vantagens = await _vantagemRepositorio.ObterTodos();

            if (!vantagens.Any())
                return null;

            return LayoutHandler
              .MontarInfo("Vantagens", "Todos as vantagens", "Qualificação do Herói", vantagens.Select(o => new Tuple<string, string>(o.Nome, o.Descricao)), TipoInfo.Vantagem, DiscordColor.Blurple);
        }

        public async Task<VantagemDTO> Obter(string nome)
        {
            return await _vantagemRepositorio.Obter(nome);
        }

        public async Task Cadastrar(VantagemDTO vantagemDTO)
        {
            await VerificarExistente(vantagemDTO.Nome);

            await _vantagemRepositorio.Cadastrar(vantagemDTO);
        }

        public async Task Editar(VantagemDTO vantagemDTO)
        {
            await _vantagemRepositorio.Editar(vantagemDTO);
        }

        public async Task Excluir(string nome)
        {
            await _vantagemRepositorio.Excluir(nome);
        }

        public async Task VerificarExistente(string nome)
        {
            if (await _vantagemRepositorio.VerificarExistente(nome))
            {
                throw new RegraException(Mensagens.MensagensCrud.JaExistente);
            };
        }
    }
}
