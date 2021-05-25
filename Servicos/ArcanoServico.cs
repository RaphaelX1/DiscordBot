using Comum;
using DSharpPlus.Entities;
using DTO;
using Enumeradores;
using Repositorio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicos
{
    public class ArcanoServico : BaseServico
    {
        public ArcanoRepositorio _arcanoRepositorio;

        public ArcanoServico(ArcanoRepositorio arcanoRepositorio)
        {
            _arcanoRepositorio = arcanoRepositorio;
        }

        public async Task<DiscordEmbedBuilder> ObterInfo(string nome, TipoArcano tipoArcano)
        {
            var arcano = await _arcanoRepositorio.Obter(nome, tipoArcano);

            if (arcano == null)
                return null;

            return LayoutHandler
              .MontarInfo(arcano.Nome, arcano.Descricao, arcano.TipoArcano.GetDescription(), DiscordColor.Cyan);
        }

        public async Task<ArcanoDTO> Obter(string nome, TipoArcano tipoArcano)
        {
           return await _arcanoRepositorio.Obter(nome, tipoArcano);
        }

        public async Task Cadastrar(ArcanoDTO arcanoDTO)
        {
            await VerificarExistente(arcanoDTO.Nome, arcanoDTO.TipoArcano);

            await _arcanoRepositorio.Cadastrar(arcanoDTO);
        }

        public async Task Editar(ArcanoDTO arcanoDTO)
        {
            await _arcanoRepositorio.Editar(arcanoDTO);
        }

        public async Task Excluir(string nome, TipoArcano tipoArcano)
        {
            await _arcanoRepositorio.Excluir(nome, tipoArcano);

        }

        public async Task VerificarExistente(string nome, TipoArcano tipoArcano) 
        {
            if (await _arcanoRepositorio.VerificarExistente(nome, tipoArcano))
            {
                throw new RegraException(Mensagens.MensagensCrud.JaExistente);
            };
        }
    }
}
