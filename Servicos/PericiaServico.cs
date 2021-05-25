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
    public class PericiaServico: BaseServico
    {
        public PericiaRepositorio _periciaRepositorio;

        public PericiaServico(PericiaRepositorio periciaRepositorio)
        {
            _periciaRepositorio = periciaRepositorio;
        }


        public async Task<DiscordEmbedBuilder> ObterTodosInfo()
        {
            var periciais = await _periciaRepositorio.ObterTodos();

            if (!periciais.Any())
                return null;

            return LayoutHandler
              .MontarInfo("Perícias", "Todos as perícias", "Habilidades do Herói", periciais.Select(o => new Tuple<string, string>(o.Nome, "Perícia do Herói")), TipoInfo.Pericia, DiscordColor.Orange);
        }

        public async Task<PericiaDTO> Obter(string nome)
        {
            return await _periciaRepositorio.Obter(nome);
        }

        public async Task Cadastrar(IEnumerable<string> nomes)
        {
            await VerificarExistente(nomes);

            var periciasDTO = new List<PericiaDTO>();

            foreach (var nome in nomes)
            {
                periciasDTO.Add(new PericiaDTO() { Nome = nome });
            }

            await _periciaRepositorio.Cadastrar(periciasDTO);
        }

        public async Task Editar(PericiaDTO periciaDTO)
        {
            await _periciaRepositorio.Editar(periciaDTO);
        }

        public async Task Excluir(string nome)
        {
            await _periciaRepositorio.Excluir(nome);
        }

        public async Task VerificarExistente(IEnumerable<string> nomes)
        {
            if (await _periciaRepositorio.VerificarExistente(nomes))
            {
                throw new RegraException(Mensagens.MensagensCrud.JaExistente);
            };
        }
    }
}
