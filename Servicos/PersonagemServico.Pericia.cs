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
        public async Task<DiscordEmbedBuilder> ObterPericiasInfo(string nome)
        {
            var personagem = await _personagemRepositorio.ObterPericia(nome);

            if (personagem == null)
                return null;

            var tuplas = new List<Tuple<string, string>>();

            tuplas.AddRange(MontarPericias(personagem.Periciais));

            if (!tuplas.Any())
                return null;

            return LayoutHandler
              .MontarInfo(personagem.Nome, "Perícias", "Técnicas do Herói", tuplas, TipoInfo.Pericia, DiscordColor.Orange);
        }

        public List<Tuple<string, string>> MontarPericias(IEnumerable<PersonagemPericiaDTO> personagensPericiaDTO)
        {
            var tuplas = new List<Tuple<string, string>>();

            foreach (var pericia in personagensPericiaDTO)
            {
                var icones = new List<string>();

                for (int i = 0; i < pericia.Valor; i++)
                {
                    icones.Add(MensagensEmoji.DiamondBlue);
                }

                tuplas.Add(new Tuple<string, string>(pericia.Pericia.Nome, string.Join(" ", icones.Select(o => o))));
            }

            return tuplas;
        }

        public async Task CadastrarPericia(string valor, string nomePersonagem, string nomePericia)
        {
            var personagemPericiaIds = await VerificarPericiaExistente(nomePersonagem, nomePericia);

            var personagemPericia = new PersonagemPericiaDTO() 
            { 
                PersonagemId = personagemPericiaIds.Item1,
                PericiaId = personagemPericiaIds.Item2,
                Valor = valor.ParseIfValid()
            };

            await _personagemRepositorio.CadastrarPericia(personagemPericia);
        }

        public async Task EditarPericia(string valor, string nomePersonagem, string nomePericia)
        {
            var personagemPericia = await VerificarPericiaEdicao(nomePersonagem, nomePericia);

            await _personagemRepositorio.EditarPericia(personagemPericia.PersonagemId, personagemPericia.PericiaId, valor.ParseIfValid());
        }


        public async Task ExcluirPericia(string nomePersonagem, string nomePericia)
        {
            var personagem = await _personagemRepositorio.ObterPericia(nomePersonagem);

            var pericia = await _periciaRepositorio.Obter(nomePericia);

            ValidarPersonagemPericiaDados(personagem, pericia);

            var personagemPericia = personagem.Periciais.FirstOrDefault(o => o.PericiaId == pericia.Id);

            if (personagemPericia == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);

            await _personagemRepositorio.ExcluirPericia(personagem.Id, pericia.Id);
        }

        public async Task<Tuple<Guid, Guid>> VerificarPericiaExistente(string nomePersonagem, string nomePericia)
        {
            var personagem = await _personagemRepositorio.ObterPericia(nomePersonagem);

            var pericia = await _periciaRepositorio.Obter(nomePericia);

            ValidarPersonagemPericiaDados(personagem, pericia);

            var personagemPericia = personagem.Periciais.FirstOrDefault(o => o.PericiaId == pericia.Id);

            if (personagemPericia != null)
                throw new RegraException(MensagensCrud.JaExistente);

            return new Tuple<Guid, Guid>(personagem.Id, pericia.Id);
        }

        public async Task<PersonagemPericiaDTO> VerificarPericiaEdicao(string nomePersonagem, string nomePericia)
        {
            var personagem = await _personagemRepositorio.ObterPericia(nomePersonagem);

            var pericia = await _periciaRepositorio.Obter(nomePericia);

            ValidarPersonagemPericiaDados(personagem, pericia);

            var personagemPericia = personagem.Periciais.FirstOrDefault(o => o.PericiaId == pericia.Id);

            if (personagemPericia == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);

            return personagemPericia;
        }

        public void ValidarPersonagemPericiaDados(PersonagemDTO personagemDTO, PericiaDTO periciaDTO)
        {
            if (personagemDTO == null)
                throw new RegraException(MensagensCrud.PersonagemNaoEncontrada);

            if (periciaDTO == null)
                throw new RegraException(MensagensCrud.PericiaNaoEncontrada);
        }
    }
}
