using Comum;
using DSharpPlus.Entities;
using DTO;
using Enumeradores;
using Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos
{
    public partial class PersonagemServico
    {
        public async Task<DiscordEmbedBuilder> ObterArcanosInfo(string nome)
        {
            var personagem = await _personagemRepositorio.ObterArcanos(nome);

            if (personagem == null)
                return null;

            var tuplas = new List<Tuple<string, string>>();

            tuplas.AddRange(MontarArcanos(personagem.Arcanos));

            if (!tuplas.Any())
                return null;

            return LayoutHandler
              .MontarInfo(personagem.Nome, "Arcanos", "Virtude e Hubris do Herói", tuplas, TipoInfo.Arcano, DiscordColor.Chartreuse);
        }

        public List<Tuple<string, string>> MontarArcanos(IEnumerable<PersonagemArcanoDTO> personagemArcanos)
        {
            var tuplas = new List<Tuple<string, string>>();

            tuplas.Add(new Tuple<string, string>("Virtude:", string.Join(", ", personagemArcanos
                    .Where(o=> o.Arcano.TipoArcano == TipoArcano.Virtude).Select(o=> o.Arcano.Nome))));

            tuplas.Add(new Tuple<string, string>("Húbris:", string.Join(", ", personagemArcanos
                  .Where(o => o.Arcano.TipoArcano == TipoArcano.Hubris).Select(o => o.Arcano.Nome))));

            return tuplas;
        }

        public async Task CadastrarArcano(string nomePersonagem, string nomeArcano, TipoArcano tipoArcano)
        {
            var personagemArcanoIds = await VerificarArcanoExistente(nomePersonagem, nomeArcano, tipoArcano);

            var personagemArcano = new PersonagemArcanoDTO()
            {
                PersonagemId = personagemArcanoIds.Item1,
                ArcanoId = personagemArcanoIds.Item2,
            };

            await _personagemRepositorio.CadastrarArcano(personagemArcano);
        }


        public async Task ExcluirArcano(string nomePersonagem, string nomeArcano, TipoArcano tipoArcano)
        {
            var personagem = await _personagemRepositorio.ObterArcanos(nomePersonagem);

            var arcano = await _arcanoRepositorio.Obter(nomeArcano, tipoArcano);

            ValidarPersonagemArcanoDados(personagem, arcano);

            var personagemArcano = personagem.Arcanos.FirstOrDefault(o => o.ArcanoId == arcano.Id);

            if (personagemArcano == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);

            await _personagemRepositorio.ExcluirArcano(personagem.Id, arcano.Id);
        }

        public async Task<Tuple<Guid, Guid>> VerificarArcanoExistente(string nomePersonagem, string nomeArcano, TipoArcano tipoArcano)
        {
            var personagem = await _personagemRepositorio.ObterArcanos(nomePersonagem);

            var arcano = await _arcanoRepositorio.Obter(nomeArcano, tipoArcano);

            ValidarPersonagemArcanoDados(personagem, arcano);

            var personagemArcano = personagem.Arcanos.FirstOrDefault(o => o.ArcanoId == arcano.Id);

            if (personagemArcano != null)
                throw new RegraException(MensagensCrud.JaExistente);

            return new Tuple<Guid, Guid>(personagem.Id, arcano.Id);
        }


        public void ValidarPersonagemArcanoDados(PersonagemDTO personagemDTO, ArcanoDTO arcanoDTO)
        {
            if (personagemDTO == null)
                throw new RegraException(MensagensCrud.PersonagemNaoEncontrada);

            if (arcanoDTO == null)
                throw new RegraException(MensagensCrud.ArcanoNaoEncontrado);
        }
    }
}
