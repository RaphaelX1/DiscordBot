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
    public class EfeitoServico: BaseServico
    {
        public EfeitoRepositorio _efeitoRepositorio;

        public EfeitoServico(EfeitoRepositorio efeitoRepositorio)
        {
            _efeitoRepositorio = efeitoRepositorio;
        }

        public async Task<DiscordEmbedBuilder> ObterInfo(string nome)
        {
            var efeito = await _efeitoRepositorio.Obter(nome);

            if (efeito == null)
                return null;

            return LayoutHandler
              .MontarInfo(efeito.Nome, efeito.Descricao, "Implicações ao Herói", TipoInfo.Efeito, DiscordColor.Gray);
        }

        public async Task<DiscordEmbedBuilder> ObterTodosInfo()
        {
            var efeitos = await _efeitoRepositorio.ObterTodos();

            if (!efeitos.Any())
                return null;

            return LayoutHandler
              .MontarInfo("Efeitos", "Todos os efeitos", "Implicações ao Herói", efeitos.Select(o => new Tuple<string, string>(o.Nome, o.Descricao)), TipoInfo.Efeito, DiscordColor.Gray);
        }

        public async Task<EfeitoDTO> Obter(string nome)
        {
            return await _efeitoRepositorio.Obter(nome);
        }

        public async Task Cadastrar(EfeitoDTO efeitoDTO)
        {
            await VerificarExistente(efeitoDTO.Nome);

            await _efeitoRepositorio.Cadastrar(efeitoDTO);
        }

        public async Task Editar(EfeitoDTO efeitoDTO)
        {
            await _efeitoRepositorio.Editar(efeitoDTO);
        }

        public async Task Excluir(string nome)
        {
            await _efeitoRepositorio.Excluir(nome);

        }

        public async Task VerificarExistente(string nome)
        {
            if (await _efeitoRepositorio.VerificarExistente(nome))
            {
                throw new RegraException(Mensagens.MensagensCrud.JaExistente);
            };
        }
    }
}
