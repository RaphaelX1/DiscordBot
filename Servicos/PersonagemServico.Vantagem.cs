using Comum;
using DSharpPlus.Entities;
using DTO;
using Enumeradores;
using Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicos
{
    public partial class PersonagemServico
    {
        public async Task<DiscordEmbedBuilder> ObterVantagensInfo(string nome)
        {
            var personagem = await _personagemRepositorio.ObterVantagens(nome);

            if (personagem == null)
                return null;

            var tuplas = new List<Tuple<string, string>>();

            tuplas.Add(MontarVantagens(personagem.Vantagens));

            if (!tuplas.Any())
                return null;

            return LayoutHandler
              .MontarInfo(personagem.Nome, "Atuais", "Especialidades do Herói", tuplas, TipoInfo.Vantagem, DiscordColor.Chartreuse);
        }

        public Tuple<string, string> MontarVantagens(IEnumerable<PersonagemVantagemDTO> personagemVantagensDTO)
        {
            return new Tuple<string, string>("Vantagens: ", string.Join(", ", personagemVantagensDTO.Select(o => o.Vantagem.Nome)));
        }

        public async Task CadastrarVantagem(string nomePersonagem, string nomeVantagem)
        {
            var personagemVantagemIds = await VerificarVantagemExistente(nomePersonagem, nomeVantagem);

            var personagemFormacao = new PersonagemVantagemDTO() 
            { 
                PersonagemId = personagemVantagemIds.Item1,
                VantagemId = personagemVantagemIds.Item2
            };

            await _personagemRepositorio.CadastrarVantagem(personagemFormacao);
        }

        public async Task ExcluirVantagem(string nomePersonagem, string nomeVantagem)
        {
            var personagem = await _personagemRepositorio.ObterVantagens(nomePersonagem);

            var vantagem = await _vantagemRepositorio.Obter(nomeVantagem);

            ValidarPersonagemVantagemDados(personagem, vantagem);

            var personagemFormacao = personagem.Vantagens.FirstOrDefault(o => o.VantagemId == vantagem.Id);

            if (personagemFormacao == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);

            await _personagemRepositorio.ExcluirAtributo(personagem.Id, vantagem.Id);
        }

        public async Task<Tuple<Guid, Guid>> VerificarVantagemExistente(string nomePersonagem, string nomeVantagem)
        {
            var personagem = await _personagemRepositorio.ObterFormacoes(nomePersonagem);

            var vantagem = await _vantagemRepositorio.Obter(nomeVantagem);

            ValidarPersonagemVantagemDados(personagem, vantagem);

            var personagemFormacao = personagem.Vantagens.FirstOrDefault(o => o.VantagemId == vantagem.Id);

            if (personagemFormacao != null)
                throw new RegraException(MensagensCrud.JaExistente);

            return new Tuple<Guid, Guid>(personagem.Id, vantagem.Id);
        }

      
        public void ValidarPersonagemVantagemDados(PersonagemDTO personagemDTO, VantagemDTO vantagemDTO)
        {
            if (personagemDTO == null)
                throw new RegraException(MensagensCrud.PersonagemNaoEncontrada);

            if (vantagemDTO == null)
                throw new RegraException(MensagensCrud.FormacaoNaoEncontrada);
        }
    }
}
